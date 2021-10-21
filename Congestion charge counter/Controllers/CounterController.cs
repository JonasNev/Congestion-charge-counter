using Congestion_charge_counter.Models;
using Congestion_charge_counter.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Congestion_charge_counter.Controllers
{
    public class CounterController : Controller
    {
        private readonly CounterService _counterService;

        DateTime startDate = new DateTime(2008, 04, 25, 10, 23, 00);
        DateTime EndDate = new DateTime(2008, 04, 28, 09, 02, 00);
        private double AMprice = 0.033;
        private double PMpricePerMinute = 0.04166667;
        private double freeTime = 0;
        public CounterController(CounterService counterService)
        {
            _counterService = counterService;
        }

        public IActionResult Index(CounterModel counter)
        {
            return View(counter);
        }

        public IActionResult Create()
        {
            var model = new CounterModel();
            model.StartDate = startDate;
            model.EndDate = EndDate;
            model.SelectedCar = "Car";
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CounterModel counter)
        {
            var StartDate = counter.StartDate;
            while (counter.EndDate > StartDate)
            {
                if (StartDate.DayOfWeek == DayOfWeek.Saturday || StartDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    StartDate = StartDate.AddDays(1);
                }
                if (StartDate.TimeOfDay.Hours >= 0 && StartDate.TimeOfDay.TotalHours < 7)
                {
                    StartDate = StartDate.AddHours(7);
                }
                if (StartDate.TimeOfDay.Hours >= 7 && StartDate.TimeOfDay.Hours < 12)
                {
                    StartDate = StartDate.AddMinutes(1);
                    counter.AM_rate += AMprice;
                    counter.TotalAMTime = counter.TotalAMTime.AddMinutes(1);
                }
                if (StartDate.TimeOfDay.Hours >= 12 && StartDate.TimeOfDay.TotalHours < 19)
                {
                    StartDate = StartDate.AddMinutes(1);
                    counter.PM_rate += PMpricePerMinute;
                    counter.TotalPMTime = counter.TotalPMTime.AddMinutes(1);
                }
                if (StartDate.TimeOfDay.Hours >= 19 && StartDate.TimeOfDay.TotalHours < 24)
                {
                    StartDate = StartDate.AddHours(5);
                }
            }
            counter.PM_rate = Math.Round(counter.PM_rate, 1);
            counter.AM_rate = Math.Round(counter.AM_rate, 1);
            return RedirectToAction("Index", counter);
        }
    }
}

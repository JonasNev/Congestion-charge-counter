using Congestion_charge_counter.Models;
using Congestion_charge_counter.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Congestion_charge_counter.Controllers
{
    public class CounterController : Controller
    {
        private readonly CounterService _counterService;
        // Hard coded values for easier testing (Using UI for imput is also viable)
        readonly DateTime startDate = new DateTime(2008, 04, 25, 10, 23, 00);
        readonly DateTime EndDate = new DateTime(2008, 04, 28, 09, 02, 00);
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
            //Passing a model with set values for easier testing
            var model = new CounterModel();
            model.StartDate = startDate;
            model.EndDate = EndDate;
            model.SelectedCar = "Car";
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CounterModel counter)
        {
            FeeModel fees = new();
            switch (counter.SelectedCar)
            {
                case "Motorbike":
                    //Configure fees for seperate vehicle types by choise.
                    //Fees calculated by dividing hourly rate / 60 (minutes)
                    fees.AM_fee = 0.016666;
                    fees.PM_fee = 0.016666;
                    counter = _counterService.FeeCounter(counter, fees);
                    break;
                default:
                    //Default is set for every other vehicle type, except motorbike, because its fees are different.
                    fees.AM_fee = 0.033333;
                    fees.PM_fee = 0.041666;
                    counter = _counterService.FeeCounter(counter, fees);
                    break;
            }
            counter = _counterService.FeeRounding(counter);
            //Getting the full sum for futher calculation if needed and also frontend
            counter.TotalCharge = _counterService.TotalSum(counter.AM_rate, counter.PM_rate);
            //Convert fees to string for display
            counter = _counterService.FeesToString(counter);
            return RedirectToAction("Index", counter);
        }
    }
}

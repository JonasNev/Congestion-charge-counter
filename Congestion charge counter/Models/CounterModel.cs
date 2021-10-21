using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congestion_charge_counter.Models
{
    public class CounterModel
    {
        public string[] CarTypes { get; set; } = { "Motorbike", "Car", "Van" };
        public string SelectedCar { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AM_rate { get; set; }
        public double PM_rate { get; set; }
        public int TotalCharge { get; set; }
        public DateTime TotalAMTime { get; set; }
        public DateTime TotalPMTime { get; set; }
    }
}

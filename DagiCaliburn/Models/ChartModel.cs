using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.Models
{
    public class ChartModel
    {
        public string Element { get; set; }

        public decimal TotalSales { get; set; }

        public double Percent { get; set; }
        //public double Percent2 { get; set; }


        public ChartModel(string element, double percent)
        {
            Element = element;
            Percent = percent;

        }

        public ChartModel(string element, decimal totalSales)
        {
            Element = element;
            TotalSales = totalSales;
        }

        public ChartModel()
        {

        }

    }
}

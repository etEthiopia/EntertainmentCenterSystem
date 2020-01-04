using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class StatsViewModel : Conductor<object>
    {
        public static StatsViewModel statsView;
        public static WeeklyDataViewModel weeklyData;
        public static MonthlyDataViewModel monthlyData;
        public static DailyDataViewModel dailyData;
        public static CustomersViewModel customerData;

        public StatsViewModel()
        {
            statsView = this;
            weeklyData = new WeeklyDataViewModel();
            monthlyData = new MonthlyDataViewModel();
            dailyData = new DailyDataViewModel();
            customerData = new CustomersViewModel();
            ActivateItem(dailyData);
        }

        public void DailyBtn()
        {
            ActivateItem(dailyData);
        }

        public void WeeklyBtn()
        {
            ActivateItem(weeklyData);
        }

        public void MonthlyBtn()
        {
            ActivateItem(monthlyData);
        }

        public void CusomterBtn()
        {
            Console.WriteLine("Btn");
            ActivateItem(customerData);
        }

    }
}

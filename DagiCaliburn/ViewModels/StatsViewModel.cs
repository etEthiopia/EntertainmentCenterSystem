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
        //public static CustomersViewModel customerData;

        public StatsViewModel()
        {
            statsView = this;
            
            DailyBtn();
        }

        public void DailyBtn()
        {
            dailyData = new DailyDataViewModel();
            ActivateItem(dailyData);
        }

        public void WeeklyBtn()
        {
            weeklyData = new WeeklyDataViewModel();
            ActivateItem(weeklyData);
        }

        public void MonthlyBtn()
        {
            monthlyData = new MonthlyDataViewModel();
            ActivateItem(monthlyData);
        }

        //public void CusomterBtn()
        //{
        //    customerData = new CustomersViewModel();
        //    ActivateItem(customerData);
        //}

    }
}

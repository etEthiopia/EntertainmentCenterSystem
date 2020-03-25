using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class MonthlyDataViewModel : Screen
    {
        private List<ChartModel> _teList = new List<ChartModel>();
        private StatisticsModel statisticsModel = new StatisticsModel();
        private BindableCollection<TopSellModel> _topSellers = new BindableCollection<TopSellModel>();



        private string _tsitems = "0 SELLS";
        private string _tsbirr = "0 BIRR";

        public BindableCollection<TopSellModel> TopSellers
        {
            get
            {
                return _topSellers;
            }
            set
            {
                _topSellers = value;
                NotifyOfPropertyChange(() => TopSellers);
            }
        }

        public string MonthSoldMoney
        {
            get
            {
                return _tsbirr;
            }
            set
            {
                _tsbirr = value + " BIRR";
                NotifyOfPropertyChange(() => MonthSoldMoney);
            }
        }

        public string MonthSoldST
        {
            get
            {
                return _tsitems;
            }
            set
            {
                _tsitems = value;
                NotifyOfPropertyChange(() => MonthSoldST);
            }
        }

        public List<ChartModel> MonthEarnings
        {
            get { return _teList; }
            set
            {
                _teList = value;
                NotifyOfPropertyChange(() => MonthEarnings);
            }
        }

        


        public MonthlyDataViewModel()
        {

            CalculateToday(getMainDailySells());


        }

        private Dictionary<string, double> getMainDailySells()
        {
            return statisticsModel.getMainDailySell();
        }

        public void Refresh()
        {
            StatsViewModel.statsView.MonthlyBtn();
        }

        private void CalculateToday(Dictionary<string, double> dsells)
        {
            string[] types = dsells.Keys.ToArray();
            double[] money = dsells.Values.ToArray();
            TopSellers = new BindableCollection<TopSellModel>(statisticsModel.getMainTopMonthlySell());


            for (int i = 0; i < types.Length; i++)
            {
                Console.WriteLine("type:, money: " + types[i] + ", " + money[i]);
                ChartModel c = new ChartModel(types[i], money[i]);

                MonthEarnings.Add(c);

                MonthSoldMoney = money.Sum().ToString();
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                MonthSoldST = "Sales Data for "+ months[DateTime.Now.Month-1];

            }



            

        }
    }
}


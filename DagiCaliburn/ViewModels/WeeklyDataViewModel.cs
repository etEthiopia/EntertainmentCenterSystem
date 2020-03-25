using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class WeeklyDataViewModel : Screen
    {
        private List<ChartModel> _e810am = new List<ChartModel>();
        private List<ChartModel> _e1012pm = new List<ChartModel>();
        private List<ChartModel> _e122pm = new List<ChartModel>();
        private List<ChartModel> _e24pm = new List<ChartModel>();
        private List<ChartModel> _e46pm = new List<ChartModel>();
        private List<ChartModel> _e68pm = new List<ChartModel>();
        private List<ChartModel> _e810pm = new List<ChartModel>();
        private StatisticsModel statisticsModel = new StatisticsModel();
        private BindableCollection<TopSellModel> _topSellers = new BindableCollection<TopSellModel>();
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

        public string TodaysSoldMoney
        {
            get
            {
                return _tsbirr;
            }
            set
            {
                _tsbirr = value + " BIRR";
                NotifyOfPropertyChange(() => TodaysSoldMoney);
            }
        }


        public WeeklyDataViewModel()
        {

            CalculateToday();


        }

        public List<ChartModel> Earnings810am
        {
            get { return _e810am; }
            set
            {
                _e810am = value;
                NotifyOfPropertyChange(() => Earnings810am);
            }
        }
        public List<ChartModel> Earnings1012pm
        {
            get { return _e1012pm; }
            set
            {
                _e1012pm = value;
                NotifyOfPropertyChange(() => Earnings1012pm);
            }
        }
        public List<ChartModel> Earnings122pm
        {
            get { return _e122pm; }
            set
            {
                _e122pm = value;
                NotifyOfPropertyChange(() => Earnings122pm);
            }
        }
        public List<ChartModel> Earnings24pm
        {
            get { return _e24pm; }
            set
            {

                _e24pm = value;
                NotifyOfPropertyChange(() => Earnings24pm);
            }
        }
        public List<ChartModel> Earnings46pm
        {
            get { return _e46pm; }
            set
            {

                _e46pm = value;
                NotifyOfPropertyChange(() => Earnings46pm);
            }
        }
        public List<ChartModel> Earnings68pm
        {
            get { return _e68pm; }
            set
            {

                _e68pm = value;
                NotifyOfPropertyChange(() => Earnings68pm);
            }
        }
        public List<ChartModel> Earnings810pm
        {
            get { return _e810pm; }
            set
            {

                _e810pm = value;
                NotifyOfPropertyChange(() => Earnings810pm);
            }
        }

        public void Refresh()
        {
            StatsViewModel.statsView.WeeklyBtn();
        }



        private void CalculateToday()
        {
            TopSellers = new BindableCollection<TopSellModel>(statisticsModel.getMainTopWeeklySell());

            Dictionary<int, double> hours = statisticsModel.getMainWeeklySellByDays();


            int[] hs = hours.Keys.ToArray();
            double[] dinero = hours.Values.ToArray();
            TodaysSoldMoney = dinero.Sum().ToString();
            Console.WriteLine("hs length: " + hs.Length);

            for (int i = 0; i < hs.Length; i++)
            {
                Console.WriteLine("type:, money: " + hs[i] + ", " + dinero[i]);
                ChartModel c = new ChartModel("Weekly PerDay Data", dinero[i]);


                if (hs[i] == 1 )
                {
                    Earnings810am.Add(c);
                }
                if (hs[i] == 2)
                {
                    Earnings1012pm.Add(c);
                }
                if (hs[i] == 3)
                {
                    Earnings122pm.Add(c);
                }
                if (hs[i] == 4)
                {
                    Earnings24pm.Add(c);
                }
                if (hs[i] == 5)
                {
                    Earnings46pm.Add(c);
                }
                if (hs[i] == 6)
                {
                    Earnings68pm.Add(c);
                }
                if (hs[i] == 7)
                {
                    Earnings810pm.Add(c);
                }

            }

        }
    }
}

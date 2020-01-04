using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagiCaliburn.ViewModels
{
    class DailyDataViewModel : Screen
    {
        private List<ChartModel> _teList = new List<ChartModel>();
        private List<ChartModel> _e810am = new List<ChartModel>();
        private List<ChartModel> _e1012pm = new List<ChartModel>();
        private List<ChartModel> _e122pm = new List<ChartModel>();
        private List<ChartModel> _e24pm = new List<ChartModel>();
        private List<ChartModel> _e46pm = new List<ChartModel>();
        private List<ChartModel> _e68pm = new List<ChartModel>();
        private List<ChartModel> _e810pm = new List<ChartModel>();
        private StatisticsModel statisticsModel = new StatisticsModel();
        

        public List<ChartModel> TodaysEarnings
        {
            get { return _teList; }
            set
            {
                _teList = value;
                NotifyOfPropertyChange(() => TodaysEarnings);
            }
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


        public DailyDataViewModel()
        {
            
            CalculateToday(getMainDailySells());
            
        }

        private Dictionary<string, double> getMainDailySells()
        {
            statisticsModel.getMainTopDailySell();
            return statisticsModel.getMainDailySell();
        }

        private void CalculateToday(Dictionary<string, double> dsells)
        {
            string[] types = dsells.Keys.ToArray();
            double[] money = dsells.Values.ToArray();
            Console.WriteLine("types length: " + types.Length);

            //double[] money = { 50, 100, 34, 30, 90, 39, 10, 0 };
            for(int i=0; i < types.Length; i++)
            {
                Console.WriteLine("type:, money: " + types[i] + ", " + money[i]);
                ChartModel c = new ChartModel(types[i], money[i]);

                TodaysEarnings.Add(c);
                if(i < 3)
                {
                    Earnings810am.Add(c);
                }
                if (i < 7 && i > 3)
                {
                    Earnings1012pm.Add(c);
                }
                if (i < 6 && i > 1)
                {
                    Earnings122pm.Add(c);
                }
                if (i < 8 && i > 0)
                {
                    Earnings24pm.Add(c);
                }
                if (i < 6 && i > 0)
                {
                    Earnings46pm.Add(c);
                }
                if (i < 7)
                {
                    Earnings68pm.Add(c);
                }
                if (i < 8 && i > 3)
                {
                    Earnings810pm.Add(c);
                }

            }
            
        }
    }
}

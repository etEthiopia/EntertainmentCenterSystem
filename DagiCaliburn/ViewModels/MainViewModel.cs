using Caliburn.Micro;
using DagiCaliburn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DagiCaliburn.ViewModels
{
    class MainViewModel : Screen
    {
        //private static BindableCollection<SaleModel> _sells = new BindableCollection<SaleModel>(SaleModel.getTodaysSales());
        private BindableCollection<SellModel> _sells = new BindableCollection<SellModel>();
        private BindableCollection<SellModel> _fsells;
        SellModel so = new SellModel();
        bool sobool = false;
        private BindableCollection<Disk> _drives = new BindableCollection<Disk>();
        private BindableCollection<TypeModel> _categoryTypes = new BindableCollection<TypeModel>();
        private BindableCollection<SellModel> _dPurchased = new BindableCollection<SellModel>();
        private SellModel _selectedSell = new SellModel();
        private string _selectedName;
        private string _selectedPrice;
        private string _selectedDate;
        private string _selectedReciever;
        private string _selectedSize;
        private string _selectedDir;
        private string _clearGBName = "Clear GB Counter";
        private string _todaysEarnings = "";
        private string _yesterday = "";
        private string _goal = "";
        private string _progessPercentage = "";
        private string _progressToGo = "";
        private float _todaysProgress = 0.0f;
        private string _selectedInitials;
        private bool _setGoal;
        private bool _progressGrid;
        private bool _dgIsVisible;
        private bool _drivesIsVisible;
        private bool _buttonsIsVisible = true;
        private bool _successIsVisible = false;
        private bool _dPromptIsVisible = false;
        private bool _gridInputsIsVisible = true;
        private bool _copyingIsVisible;
        private bool _dailyIsVisible;
        private bool _selectedIsVisible;
        private bool _selectedFIsVisible;
        private bool _driveDIsVisible;
        private bool _pagIsVisible;
        private bool _clearGBIsVisible;
        private string _dPort = "";
        private string _dName = "";
        private string _dCost = "";
        private string _dItems = "";
        private string _dProps = "";
        private string _gridType = "";
        private string _gridCategory = "";
        private TypeModel _gridItemType = new TypeModel();
        private string _gridDate = "";
        private string _goalAmount = "";
        public string _specialMessage = "";

        public string SpecialMessage
        {
            get
            {
                return _specialMessage;
            }
            set
            {
                _specialMessage = value;
                NotifyOfPropertyChange(() => SpecialMessage);
            }
        }

        public bool ProgressGridIsVisible
        {
            get
            {
                return _progressGrid;
            }
            set
            {
                _progressGrid = value;
                NotifyOfPropertyChange(() => ProgressGridIsVisible);
            }
        }

        public bool SetGoalIsVisible
        {
            get
            {
                return _setGoal;
            }
            set
            {
                _setGoal = value;
                NotifyOfPropertyChange(() => SetGoalIsVisible);
            }
        }

        public string ProgressValue
        {
            get
            {
                return _progessPercentage;
            }
            set
            {
                _progessPercentage = value;
                NotifyOfPropertyChange(() => ProgressValue);
            }
        }
        public string ProgressToGo
        {
            get
            {
                return _progressToGo;
            }
            set
            {
                _progressToGo = value;
                NotifyOfPropertyChange(() => ProgressToGo);
            }
        }

        public bool SuccessIsVisible
        {
            get
            {
                return _successIsVisible;
            }
            set
            {
                _successIsVisible = value;
                NotifyOfPropertyChange(() => SuccessIsVisible);
            }
        }

        public bool DPromptIsVisible
        {
            get
            {
                return _dPromptIsVisible;
            }
            set
            {
                _dPromptIsVisible = value;
                NotifyOfPropertyChange(() => DPromptIsVisible);
            }
        }

        public bool DGIsVisible {
            get
            {
                return _dgIsVisible;
            }
            set
            {
                _dgIsVisible = value;
                NotifyOfPropertyChange(() => DGIsVisible);
            }
        }

        public bool ButtonsIsVisible
        {
            get
            {
                return _buttonsIsVisible;
            }
            set
            {
                _buttonsIsVisible = value;
                NotifyOfPropertyChange(() => ButtonsIsVisible);
            }
        }

        public bool ClearGBIsVisible
        {
            get
            {
                return _clearGBIsVisible;
            }
            set
            {
                _clearGBIsVisible = value;
                NotifyOfPropertyChange(() => ClearGBIsVisible);
            }
        }

        public bool CopyingIsVisible
        {
            get
            {
                return _copyingIsVisible;
            }
            set
            {
                _copyingIsVisible = value;
                NotifyOfPropertyChange(() => CopyingIsVisible);
            }
        }
        public bool SelectedIsVisible
        {
            get
            {
                return _selectedIsVisible;
            }
            set
            {
                _selectedIsVisible = value;
                NotifyOfPropertyChange(() => SelectedIsVisible);
            }
        }

        public bool GridInputsIsVisible
        {
            get
            {
                return _gridInputsIsVisible;
            }
            set
            {
                _gridInputsIsVisible = value;
                NotifyOfPropertyChange(() => GridInputsIsVisible);
            }
        }
        
        public string ClearGBName
        {
            get
            {
                return _clearGBName;
            }
            set
            {
                _clearGBName = value;
                NotifyOfPropertyChange(() => ClearGBName);
            }
        }

        public bool SelectedFIsVisible
        {
            get
            {
                return _selectedFIsVisible;
            }
            set
            {
                _selectedFIsVisible = value;
                NotifyOfPropertyChange(() => SelectedFIsVisible);
            }
        }
        public bool DrivesIsVisible
        {
            get
            {
                return _drivesIsVisible;
            }
            set
            {
                _drivesIsVisible = value;
                NotifyOfPropertyChange(() => DrivesIsVisible);
            }
        }
        public bool PagIsVisible
        {
            get
            {
                return _pagIsVisible;
            }
            set
            {
                _pagIsVisible = value;
                NotifyOfPropertyChange(() => PagIsVisible);
            }
        }
        public bool DailyIsVisible
        {
            get
            {
                return _dailyIsVisible;
            }
            set
            {
                _dailyIsVisible = value;
                NotifyOfPropertyChange(() => DailyIsVisible);
            }
        }
        public bool DriveDIsVisible
        {
            get
            {
                return _driveDIsVisible;
            }
            set
            {
                _driveDIsVisible = value;
                NotifyOfPropertyChange(() => DriveDIsVisible);
            }
        }
        public string DPort
        {
            get
            {
                return _dPort;
            }
            set
            {
                _dPort = value;
                NotifyOfPropertyChange(() => DPort);
            }
        }
        public string DProps
        {
            get
            {
                return _dProps;
            }
            set
            {
                _dProps = value;
                NotifyOfPropertyChange(() => DProps);
            }
        }
        public string DName
        {
            get
            {
                return _dName;
            }
            set
            {
                _dName = value;
                NotifyOfPropertyChange(() => DName);
            }
        }
        public string DCost
        {
            get
            {
                return _dCost;
            }
            set
            {
                _dCost = value;
                NotifyOfPropertyChange(() => DCost);
            }
        }
        public string DItems
        {
            get
            {
                return _dItems;
            }
            set
            {
                _dItems = value;
                NotifyOfPropertyChange(() => DItems);
            }
        }
        public BindableCollection<SellModel> Sells
        {
            get
            {
                return _sells;
            }
            set
            {
                _sells = value;
                NotifyOfPropertyChange(() => Sells);
            }
        }
        public BindableCollection<SellModel> FSells
        {
            get
            {
                return _fsells;
            }
            set
            {
                _fsells = value;
                NotifyOfPropertyChange(() => FSells);
            }
        }
        public BindableCollection<Disk> Drives
        {
            get
            {
                return _drives;
            }
            set
            {
                _drives = value;
                NotifyOfPropertyChange(() => Drives);
            }
        }
        public BindableCollection<TypeModel> CategoryTypes
        {
            get
            {
                return _categoryTypes;
            }
            set
            {
                _categoryTypes = value;
                NotifyOfPropertyChange(() => CategoryTypes);

            }
        }

        public string Goal
        {
            get
            {
                return _goal;
            }
            set
            {
                _goal = value;
                NotifyOfPropertyChange(() => Goal);
            }
        }

        public string GoalAmount
        {
            get
            {
                return _goalAmount;
            }
            set
            {
                _goalAmount = value;
                NotifyOfPropertyChange(() => GoalAmount);
            }
        }


        //SelectedSell
        public SellModel SelectedSell
        {
            get
            {
                return _selectedSell;
            }
            set
            {
                _selectedSell = value;
                NotifyOfPropertyChange(() => SelectedSell);
            }

        }
        public string SelectedName
        {
            get
            {
                return _selectedName;
            }
            set
            {
                _selectedName = value;
                NotifyOfPropertyChange(() => SelectedName);
            }
        }
        public string SelectedPrice
        {
            get
            {
                return _selectedPrice;
            }
            set
            {
                _selectedPrice = value;
                NotifyOfPropertyChange(() => SelectedPrice
);
            }
        }
        public string SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                NotifyOfPropertyChange(() => SelectedDate);
            }
        }
        public string SelectedReciever
        {
            get
            {
                return _selectedReciever;
            }
            set
            {
                _selectedReciever = value;
                NotifyOfPropertyChange(() => SelectedReciever);
            }
        }
        public string SelectedSize
        {
            get
            {
                return _selectedSize;
            }
            set
            {
                _selectedSize = value;
                NotifyOfPropertyChange(() => SelectedSize);
            }
        }
        public string SelectedDir
        {
            get
            {
                return _selectedDir;
            }
            set
            {
                _selectedDir = value;
                NotifyOfPropertyChange(() => SelectedDir);
            }
        }
        public string SelectedInitials
        {
            get
            {
                return _selectedInitials;
            }
            set
            {
                _selectedInitials = value;
                NotifyOfPropertyChange(() => SelectedInitials);
            }
        }
        public string GridType
        {
            get
            {
                return _gridType;
            }
            set
            {
                _gridType = value.Substring(38);
                NotifyOfPropertyChange(() => GridType);
                ChangeSellsView();

            }
        }
        public string GridCategory
        {
            get
            {
                return _gridCategory;
            }
            set
            {
                _gridCategory = value.Substring(38);
                NotifyOfPropertyChange(() => GridCategory);
                //NotifyOfPropertyChange(() => GridItemType);
                //NotifyOfPropertyChange(() => CategoryTypes);
                CategoryTypes = new BindableCollection<TypeModel>(ChangeCategoryTypes());
                ChangeSellsView();

            }
        }

        public BindableCollection<SellModel> DPurchased
        {
            get
            {
                return _dPurchased;
            }
            set
            {
                _dPurchased = value;
                NotifyOfPropertyChange(() => DPurchased);
            }
        }

        public TypeModel GridItemType
        {
            get
            {
                return _gridItemType;
            }
            set
            {
                try
                {
                    _gridItemType = value;
                    
                    //Console.WriteLine($"ItemTypes INDEX {GridItemType}, {value}; {CategoryTypes[value].Id}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ItemTypes Cast error {_gridItemType}, {value}; {e.Message} ");
                }
                NotifyOfPropertyChange(() => GridItemType);
                ChangeSellsView();
            }
        }

        public string GridDate
        {
            get
            {
                return _gridDate;
            }
            set
            {
                
                string year = value.Substring(6, 4);
                string date = value.Substring(3, 2);
                string month = value.Substring(0, 2);

                _gridDate = year+"-"+month+"-"+date;
                NotifyOfPropertyChange(() => GridDate);
                Console.WriteLine($"DATE {GridDate}");

                ChangeSellsView();
            }
        }

        public string TodaysEarnings
        {
            get
            {
                return _todaysEarnings;
            }
            set
            {
                _todaysEarnings = value;
                NotifyOfPropertyChange(() => TodaysEarnings);
            }
        }

        public string Yesterday
        {
            get
            {
                return _yesterday;
            }
            set
            {
                _yesterday = value;
                NotifyOfPropertyChange(() => Yesterday);
            }
        }
   
        public float TodaysProgress
        {
            get
            {
                return _todaysProgress;
            }
            set
            {
                _todaysProgress = value;
                NotifyOfPropertyChange(() => TodaysProgress);
            }
        }

        public static float TE = 0;

        static int paged = 1;
        static int top = 0;
        static SellModel SelectedFolder;
        
        public MainViewModel()
        {
            Console.WriteLine("MainViewModel");
            
            Drives = new BindableCollection<Disk>(Disk.GetDrives());
            DrivesIsVisible = true;
            DailyIsVisible = true;
            DGIsVisible = true;
            SelectedIsVisible = false;
            Console.WriteLine($"DGIsVisible {DGIsVisible}, SelectedIsVisible {SelectedIsVisible}");
            ChangeSellsView();
            if(Sells.Count > 0)
            {
                PagIsVisible = true;
            }

        }

        public void PerSize()
        {
            //StatisticsModel.GetAllSpecials();
            //GoalModel.yesterdaysEarning();
            StartViewModel.startview.ActivateItem(StartViewModel.gbview);
        }
        
        public void AddSale()
        {
            //GoalModel.yesterdaysEarning();
            StartViewModel.startview.ActivateItem(StartViewModel.sellview);
        }
        public void ClearSize()
        {
            ClearGBName = "Cleared !";
            GBViewModel.GBSells = new List<string>();
            
            //ClearGBName = "Back !";
            ClearGBIsVisible = false;

        }

        public void ChangeSellsView()
        {
            try
            {
                int count = (paged * 6) - 6;
                

                Sells = new BindableCollection<SellModel>(SellModel.getSells(GridType, GridCategory, GridItemType.Id, GridDate, paged).Skip(count));
                foreach(SellModel sold in Sells)
                {
                    Console.WriteLine($"Sold {sold.Name}, {sold.ID}, {sold.Initials}, {sold.Type.Name}");
                }
                Drives = new BindableCollection<Disk>(Disk.GetDrives());
                SellModel.getTodaysEarnings();
                SetTodaysEarnings();
                if(Sells.Count() > 0)
                {
                    
                    PagIsVisible = true;
                    
                }

                if (GoalModel.CheckGoal())
                {
                    ProgressGridIsVisible = true;
                    SetGoalIsVisible = false;
                    //Yesterday = GoalModel.yesterdaysEarning() + " BIRR";
                    GoalModel.GetGoal();
                    Goal = GoalModel.Goal + " BIRR";
                    ChangeProgress();
                }
                else
                {
                    ProgressGridIsVisible = false;
                    SetGoalIsVisible = true;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine($"CHangeSellsView Exception {e.Message}");
            }

            

        }

        private List<TypeModel> ChangeCategoryTypes()
        {
            var cts = TypeModel.GetTypeFromFileType(GridCategory);
            var tmp = new TypeModel();
            tmp.Id = 0;
            tmp.Name = "All";
            cts.Insert(0, tmp);
            return cts;

        }

        public void SetTodaysEarnings()
        {
             TodaysEarnings =  TE + " BIRR";
        }

        public void SellDClick()
        {
            try
            {
                Console.WriteLine($"SELL CLICKED:  {SelectedSell.Name}, {SelectedSell.Type.Name}");
                if (SelectedSell.Initials.Equals("F"))
                {
                    SelectedFIsVisible = true;
                    ButtonsIsVisible = false;
                    GridInputsIsVisible = false;
                    SelectedFolder = SelectedSell;
                    Console.WriteLine($"SF {SelectedFolder.Name}");
                    //DateTime dtf;
                    //DateTime.TryParse(SelectedSell.getSellDateFromFolder(), out dtf);
                    //SelectedSell.DateTime = dtf;
                    FSells = new BindableCollection<SellModel>(VideoModel.getEpisodes(SelectedSell));
                }
                else
                {
                    SelectedIsVisible = true;
                    SelectedFIsVisible = false;
                    ButtonsIsVisible = false;
                    GridInputsIsVisible = false;
                    if (SelectedSell.Folder > 1)
                    {
                        top = 1;

                    }
                    else
                    {
                        top = 0;
                    }
                }
                DGIsVisible = false;
                try
                {

                    if (SelectedSell.ID != 0)
                    {
                        SellModel selectedSM = null;
                        selectedSM = SelectedSell.getSell();
                        SelectedName = selectedSM.Name;
                        SelectedPrice = selectedSM.Price.ToString();
                        SelectedReciever = selectedSM.RecieverPort;
                        
                        SelectedSize = selectedSM.Size;
                        SelectedDate = selectedSM.DateTime.ToString();
                        SelectedInitials = selectedSM.Initials;
                        SelectedDir = selectedSM.Dir;
                    }
                    else
                    {
                        SelectedName = SelectedSell.Name;
                        SelectedPrice = SelectedSell.Price.ToString();
                        SelectedReciever = SelectedSell.RecieverPort;
                        SelectedSize = SelectedSell.Size;
                        SelectedDate = SelectedSell.DateTime.ToString();
                        SelectedInitials = SelectedSell.Initials;
                        SelectedDir = SelectedSell.Dir;

                    }



                }
                catch (Exception e)
                {
                    Console.WriteLine($"SelectedSM, {SelectedName}, {SelectedSell.ID}");
                    Console.WriteLine($"Clicked Exception {e.Message}");
                }
                Console.WriteLine("TOP " + top);
            }
            catch(Exception eepp)
            {
                Console.WriteLine($"SellDClick Exception, {eepp.Message}");
            }
        }

        public void DeleteSellSelected()
        {
            so.Name = SelectedSell.Name;
            so.DateTime = DateTime.Parse(SelectedDate);
            so.Folder = SelectedSell.Folder;
            so.Type = SelectedSell.Type;
            so.RecieverSerial = SelectedSell.RecieverSerial;
            sobool = false;
            DPromptIsVisible = true;
            SpecialMessage = $"Are you sure to delete {SelectedName}";
            SelectedIsVisible = false;
            //MessageBox.Show($"DELETE {SelectedSell.Name}, {SelectedDate},\n {SelectedSell.Folder}, {SelectedSell.ID}, {SelectedSell.RecieverSerial}");
        }

        public void DeleteFSellSelected()
        {
            so.Folder = SelectedSell.Folder;
            so.RecieverSerial = SelectedSell.RecieverSerial;
            sobool = true;
            DPromptIsVisible = true;
            SpecialMessage = $"Are you sure to delete {SelectedName}";
            SelectedFIsVisible = false;
        }

        public void CancelDelete()
        {
            DPromptIsVisible = false;
            if (sobool)
            {
                SelectedFIsVisible = true;
            }
            else
            {
                SelectedIsVisible = true;
            }
            
            SpecialMessage = $"";

        }

        public void OkDelete()
        {
            DPromptIsVisible = false;
            

            SpecialMessage = $"";
            if (sobool)
            {
                if (so.DeleteFolder())
                {
                    SuccessIsVisible = true;
                    SelectedIsVisible = false;
                    SpecialMessage = "Successfully Deleted";
                }
                else
                {
                    SuccessIsVisible = true;
                    SelectedIsVisible = false;
                    SpecialMessage = "Delete not Successful";
                }
            }

            else
            {
                if (so.Type != null) { 

                if (so.Delete())
                    {
                        SuccessIsVisible = true;
                        SelectedIsVisible = false;
                        SpecialMessage = "Successfully Deleted";
                    }
                    else
                    {
                        SuccessIsVisible = true;
                        SelectedIsVisible = false;
                        SpecialMessage = "Delete not Successful";
                    }
                }
                else
                {
                    SuccessIsVisible = true;
                    SelectedIsVisible = false;
                    SpecialMessage = "Refresh List";
                }
            }

            ChangeSellsView();
        }

        public void CloseSuccess()
        {
            SuccessIsVisible = false;
            if (sobool)
            {
                DGIsVisible = true;
            }
            else
            {
                DGIsVisible = true;
            }
            so = new SellModel();
            sobool = false;
        }

        

        public void DriveSelected(string port)
        {
            foreach(Disk d in Drives)
            {
                if (d.Port.Equals(port))
                {
                    DPort = d.Port;
                    DName = d.Name;
                    DProps = d.Props;
                    DCost = d.Cost;
                    DItems = d.Items;
                    DPurchased = new BindableCollection<SellModel>(d.GetPurchased());
                    DriveDIsVisible = true;
                    DrivesIsVisible = false;
                    DailyIsVisible = false;
                    break;
                }
            }
        }

        public void CloseDriveSelected()
        {
            DriveDIsVisible = false;
            DrivesIsVisible = true;
            DailyIsVisible = true;
            
        }

        public void CloseSellSelected()
        {
            
            SelectedIsVisible = false;
            if(top == 0)
            {
                DGIsVisible = true;
                SelectedFIsVisible = false;
                ButtonsIsVisible = true;
                GridInputsIsVisible = true;
                Console.WriteLine("Paged " + paged);
            }
            else
            {
                try
                {
                    Console.WriteLine($" B SF {SelectedFolder.Name}");
                    Console.WriteLine($" A SS {SelectedFolder.Name}");
                    SelectedName = SelectedFolder.Name;
                    SelectedPrice = SelectedFolder.Price.ToString();
                    SelectedReciever = SelectedFolder.RecieverPort;
                    SelectedSize = SelectedFolder.Size;
                    SelectedDate = SelectedFolder.DateTime.ToString();
                    SelectedInitials = SelectedFolder.Initials;
                    SelectedDir = SelectedFolder.Dir;
                }
                catch(Exception e)
                {
                    DGIsVisible = true;
                    SelectedFIsVisible = false;
                    Console.WriteLine($"PAGING ERROR {e.Message}");
                }
                SelectedFIsVisible = true;
                
                }
            
        }

        public void CloseSellFSelected()
        {
            top = 0;
            SelectedIsVisible = false;
            SelectedFIsVisible = false;
            DGIsVisible = true;
            ButtonsIsVisible = true;
            GridInputsIsVisible = true;

        }

        public void backpage()
        {
            if (paged > 1)
            {
                paged--;
                ChangeSellsView();
            }
            
        }

        public void firstpage()
        {
            paged = 1;
            ChangeSellsView();
        }

        public void nextpage()
        {
            paged ++;
            ChangeSellsView();
        }

        public void SetGoalAmount()
        {
            float money = 0.0f;
            if(float.TryParse(GoalAmount, out money))
            {
                if (GoalModel.SetGoal(money))
                {
                    SetGoalIsVisible = false;
                    ProgressGridIsVisible = true;
                    Yesterday = GoalModel.yesterdaysEarning() + " BIRR";
                    GoalModel.FixProgress();
                    GoalModel.GetGoal();
                    Goal = GoalModel.Goal + " BIRR";
                    ChangeProgress();
                }
                else
                {
                    GoalAmount = "Try Again";
                }
            }
            else
            {
                GoalAmount = "Numbers Only";
            }
        }

        public void ChangeProgress()
        {
            GoalModel.FixProgress();
            ProgressToGo = GoalModel.messagePerc;
            ProgressValue = GoalModel.percc + "%";
            TodaysProgress = GoalModel.percc;
            
        }


    }



    

    
}

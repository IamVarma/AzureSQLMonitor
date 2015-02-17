using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.Models;
using AzureSQLApp.Common;
using Newtonsoft.Json;
using AzureSQLApp.Views;

namespace AzureSQLApp.ViewModels
{
   public class DatabaseDetailsViewModel:ViewModelBase
    {
        private ObservableCollection<TableDetails> tablelist;
        ObservableCollection<ConnectionsCount> _connectioncount = new ObservableCollection<ConnectionsCount>();
        ObservableCollection<ConnectionsCount> _connectionsList = new ObservableCollection<ConnectionsCount>();
       private ObservableCollection<DatabaseResources> _databaseResouceUsage;
       private ObservableCollection<DatabaseSizeClass> _databaseSize;
       private ObservableCollection<DBConnectionClass> _dbConnectionDetails;
       private ObservableCollection<DBConnectionEventsClass> _dbConnectionEvents;
       private ObservableCollection<TopCPUConsumersClass> _topCpuConsumers; 


       //Exception handling
       public RelayCommand Exceptionpopupcommand { get; private set; }
       public bool isopenproperty;
       private string _exceptionResult;

      //  public RelayCommand GetTables { get; private set; }

        public RelayCommand LogOut { get; private set; }
        public RelayCommand GoBack { get; private set; }


       public ObservableCollection<DatabaseSizeClass> DatabaseSize
        {

            get
            {
                return _databaseSize;
            }

            set
            {
                _databaseSize = value;
                RaisePropertyChanged("DatabaseSize");
            }
        }




        public ObservableCollection<TableDetails> TableList
        {

            get
            {
                return tablelist;
            }
            set
            {
                tablelist = value;
                RaisePropertyChanged("TableList");
            }
        }


        public ObservableCollection<ConnectionsCount> ConnectionsList
        {
            get { return _connectioncount; }
            set {
                value = _connectioncount;
                RaisePropertyChanged("ConnectionsList");
            }

        }

       public ObservableCollection<DatabaseResources> DatabaseResourceUsage
       {
           get
           {
               return _databaseResouceUsage;
           }

           set
           {
               _databaseResouceUsage = value;
               RaisePropertyChanged("DatabaseResourceUsage");
           }
       }


       public ObservableCollection<DBConnectionClass> DBConnectionDetails
       {
           get
           {
               return _dbConnectionDetails;
           }

           set
           {
               _dbConnectionDetails = value;
               RaisePropertyChanged("DBConnectionDetails");
           }
       }

       public ObservableCollection<DBConnectionEventsClass> DBConnectionEvents
       {
           get
           {
               return _dbConnectionEvents;
           }

           set
           {
               _dbConnectionEvents = value;
               RaisePropertyChanged("DBConnectionEvents");
           }
       }

       public ObservableCollection<TopCPUConsumersClass> TopCpuConsumers
       {
           get
           {
               return _topCpuConsumers;
           }

           set
           {
               _topCpuConsumers = value;
               RaisePropertyChanged("TopCpuConsumers");
           }
       }

       public bool ExceptionOpen
       {
           get { return isopenproperty; }

           set
           {
               isopenproperty = value;
               RaisePropertyChanged("ExceptionOpen");
           }
       }

       public string ExceptionResult
       {
           get
           {
               return _exceptionResult;
           }
           set
           {
               _exceptionResult = value;
               RaisePropertyChanged("ExceptionResult");
           }
       }


        public DatabaseDetailsViewModel()
        {
//          ConnectionsList = new ObservableCollection<ConnectionsCount>();
          DatabaseSize = new ObservableCollection<DatabaseSizeClass>();
          DBConnectionDetails = new ObservableCollection<DBConnectionClass>();
            TopCpuConsumers = new ObservableCollection<TopCPUConsumersClass>();
            GoBack = new RelayCommand(() => goBack());
            LogOut = new RelayCommand(() => LogoutNow());
            Exceptionpopupcommand = new RelayCommand(() => HandleException());
        }


        public async Task GetTablesCommand(string selecteddatabase)
        {
            string tblist = null;
            try
            {

                tblist = await App.Servicehandle.GetTableSizeDetailsAsync(selecteddatabase);
                ObservableCollection<TableDetails> templist = JsonConvert.DeserializeObject<ObservableCollection<TableDetails>>(tblist);
                TableList = templist;

            }

            catch (Exception e)
            {
                ExceptionResult = "Error:" + tblist;
                ExceptionOpen = true;
            }


        }

       public async Task GetDatabaseSize(string selecteddatabase)
        {
            string databasesize = null;
            try
            {
                databasesize = await App.Servicehandle.GetDatabaseSizeAsync(selecteddatabase);
                DatabaseSize = JsonConvert.DeserializeObject<ObservableCollection<DatabaseSizeClass>>(databasesize);
            }

           catch(Exception e)
            {
                ExceptionResult = "Error:" + databasesize;
                ExceptionOpen = true;

            }
        }


       public async Task GetConnectionCount(string selecteddatabaase)
        {
            string connections = null;

            try
            {
                connections = await App.Servicehandle.GetConnectionCountAsync(selecteddatabaase);
                ConnectionsCount templist = JsonConvert.DeserializeObject<ConnectionsCount>(connections);
                 if (ConnectionsList.Count >= 5)
                   {
                       ConnectionsList.RemoveAt(0);
                       ConnectionsList.Add(templist);
                   }
                   else
                   {
                       ConnectionsList.Add(templist);
                   }

               var x=  ConnectionsList;

                    }
                
            catch(Exception e)
            {
                ExceptionResult = "Error:" + connections;
                ExceptionOpen = true;
            }
          
          

         //  ConnectionsList = _connectionsList;


        }

       public async Task GetResourceUsage(string selecteddatabase)
       {
           string usagedetails=null;

           try { 
           usagedetails = await App.Servicehandle.GetDBResourceUsageAsync(selecteddatabase);
           DatabaseResourceUsage = JsonConvert.DeserializeObject<ObservableCollection<DatabaseResources>>(usagedetails);
               }

           catch (Exception e)
           {
               ExceptionResult = "Error:" + usagedetails;
               ExceptionOpen = true;
           }
       }

       public async Task GetDBConnectionDetails(string selecteddatabase)
       {
           string[] info= new string[2];
           string connectionDetails = null;
           try
           {
               connectionDetails = await App.Servicehandle.GetDBConnectionDetailsAsync(selecteddatabase);
               info = JsonConvert.DeserializeObject<string[]>(connectionDetails);

               DBConnectionDetails = JsonConvert.DeserializeObject<ObservableCollection<DBConnectionClass>>(info[0]);
               DBConnectionEvents = JsonConvert.DeserializeObject<ObservableCollection<DBConnectionEventsClass>>(info[1]);
           }

           catch(Exception e)
           {
               ExceptionResult = "Error:" + connectionDetails;
               ExceptionOpen = true;
           }


       }


       public async Task GetTopCpuConsumers(string selecteddatabase)
       {
           string topconsumers = null;
           try
           {
               topconsumers = await App.Servicehandle.GetTopCpuConsumersAsync(selecteddatabase);
               TopCpuConsumers = JsonConvert.DeserializeObject<ObservableCollection<TopCPUConsumersClass>>(topconsumers);
           }

           catch (Exception e)
           {
               ExceptionResult = "Error:" + topconsumers;
               ExceptionOpen = true;
           }



       }

       public void HandleException()
       {
           ExceptionOpen = false;
           App.AppFrame.Navigate(typeof(HomePageView));
       }


        private void LogoutNow()
        {
            App.AppFrame.Navigate(typeof(HomePageView));
        }

       private void goBack()
        {
            App.AppFrame.Navigate(typeof(ListDatabasesView));
        }

    }
}

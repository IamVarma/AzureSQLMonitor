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




        public DatabaseDetailsViewModel()
        {
          ConnectionsList = new ObservableCollection<ConnectionsCount>();
          DatabaseSize = new ObservableCollection<DatabaseSizeClass>();
          DBConnectionDetails = new ObservableCollection<DBConnectionClass>();
            GoBack = new RelayCommand(() => goBack());
            LogOut = new RelayCommand(() => LogoutNow());
        }


        public async Task GetTablesCommand(string selecteddatabase)
        {
            var tblist = await App.Servicehandle.GetTableSizeDetailsAsync(selecteddatabase);
            ObservableCollection<TableDetails> templist = JsonConvert.DeserializeObject<ObservableCollection<TableDetails>>(tblist);
            TableList = templist;

        }

       public async Task GetDatabaseSize(string selecteddatabase)
        {
            var databasesize = await App.Servicehandle.GetDatabaseSizeAsync(selecteddatabase);
            DatabaseSize = JsonConvert.DeserializeObject<ObservableCollection<DatabaseSizeClass>>(databasesize);
            
        }


       public async Task GetConnectionCount(string selecteddatabaase)
        {

            var connections = await App.Servicehandle.GetConnectionCountAsync(selecteddatabaase);
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
          

         //  ConnectionsList = _connectionsList;


        }

       public async Task GetResourceUsage(string selecteddatabase)
       {
           var usagedetails = await App.Servicehandle.GetDBResourceUsageAsync(selecteddatabase);
           DatabaseResourceUsage = JsonConvert.DeserializeObject<ObservableCollection<DatabaseResources>>(usagedetails);

       }

       public async Task GetDBConnectionDetails(string selecteddatabase)
       {
           var connectionDetails = await App.Servicehandle.GetDBConnectionDetailsAsync(selecteddatabase);
           DBConnectionDetails = JsonConvert.DeserializeObject<ObservableCollection<DBConnectionClass>>(connectionDetails);

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

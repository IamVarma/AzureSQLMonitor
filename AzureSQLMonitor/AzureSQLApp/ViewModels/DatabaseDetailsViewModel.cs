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
      //  public RelayCommand GetTables { get; private set; }

        public RelayCommand LogOut { get; private set; }
        public RelayCommand GoBack { get; private set; }

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


        public DatabaseDetailsViewModel()
        {
            GoBack = new RelayCommand(() => goBack());
            LogOut = new RelayCommand(() => LogoutNow());
        }


        public async Task GetTablesCommand(string selecteddatabase)
        {
            var tblist = await App.Servicehandle.GetTableSizeDetailsAsync(selecteddatabase);
            ObservableCollection<TableDetails> templist = JsonConvert.DeserializeObject<ObservableCollection<TableDetails>>(tblist);
            TableList = templist;

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

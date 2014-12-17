using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSQLApp.Models;
using AzureSQLApp.Common;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AzureSQLApp.ViewModels
{
    public class ListDatabasesViewModel:ViewModelBase
    {

        private ObservableCollection<Databases> databaselist;
        public RelayCommand GetDatabases { get; private set; }

        public ObservableCollection<Databases> DatabaseList
        {
            get
            {
                return databaselist;
            }
            set
            {
                databaselist = value;
                RaisePropertyChanged("DatabaseList");
            }
        }

        public ListDatabasesViewModel()
        {
            GetDatabases = new RelayCommand(()=>GetDatabasesCommand());
        }

        public async Task GetDatabasesCommand()
        {
           

            var dblist = await App.Servicehandle.GetDatabaseListAsync();
          ObservableCollection<Databases> templist = JsonConvert.DeserializeObject<ObservableCollection<Databases>>(dblist);


          DatabaseList = templist;

        }


    }
}

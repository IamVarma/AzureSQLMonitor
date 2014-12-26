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
        string[] sysinfocollection = new string[3];
        public string servername;

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

        public string Servername
        {
            get
            {
                return servername;
            }
            set
            {
                servername = value;
                RaisePropertyChanged("Servername");
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
            //Databases templist = JsonConvert.DeserializeObject<Databases>(dblist);

          DatabaseList = templist;

        }

      /*  public async Task getSysInfo()
        {
            var sysinfo1 = await App.Servicehandle.getSystemInfoAsync();
            
        }*/


    }
}

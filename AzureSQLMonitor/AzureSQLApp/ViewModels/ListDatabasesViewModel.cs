using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSQLApp.Models;
using AzureSQLApp.Common;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using AzureSQLApp.Views;

namespace AzureSQLApp.ViewModels
{
    public class ListDatabasesViewModel:ViewModelBase
    {

        private ObservableCollection<Databases> databaselist;
        private ObservableCollection<string> sysinfoobject = new ObservableCollection<string>();
        public RelayCommand GetDatabases { get; private set; }
        public RelayCommand LogOut { get; private set; }
        public string servername;
        public string userid;
        public string version;
        public string datetext;
        public string timetext;

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

        public string Userid
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
                RaisePropertyChanged("Userid");
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                RaisePropertyChanged("Version");
            }
        }
        public string Datetext
        {
            get
            {
                return datetext;
            }
            set
            {
                datetext = value;
                RaisePropertyChanged("Datetext");
            }
        }
        public string Timetext
        {
            get
            {
                return timetext;
            }
            set
            {
                timetext = value;
                RaisePropertyChanged("Timetext");
            }
        }

        public ListDatabasesViewModel()
        {
            //GetDatabases = new RelayCommand(()=>GetDatabasesCommand());
            LogOut = new RelayCommand(() => LogoutNow());
        }

        public async Task GetDatabasesCommand()
        {
           

            var dblist = await App.Servicehandle.GetDatabaseListAsync();
            ObservableCollection<Databases> templist = JsonConvert.DeserializeObject<ObservableCollection<Databases>>(dblist);
            

          DatabaseList = templist;

        }

        public async Task getSysInfo()
        {
            sysinfoobject = await App.Servicehandle.getSystemInfoAsync();
            if (sysinfoobject[0] != null)
            {
                Servername = sysinfoobject[0].ToString();
            }   
            else
            {
                Servername = "null";
            }
            if (sysinfoobject[1] != null)
            {
                Userid = sysinfoobject[1].ToString();
            }
            else
            {
                Userid = "null";
            }
            if (sysinfoobject[2] != null)
            {
                Version = sysinfoobject[2].ToString();
                string[] tmpversion = Version.Split('\n');
                Version = tmpversion[0];
            }
            else
            {
                Version = "null";
            }
            if (sysinfoobject[3] != null)
            {
                Datetext = sysinfoobject[3].ToString();
                string[] tempdatetimetext = Datetext.Split(' ');
                Datetext = tempdatetimetext[0];
                Timetext = tempdatetimetext[1];
            }
            else
            {
                Datetext = Timetext = "null";
            }
        }


        public void LogoutNow()
        {
            App.AppFrame.Navigate(typeof(HomePageView));
        }

    }
}

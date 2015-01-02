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
       // private ObservableCollection<Sysinfo> sysinfoobject = new ObservableCollection<Sysinfo>();
        public RelayCommand GetDatabases { get; private set; }
      
        public RelayCommand LogOut { get; private set; }
        public RelayCommand SelectedDatabase { get; private set; }
         string servername;
         string userid;
         string version;
         string datetext;
         string timetext;

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
            LogOut = new RelayCommand(()=> LogoutNow(App.SelectedDatabase));
        }

        public async Task GetDatabasesCommand()
        {
           

            var dblist = await App.Servicehandle.GetDatabaseListAsync();
            ObservableCollection<Databases> templist = JsonConvert.DeserializeObject<ObservableCollection<Databases>>(dblist);
            

          DatabaseList = templist;

        }

        public async Task getSysInfo()
        {
            var sysinfoobject = await App.Servicehandle.getSystemInfoAsync();
            Sysinfo templist = JsonConvert.DeserializeObject<Sysinfo>(sysinfoobject);

            if (templist.ServerName != null)
            {
                Servername = templist.ServerName.ToString();
            }   
            else
            {
                Servername = "null";
            }
            if (templist.LoginName != null)
            {
                Userid = templist.LoginName.ToString();
            }
            else
            {
                Userid = "null";
            }
            if (templist.VersionInfo != null)
            {
                Version = templist.VersionInfo.ToString();
                string[] tmpversion = Version.Split('\n');
                Version = tmpversion[0];
            }
            else
            {
                Version = "null";
            }
            if (templist.DatetimeInfo != null)
            {
                Datetext = templist.DatetimeInfo.ToString();
                string[] tempdatetimetext = Datetext.Split(' ');
                Datetext = tempdatetimetext[0];
                Timetext = tempdatetimetext[1];
            }
            else
            {
                Datetext = Timetext = "null";
            }
        }


        public void LogoutNow(string dbname)
        {
            App.AppFrame.Navigate(typeof(DatabaseDetailsView),dbname);
        }



    }
}

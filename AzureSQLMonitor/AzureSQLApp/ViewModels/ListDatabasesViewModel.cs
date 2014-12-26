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
            Servername = sysinfoobject[0].ToString();
            Userid = sysinfoobject[1].ToString();
            Version = sysinfoobject[2].ToString();
            string[] tmpversion = Version.Split('\n');
            Version = tmpversion[0];
            Datetext = sysinfoobject[3].ToString();
            string[] tempdatetimetext = Datetext.Split(' ');
            Datetext = tempdatetimetext[0];
            Timetext = tempdatetimetext[1];
        }


    }
}

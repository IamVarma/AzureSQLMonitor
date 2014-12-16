using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.AzureSQLService;
using AzureSQLApp.Common;
using AzureSQLApp.Views;

namespace AzureSQLApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private AzureSQLService.SqlMonitorServiceClient Servicehandle = null;
        private string result;
        private string servername;
        private string username;
        private string password;
        public RelayCommand GetLogin { get; private set; }
        

        public string ServerName
        {
            get
            {
                return servername;
            }

            set
            {

                servername = value;
                RaisePropertyChanged("ServerName");
            }

        }

        public string UserName
        {
            get
            {
                return username;
            }

            set
            {

                username = value;
                RaisePropertyChanged("UserName");
            }

        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {

                password = value;
                RaisePropertyChanged("Password");
            }

        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                RaisePropertyChanged("Result");
            }
        }


        public  LoginViewModel()
        {

          GetLogin = new RelayCommand(()=> Login());

        }
        
        public async Task Login()
        {
            Servicehandle = new AzureSQLService.SqlMonitorServiceClient();

            Result = await Servicehandle.ConnectSQLAzureAsync(ServerName,UserName,Password);

            if (Result == "Success")
            {

                App.AppFrame.Navigate(typeof(ListDatabasesView));

            }

        }

        

    }
}

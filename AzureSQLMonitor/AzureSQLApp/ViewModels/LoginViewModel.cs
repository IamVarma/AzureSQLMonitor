using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.AzureSQLService;
using AzureSQLApp.Common;
using AzureSQLApp.Views;
using Windows.UI.Xaml;

namespace AzureSQLApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        
        private string result;
        private string servername;
        private string username;
        private string password;
        private Visibility popupvisibility;
        private bool shouldShowPopUp;
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


       
        public Visibility PopUpVisibility
        {
            get { return shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed; }

            set
            {
                popupvisibility = shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged("PopUpVisibility");
            }
        }
        
        public  LoginViewModel()
        {

          GetLogin = new RelayCommand(() => Login());
          shouldShowPopUp = false;
        }
        
        public async Task Login()
        {
            
            Result = await App.Servicehandle.ConnectSQLAzureAsync(ServerName,UserName,Password);

            if (Result == "Success")
            {
               App.AppFrame.Navigate(typeof(ListDatabasesView));

            }
            else
            {
                shouldShowPopUp = true;
                PopUpVisibility =  shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed;

            }

            
                       
        }

        

    }
}

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
        private bool popupvisibility;
        private bool shouldShowImage;
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


        public bool PopUpVisibility
        {
            get { return popupvisibility; }
            set
            {
                popupvisibility = value;
                RaisePropertyChanged("PopUpVisibility");
            }
        }

       // public Visibility ImageVisibility
       //  {
       //      get { return shouldShowImage ? Visibility.Visible : Visibility.Collapsed; }

       //      set
       //      {
       //          popupvisibility = shouldShowImage ? Visibility.Visible : Visibility.Collapsed;
       //          RaisePropertyChanged("ImageVisibility");
       //      }
       //}

        public  LoginViewModel()
        {

          GetLogin = new RelayCommand(() => Login());
          popupvisibility = false;
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

                popupvisibility = true;

            }

           // var ggg = ImageVisibility;
                       
        }

        

    }
}

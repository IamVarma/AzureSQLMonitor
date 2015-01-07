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
using System.ComponentModel;
using AzureSQLApp.Models;

namespace AzureSQLApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        
        private string result;
        private string servername;
        private string username;
        private string password;
        LoginDetails logindetails;
        public RelayCommand GetLogin { get; private set; }
        public bool isopenproperty;
        public bool isringenabled;
        public event PropertyChangedEventHandler PropertyChanged;

        
        public string ServerName
        {
            get
            {
                return servername;
            }

            set
            {

                servername = value;
                logindetails.ServerName = servername;
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
                logindetails.LoginId = username;
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
                logindetails.Password = password;
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

        public bool OnCanLogin()
        {

            return !string.IsNullOrEmpty(ServerName)

                && !string.IsNullOrEmpty(UserName)

                && !string.IsNullOrEmpty(Password);

        }

     
      /*  public Visibility PopUpVisibility
        {
            get { return shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed; }

            set
            {
                popupvisibility = shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged("PopUpVisibility");
            }
        }*/

        public bool Isopenproperty
        {
            get { return isopenproperty; }

            set
            {
                isopenproperty = value;
                RaisePropertyChanged("Isopenproperty");
            }
        }

        public bool IsRingEnabled
        {
            get { return isringenabled; }

            set
            {
                isringenabled = value;
                RaisePropertyChanged("IsRingEnabled");
            }
        }

        public  LoginViewModel()
        {

            GetLogin = new RelayCommand(() => Login(),OnCanLogin);
            logindetails = new LoginDetails();
            logindetails.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(logindetails_PropertyChanged);
            
        }

        void logindetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            GetLogin.RaiseCanExecuteChanged();

        }

        public async Task Login()
        {
            IsRingEnabled = true;
            Result = await App.Servicehandle.ConnectSQLAzureAsync(ServerName,UserName,Password);
            IsRingEnabled = false;
            if (Result == "Success")
            {
               App.AppFrame.Navigate(typeof(ListDatabasesView));

            }
            else
            {
               // shouldShowPopUp = true;
               // PopUpVisibility =  shouldShowPopUp ? Visibility.Visible : Visibility.Collapsed;
                Isopenproperty = true;
            }

            
                       
        }

        

    }
}

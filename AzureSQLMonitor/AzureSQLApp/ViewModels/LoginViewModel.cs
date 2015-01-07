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
using System.Threading;

namespace AzureSQLApp.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        
        private string result;
        private string servername;
        private string username;
        private string password;
        private string loginbuttonvisibility;
        private string cancelbuttonvisibility;
        LoginDetails logindetails;
        public RelayCommand GetLogin { get; private set; }
        public RelayCommand CancelLogin { get; private set; }
        public RelayCommand Exceptionpopupcommand { get; private set; }
        public bool isopenproperty;
        public bool isringenabled;
        public event PropertyChangedEventHandler PropertyChanged;
        CancellationTokenSource cts;
        
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

        public string Cancelbuttonvisibility
        {
            get
            {
                return cancelbuttonvisibility;
            }
            set
            {
                cancelbuttonvisibility = value;
                RaisePropertyChanged("Cancelbuttonvisibility");
            }
        }

        public string Loginbuttonvisibility
        {
            get
            {
                return loginbuttonvisibility;
            }
            set
            {
                loginbuttonvisibility = value;
                RaisePropertyChanged("Loginbuttonvisibility");
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

        public bool Isexceptionopenproperty
        {
            get { return isopenproperty; }

            set
            {
                isopenproperty = value;
                RaisePropertyChanged("Isexceptionopenproperty");
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
            CancelLogin = new RelayCommand(() => cancleLogin());
            Exceptionpopupcommand = new RelayCommand(() => dismisException());
            logindetails = new LoginDetails();
            logindetails.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(logindetails_PropertyChanged);
            Loginbuttonvisibility = "Visible";
            Cancelbuttonvisibility = "Collapsed";
            cts = new CancellationTokenSource();
            
        }

        void logindetails_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            GetLogin.RaiseCanExecuteChanged();

        }

        public async Task Login()
        {
            IsRingEnabled = true;
            Loginbuttonvisibility = "Collapsed";
            Cancelbuttonvisibility = "Visible";
            Result = await asyncConnectToAzure(cts.Token);
            // Result = await App.Servicehandle.ConnectSQLAzureAsync(ServerName,UserName,Password);
            IsRingEnabled = false;
            if (Result == "Success")
            {
               App.AppFrame.Navigate(typeof(ListDatabasesView));

            }
            else
            {
                Isexceptionopenproperty = true;
            }

            
                       
        }

        async Task<string> asyncConnectToAzure(CancellationToken ct)
        {
            string tempresult;
            tempresult = await App.Servicehandle.ConnectSQLAzureAsync(ServerName, UserName, Password);
            return tempresult;
        }

        public void cancleLogin()
        {
            cts.Cancel();
            //App.AppFrame.Navigate(typeof(HomePageView));
            Loginbuttonvisibility = "Visibile";
            Cancelbuttonvisibility = "Collapsed";
        }

        public void dismisException()
        {
            Isexceptionopenproperty = false;
            Loginbuttonvisibility = "Visibile";
            Cancelbuttonvisibility = "Collapsed";
        }

        

    }
}

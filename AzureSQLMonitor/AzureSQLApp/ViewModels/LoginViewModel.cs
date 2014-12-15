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
        public RelayCommand GetLogin { get; private set; }
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

            Result = await Servicehandle.ConnectSQLAzureAsync("qifwztjnhi.database.windows.net", "varmag", "asdfg123?");

            if (Result == "Success")
            {

                App.AppFrame.Navigate(typeof(ListDatabasesView));

            }

        }

        

    }
}

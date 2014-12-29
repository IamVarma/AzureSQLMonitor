using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AzureSQLApp.Models;
using AzureSQLApp.Common;
using Newtonsoft.Json;

namespace AzureSQLApp.ViewModels
{
   public class DatabaseDetailsViewModel:ViewModelBase
    {
        private ObservableCollection<TableDetails> tablelist;
      //  public RelayCommand GetTables { get; private set; }

        

        public ObservableCollection<TableDetails> TableList
        {

            get
            {
                return tablelist;
            }
            set
            {
                tablelist = value;
                RaisePropertyChanged("TableList");
            }
        }


        public DatabaseDetailsViewModel()
        {

        }


        public async Task GetTablesCommand()
        {
            var tblist = await App.Servicehandle.GetTableSizeDetailsAsync("AdventureWorks2012");
            ObservableCollection<TableDetails> templist = JsonConvert.DeserializeObject<ObservableCollection<TableDetails>>(tblist);
            TableList = templist;

        }



    }
}

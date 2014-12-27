using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;

namespace AzureSQLApp.Helpers
{
    class ShowDialogMessage : MessageBase
    {
        public ICommand Yes { get; set; }
        public ICommand No { get; set; }
        public ICommand Cancel { get; set; }
    
    }
}

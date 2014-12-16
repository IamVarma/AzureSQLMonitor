using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AzureSQLApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AzureSQLApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListDatabasesView : Page
    {
       private ListDatabasesViewModel listdatabasesviewmodel = new ListDatabasesViewModel();


        public ListDatabasesViewModel ListDatabasesViewModel
        {

            get { return listdatabasesviewmodel; }

        }



        public ListDatabasesView()
        {
            this.InitializeComponent();
        }

       /* protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
           

        }*/
    }
}

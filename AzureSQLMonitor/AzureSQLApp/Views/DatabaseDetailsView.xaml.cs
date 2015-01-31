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
using AzureSQLApp.Common;
using AzureSQLApp.ViewModels;
using AzureSQLApp.Models;
using System.Threading.Tasks;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AzureSQLApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DatabaseDetailsView : Page
    {
        Databases selectedDatabase = new Databases();

        private NavigationHelper navigationHelper;
        DatabaseDetailsViewModel _databasedetails = new DatabaseDetailsViewModel();
        DispatcherTimer DispatchTimerTab1;
        DispatcherTimer DispatchTimerTab2;
        DispatcherTimer DispatchTimerTab3;
        DispatcherTimer DispatchTimerTab4;

        public void DispactcherTimeSetupTab1()
        {
            DispatchTimerTab1 = new DispatcherTimer();
            DispatchTimerTab1.Tick += DispatchTimer_Tick_Tab1;
            DispatchTimerTab1.Interval = new TimeSpan(0, 0, 5);
            DispatchTimerTab1.Start();

        }

        

        public void DispactcherTimeSetupTab2()
        {
            DispatchTimerTab2 = new DispatcherTimer();
            DispatchTimerTab2.Tick += DispatchTimer_Tick_Tab2;
            DispatchTimerTab2.Interval = new TimeSpan(0, 0, 10);
           

        }

        public void DispactcherTimeSetupTab3()
        {
            DispatchTimerTab3 = new DispatcherTimer();
            DispatchTimerTab3.Tick += DispatchTimer_Tick_Tab3;
            DispatchTimerTab3.Interval = new TimeSpan(0, 0, 10);
            

        }

        public void DispactcherTimeSetupTab4()
        {
            DispatchTimerTab4 = new DispatcherTimer();
            DispatchTimerTab4.Tick += DispatchTimer_Tick_Tab4;
            DispatchTimerTab4.Interval = new TimeSpan(0, 0, 15);
           

        }


        async void DispatchTimer_Tick_Tab1(object sender, object e)
        {
            if(BasicDetailsGrid.Visibility==Visibility.Visible)
                await DatabaseDetails.GetConnectionCount(selectedDatabase.DatabaseName);
    
        }


        async void DispatchTimer_Tick_Tab2(object sender, object e)
        {
            if (ResourceUsageGrid.Visibility == Visibility.Visible)
                await DatabaseDetails.GetResourceUsage(selectedDatabase.DatabaseName);


        }
        async void DispatchTimer_Tick_Tab3(object sender, object e)
        {
            if (ConnectionsGrid.Visibility == Visibility.Visible)
                await DatabaseDetails.GetDBConnectionDetails(selectedDatabase.DatabaseName);

        }

        async void DispatchTimer_Tick_Tab4(object sender, object e)
        {
            if (CPUConsumersGrid.Visibility == Visibility.Visible)
            {
                await DatabaseDetails.GetTopCpuConsumers(selectedDatabase.DatabaseName);
            }
        }


            public DatabaseDetailsViewModel DatabaseDetails
            {
                get { return _databasedetails; }
            }

        public DatabaseDetailsView()
        {
            this.InitializeComponent();
               this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

          private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

            selectedDatabase = (Databases)e.NavigationParameter;
            DBName.Text = selectedDatabase.DatabaseName;
            state.Text = selectedDatabase.DatabaseState;
            slo.Text = selectedDatabase.DatabaseSlo;
            access.Text = selectedDatabase.AccessDesc;
            readstatus.Text = selectedDatabase.IsReadOnly;
            encryptonly.Text = selectedDatabase.IsEncrypt;
            await DatabaseDetails.GetDatabaseSize(selectedDatabase.DatabaseName);
            await DatabaseDetails.GetTablesCommand(selectedDatabase.DatabaseName);
            await DatabaseDetails.GetConnectionCount(selectedDatabase.DatabaseName);
            DispactcherTimeSetupTab1();
            DispatchTimerTab1.Start();
            BasicDetails.IsEnabled = false;
            DispactcherTimeSetupTab2();
            DispactcherTimeSetupTab3();
            DispactcherTimeSetupTab4();
        }


        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void BasicDetails_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimerTab2.Stop();
            DispatchTimerTab3.Stop();
            DispatchTimerTab4.Stop();
            ResourceUsageGrid.Visibility = Visibility.Collapsed;
            ConnectionsGrid.Visibility = Visibility.Collapsed;
            CPUConsumersGrid.Visibility = Visibility.Collapsed;
            
            BasicDetailsGrid.Visibility = Visibility.Visible;
            BasicDetails.IsEnabled = false;
            ResourceDetails.IsEnabled = true;
            ConnectionDetails.IsEnabled = true;
            TopCPUConsumers.IsEnabled = true;

            await DatabaseDetails.GetDatabaseSize(selectedDatabase.DatabaseName);
            await DatabaseDetails.GetTablesCommand(selectedDatabase.DatabaseName);
            await DatabaseDetails.GetConnectionCount(selectedDatabase.DatabaseName);
            DispatchTimerTab1.Start();
        }


        private async void ResourceDetails_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimerTab1.Stop();
            DispatchTimerTab3.Stop();
            DispatchTimerTab4.Stop();
            BasicDetailsGrid.Visibility = Visibility.Collapsed;
            ConnectionsGrid.Visibility = Visibility.Collapsed;
            CPUConsumersGrid.Visibility = Visibility.Collapsed;
            ResourceUsageGrid.Visibility = Visibility.Visible;
            BasicDetails.IsEnabled = true;
            ResourceDetails.IsEnabled = false;
            ConnectionDetails.IsEnabled = true;
            TopCPUConsumers.IsEnabled = true;
            await DatabaseDetails.GetResourceUsage(selectedDatabase.DatabaseName);
            DispatchTimerTab2.Start();
            
        }

       

        private async void ConnectionDetails_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimerTab1.Stop();
            DispatchTimerTab2.Stop();
            DispatchTimerTab4.Stop();
            ResourceUsageGrid.Visibility = Visibility.Collapsed;
            BasicDetailsGrid.Visibility = Visibility.Collapsed;
            CPUConsumersGrid.Visibility = Visibility.Collapsed;
            ConnectionsGrid.Visibility = Visibility.Visible;
            BasicDetails.IsEnabled = true;
            ResourceDetails.IsEnabled = true;
            ConnectionDetails.IsEnabled = false;
            TopCPUConsumers.IsEnabled = true;
            await DatabaseDetails.GetDBConnectionDetails(selectedDatabase.DatabaseName);
            DispatchTimerTab3.Start();
        }


        private async void TopCPUConsumers_OnClick(object sender, RoutedEventArgs e)
        {
            DispatchTimerTab1.Stop();
            DispatchTimerTab2.Stop();
            DispatchTimerTab3.Stop();
            ResourceUsageGrid.Visibility = Visibility.Collapsed;
            BasicDetailsGrid.Visibility = Visibility.Collapsed;
            ConnectionsGrid.Visibility = Visibility.Collapsed;
            CPUConsumersGrid.Visibility = Visibility.Visible;
            BasicDetails.IsEnabled = true;
            ResourceDetails.IsEnabled = true;
            ConnectionDetails.IsEnabled = true;
            TopCPUConsumers.IsEnabled = false;
            await DatabaseDetails.GetTopCpuConsumers(selectedDatabase.DatabaseName);
            DispatchTimerTab4.Start();


        }
    }
}

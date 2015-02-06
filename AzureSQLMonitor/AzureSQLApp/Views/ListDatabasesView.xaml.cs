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
using AzureSQLApp.Common;
using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AzureSQLApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListDatabasesView : Page
    {
        private NavigationHelper navigationHelper;

      private ListDatabasesViewModel listdatabasesviewmodel = new ListDatabasesViewModel();
       public ListDatabasesViewModel ListDatabasesViewModel
        {

            get { return listdatabasesviewmodel; }

        }



        public ListDatabasesView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState; 
            this.Unloaded += page_Unloaded;
           
            gdChild.Width = Window.Current.Bounds.Width;
            PrivacyCharm.Height = Window.Current.Bounds.Height;
            DevelopedBy.Height = Window.Current.Bounds.Height;
            HowCharm.Height = Window.Current.Bounds.Height;
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;

        }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            DetermineVisualState();

            ProgressBar.IsActive = true;
            databasesgrid.Visibility = Visibility.Collapsed;
            await ListDatabasesViewModel.getSysInfo();
            await ListDatabasesViewModel.GetDatabasesCommand();
         
            DatabaseGridView.SelectedItem = null;
            databasesgrid.Visibility = Visibility.Visible;
            ProgressBar.IsActive = false;
            
            /*DateTime dt = new DateTime();
            dt = DateTime.Now.ToUniversalTime();
            timetextdata.Text = dt.ToString("dd-MM-yyyy HH:mm:ss");*/
         
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

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("a", "Privacy Policy", (p) => { PrivacyCharm.IsOpen = true; }));
            args.Request.ApplicationCommands.Add(new SettingsCommand("b", "How does it work?", (p) => { HowCharm.IsOpen = true; }));
            args.Request.ApplicationCommands.Add(new SettingsCommand("s", "Developed By", (p) => { DevelopedBy.IsOpen = true; }));
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SettingsPane.GetForCurrentView().CommandsRequested -= CommandsRequested;
            base.OnNavigatingFrom(e);
        }


        //private void page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Window.Current.SizeChanged += Window_SizeChanged;
        //    DetermineVisualState();
        //}

        private void page_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            DetermineVisualState();
        }

        private void DetermineVisualState()
        {
            var state = string.Empty;
            var applicationView = ApplicationView.GetForCurrentView();
            var size = Window.Current.Bounds;

            if (applicationView.IsFullScreen)
            {
                if (applicationView.Orientation == ApplicationViewOrientation.Landscape)
                {
                    state = "FullScreenLandscape";
                }
                else
                {
                    ScreenSzieGrid.Width = Window.Current.Bounds.Width;
                    state = "FullScreenPortrait";
                }
            }
            else
            {

                state = "Narrow";
                ScreenSzieGrid.Width = Window.Current.Bounds.Width;

            }

            VisualStateManager.GoToState(this, state, true);
        }



    }
}

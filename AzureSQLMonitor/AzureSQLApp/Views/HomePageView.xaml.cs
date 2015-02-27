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
using AzureSQLApp.Views;
using AzureSQLApp.Common;
using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AzureSQLApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePageView : Page
    {
        private LoginViewModel loginviewmodel = new LoginViewModel();
       


        public LoginViewModel LoginModel
        {

            get { return loginviewmodel; }

        }

        

        public HomePageView()
        {
            this.InitializeComponent();

            this.Loaded += page_Loaded;
            this.Unloaded += page_Unloaded;


            gdChild.Width = Window.Current.Bounds.Width;
            gdChild1.Width = Window.Current.Bounds.Width;
            PrivacyCharm.Height = Window.Current.Bounds.Height;
            RateApp.Height = Window.Current.Bounds.Height;
           
            DevelopedBy.Height = Window.Current.Bounds.Height;
            HowCharm.Height = Window.Current.Bounds.Height;
            SettingsPane.GetForCurrentView().CommandsRequested += CommandsRequested;
            
            
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SettingsPane.GetForCurrentView().CommandsRequested -= CommandsRequested;
            base.OnNavigatingFrom(e);
        }


        private void HelpButton_OnClick(object sender, RoutedEventArgs e)
        {
            HelpPop.IsOpen = true;


        }

        private void GotIt_OnClick(object sender, RoutedEventArgs e)
        {
            HelpPop.IsOpen = false;
        }

        private void HelpPop_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("a", "Privacy Policy", (p) => { PrivacyCharm.IsOpen = true; }));
            args.Request.ApplicationCommands.Add(new SettingsCommand("b", "How does it work?", (p) => { HowCharm.IsOpen = true; }));
            args.Request.ApplicationCommands.Add(new SettingsCommand("s", "Developed By", (p) => { DevelopedBy.IsOpen = true; }));
            args.Request.ApplicationCommands.Add(new SettingsCommand("s", "Rate AzureSQLMonitor", (p) => { RateApp.IsOpen = true; }));
        }


        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Window_SizeChanged;
            DetermineVisualState();
        }

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


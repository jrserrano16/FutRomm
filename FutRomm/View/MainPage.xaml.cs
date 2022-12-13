using FutRomm.Model;
using FutRomm.View;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static FutRomm.Model.LeagueSearch;
using static FutRomm.Model.NationsSearch;
using static FutRomm.Model.PlayersSearch;
using static FutRomm.Model.TeamApi;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace FutRomm
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationViewItem _lastNav;
        public MainPage()
        {
            this.InitializeComponent();
            // for(int i = 0; i < 2000; i++)
            // {
            //  Controller.Controller.deleteNodes();
            //}        
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
            }
            else
            {
                view.TryEnterFullScreenMode();
            }
        }
        private void NaviView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == _lastNav)
                return;
            var clickedView = item.Tag?.ToString() ?? "SettingsView";
            if (!NavigateToView(clickedView)) return;
            _lastNav = item;
        }

        private bool NavigateToView(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"FutRomm.View.{clickedView}");



            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;


            frame.Navigate(view, this, new EntranceNavigationTransitionInfo());
            return true;
        }
        public void getPlayerInfo(Player p)
        {
            frame.Navigate(typeof(PlayerInfo), p);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }
        public void navigateMillionaire()
        {
            frame.Navigate(typeof(MillionaireGame));
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void NaviView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        // App.xaml.cs
        //
        // Add this method to the App class.
        
    }
}

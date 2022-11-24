using FutRomm.Model;
using System.Collections.Generic;
using System.Reflection;
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

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }
        private void yeahPlayer()
        {
            for (int i = 1; i <= 843; i++)
            {
                PlayersResult result = Controller.Controller.GetPlayers(i);

                if (result == null)
                {
                    return;
                }

                var listaPlayers = new List<Player>();
                foreach (PlayerR item in result.items)
                {
                    Controller.Controller.addPlayer(item);
                }
            }
        }

        private void yeahNation()
        {
            for (int i = 1; i <= 8; i++)
            {
                NationsResult result = Controller.Controller.GetNations(i);

                if (result == null)
                {
                    return;
                }

                foreach (NationR item in result.items)
                {
                    Controller.Controller.addNation(item);
                }
            }
        }
        private void yeahTeam()
        {
            for (int i = 1; i <= 35; i++)
            {
                TeamsResult result = Controller.Controller.GetTeams(i);

                if (result == null)
                {
                    return;
                }

                foreach (TeamR item in result.items)
                {
                    Controller.Controller.addTeams(item);
                }
            }
        }
        private void yeahLeague()
        {
            for (int i = 1; i <= 3; i++)
            {
                LeagueResult result = Controller.Controller.GetLeagues(i);

                if (result == null)
                {
                    return;
                }

                foreach (LeagueR item in result.items)
                {
                    Controller.Controller.addleague(item);
                }
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

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void NaviView_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}

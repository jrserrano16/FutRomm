using FutRomm.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace FutRomm.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PlayerList : Page
    {
        List<Player> playerList = new List<Player>();
        List<Player> playerListAUX = new List<Player>();
        MainPage padre;
        public PlayerList()
        {
            this.InitializeComponent();

            playerList = Controller.Controller.loadPlayers();
            playerListAUX = playerList;
            filterSearch();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            padre = (MainPage)e.Parameter;
        }

        private void Panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*  if (cmb_Filter.SelectedIndex==4)
              {
                  padre.irHookedex2(PlayerMixListAUX[ventana.SelectedIndex]);
              }
              padre.irHookedex(playerListAUX[ventana.SelectedIndex]);
          */
        }

        private void tbx_Filtro_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            filterSearch();
        }
        private void filterSearch()
        {
            PlayerTemplate Player;
            playerListAUX = new List<Player>();
            ventana.Items.Clear();
            switch (cmb_Filter.SelectedIndex)
            {
                case 0:
                    foreach (Player p in playerList)
                    {
                        playerListAUX.Add(p);
                        Player = new PlayerTemplate(p);
                        ventana.Items.Add(Player);
                    }
                    break;
                case 1:
                    foreach (Player p in playerList)
                    {
                        if (p.name.ToLower().Contains(tbx_Filtro.Text.ToLower()))
                        {
                            playerListAUX.Add(p);
                            Player = new PlayerTemplate(p);
                            ventana.Items.Add(Player);
                        }

                    }
                    break;

                case 2:
                    foreach (Player p in playerList)
                    {
                        if (p.club.ToLower().Contains(tbx_Filtro.Text.ToLower()))
                        {
                            playerListAUX.Add(p);
                            Player = new PlayerTemplate(p);
                            ventana.Items.Add(Player);
                        }

                    }
                    break;

                case 3:
                    foreach (Player p in playerList)
                    {
                        if (p.nationality.ToLower().Contains(tbx_Filtro.Text.ToLower()))
                        {
                            playerListAUX.Add(p);
                            Player = new PlayerTemplate(p);
                            ventana.Items.Add(Player);
                        }

                    }
                    break;

                case 4:
                    foreach (Player p in playerList)
                    {
                        if (p.position.ToLower().Contains(tbx_Filtro.Text.ToLower()))
                        {
                            playerListAUX.Add(p);
                            Player = new PlayerTemplate(p);
                            ventana.Items.Add(Player);
                        }

                    }
                    break;


                default:
                    foreach (Player p in playerList)
                    {
                        playerListAUX.Add(p);
                        Player = new PlayerTemplate(p);
                        ventana.Items.Add(Player);
                    }
                    break;

            }
        }

        private void cmb_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbx_Filtro.IsEnabled = true;
            tbx_Filtro.Text = String.Empty;
            if (cmb_Filter.SelectedIndex==0)
            {
                tbx_Filtro.IsEnabled = false;
                tbx_Filtro.Text = "Select Filter";
            }
            filterSearch();
        }

        private void tbx_Filtro_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            tbx_Filtro.Text = String.Empty;
            tbx_Filtro.FontStyle = Windows.UI.Text.FontStyle.Normal;


        }


    }
}



using FutRomm.Model;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace FutRomm.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PlayerGuess : Page
    {
        private static List<Player> playerList = new List<Player>();
        private static List<Player> playerListAUX = new List<Player>();
        private static List<TextBox> textBoxes = new List<TextBox>();
        private int ntry = 0;

        private BitmapImage interrogation = new BitmapImage(new Uri("ms-appx:///Assets//INT.png"));
        private BitmapImage correct = new BitmapImage(new Uri("ms-appx:///Assets//correct.png"));
        private BitmapImage incorrect = new BitmapImage(new Uri("ms-appx:///Assets//incorrect.png"));

        private Random rd = new Random();
        private static Player player;
        public PlayerGuess()
        {
            this.InitializeComponent();
            playerList = Controller.Controller.loadPlayers();
            repeat();
        }

        private void tbx_guess_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (tbx_guess.Text == string.Empty)
            {
                desp.Items.Clear();
            }
            else
            {
                filterSearch();
            }

        }
        private void filterSearch()
        {
            SearchTemplate Player;
            playerListAUX = new List<Player>();
            desp.Items.Clear();

            foreach (Player p in playerList)
            {
                if (p.name.ToLower().Contains(tbx_guess.Text.ToLower()))
                {
                    playerListAUX.Add(p);
                    Player = new SearchTemplate(p);
                    desp.Items.Add(Player);
                }
            }
        }
        private void getTextBoxes(Grid grid)
        {
            foreach (var element in grid.Children)
            {
                if (element is TextBox)
                {
                    TextBox tbx = (TextBox)element;
                    if (tbx.Tag != null && tbx.Tag.Equals("Try"))
                    {
                        textBoxes.Add(tbx);
                    }
                }
            }
        }
        private void desp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int percentaje = 0;
            if (desp.SelectedIndex >= 0)
            {
                Player pfound = playerListAUX[desp.SelectedIndex];
                if (pfound != null)
                {
                    if (pfound.nation.Equals(player.nation))
                    {
                        nation.Source = correct;
                        img_nation.Source = new BitmapImage(new Uri(player.nation_logo));
                        percentaje+=20;
                    }
                    if (pfound.league.Equals(player.league))
                    {
                        league.Source = correct;
                        img_league.Source = new BitmapImage(new Uri(player.league_logo));
                        percentaje+=20;
                    }
                    if (pfound.club.Equals(player.club))
                    {
                        team.Source = correct;
                        img_team.Source = new BitmapImage(new Uri(player.club_logo));
                        percentaje+=20;
                    }
                    if (pfound.position.Equals(player.position))
                    {
                        position.Source = correct;

                        tbx_position.Text = player.position;
                        img_position.Visibility = Visibility.Collapsed;
                        percentaje+=20;
                    }
                    if (pfound.age.Equals(player.age))
                    {
                        age.Source = correct;
                        tbx_age.Text = pfound.age.ToString();
                        img_age.Visibility = Visibility.Collapsed;
                        percentaje+=20;
                    }
                }
                tbx_guess.Text = string.Empty;
                desp.Items.Clear();
                textBoxes[ntry++].Text += pfound.name +" - " +percentaje +" %";
                if (ntry == 6)
                {
                    tbx_name.Text = player.name;
                    tbx_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 190, 63, 63));
                    tbx_guess.IsEnabled = false;
                }
                if (pfound.id.Equals(player.id))
                {
                    tbx_name.Text = player.name;
                    tbx_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 190, 89));
                    tbx_guess.IsEnabled = false;
                }
            }
        }
        private void repeat()
        {
            int number = rd.Next(0, playerList.Count);
            player = playerList[number];
            img_player.Source = new BitmapImage(new Uri(playerList[number].photo));
            textBoxes.Clear();
            getTextBoxes(grid);
            ntry = 0;
            foreach (TextBox t in textBoxes)
            {
                t.Text = "#"+(++ntry)+" : ";
            }
            ntry = 0;
            tbx_name.Text = "???????????";
            tbx_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 6, 5, 10));
            nation.Source = incorrect;
            league.Source = incorrect;
            team.Source = incorrect;
            age.Source = incorrect;
            position.Source = incorrect;

            tbx_age.Text = string.Empty;
            tbx_position.Text = string.Empty;


            img_nation.Source = interrogation;
            img_league.Source = interrogation;
            img_team.Source = interrogation;
            img_age.Source = interrogation;
            img_age.Visibility = Visibility.Visible;
            img_position.Visibility = Visibility.Visible;
            img_position.Source = interrogation;
            tbx_guess.IsEnabled = true;
            tbx_guess.Text = "Buscar Jugador";
            tbx_guess.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }
        private void btn_info_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            repeat();
        }

        private void tbx_guess_GotFocus(object sender, RoutedEventArgs e)
        {
            tbx_guess.Text = String.Empty;
            tbx_guess.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }
    }
}

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
    public sealed partial class TeamGuess : Page
    {
        private static List<Team> teamList = new List<Team>();
        private static List<Team> teamListAUX = new List<Team>();
        private static List<TextBox> textBoxes = new List<TextBox>();
        private int ntry = 0;

        private Random rd = new Random();
        private static Team team;
        public TeamGuess()
        {
            this.InitializeComponent();
            teamList = Controller.Controller.loadTeams();
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
            SearchTemplateTeam team;
            teamListAUX = new List<Team>();
            desp.Items.Clear();

            foreach (Team t in teamList)
            {
                if (t.name.ToLower().Contains(tbx_guess.Text.ToLower()))
                {
                    teamListAUX.Add(t);
                    team = new SearchTemplateTeam(t);
                    desp.Items.Add(team);
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
                Team tfound = teamListAUX[desp.SelectedIndex];
                if (tfound != null)
                {
                    if (tfound.country.Equals(team.country))
                    {
                        country.Source = new BitmapImage(new Uri("ms-appx:///Assets//correct.png"));
                        img_country.Source = new BitmapImage(new Uri(team.country_photo));
                        percentaje+=33;
                    }
                    if (tfound.league.Equals(team.league))
                    {
                        league.Source = new BitmapImage(new Uri("ms-appx:///Assets//correct.png"));
                        img_league.Source = new BitmapImage(new Uri(team.league_photo));
                        percentaje+=33;
                    }
                }
                tbx_guess.Text = string.Empty;
                desp.Items.Clear();
                textBoxes[ntry++].Text += tfound.name +" - " +percentaje +" %";
                if (ntry == 6)
                {
                    tbx_name.Text = team.name;
                    tbx_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 190, 63, 63));
                    tbx_guess.IsEnabled = false;
                }
                if (tfound.id.Equals(team.id))
                {
                    textBoxes[--ntry].Text = "#"+(+ntry+1)+" : "+tfound.name +" - 100 %";
                    tbx_name.Text = team.name;
                    tbx_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 63, 190, 89));
                    tbx_guess.IsEnabled = false;
                }
            }
        }
        private void repeat()
        {
            int number = rd.Next(0, teamList.Count);
            team = teamList[number];
            img_team.Source = new BitmapImage(new Uri(teamList[number].photo));
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
            country.Source = new BitmapImage(new Uri("ms-appx:///Assets//incorrect.png"));
            league.Source = new BitmapImage(new Uri("ms-appx:///Assets//incorrect.png"));
            img_country.Source = new BitmapImage(new Uri("ms-appx:///Assets//INT.png"));
            img_league.Source = new BitmapImage(new Uri("ms-appx:///Assets//INT.png"));
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

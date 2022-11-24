using FutRomm.Model;
using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace FutRomm.View
{
    public sealed partial class PlayerTemplate : UserControl
    {
        public PlayerTemplate(Player p)
        {
            {
                InitializeComponent();
                name.Text = p.name;
                pos.Text = p.position;

                club.Source = new BitmapImage(new Uri(p.club_logo));
                nation.Source = new BitmapImage(new Uri(p.nation_logo));
                switch (p.league)
                {
                    case "Eredivisie":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//eredivisie.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    case "Premier League":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//premier.png"));
                        name.Foreground = new SolidColorBrush(Color.FromArgb(255, 54, 0, 61));
                        pos.Foreground = new SolidColorBrush(Color.FromArgb(255, 54, 0, 61));
                        break;
                    case "Ligue 1":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//ligue1.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    case "Bundesliga":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//bundes.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    case "Serie A TIM":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//serieA.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    case "LaLiga Santander":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//laliga.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    case "Liga NOS":
                        template.Source = new BitmapImage(new Uri("ms-appx:///Assets//templates//nos.png"));
                        name.Foreground = new SolidColorBrush(Colors.White);
                        pos.Foreground = new SolidColorBrush(Colors.White);
                        break;

                }
                Image.Source = new BitmapImage(new Uri(p.photo));
            }
        }
    }
}


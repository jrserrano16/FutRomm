using FutRomm.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace FutRomm.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PlayerInfo : Page
    {
        Player p;
        public PlayerInfo()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = (Player)e.Parameter;
            txt_age.Text = "Age : "+p.age.ToString();
            txt_nombre.Text = p.name;
            txt_nation.Text = "Nation : "+ p.nation;
            txt_league.Text = "League : "+p.league;
            txt_foot.Text = "Foot : "+p.foot;
            txt_position.Text = "Position : "+p.position;
            txt_team.Text = "Team : "+p.club;

            if (p.foot.Equals("Left"))
            {
                img_foot.Source = new BitmapImage(new Uri("ms-appx:///Assets//left.png"));
            }
            else
            {
                img_foot.Source = new BitmapImage(new Uri("ms-appx:///Assets//right.png"));
            }
            img_league.Source = new BitmapImage(new Uri(p.league_logo));
            img_team.Source = new BitmapImage(new Uri(p.club_logo));
            img_nation.Source = new BitmapImage(new Uri(p.nation_logo));
            img_player.Source = new BitmapImage(new Uri(p.photo));
            getPosition(grid);

        }
        private void getPosition(Grid grid)
        {
            foreach (var element in grid.Children)
            {
                if (element is Image)
                {
                    Image image = (Image)element;
                    if (image.Tag != null && image.Tag.Equals(p.position))
                    {
                        element.Opacity = 100;
                    }
                }
                
            }
        }
    }
}

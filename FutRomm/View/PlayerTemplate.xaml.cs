using FutRomm.Model;
using System;
using Windows.UI.Xaml.Controls;
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
                Image.Source = new BitmapImage(new Uri(p.photo));
                club.Source = new BitmapImage(new Uri(p.club_logo));
                nation.Source = new BitmapImage(new Uri(p.flag));
            }
        }
    }
}


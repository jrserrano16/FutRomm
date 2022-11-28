using FutRomm.Model;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace FutRomm.View
{
    public sealed partial class SearchTemplate : UserControl
    {
        public SearchTemplate(Player p)
        {
            this.InitializeComponent();
            nation.Source = new BitmapImage(new Uri(p.nation_logo));
            team.Source = new BitmapImage(new Uri(p.club_logo));
            name.Text = p.name;
        }
    }
}

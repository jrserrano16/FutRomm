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
    public sealed partial class SearchTemplateTeam : UserControl
    {
        public SearchTemplateTeam(Team t)
        {
            this.InitializeComponent();
            nation.Source = new BitmapImage(new Uri(t.country_photo));
            league.Source = new BitmapImage(new Uri(t.league_photo));
            name.Text = t.name;
        }
    }
}

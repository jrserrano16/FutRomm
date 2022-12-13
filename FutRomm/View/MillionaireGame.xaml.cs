using FutRomm.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Image = Windows.UI.Xaml.Controls.Image;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace FutRomm.View
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MillionaireGame : Page
    {
        private Random rd = new Random();
        private static List<Question> listaQuestions;
        private string solution;
        private static Question[] questions = new Question[15];
        DispatcherTimer dtEspera = new DispatcherTimer();
        private static int count = 0;
        private static int aciertos = 0;
        private static int fallos = 0;
        private static List<TextBlock> options = new List<TextBlock>();
        private static int but = 0;
        private static string money = "0€";
        private static int c1 = 0;
        private static int c2 = 0;
        private static int c3 = 0;

        private BitmapImage black_ans = new BitmapImage(new Uri("ms-appx:///Assets//mill-black.png"));
        private BitmapImage wrong_ans = new BitmapImage(new Uri("ms-appx:///Assets//mill-incorrect.png"));
        private BitmapImage corr_ans = new BitmapImage(new Uri("ms-appx:///Assets//mill-correct.png"));



        public MillionaireGame()
        {
            this.InitializeComponent();
            listaQuestions = Controller.Controller.loadQuestions();
            for (int i = 0; i < 15; i++)
            {
                int number = rd.Next(0, listaQuestions.Count);
                questions[i] = listaQuestions[number];
            }
            espera();
        }
        private void espera()
        {
            dtEspera.Interval = TimeSpan.FromMilliseconds(5500);
            dtEspera.Tick += millionaireGame;
            dtEspera.Start();
        }
        private void millionaireGame(object sender, object e)
        {
            if (count < 15 && fallos < 3)
            {
                if (but == 0)
                {
                    tbx_Maldini.Text = "Siguiente Pregunta : "+(count+1);
                    solution = questions[count].solution;
                    tbx_Q.Text = questions[count].question;
                    A_o.Text= questions[count].answers[0];
                    B_o.Text = questions[count].answers[1];
                    C_o.Text = questions[count].answers[2];
                    D_o.Text = questions[count].answers[3];
                    resetOptions(true);
                    if (c1 == 0)
                    {
                        btn_50.IsEnabled = true;
                    }
                    if (c2 == 0)
                    {
                        btn_public.IsEnabled = true;
                    }
                    if (c3 == 0)
                    {
                        btn_call.IsEnabled = true;
                    }
                }
            }
            else
            {
                img_maldini.Visibility = Visibility.Collapsed;
                if (aciertos < 15)
                {
                    
                    img_maldini__15.Visibility = Visibility.Visible;
                }
                else
                {
                    img_maldini_15.Visibility = Visibility.Visible;

                }
                btn_call.IsEnabled = false;
                btn_50.IsEnabled = false;
                btn_public.IsEnabled = false;
                tbx_Maldini.Text = $"Fin del Juego!!! Has respondido {aciertos} preguntas correctas y {fallos} preguntas incorrectas. La cantidad ganada es de {money} ";
            }
        }
        private void selectOption(object sender, PointerRoutedEventArgs e)
        {
            var item = e.OriginalSource;

            if (item is Grid)
            {
                checkOption((Grid)item);
            }
            else if (item is TextBlock)
            {
                TextBlock tb = (TextBlock)item;
                checkOption((Grid)tb.Parent);
            }
            else
            {
                Image im = (Image)item;
                checkOption((Grid)im.Parent);
            }
        }
        private void checkOption(Grid g)
        {
            if (g.IsTapEnabled)
            {
                btn_public.IsEnabled = false;
                btn_50.IsEnabled = false;
                btn_call.IsEnabled = false;
                but = 0;
                TextBlock tb_opt = null;
                Image img_opt = null;
                foreach (var element in g.Children)
                {
                    if (element is TextBlock)
                    {
                        TextBlock tbx = (TextBlock)element;
                        if (tbx.Tag != null && tbx.Tag.Equals("Option"))
                        {
                            tb_opt = (TextBlock)tbx;
                            break;
                        }
                    }
                }
                foreach (var element in g.Children)
                {
                    if (element is Image)
                    {
                        img_opt = (Image)element;
                        break;
                    }
                }

                if (tb_opt.Text.Equals(solution))
                {
                    img_opt.Source = corr_ans;
                    tbx_Maldini.Text = $"Correcto!! Asciendes de escalón. Aún tienes {tbx_lives.Text} vidas";
                    aciertos++;
                    getEscalon();
                }
                else
                {
                    img_opt.Source = wrong_ans;
                    fallos++;
                    tbx_lives.Text = (Convert.ToInt32(tbx_lives.Text) - 1).ToString();
                    tbx_Maldini.Text = $"Vaya, la opción es incorrecta... Te mantienes en el escalón. Tienes {tbx_lives.Text} intentos más";

                    searchCorrect();
                }
                count++;
                resetOptions(false);
                espera();

            }
        }

        private void getEscalon()
        {
            StackPanel sp;
            string bote = $"bote{aciertos+1}";
            string bote_prev = $"bote{aciertos}";
            TextBox tb;
            string[] isvisited = { "none", "none" };
            foreach (var e in grid.Children)
            {
                if (e is StackPanel)
                {
                    sp = (StackPanel)e;
                    foreach (var t in sp.Children)
                    {
                        tb = (TextBox)t;
                        if (tb.Name.Equals(bote_prev))
                        {
                            money = tb.Tag.ToString();
                            tb.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 63, 190, 89));
                        }
                        if (tb.Name.Equals(bote))
                        {
                            tb.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 122, 197, 254));
                        }
                    }
                }
            }
        }

        private void searchCorrect()
        {
            if (A_o.Text.Equals(solution))
            {
                img_A.Source = corr_ans;
            }
            else if (B_o.Text.Equals(solution))
            {
                img_B.Source = corr_ans;
            }
            else if (D_o.Text.Equals(solution))
            {
                img_D.Source = corr_ans;
            }
            else if (C_o.Text.Equals(solution))
            {
                img_C.Source = corr_ans;
            }

        }

        private void resetOptions(bool valor)
        {
            opt_A.IsTapEnabled = valor;
            opt_B.IsTapEnabled = valor;
            opt_C.IsTapEnabled=valor;
            opt_D.IsTapEnabled=valor;


            if (valor)
            {
                img_A.Source = black_ans;
                img_B.Source = black_ans;
                img_C.Source = black_ans;
                img_D.Source = black_ans;
            }
            else
            {
                opt_A.Opacity = 1;
                opt_B.Opacity = 1;
                opt_C.Opacity = 1;
                opt_D.Opacity = 1;
            }

        }

        private void btn_50_Click(object sender, RoutedEventArgs e)
        {
            if (btn_50.IsEnabled)
            {
                c1 = 1;
                btn_call.IsEnabled = false;
                btn_50.IsEnabled = false; 
                btn_public.IsEnabled = false;
                tbx_Maldini.Text = "El jugador utiliza el comodín 50:50!! Se han deshabilitado 2 opciones!";
                
                foreach (var item in grid.Children)
                {
                    if (item is Grid)
                    {
                        Grid newGrid = (Grid)item;
                        foreach (var it in newGrid.Children)
                        {
                            if (it is TextBlock)
                            {
                                TextBlock tb = (TextBlock)it;
                                if (!tb.Text.Equals(solution) && tb.Tag != null)
                                {
                                    options.Add(tb);
                                    newGrid.Opacity = 0.5;
                                    newGrid.IsTapEnabled = false;
                                }
                            }
                        }
                    }
                }
                int numb = rd.Next(0, options.Count);
                options[numb].Opacity = 0.5;
                but = 1;
                foreach (var item in grid.Children)
                {
                    if (item is Grid)
                    {
                        Grid newGrid = (Grid)item;
                        foreach (var it in newGrid.Children)
                        {
                            if (it is TextBlock)
                            {
                                TextBlock tb = (TextBlock)it;
                                if (tb.Text.Equals(options[numb].Text))
                                {
                                    newGrid.Opacity = 1;
                                    tb.Opacity = 1;
                                    newGrid.IsTapEnabled = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_call_Click(object sender, RoutedEventArgs e)
        {
            if (btn_call.IsEnabled)
            {
                c3 = 1;
                btn_call.IsEnabled = false;
                btn_50.IsEnabled = false;
                btn_public.IsEnabled = false;
                foreach (var item in grid.Children)
                {
                    if (item is Grid)
                    {
                        Grid newGrid = (Grid)item;
                        foreach (var it in newGrid.Children)
                        {
                            if (it is TextBlock)
                            {
                                TextBlock tb = (TextBlock)it;
                                if (!tb.Text.Equals(solution) && tb.Tag != null)
                                {
                                    newGrid.Opacity = 0.5;
                                    newGrid.IsTapEnabled = false;
                                }
                                else if (tb.Text.Equals(solution))
                                {
                                    tbx_Maldini.Text = $"El jugador utiliza el comodín Llamada de Maldini!! Según mi experiencia y tras tantos años analizando fútbol, diría que la respuesta es: '{tb.Text}'";

                                }
                            }
                        }
                    }
                }
                but = 1;
            }
        }
        private void btn_public_Click(object sender, RoutedEventArgs e)
        {
            if (btn_public.IsEnabled)
            {
                c2 = 1;
                btn_call.IsEnabled = false;
                btn_50.IsEnabled = false;
                btn_public.IsEnabled = false;
                int number = rd.Next(0, 4);
                tbx_Maldini.Text = $"El jugador utiliza el comodín del Público!! La mayoría del público piensa que la respuesta correcta es: '{questions[count].answers[number]}'";

            }
            but = 1;
        }
    }
}



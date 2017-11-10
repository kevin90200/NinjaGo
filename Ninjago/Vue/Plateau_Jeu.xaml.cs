using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Plateau_Jeu.xaml
    /// </summary>
    public partial class Plateau_Jeu 
    {
        String choixMain;

        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
        }
        public Plateau_Jeu()
        {
            InitializeComponent();
            Main.Visibility = Visibility.Hidden;
            C1.Visibility = Visibility.Hidden;
            C2.Visibility = Visibility.Hidden;
            C3.Visibility = Visibility.Hidden;
            C4.Visibility = Visibility.Hidden;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Content = choixMain;
           
            C1.IsChecked = false;
            C2.IsChecked = false;
            C3.IsChecked = false;
            C4.IsChecked = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        { 
            RadioButton choix = sender as RadioButton;
            choixMain=choix.Content.ToString();
        }
        Boolean retourner = true;
        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Hidden;
            C1.Visibility = Visibility.Hidden;
            C2.Visibility = Visibility.Hidden;
            C3.Visibility = Visibility.Hidden;
            C4.Visibility = Visibility.Hidden;
            int val_ret;
            if (btn_jouer.Content.ToString()== "Jouer") {
                if (retourner == true)
                {
                    val_ret = 0;
                    retourner = false;
                }
                else
                {
                    val_ret = 180;
                    retourner = true;
                }
                RotateTransform rotateTransform = new RotateTransform(val_ret);
                rotateTransform.CenterX = 0;
                rotateTransform.CenterY = 0;
                Plateau.RenderTransform = rotateTransform;
                btn_jouer.Content = "Passer";
                
                Main.Visibility= Visibility.Visible;
                C1.Visibility = Visibility.Visible;
                C2.Visibility = Visibility.Visible;
                C3.Visibility = Visibility.Visible;
                C4.Visibility = Visibility.Visible;



                for (int i = 1; i <= 3; i++)
                {
                    RotateTransform rotateTransformBtn = new RotateTransform(val_ret);
                    rotateTransformBtn.CenterX = 40;
                    rotateTransformBtn.CenterY = 40;
                    

                    Button btn1 = (Button)FindName("btn1_"+i);
                    if (btn1.Content.Equals(""))
                    {
                        btn1.RenderTransform = rotateTransformBtn;
                    }
                    Button btn2 = (Button)FindName("btn2_" + i);
                    if (btn2.Content.Equals(""))
                    {
                        btn2.RenderTransform = rotateTransformBtn;
                    }
                    Button btn3 = (Button)FindName("btn3_" + i);
                    if (btn3.Content.Equals(""))
                    {
                        btn3.RenderTransform = rotateTransformBtn;
                    }
                }
            }
           else
            {
                btn_jouer.Content = "Jouer";
            }
        }
        
    }
}

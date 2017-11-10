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
        //pour comit
        String choixMain;
        int action;
        List<Joueur> lesJ = new List<Joueur>();
        Joueur j1, j2;
        
        public Plateau_Jeu()
        {
            InitializeComponent();
            Main.Visibility = Visibility.Hidden;
            C1.Visibility = Visibility.Hidden;
            C2.Visibility = Visibility.Hidden;
            C3.Visibility = Visibility.Hidden;
            C4.Visibility = Visibility.Hidden;
            

            if (lesJ[1].dateNaissance > lesJ[2].dateNaissance)
            {
                j1 = lesJ[0];
                j2 = lesJ[1];
                
            }
            else
            {
                j1 = lesJ[1];
                j2 = lesJ[0];
            }

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (action.Equals(1))
            {
                Button btn = sender as Button;
                if (btn.Content.Equals(""))
                {
                    btn.Content = choixMain;
                    choixMain = null;
                    action = 0;
                }
            }
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
        { //poser qu'une carte par tour : variable globale utiliser aussi dans btn_click
            action = 1;
          // action pour retourner le plateau et indiquer le nom du personnage sur les radiobutton et transition entre j1 et j2
            int val_ret;
            if (btn_jouer.Content.ToString()== "Jouer") {
                if (retourner == true)
                {
                    val_ret = 0;
                    retourner = false;
                    nomJoueur.Content = j1.Nom + " " + j1.Prenom;
                    int i = 1;
                   foreach (Carte carteMain in j1.Main)
                    {
                        RadioButton c = (RadioButton)FindName("C" + i);
                        c.Content = carteMain.Nom;
                        i = i + 1;
                    }
                }
                else
                {
                    val_ret = 180;
                    retourner = true;
                    nomJoueur.Content = j2.Nom + " " + j2.Prenom;
                    int i = 1;
                    foreach (Carte carteMain in j2.Main)
                    {
                        RadioButton c = (RadioButton)FindName("C" + i);
                        c.Content = carteMain.Nom;
                        i = i + 1;
                    }
                }
                RotateTransform rotateTransform = new RotateTransform(val_ret);
                rotateTransform.CenterX = 0;
                rotateTransform.CenterY = 0;
                Plateau.RenderTransform = rotateTransform;
                //changer le label jouer en passer
                btn_jouer.Content = "Passer";
                //rendre visible la main du joueur
                Main.Visibility= Visibility.Visible;
                C1.Visibility = Visibility.Visible;
                C2.Visibility = Visibility.Visible;
                C3.Visibility = Visibility.Visible;
                C4.Visibility = Visibility.Visible;
                //action pour retourner les boutons vides ( comme sa le joueur aura toujours ses cartes a lui dans son sens)
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
            // cacher ses cartes
           else
            {
                btn_jouer.Content = "Jouer";
                nomJoueur.Content = "";
                Main.Visibility = Visibility.Hidden;
                C1.Visibility = Visibility.Hidden;
                C2.Visibility = Visibility.Hidden;
                C3.Visibility = Visibility.Hidden;
                C4.Visibility = Visibility.Hidden;
            }

        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
        }

    }
}

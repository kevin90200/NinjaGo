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
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Plateau_Jeu.xaml
    /// </summary>
    public partial class Plateau_Jeu
    {

        int action;
        List<Joueur> lesJ = new List<Joueur>();
        Joueur j1, j2;
        CartePersonnage selected = null;

        public Plateau_Jeu()
        {

            InitializeComponent();
            Main.Visibility = Visibility.Hidden;
            btn_jouer.Content = "Jouer";
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("joueur.json"));
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON

            foreach (var j in json)
            {
                List<CartePersonnage> deck = new List<CartePersonnage>();
                foreach (var cp in j.Deck)
                {
                    CartePersonnage c = new CartePersonnage(cp.Nom.ToString(), cp.Numero.ToString(), Convert.ToInt32(cp.Exemplaire), cp.Description.ToString(), cp.Type.ToString(), Convert.ToBoolean(cp.Deck1), Convert.ToBoolean(cp.Deck2), Convert.ToBoolean(cp.Deck3), Convert.ToInt32(cp.Attaque), Convert.ToInt32(cp.Defense), Convert.ToInt32(cp.Vitesse), Convert.ToInt32(cp.Force));
                    deck.Add(c);
                }
                Joueur player = new Joueur(Convert.ToString(j.Nom), Convert.ToString(j.Prenom), Convert.ToDateTime(j.DateNaissance), deck);

                lesJ.Add(player);
            }




            if (lesJ[0].DateNaissance.Date < lesJ[1].DateNaissance.Date)
            {
                j1 = lesJ[0];
                j2 = lesJ[1];

            }
            else
            {
                j1 = lesJ[1];
                j2 = lesJ[0];
            }
            j1.TirerCarte();
            j2.TirerCarte();

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (action.Equals(1))
            {
                Button btn = sender as Button;
                if (btn.Content.Equals(""))
                {
                    btn.Content = selected.Numero;
                    try
                    {
                        btn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btn.Content) + ".png")));
                    }
                    catch
                    {
                        btn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btn.Content + ".png")));
                    }
                    if (retourner == true)
                    {
                        j1.Main.Remove(selected);
                    }
                    else
                    {
                        j2.Main.Remove(selected);
                    }
                    selected = null;
                    action = 0;
                }
            }

        }

        Boolean retourner = true;
        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        { //poser qu'une carte par tour : variable globale utiliser aussi dans btn_click
            action = 1;
            btnCarte1.Opacity = 1;
            btnCarte2.Opacity = 1;
            btnCarte3.Opacity = 1;
            btnCarte4.Opacity = 1;
            // action pour retourner le plateau et indiquer le nom du personnage sur les radiobutton et transition entre j1 et j2
            int val_ret;
            if (btn_jouer.Content.ToString() == "Jouer")
            {
                if (retourner == true)
                {
                    j1.Piocher();
                    val_ret = 0;
                    retourner = false;
                    lblNomJoueur.Content = j1.Nom.ToString();
                    btnCarte1.Content = j1.Main[0].Numero;
                    btnCarte2.Content = j1.Main[1].Numero;
                    btnCarte3.Content = j1.Main[2].Numero;
                    btnCarte4.Content = j1.Main[3].Numero;
                    try
                    {
                        btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte1.Content) + ".png")));
                        btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte2.Content) + ".png")));
                        btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte3.Content) + ".png")));
                        btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte4.Content) + ".png")));
                    }
                    catch
                    {
                        btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte1.Content + ".png")));
                        btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte2.Content + ".png")));
                        btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte3.Content + ".png")));
                        btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte4.Content) + ".png")));
                    }
                }
                else
                {
                    j2.Piocher();
                    val_ret = 180;
                    retourner = true;
                    lblNomJoueur.Content = j2.Nom.ToString();
                    btnCarte1.Content = j2.Main[0].Numero;
                    btnCarte2.Content = j2.Main[1].Numero;
                    btnCarte3.Content = j2.Main[2].Numero;
                    btnCarte4.Content = j2.Main[3].Numero;
                    try
                    {
                        btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte1.Content) + ".png")));
                        btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte2.Content) + ".png")));
                        btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte3.Content) + ".png")));
                        btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte4.Content) + ".png")));
                    }
                    catch
                    {
                        btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte1.Content + ".png")));
                        btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte2.Content + ".png")));
                        btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte3.Content + ".png")));
                        btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte4.Content) + ".png")));
                    }
                }
                RotateTransform rotateTransform = new RotateTransform(val_ret);
                rotateTransform.CenterX = -5.5;
                rotateTransform.CenterY = -17.5;
                Plateau.RenderTransform = rotateTransform;
                //changer le label jouer en passer
                btn_jouer.Content = "Passer";
                //rendre visible la main du joueur
                Main.Visibility = Visibility.Visible;


                //action pour retourner les boutons vides ( comme ça le joueur aura toujours ses cartes a lui dans son sens)
                for (int i = 1; i <= 3; i++)
                {
                    RotateTransform rotateTransformBtn = new RotateTransform(val_ret);
                    rotateTransformBtn.CenterX = 40;
                    rotateTransformBtn.CenterY = 40;


                    Button btn1 = (Button)FindName("btn1_" + i);
                    if (btn1.Background.Equals(null))
                    {
                        btn1.RenderTransform = rotateTransformBtn;
                    }
                    Button btn2 = (Button)FindName("btn2_" + i);
                    if (btn2.Background.Equals(null))
                    {
                        btn2.RenderTransform = rotateTransformBtn;
                    }
                    Button btn3 = (Button)FindName("btn3_" + i);
                    if (btn3.Background.Equals(null))
                    {
                        btn3.RenderTransform = rotateTransformBtn;
                    }
                }
            }
            // cacher ses cartes
            else
            {
                btn_jouer.Content = "Jouer";
                lblNomJoueur.Content = "";
                Main.Visibility = Visibility.Hidden;

            }

        }

        private void btnCarte1_Click(object sender, RoutedEventArgs e)
        {
            if (retourner == true)
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j1.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte1.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte1.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
            else
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j2.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte1.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte1.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
        }

        private void btnCarte2_Click(object sender, RoutedEventArgs e)
        {
            if (retourner == true)
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 1;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j1.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte2.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte2.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
            else
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 1;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j2.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte2.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j2.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte2.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
        }

        private void btnCarte3_Click(object sender, RoutedEventArgs e)
        {
            if (retourner == true)
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 1;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j1.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte3.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte3.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
            else
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 1;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j2.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte3.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte3.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
        }

        private void btnCarte4_Click(object sender, RoutedEventArgs e)
        {
            if (retourner == true)
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j1.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte4.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte4.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
            else
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;
                foreach (CartePersonnage c in j2.Deck)
                {
                    if (c.Numero == Convert.ToString(btnCarte4.Content))
                    {
                        selected = c;
                    }
                }
                if (selected == null)
                {
                    foreach (CartePersonnage c in j1.Main)
                    {
                        if (c.Numero == Convert.ToString(btnCarte4.Content))
                        {
                            selected = c;
                        }
                    }
                }
            }
        }

        private void btnCarte1_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
        }

        private void btn_retour_plateau_click(object sender, RoutedEventArgs e)
        {
            Accueil_Jeu fenetre = new Accueil_Jeu();
            fenetre.Show();
            this.Close();
        }



    }
}

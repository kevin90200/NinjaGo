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
using Newtonsoft.Json;
using System.IO;

namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Vue_Carte.xaml
    /// </summary>
    public partial class Vue_Carte
    {
        List<Carte> lesCartes = new List<Carte>();          //liste de toutes les cartes
        List<Carte> maCollection = new List<Carte>();           //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        int i = 0;
        int p = 1;
        public Vue_Carte()
        {
            InitializeComponent();
            #region Initialisation Liste générale 
            //recuperation du JSON du fichier local
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("ninjago.json"));
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON
            try
            {
                foreach (var carte in json)
                {

                    if (carte.Type == "P")
                    {
                        CartePersonnage cp = new CartePersonnage(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3), Convert.ToInt32(carte.Attaque), Convert.ToInt32(carte.Defense), Convert.ToInt32(carte.Vitesse), Convert.ToInt32(carte.Force));
                        lesCartes.Add(cp);

                    }
                    else if (carte.Type == "A")
                    {
                        CarteAction ca = new CarteAction(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString());
                        lesCartes.Add(ca);
                    }
                    else if (carte.Type == "V")
                    {
                        CarteVehicule cv = new CarteVehicule(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString());
                        lesCartes.Add(cv);
                    }
                }
            }
            catch
            {

            }

            #endregion

            #region Initailisation maCollection

            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON
            foreach (Carte c in lesCartes)       //Pour chaque carte de la liste complete
            {
                if (c.Exemplaire > 0)           //Si le nbr d'exemplaire et supérieur à 0 on l'ajoute dans la collection du joueur
                {
                    maCollection.Add(c);
                }
            }
            #endregion
            #region compteur page
            lbl_nb_page.Content = "Page " + p + "/19";
            if (p == 1)
            {
                btn_page_prec.Visibility = Visibility.Hidden;
            }
            else if (p == 19)
            {
                btn_page_suiv.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_page_prec.Visibility = Visibility.Visible;
                btn_page_suiv.Visibility = Visibility.Visible;
            }
#endregion
            for (int r = 1; r <= 2; r++)
            {
                for (int c = 1; c <= 5; c++)
                {
                    i = i + 1;
                    Button btnCollection = (Button)FindName("btn" + r + "_" + c);
                    Label lblCarte = (Label)FindName("lbl" + r + "_" + c);
                    try
                    {
                        btnCollection.Content = maCollection[i].Numero;
                        lblCarte.Content = maCollection[i].Exemplaire;
                    }
                    catch
                    {
                        btnCollection.Content = "cartevide";
                        lblCarte.Content = "";
                        lblCarte.Visibility = Visibility.Hidden;
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                    try
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCollection.Content).ToString() + ".png")));
                    }
                    catch
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                }

            }
        }

        private void btn_suivant(object sender, RoutedEventArgs e)
        {
            #region compteur page
            p = p + 1;
            lbl_nb_page.Content = "Page " + p + "/19";
            if (p == 1)
            {
                btn_page_prec.Visibility = Visibility.Hidden;
            }
            else if (p == 19)
            {
                btn_page_suiv.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_page_prec.Visibility = Visibility.Visible;
                btn_page_suiv.Visibility = Visibility.Visible;
            }
            #endregion
            for (int r = 1; r <= 2; r++)
            {
                for (int c = 1; c <= 5; c++)
                {
                    i = i + 1;
                    Button btnCollection = (Button)FindName("btn" + r + "_" + c);
                    Label lblCarte = (Label)FindName("lbl" + r + "_" + c);
                    lblCarte.Content = "";
                    btnCollection.Content = "";
                    btnCollection.Background = new ImageBrush(null);
                    try
                    {
                        btnCollection.Content = maCollection[i].Numero;
                        lblCarte.Content = maCollection[i].Exemplaire;
                    }
                    catch
                    {
                        btnCollection.Content = "cartevide";
                        lblCarte.Content = "";
                        lblCarte.Visibility = Visibility.Hidden;
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                    try
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCollection.Content).ToString() + ".png")));
                    }
                    catch
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                }

            }
        }

        private void btn_precedent(object sender, RoutedEventArgs e)
        {
            #region compteur page
            p = p - 1;
            lbl_nb_page.Content = "Page " + p + "/19";
            if (p == 1)
            {
                btn_page_prec.Visibility = Visibility.Hidden;
            }
            else if (p == 19)
            {
                btn_page_suiv.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_page_prec.Visibility = Visibility.Visible;
                btn_page_suiv.Visibility = Visibility.Visible;
            }
            #endregion
            i = i - 20;
            for (int r = 1; r <= 2; r++)
            {
                for (int c = 1; c <= 5; c++)
                {
                    i = i + 1;
                    Button btnCollection = (Button)FindName("btn" + r + "_" + c);
                    Label lblCarte = (Label)FindName("lbl" + r + "_" + c);
                    lblCarte.Content = "";
                    btnCollection.Content = "";
                    btnCollection.Background = new ImageBrush(null);
                    try
                    {
                        btnCollection.Content = maCollection[i].Numero;
                        lblCarte.Content = maCollection[i].Exemplaire;
                    }
                    catch
                    {
                        btnCollection.Content = "cartevide";
                        lblCarte.Content = "";
                        lblCarte.Visibility = Visibility.Hidden;
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                    try
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCollection.Content).ToString() + ".png")));
                    }
                    catch
                    {
                        btnCollection.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCollection.Content.ToString() + ".png")));
                    }
                }

            }
        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            //Envoie des données au fichier Json local
            File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));

            Gestion fenetre = new Gestion();
            fenetre.Show();
            this.Close();

        }
    }
}

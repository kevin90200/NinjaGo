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
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Deck.xaml
    /// </summary>
    public partial class Deck
    {

        //
        //Attention ! Pour être fonctionnelle la partie deck nécéssite de modifier le JSON (ajout du bolleen deck et des exemplaires) pour pouvoir initialiser les listes
        //
        //Reste à renvoyer les informations dans le JSON afin de stocker les collections 
        //
        //La partie deck nécessite de revoir l'affichage (pas responsive)


        //Instanciation de totues les listes et carte pour la gestion du deck
        List<Carte> maCollection = new List<Carte>();            //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte;                                            //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction = new CarteAction();
        CartePersonnage cartePersonnage = new CartePersonnage();
        CarteVehicule carteVehicule = new CarteVehicule();
        public Deck()
        {
            InitializeComponent();

            #region Initailisation collection

            //recuperation du JSON de l'API
            String url = "http://127.0.0.1/ninjago/public/api";
            var res = new WebClient();
            var json = res.DownloadString(url);                 // il existe uploadString aussi
            JArray o = JArray.Parse(json);
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON
            foreach (JObject carte in o)
            {
                //on ajoute à la collcetion seulement les cartes pour lesquelles le JSON renvoie un nombre d'exemplaire exemplaire suppérieur à 0
                if (Convert.ToUInt32(carte.GetValue("exemplaire")) > 0)
                {
                    Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck")));

                    if (c.Type == "P")      //on ajoute à la collection seulement les cartes personnage, car on ne souhaite pas jouer avec les autres cartes
                    {
                        CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck")), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
                        maCollection.Add(cp);
                    }
                }
            }

            foreach (Carte c in maCollection)       //Pour chaque carte de la collection du joueur
            {
                if (c.Deck == true)           //Si le booleen deck est vrai on l'ajoute dans le deck du joueur
                {
                    monDeck.Add(c);
                }
            }

            #endregion
            //Remplissage des 2 listes box
            lbox_collection.ItemsSource = maCollection;
            lbox_deck.ItemsSource = monDeck;
        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            ////Revnoie des données en JSON à l'API
            //String url = "http://127.0.0.1/ninjago/public/api";
            //var res = new WebClient();
            //String lesParametres="";
            //foreach (Carte c in maCollection)       //Pour chaque carte on serialize l'objet pour le transformer en string
            //{
            //    lesParametres = lesParametres + JsonConvert.SerializeObject(c);
            //}
            //var json = res.UploadString(url, lesParametres);

            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
            
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            btn_ajouter_supprimer_deck.Visibility = Visibility.Visible;
            btn_ajouter_supprimer_deck.Content = "Ajouter à mon deck";
            btn_ajouter_supprimer_deck.Background = Brushes.Green;
        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            btn_ajouter_supprimer_deck.Visibility = Visibility.Visible;
            btn_ajouter_supprimer_deck.Content = "Supprimer de mon deck";
            btn_ajouter_supprimer_deck.Background = Brushes.Red;
        }

        //Bouton ajouer_supprimer permet de créer une sorte de validation (2 cliques pour ajouter/supprimer)
        private void btn_ajouter_supprimer_deck_Click(object sender, RoutedEventArgs e)
        {
            if (btn_ajouter_supprimer_deck.Content.ToString() == "Ajouter à mon deck")
            {
                bool ajout = true;                     //booleen necessaire pour eviter d'ajouter 2 fois la carte au deck, de base on considère qu'on va ajouter la carte
                if (carte != null)                      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                {
                    if (monDeck.Count() < 20)           //on vérifie que le deck possède moins de 20 cartes
                    {
                        if (monDeck.Count() == 0)
                        {
                            monDeck.Add(carte);
                            carte.ajoutDeck();
                        }
                        else                            //si elle n'est pas vide, on vérifie si la carte séléctionnée existe déjà dans le deck
                        {
                            foreach (Carte c in monDeck)
                            {
                                if (c == carte)
                                {
                                    ajout = false;      //si la carte existe dejà dans la collection, on ne l'ajoute pas
                                }
                            }
                            if (ajout == true)
                            {
                                monDeck.Add(carte);
                                carte.ajoutDeck();
                            }
                        }
                    }
                }
                refresh();

            }
            else if (btn_ajouter_supprimer_deck.Content.ToString() == "Supprimer de mon deck")
            {
                if (carte != null)                      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                {       
                    monDeck.Remove(carte);
                    carte.suppressionDeck();
                }
                refresh();
            }
        }

        private void lbox_collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox_collection.SelectedItem == null)       //si la selection est null, on ne fait rien
            {

            }
            else                                        //sinon on recupere la carte 
            {
                carte = new Carte();
                carte = (Carte)lbox_collection.SelectedItem;
                lbox_deck.SelectedIndex = -1;          //on passe l'item selectionner de l'autre liste à la valeur null
                refresh();
            }
        }



        private void lbox_deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox_deck.SelectedItem == null)       //si la selection est null, on ne fait rien
            {

            }
            else            //sinon on recupere la carte 
            {
                carte = new Carte();
                carte = (Carte)lbox_deck.SelectedItem;
                lbox_collection.SelectedIndex = -1;  //on passe l'item selectionner de l'autre liste à la valeur null
                refresh();
            }
        }

        public void refresh()
        {
            lbox_collection.Items.Refresh();
            lbox_deck.Items.Refresh();
            if (carte != null)      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
            {
                lbl_nom.Content = carte.ToString();
                lbl_numero.Content = carte.Numero.ToString();
                if (carte.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage();
                    cp = (CartePersonnage)carte;
                    lbl_vitesse.Content = cp.Vitesse;
                    lbl_attaque.Content = cp.Attaque;
                    lbl_force.Content = cp.Force;
                    lbl_defense.Content = cp.Defense;
                }
                //Recuperation des images (try catch nécessaire pour éviter le plantage si la carte ne correspond à aucune image)
                //certaines images ne correspondent pas à la carte car le fichier JSON est peuplé avec des exemples qui n'existent pas
                try
                {
                    img_carte.Visibility = Visibility.Visible;
                    carte.UrlImage = "pack://application:,,,/Ressource/cartes/" + carte.Numero.ToString() + ".png";
                    var uri = new Uri(carte.UrlImage);
                    var bitmap = new BitmapImage(uri);
                    img_carte.Source = bitmap;
                }
                catch
                {
                    img_carte.Visibility = Visibility.Hidden;
                }
            }
            else    //si la carte est nulle, ont vide tous les affichages
            {
                lbl_nom.Content = "";
                lbl_numero.Content = "";
                lbl_vitesse.Content = "";
                lbl_attaque.Content = "";
                lbl_force.Content = "";
                lbl_defense.Content = "";
                img_carte.Visibility = Visibility.Hidden;
            }
        }

    private void btn_suppression_deck_Click(object sender, RoutedEventArgs e)
        {
                foreach (Carte c in monDeck)
                {
                    c.suppressionDeck();

                }
                monDeck.Clear();  
            refresh();
        }
    }
}

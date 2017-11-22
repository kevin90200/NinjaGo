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
using System.IO;

namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Deck.xaml
    /// </summary>
    public partial class Deck
    {

        //La partie deck nécessite de revoir l'affichage (pas responsive)


        //Instanciation de totues les listes et carte pour la gestion du deck
        List<Carte> lesCartes = new List<Carte>();        //liste de toutes les cartes
        List<Carte> maCollection = new List<Carte>();            //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck1 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck2 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck3 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte;                                            //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction = new CarteAction();
        CartePersonnage cartePersonnage = new CartePersonnage();
        CarteVehicule carteVehicule = new CarteVehicule();
        public Deck()
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
                    Carte c = new Carte(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3));
                    if (c.Type == "P")
                    {
                        CartePersonnage cp = new CartePersonnage(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3), Convert.ToInt32(carte.Attaque), Convert.ToInt32(carte.Defense), Convert.ToInt32(carte.Vitesse), Convert.ToInt32(carte.Force));
                        lesCartes.Add(cp);

                    }
                    else if (c.Type == "A")
                    {
                        CarteAction ca = new CarteAction(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString());
                        lesCartes.Add(ca);
                    }
                    else if (c.Type == "V")
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
            #region Initialisation monDeck
            foreach (Carte c in maCollection)       //Pour chaque carte de la collection du joueur
            {
                if (c.Deck1 == true)           //Si le booleen deck est vrai on l'ajoute dans le deck du joueur
                {
                    monDeck1.Add(c);
                }
                if (c.Deck2 == true)
                {
                    monDeck2.Add(c);
                }
                if (c.Deck3 == true)
                {
                    monDeck3.Add(c);
                }
            }
            #endregion
            //Remplissage de la liste box deck
            cbbx_choix_deck.SelectedIndex = 0;  //permet de remplir la list box deck avec par défaut, le deck 1
            if (cbbx_choix_deck.SelectedIndex == 0)
            {
                lbox_deck.ItemsSource = monDeck1;
            }
            else if (cbbx_choix_deck.SelectedIndex == 1)
            {
                lbox_deck.ItemsSource = monDeck2;
            }
            else if (cbbx_choix_deck.SelectedIndex == 2)
            {
                lbox_deck.ItemsSource = monDeck3;
            }
            //Remplissage de la liste box collection
            lbox_collection.Items.Clear();
            foreach (Carte c in maCollection)
            {
                if (cbbx_choix_deck.SelectedIndex == 0)
                {

                    lbl_deck_count.Content = monDeck1.Count() + " / 20";
                    if (c.Deck1 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
                else if (cbbx_choix_deck.SelectedIndex == 1)
                {
                    lbl_deck_count.Content = monDeck2.Count() + " / 20";
                    if (c.Deck2 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
                else if (cbbx_choix_deck.SelectedIndex == 2)
                {
                    lbl_deck_count.Content = monDeck1.Count() + " / 20";
                    if (c.Deck3 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
            }
            //initialisation compteur de carte
            lbl_collection_count.Content = lbox_collection.Items.Count + " / " + lesCartes.Count();
            lbl_deck_count.Content = monDeck1.Count() + " / 20";

            




        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            //Envoie des données au fichier Json local
            File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));

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
            #region gestion ajout deck1
            if (cbbx_choix_deck.SelectedIndex == 0)
            {
                if (btn_ajouter_supprimer_deck.Content.ToString() == "Ajouter à mon deck")
                {
                    bool ajout = true;                     //booleen necessaire pour eviter d'ajouter 2 fois la carte au deck, de base on considère qu'on va ajouter la carte

                    if (carte != null)                      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                    {
                        if (monDeck1.Count() < 20)           //on vérifie que le deck possède moins de 20 cartes
                        {
                            if (monDeck1.Count() == 0)
                            {
                                monDeck1.Add(carte);
                                carte.ajoutDeck1();
                                lbox_collection.Items.Remove(carte);
                            }
                            else                            //si elle n'est pas vide, on vérifie si la carte séléctionnée existe déjà dans le deck
                            {
                                foreach (Carte c in monDeck1)
                                {
                                    if (c == carte)
                                    {
                                        ajout = false;      //si la carte existe dejà dans la collection, on ne l'ajoute pas
                                    }
                                }
                                if (ajout == true)
                                {
                                    monDeck1.Add(carte);
                                    carte.ajoutDeck1();
                                    lbox_collection.Items.Remove(carte);
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
                        monDeck1.Remove(carte);
                        carte.suppressionDeck1();
                        lbox_collection.Items.Add(carte);
                    }
                    refresh();
                }
            }
            #endregion
            #region gestion ajout deck2
            if (cbbx_choix_deck.SelectedIndex == 1)
            {
                if (btn_ajouter_supprimer_deck.Content.ToString() == "Ajouter à mon deck")
                {
                    bool ajout = true;                     //booleen necessaire pour eviter d'ajouter 2 fois la carte au deck, de base on considère qu'on va ajouter la carte

                    if (carte != null)                      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                    {
                        if (monDeck2.Count() < 20)           //on vérifie que le deck possède moins de 20 cartes
                        {
                            if (monDeck2.Count() == 0)
                            {
                                monDeck2.Add(carte);
                                carte.ajoutDeck2();
                                lbox_collection.Items.Remove(carte);
                            }
                            else                            //si elle n'est pas vide, on vérifie si la carte séléctionnée existe déjà dans le deck
                            {
                                foreach (Carte c in monDeck2)
                                {
                                    if (c == carte)
                                    {
                                        ajout = false;      //si la carte existe dejà dans la collection, on ne l'ajoute pas
                                    }
                                }
                                if (ajout == true)
                                {
                                    monDeck2.Add(carte);
                                    carte.ajoutDeck2();
                                    lbox_collection.Items.Remove(carte);
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
                        monDeck2.Remove(carte);
                        carte.suppressionDeck2();
                        lbox_collection.Items.Add(carte);
                    }
                    refresh();
                }
            }
            #endregion
            #region gestion ajout deck3
            if (cbbx_choix_deck.SelectedIndex == 2)
            {
                if (btn_ajouter_supprimer_deck.Content.ToString() == "Ajouter à mon deck")
                {
                    bool ajout = true;                     //booleen necessaire pour eviter d'ajouter 2 fois la carte au deck, de base on considère qu'on va ajouter la carte

                    if (carte != null)                      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                    {
                        if (monDeck3.Count() < 20)           //on vérifie que le deck possède moins de 20 cartes
                        {
                            if (monDeck3.Count() == 0)
                            {
                                monDeck3.Add(carte);
                                carte.ajoutDeck3();
                                lbox_collection.Items.Remove(carte);
                            }
                            else                            //si elle n'est pas vide, on vérifie si la carte séléctionnée existe déjà dans le deck
                            {
                                foreach (Carte c in monDeck3)
                                {
                                    if (c == carte)
                                    {
                                        ajout = false;      //si la carte existe dejà dans la collection, on ne l'ajoute pas
                                    }
                                }
                                if (ajout == true)
                                {
                                    monDeck3.Add(carte);
                                    carte.ajoutDeck3();
                                    lbox_collection.Items.Remove(carte);
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
                        monDeck3.Remove(carte);
                        carte.suppressionDeck3();
                        lbox_collection.Items.Add(carte);
                    }
                    refresh();
                }
            }
            #endregion
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
            //refresh de la collection
            lbox_collection.Items.Clear();
            foreach (Carte c in maCollection)
            {
                if (cbbx_choix_deck.SelectedIndex == 0)
                {
                    
                    lbl_deck_count.Content = monDeck1.Count() + " / 20";
                    if (c.Deck1 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
                else if (cbbx_choix_deck.SelectedIndex == 1)
                {
                    lbl_deck_count.Content = monDeck2.Count() + " / 20";
                    if (c.Deck2 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
                else if (cbbx_choix_deck.SelectedIndex == 2)
                {
                    lbl_deck_count.Content = monDeck1.Count() + " / 20";
                    if (c.Deck3 == true)
                    {

                    }
                    else
                    {
                        lbox_collection.Items.Add(c);
                    }
                }
            }
            lbox_deck.Items.Refresh();
            //compteur de carte
            lbl_collection_count.Content = lbox_collection.Items.Count + " / " + lesCartes.Count();
            if (cbbx_choix_deck.SelectedIndex == 0)
            {
                lbl_deck_count.Content = monDeck1.Count() + " / 20";
            }
            else if (cbbx_choix_deck.SelectedIndex == 1)
            {
                lbl_deck_count.Content = monDeck2.Count() + " / 20";
            }
            else if (cbbx_choix_deck.SelectedIndex == 2)
            {
                lbl_deck_count.Content = monDeck3.Count() + " / 20";
            }

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
                try
                {
                    img_carte.Visibility = Visibility.Visible;
                    try                 //Conversion en int puis à nouveau en string pour supprier les 0 de l'affichage //Try catch nécessaire pour les cartes LEx car on ne peut aps convertir en int
                    {
                        carte.UrlImage = "pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(carte.Numero).ToString() + ".png";  //l'attribut UrlImage prend les attributs de la carte pour les metttre en forme comme le nom de l'image qui lui correspond
                    }
                    catch
                    {
                        carte.UrlImage = "pack://application:,,,/Ressource/cartes/" + carte.Numero.ToString() + ".png";  //l'attribut UrlImage prend les attributs de la carte pour les metttre en forme comme le nom de l'image qui lui correspond
                    }
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
            if (cbbx_choix_deck.SelectedIndex == 0)
            {
                foreach (Carte c in monDeck1)
                {
                    c.suppressionDeck1();
                    lbox_collection.Items.Add(c);
                }
                monDeck1.Clear();
            }
            else if (cbbx_choix_deck.SelectedIndex == 1)
            {
                foreach (Carte c in monDeck2)
                {
                    c.suppressionDeck2();
                    lbox_collection.Items.Add(c);
                }
                monDeck2.Clear();
            }
            else if (cbbx_choix_deck.SelectedIndex == 2)
            {
                foreach (Carte c in monDeck3)
                {
                    c.suppressionDeck3();
                    lbox_collection.Items.Add(c);
                }
                monDeck3.Clear();
            }
              
            refresh();
        }

        private void cbbx_choix_deck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbx_choix_deck.SelectedIndex == 0)
            {
                lbox_deck.ItemsSource = monDeck1;
            }
            else if (cbbx_choix_deck.SelectedIndex == 1)
            {
                lbox_deck.ItemsSource = monDeck2;
            }
            else if (cbbx_choix_deck.SelectedIndex == 2)
            {
                lbox_deck.ItemsSource = monDeck3;
            }
            lbox_deck.Items.Refresh();
            refresh();
        }
    }
}

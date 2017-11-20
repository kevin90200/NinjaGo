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
    /// Logique d'interaction pour Gestion.xaml
    /// </summary>
    public partial class Gestion
    {

        //La partie collection nécessite de revoir l'affichage (pas responsive)



        //Instanciation de totues les listes et carte pour la gestion
        List<Carte> lesCartes = new List<Carte>();          //liste de toutes les cartes
        List<Carte> maCollection = new List<Carte>();           //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck1 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck2 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck3 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte;                 //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction= new CarteAction();
        CartePersonnage cartePersonnage= new CartePersonnage();
        CarteVehicule carteVehicule= new CarteVehicule();
        public Gestion()
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
                else
                {

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
            //Remplissage des 2 listes box
            lbox_collection.ItemsSource = maCollection;
            foreach(Carte c in lesCartes)
            {
                if (c.Exemplaire >= 1)
                {
                    
                }
                else
                {
                    lbox_cartes.Items.Add(c);
                }
            }
            //initialisation compteur de carte
            lbl_collection_count.Content = maCollection.Count() + " / " + lesCartes.Count();
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
            btn_ajouter_supprimer_collection.Visibility = Visibility.Visible;
            btn_ajouter_supprimer_collection.Content = "Ajouter un exemplaire";
            btn_ajouter_supprimer_collection.Background = Brushes.Green ;
        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            btn_ajouter_supprimer_collection.Visibility = Visibility.Visible;
            btn_ajouter_supprimer_collection.Content = "Supprimer un exemplaire";
            btn_ajouter_supprimer_collection.Background = Brushes.Red;
        }
        //Bouton ajouer_supprimer permet de créer une sorte de validation (2 cliques pour ajouter/supprimer)
        private void btn_ajouter_supprimer_collection_Click(object sender, RoutedEventArgs e)
        {
            if (btn_ajouter_supprimer_collection.Content.ToString() == "Ajouter un exemplaire")
            {
                bool ajout = true;     //booleen necessaire pour eviter d'ajouter 2 fois la carte à la collection, de base on considère qu'on va ajouter la carte
                
                if (carte != null)      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                {

                    if (maCollection.Count() == 0)      
                    {
                        maCollection.Add(carte);
                        carte.ajoutExemplaire();
                        lbox_cartes.Items.Remove(carte);
                    }
                    else            //si elle n'est pas vide, on vérifie si la carte séléctionnée existe déjà dans la collection
                    {
                        foreach (Carte c in maCollection)
                        {
                            if (c == carte)             
                            {
                                ajout = false;                  //si la carte existe dejà dans la collection, on ne l'ajoute pas
                                carte.ajoutExemplaire();        //on ajoute quand même un exemplaire
                            }
                           
                        }
                        if (ajout == true)
                        {
                            maCollection.Add(carte);
                            carte.ajoutExemplaire();
                            lbox_cartes.Items.Remove(carte);
                        }
                    }
                }
                refresh();
                
            }
            else if (btn_ajouter_supprimer_collection.Content.ToString() == "Supprimer un exemplaire")
            {
                if (carte != null)      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
                {
                    if (carte.Exemplaire == 0)      //si il n'y a pas d'exmplaire de la carte, on ne fait rien
                    {

                    }
                    else         //sinon on supprime un exemplaire
                    {
                        carte.supressionExemplaire();
                        if (carte.Exemplaire == 0)      //si le nombre d'exemplaire arrive à 0, on supprime la carte de la collection
                        {
                            maCollection.Remove(carte);
                            lbox_cartes.Items.Add(carte);
                        }
                    }
                }
                refresh();
            }
        }

        private void lbox_cartes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox_cartes.SelectedItem == null)       //si la selection est null, on ne fait rien
            {

            }
            else            //sinon on recupere la carte 
            {
                carte = new Carte();
                carte = (Carte)lbox_cartes.SelectedItem;
                lbox_collection.SelectedIndex = -1;     //on passe l'item selectionner de l'autre liste à la valeur null
                refresh();
            }
        }

        

        private void lbox_collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbox_collection.SelectedItem == null)   //si la selection est null, on ne fait rien
            {

            }
            else            //sinon on recupere la carte 
            {
                carte = new Carte();
                carte = (Carte)lbox_collection.SelectedItem;
                lbox_cartes.SelectedIndex = -1;     //on passe l'item selectionner de l'autre liste à la valeur null
                refresh();
            }
        }

        public void refresh()
        {
            lbox_cartes.Items.Refresh();
            lbox_collection.Items.Refresh();
            //compteur de carte
            lbl_collection_count.Content = maCollection.Count() + " / " + lesCartes.Count();
            if (carte != null)      //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
            {
                lbl_nb_exemplaire.Content = carte.Exemplaire.ToString();
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
                    txt_description.Text = "";
                }
                else if (carte.Type == "A")
                {
                    CarteAction ca = new CarteAction();
                    ca = (CarteAction)carte;
                    lbl_vitesse.Content = "";
                    lbl_attaque.Content = "";
                    lbl_force.Content = "";
                    lbl_defense.Content = "";
                    txt_description.Text = ca.Description;
                }
                else if (carte.Type == "V")
                {
                    CarteVehicule cv = new CarteVehicule();
                    cv = (CarteVehicule)carte;
                    lbl_vitesse.Content = "";
                    lbl_attaque.Content = "";
                    lbl_force.Content = "";
                    lbl_defense.Content = "";
                    txt_description.Text = cv.Description;
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
                    lbl_nom.Content = "";
                    lbl_numero.Content = "";
                    lbl_vitesse.Content = "";
                    lbl_attaque.Content = "";
                    lbl_force.Content = "";
                    lbl_defense.Content = "";
                    txt_description.Text = "";
                }
                catch
                {
                    img_carte.Visibility = Visibility.Hidden;
                }
            }
            else         //si la carte est nulle, ont vide tous les affichages
            {
                lbl_nb_exemplaire.Content = "";
                lbl_nom.Content = "";
                lbl_numero.Content = "";
                lbl_vitesse.Content = "";
                lbl_attaque.Content = "";
                lbl_force.Content = "";
                lbl_defense.Content = "";
                txt_description.Text = "";
                img_carte.Visibility = Visibility.Hidden;
            } 

        }

        private void btn_suppression_exemplaires_Click(object sender, RoutedEventArgs e)
        {
            if (carte != null) //on vérifie que la carte séléctionnée n'es pas null pour éviter le plantage
            {
                maCollection.Remove(carte);
                carte.Exemplaire = 0;
                lbox_cartes.Items.Add(carte);
            }
            refresh();
        }

    }
}

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
    /// Logique d'interaction pour Gestion.xaml
    /// </summary>
    public partial class Gestion
    {


        //
        //Attention ! Pour être fonctionnelle la partie collection nécéssite de modifier le JSON (ajout du bolleen deck et des exemplaires) pour pouvoir initialiser les listes
        //
        //Reste à renvoyer les informations dans le JSON afin de stocker les collections 
        //
        //La partie collection nécessite de revoir l'affichage (pas responsive)



        //Instanciation de totues les listes et carte pour la gestion
        List<Carte> lesCartes = new List<Carte>();          //liste de toutes les cartes
        List<Carte> maCollection = new List<Carte>();           //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        Carte carte;                 //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction= new CarteAction();
        CartePersonnage cartePersonnage= new CartePersonnage();
        CarteVehicule carteVehicule= new CarteVehicule();
        public Gestion()
        {
            InitializeComponent();

            #region Initailisation collection
            
            //recuperation du JSON de l'API
            String url = "http://127.0.0.1/ninjago/public/api";
            var res = new WebClient();
            var json = res.DownloadString(url);         // il existe uploadString aussi
            JArray o = JArray.Parse(json);
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON
            foreach (JObject carte in o) {
                Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString());  //GetValue("xxx") permet de récupérer les données du JSON
                
                if (c.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
                    lesCartes.Add(cp);

                }
                else if (c.Type == "A")
                {
                    CarteAction ca = new CarteAction(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lesCartes.Add(ca);
                }
                else if (c.Type == "V")
                {
                    CarteVehicule cv = new CarteVehicule(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lesCartes.Add(cv);
                }
            }

            foreach(Carte c in lesCartes)       //Pour chaque carte de la liste complete
            {
                if (c.Exemplaire > 0)           //Si le nbr d'exemplaire et supérieur à 0 on l'ajoute dans la collection du joueur
                {
                    maCollection.Add(c);
                }
            }

            #endregion
            //Remplissage des 2 listes box
            lbox_cartes.ItemsSource = lesCartes;
            lbox_collection.ItemsSource = maCollection;
        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            ////Revnoie des données en JSON à l'API
            //String url = "http://127.0.0.1/ninjago/public/api";
            //var res = new WebClient();
            //String lesParametres = "";
            //foreach (Carte c in lesCartes)       //Pour chaque carte on serialize l'objet pour le transformer en string
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
                //certaines images ne correspondent pas à la carte car le fichier JSON est peuplé avec des exemples qui n'existent pas
                try
                {
                    img_carte.Visibility = Visibility.Visible;
                    carte.UrlImage = "pack://application:,,,/Ressource/cartes/" + carte.Numero.ToString() + ".png";  //l'attribut UrlImage prend les attributs de la carte pour les metttre en forme comme le nom de l'image qui lui correspond
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
            }
            refresh();
        }
    }
}

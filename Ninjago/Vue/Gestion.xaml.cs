﻿using System;
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
        List<Carte> lesCartes = new List<Carte>();
        List<Carte> maCollection = new List<Carte>();
        Carte carte= new Carte();
        CarteAction carteAction= new CarteAction();
        CartePersonnage cartePersonnage= new CartePersonnage();
        CarteVehicule carteVehicule= new CarteVehicule();
        public Gestion()
        {
            InitializeComponent();

            #region Initailisation collection
            

            String url = "http://127.0.0.1/ninjago/public/api";
            var res = new WebClient();
            var json = res.DownloadString(url);// il existe uploadString aussi
            JArray o = JArray.Parse(json);
            List<Carte> lesCartes = new List<Carte>();
            foreach (JObject carte in o) {
                Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                
                if (c.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
                    lesCartes.Add(cp);

                }
                else if (c.Type == "A")
                {
                    CarteAction ca = new CarteAction(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lesCartes.Add(ca);
                }
                else if (c.Type == "V")
                {
                    CarteVehicule cv = new CarteVehicule(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lesCartes.Add(cv);
                }
            }

            #endregion
            lbox_cartes.ItemsSource = lesCartes;
            lbox_collection.ItemsSource = maCollection;
        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
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

        private void btn_ajouter_supprimer_collection_Click(object sender, RoutedEventArgs e)
        {
            if (btn_ajouter_supprimer_collection.Content.ToString() == "Ajouter un exemplaire")
            {
                bool ajout = false;
                
                if (maCollection.Count() == 0)
                {
                    maCollection.Add(carte);
                    carte.ajoutExemplaire();  
                }
                else
                {
                    foreach (Carte c in maCollection)
                    {
                        if (c == carte){
                            ajout = false;
                            carte.ajoutExemplaire();  
                        }
                        else
                        {
                            ajout = true;   
                        }
                    }
                    if (ajout == true)
                    {
                        maCollection.Add(carte);
                        carte.ajoutExemplaire();
                    }
                }
                refresh();
                
            }
            else if (btn_ajouter_supprimer_collection.Content.ToString() == "Supprimer un exemplaire")
            {
                if (carte.Exemplaire == 0)
                {

                }
                else
                {
                    carte.supressionExemplaire();
                    if (carte.Exemplaire == 0)
                    {
                        maCollection.Remove(carte);
                    }
                }
                refresh();
            }
        }

        private void lbox_cartes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            carte = new Carte();
            carte = (Carte)lbox_cartes.SelectedItem;    
            refresh();
        }

        

        private void lbox_collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            carte = new Carte();
            carte = (Carte)lbox_collection.SelectedItem; 
            refresh();
        }

        public void refresh()
        {
            lbox_cartes.Items.Refresh();
            lbox_collection.Items.Refresh();

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
            //Recuperation des images
            try
            {
                img_carte.Visibility = Visibility.Visible;
                carte.UrlImage = "pack://application:,,,/Ressource/cartes/" + carte.Numero.ToString() + "-" + carte.Nom.ToString() + ".jpg";
                var uri = new Uri(carte.UrlImage);
                var bitmap = new BitmapImage(uri);
                img_carte.Source = bitmap;
            }
            catch
            {
                img_carte.Visibility = Visibility.Hidden;
            }
        }

        private void btn_suppression_exemplaires_Click(object sender, RoutedEventArgs e)
        {
            maCollection.Remove(carte);
            carte.Exemplaire = 0;
            refresh();
        }
    }
}
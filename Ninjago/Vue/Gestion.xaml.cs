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
        List<Carte> lc = new List<Carte>();
        Carte carte = new Carte();
        public Gestion()
        {
            InitializeComponent();

            #region Initailisation collection

            String url = "http://127.0.0.1/ninjago/public/api";
            var res = new WebClient();
            var json = res.DownloadString(url);// il existe uploadString aussi
            JArray o = JArray.Parse(json);
            List<Carte> lc = new List<Carte>();
            foreach (JObject carte in o) {
                Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                if (c.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
                    lc.Add(cp);
                }
                else if (c.Type == "A")
                {
                    CarteAction ca = new CarteAction(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lc.Add(ca);
                }
                else if (c.Type == "V")
                {
                    CarteVehicule cv = new CarteVehicule(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), 0, carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                    lc.Add(cv);
                }
            }
            foreach (Carte c in lc) //pour chaque item dans le JSON
            {
                lbox_cartes.Items.Add(c.Nom);
                lbox_cartes_num.Items.Add(c.Exemplaire);
            }

            #endregion
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
            btn_ajouter_supprimer_collection.Content = "Ajouter à ma collection";
            foreach (Carte c in lc)
            {
                if (c.Nom == lbox_cartes.SelectedValue.ToString())
                {
                    carte = c;
                }
            }
            carte.Exemplaire = carte.Exemplaire + 1;

        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            btn_ajouter_supprimer_collection.Visibility = Visibility.Visible;
            btn_ajouter_supprimer_collection.Content = "Supprimer de ma collection";
        }

        private void lbox_cartes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            foreach(Carte c in lc)
            {
                if (c.Nom == lbox_cartes.SelectedValue.ToString())
                {
                    carte = c;
                }
            }
            lbl_nom.Content = carte.Nom;
        }
    }
}

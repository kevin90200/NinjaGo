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
    /// Logique d'interaction pour Launcher.xaml
    /// </summary>
    public partial class Launcher
    {
        List<Carte> lesCartes = new List<Carte>();          //liste de toutes les cartes
        List<Carte> maCollection = new List<Carte>();            //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck1 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck2 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        List<Carte> monDeck3 = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte=null;                 //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction = new CarteAction();
        CartePersonnage cartePersonnage = new CartePersonnage();
        CarteVehicule carteVehicule = new CarteVehicule();
        public Launcher()
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
            //Envoie des données au fichier Json local
            File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));
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
        }


        private void btn_jeu_Click(object sender, RoutedEventArgs e)
        {

            Accueil_Jeu fenetre = new Accueil_Jeu();
            fenetre.Show();
            this.Close();
        }

        private void btn_gestion_Click(object sender, RoutedEventArgs e)
        {
            //Envoie des données au fichier Json local
            File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));

            Gestion fenetre = new Gestion();
            fenetre.Show();
            this.Close();
        }

        private void btn_deck_Click(object sender, RoutedEventArgs e)
        {
            //Envoie des données au fichier Json local
            File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));

            Deck fenetre = new Deck();
            fenetre.Show();
            this.Close();
        }

        private void btn_recup_api_Click(object sender, RoutedEventArgs e)
        {
            bool ajout = true;
            //recuperation du JSON de l'API
            String url = "http://127.0.0.1/ninjago/public/api";
            var res = new WebClient();
            var json = res.DownloadString(url);         // il existe uploadString aussi
            JArray o = JArray.Parse(json);
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON
            if (lesCartes.Count() == 0)
            {
                foreach (JObject carte in o)
                {
                    Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck1")), Convert.ToBoolean(carte.GetValue("deck2")), Convert.ToBoolean(carte.GetValue("deck3")));  //GetValue("xxx") permet de récupérer les données du JSON

                    if (c.Type == "P")
                    {
                        CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck1")), Convert.ToBoolean(carte.GetValue("deck2")), Convert.ToBoolean(carte.GetValue("deck3")), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
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
            }
            else
            {
                foreach (JObject carte in o)
                {
                    foreach (Carte laCarte in lesCartes)
                    {
                        Carte c = new Carte(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck1")), Convert.ToBoolean(carte.GetValue("deck2")), Convert.ToBoolean(carte.GetValue("deck3")));  //GetValue("xxx") permet de récupérer les données du JSON
                        if (c.Nom == laCarte.Nom)
                        {
                            ajout = false;
                        }
                        else
                        {
                            
                        }
                    }
                    if (ajout == true)
                    {
                        if (carte.GetValue("type").ToString() == "P")
                        {
                            CartePersonnage cp = new CartePersonnage(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString(), Convert.ToBoolean(carte.GetValue("deck1")), Convert.ToBoolean(carte.GetValue("deck2")), Convert.ToBoolean(carte.GetValue("deck3")), Convert.ToInt32(carte.GetValue("attaque")), Convert.ToInt32(carte.GetValue("defense")), Convert.ToInt32(carte.GetValue("vitesse")), Convert.ToInt32(carte.GetValue("force")));
                            lesCartes.Add(cp);
                        }
                        else if (carte.GetValue("type").ToString() == "A")
                        {
                            CarteAction ca = new CarteAction(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                            lesCartes.Add(ca);
                        }
                        else if (carte.GetValue("type").ToString() == "V")
                        {
                            CarteVehicule cv = new CarteVehicule(carte.GetValue("nom").ToString(), carte.GetValue("numero").ToString(), Convert.ToInt32(carte.GetValue("exemplaire")), carte.GetValue("description").ToString(), carte.GetValue("type").ToString());
                            lesCartes.Add(cv);
                        }
                    }
                }
            }
        //Ensuite on réécrit les données récupérer dans le fichier local (!!!Attention reset du nombre d'exemplaire et du booléen ==> a corriger)
        File.WriteAllText("ninjago.json", JsonConvert.SerializeObject(lesCartes, Formatting.Indented));

        }
    }
}

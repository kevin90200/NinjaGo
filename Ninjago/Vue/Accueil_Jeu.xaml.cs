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
    /// Logique d'interaction pour Accueil_Jeu.xaml
    /// </summary>
    public partial class Accueil_Jeu
    {
        List<Joueur> lesJ = new List<Joueur>();
        List<CartePersonnage> lesCartes = new List<CartePersonnage>();
        List<CartePersonnage> maCollection = new List<CartePersonnage>();            
        List<CartePersonnage> deck1 = new List<CartePersonnage>();                
        List<CartePersonnage> deck2 = new List<CartePersonnage>();
        List<CartePersonnage> deck3 = new List<CartePersonnage>();
        List<CartePersonnage> DeckJ1 = new List<CartePersonnage>();
        List<CartePersonnage> DeckJ2 = new List<CartePersonnage>();
      

        private void MonDeckJ1_Checked(object sender, RoutedEventArgs e)
        { //pour affficher la comboBox quand on clique sur "deck personnalisé"
            if (MonDeckJ1.IsChecked == true)
            {
                cbxDeckJ1.Visibility = Visibility.Visible;
            }
            if (MonDeckJ2.IsChecked == true)
            {
                cbxDeckJ2.Visibility = Visibility.Visible;
            }
        }

        public Accueil_Jeu()
        {
            InitializeComponent();
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("ninjago.json"));
            //pour recuperer les cartes depuis le Json et instancié les decks et ma collections
            foreach (var carte in json)
            {
                Carte c = new Carte(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3));
                if (c.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3), Convert.ToInt32(carte.Attaque), Convert.ToInt32(carte.Defense), Convert.ToInt32(carte.Vitesse), Convert.ToInt32(carte.Force));
                    lesCartes.Add(cp);
                    if (cp.Deck1 == true)
                    {
                        deck1.Add(cp);
                    }
                    if (cp.Deck2 == true)
                    {
                        deck2.Add(cp);
                    }
                    if (cp.Deck3 == true)
                    {
                        deck3.Add(cp);
                    }
                    if (cp.Exemplaire > 0)
                    {
                        maCollection.Add(cp);
                    }
                }

            }
            // pour remplir la comboBox des decks si un deck contient 20 cartes il sera ajouter dans la combo box
            if (deck1 != null)
            {
                if (deck1.Count() == 20)
                {
                    cbxDeckJ1.Items.Add("deck 1");
                    cbxDeckJ2.Items.Add("deck 1");
                }
            }
            if (deck2 != null)
            {
                if (deck2.Count() == 20)
                {
                    cbxDeckJ1.Items.Add("deck 2");
                    cbxDeckJ2.Items.Add("deck 2");
                }
            }
            if (deck3 != null)
            {
                if (deck3.Count() == 20)
                {
                    cbxDeckJ1.Items.Add("deck 3");
                    cbxDeckJ2.Items.Add("deck 3");
                }
            }

        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            // permet de remplir le deck de j1 ou j2  avec un deck perso     // reste a faire un message d'erreur si aucun deck n'est créer
            #region deck perso
            //SI ma collection existe
            if (maCollection != null)
            {
                //SI ma collection est complete
                if (maCollection..count = 20) {
                    if (MonDeckJ1.IsChecked == true || MonDeckJ2.IsChecked == true)
                    {
                        if (MonDeckJ1.IsChecked == true)
                        {
                            for (int i = 1; i <= 3; i++)
                            {
                                string deckNom = "deck " + i;
                                if (cbxDeckJ1.SelectedItem.ToString() == deckNom)
                                {
                                    if (i == 1)
                                    {
                                        foreach (CartePersonnage cp in deck1)
                                        {
                                            DeckJ1.Add(cp);
                                        }
                                    }
                                    if (i == 2)
                                    {
                                        foreach (CartePersonnage cp in deck2)
                                        {
                                            DeckJ1.Add(cp);
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        foreach (CartePersonnage cp in deck3)
                                        {
                                            DeckJ1.Add(cp);
                                        }
                                    }
                                }
                            }
                        }

                        if (MonDeckJ2.IsChecked == true)
                        {
                            for (int i = 1; i <= 3; i++)
                            {
                                string deckNom = "deck " + i;
                                if (cbxDeckJ2.SelectedItem.ToString() == deckNom)
                                {
                                    if (i == 1)
                                    {
                                        foreach (CartePersonnage cp in deck1)
                                        {
                                            DeckJ2.Add(cp);
                                        }
                                    }
                                    if (i == 2)
                                    {
                                        foreach (CartePersonnage cp in deck2)
                                        {
                                            DeckJ2.Add(cp);
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        foreach (CartePersonnage cp in deck3)
                                        {
                                            DeckJ2.Add(cp);
                                        }
                                    }
                                }
                            }
                        }
                    }
            }
        }
            #endregion
            // permet de remplir le deck de j1 ou j2  avec un deck aleatoire de ma collection // reste a faire un message d'erreur si il y a pas asser de carte dans la collection ou faire une auto selection sur deck aleatoire de toute les cartes du jeu
            #region deck aleatoire à partir de ma collection
            if (AleatoireColJ1.IsChecked == true || AleatoireColJ2.IsChecked == true)
            {

                int nombreCarteCollection = maCollection.Count();// pour savoir si j'ai asser de carte dans ma collection pour faire un deck
                if (nombreCarteCollection >= 20) 
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        Random aleatoire = new Random();
                        int numeroAlea = aleatoire.Next(nombreCarteCollection - 1);
                        Boolean ajout = true;
                        foreach (CartePersonnage cp in DeckJ1)
                        {
                            if (maCollection[numeroAlea] == cp)
                            {
                                ajout = false;
                                i = i - 1;
                            }
                        }
                        if (AleatoireColJ1.IsChecked == true && ajout == true)
                        {
                            DeckJ1.Add(maCollection[numeroAlea]);
                        }
                        if (AleatoireColJ2.IsChecked == true && ajout == true)
                        {
                            DeckJ2.Add(maCollection[numeroAlea]);
                        }
                    }
                }
            }
            #endregion
            // permet de remplir le deck de j1 ou j2  avec un deck aleatoire de toute les cartes
            #region deck aleatoire de toute les cartes
            if (AleatoireAllJ1.IsChecked == true || AleatoireAllJ2.IsChecked == true)
            {
                int nombreCarteAll = lesCartes.Count();
                if (nombreCarteAll >= 20)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        Random aleatoire = new Random();
                        int numeroAlea = aleatoire.Next(nombreCarteAll - 1);
                        Boolean ajout = true;
                        foreach (CartePersonnage cp in DeckJ1)
                        {
                            if (maCollection[numeroAlea] == cp)
                            {
                                ajout = false;
                                i = i - 1;
                            }
                        }
                        if (AleatoireColJ1.IsChecked == true && ajout == true)
                        {
                            DeckJ1.Add(lesCartes[numeroAlea]);

                        }
                        if (AleatoireColJ2.IsChecked == true && ajout == true)
                        {
                            DeckJ2.Add(lesCartes[numeroAlea]);
                        }
                    }
                }
            }
#endregion
            Joueur J1 = new Joueur(PseudoJ1.Text.ToString(), "", Convert.ToDateTime(DateJ1.Text), DeckJ1);
            Joueur J2 = new Joueur(PseudoJ2.Text.ToString(), "", Convert.ToDateTime(DateJ2.Text), DeckJ2);
            lesJ.Add(J1);
            lesJ.Add(J2);
            // comment renvoyer J1 et J2 sur plateau
            File.WriteAllText("joueur.json", JsonConvert.SerializeObject(lesJ, Formatting.Indented));
            Plateau_Jeu fenetre = new Plateau_Jeu();
            fenetre.Show();
            this.Close();

        }

        private void btn_retour_accueil_jeu_Click(object sender, RoutedEventArgs e)
        {
            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
        }
    }

    
}

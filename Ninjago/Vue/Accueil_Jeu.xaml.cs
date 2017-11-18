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
    { List<Joueur> lesJ = new List<Joueur>();
        List<CartePersonnage> lesCartes = new List<CartePersonnage>();
        List<CartePersonnage> maCollection = new List<CartePersonnage>();            //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte;                                            //Correspond à la carte séléctionnée dans l'interface (peut être null)
        List<CartePersonnage> DeckJ1 = new List<CartePersonnage>();
        List<CartePersonnage> DeckJ2 = new List<CartePersonnage>();
        CartePersonnage cartePersonnage = new CartePersonnage();
       
        public Accueil_Jeu()
        {
            InitializeComponent();
            
        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
           
           
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("ninjago.json"));
            foreach (var carte in json)
            {
                Carte c = new Carte(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck)); 
                if (c.Type == "P")
                {
                    CartePersonnage cp = new CartePersonnage(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck), Convert.ToInt32(carte.Attaque), Convert.ToInt32(carte.Defense), Convert.ToInt32(carte.Vitesse), Convert.ToInt32(carte.Force));
                    lesCartes.Add(cp);
                    if (cp.Deck == true)
                    {
                        monDeck.Add(cp);
                    }
                    if (cp.Exemplaire > 0)
                    {
                        maCollection.Add(cp);
                    }
                }

            }
            if (MonDeckJ1.IsChecked == true || MonDeckJ2.IsChecked == true)
            { 
                foreach (CartePersonnage cp in monDeck)
                {
                     if (cp.Deck == true)
                     {
                          if (MonDeckJ1.IsChecked == true)
                          {
                                
                            DeckJ1.Add(cp);
                          }
                    
                          if (MonDeckJ2.IsChecked == true)
                            {
                            DeckJ2.Add(cp);
                        
                            }
                     }
                }
            }
            if (AleatoireColJ1.IsChecked == true || AleatoireColJ2.IsChecked == true)
            {

                int nombreCarteCollection = maCollection.Count();
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
            Joueur J1 = new Joueur(PseudoJ1.ToString(), "", Convert.ToDateTime(DateJ1.Text), DeckJ1);
            Joueur J2 = new Joueur(PseudoJ2.ToString(), "", Convert.ToDateTime(DateJ2.Text), DeckJ2);
            lesJ.Add(J1);
            lesJ.Add(J2);
            // comment renvoyer J1 et J2 sur plateau
            File.WriteAllText("joueur.json", JsonConvert.SerializeObject(lesJ, Formatting.Indented));
            Plateau_Jeu fenetre = new Plateau_Jeu();
            fenetre.Show();
            this.Close();
            
        }

       
    }
   
}

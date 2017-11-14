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
    /// Logique d'interaction pour Accueil_Jeu.xaml
    /// </summary>
    public partial class Accueil_Jeu
    { List<Joueur> lesJ = new List<Joueur>();
        List<Carte> maCollection = new List<Carte>();            //collection du joueur (toutes les cartes qui ont un exemplaire > 0)
        List<Carte> monDeck = new List<Carte>();                //deck du joueur, liste de carte utilisé pour le jeu
        Carte carte;                                            //Correspond à la carte séléctionnée dans l'interface (peut être null)
        CarteAction carteAction = new CarteAction();
        CartePersonnage cartePersonnage = new CartePersonnage();
        CarteVehicule carteVehicule = new CarteVehicule();
        public Accueil_Jeu()
        {
            InitializeComponent();
            
        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            Joueur J1 = new Joueur(PseudoJ1.ToString(), "" );
            Joueur J2 = new Joueur(PseudoJ2.ToString(), "");
            J1.DateNaissance= Convert.ToDateTime(DateJ1);
            J2.DateNaissance = Convert.ToDateTime(DateJ2);
            if (MonDeckJ1.IsChecked == true)
            {foreach (CartePersonnage d in monDeck)
                {if(d.Type == "p")
                    J1.Deck.Add(d);
                }
            }
           
        }



    }
}

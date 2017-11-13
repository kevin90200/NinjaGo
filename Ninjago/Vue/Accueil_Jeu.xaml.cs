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
namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Accueil_Jeu.xaml
    /// </summary>
    public partial class Accueil_Jeu
    { List<Joueur> lesJ = new List<Joueur>();
        List<CartePersonnage> lesC = new List<CartePersonnage>();
        public Accueil_Jeu()
        {
            //InitializeComponent();
            //lesC.Add(Collection.lesCartes.Get);
        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            Joueur J1 = new Joueur(PseudoJ1.ToString(), "" );
            Joueur J2 = new Joueur(PseudoJ2.ToString(), "");
            J1.DateNaissance= Convert.ToDateTime(DateJ1);
            J2.DateNaissance = Convert.ToDateTime(DateJ2);
           
        }


        }
}

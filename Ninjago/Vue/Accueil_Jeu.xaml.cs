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
        public Accueil_Jeu()
        {
            InitializeComponent();
        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            Joueur J1 = new Joueur(PseudoJ1.ToString(), "" );


        }


        }
}

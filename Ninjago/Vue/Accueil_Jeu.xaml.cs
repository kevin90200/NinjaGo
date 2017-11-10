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
    {
        public Accueil_Jeu()
        {
            InitializeComponent();
        }

        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        {
            Plateau_Jeu fenetre = new Plateau_Jeu();
            fenetre.Show();
            this.Close();
        }
        private void btn_retour_plateau_Click(object sender, RoutedEventArgs e)
        {
            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
        }

    }
}

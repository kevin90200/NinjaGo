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
    /// Logique d'interaction pour Launcher.xaml
    /// </summary>
    public partial class Launcher
    {
        public Launcher()
        {
            InitializeComponent();
        }


        private void btn_jeu_Click(object sender, RoutedEventArgs e)
        {
            Accueil_Jeu fenetre = new Accueil_Jeu();
            fenetre.Show();
            this.Close();
        }

        private void btn_gestion_Click(object sender, RoutedEventArgs e)
        {
            Gestion fenetre = new Gestion();
            fenetre.Show();
            this.Close();
        }

        private void btn_deck_Click(object sender, RoutedEventArgs e)
        {
            Deck fenetre = new Deck();
            fenetre.Show();
            this.Close();
        }
    }
}

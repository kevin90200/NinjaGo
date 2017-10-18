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
    /// Logique d'interaction pour Plateau_Jeu.xaml
    /// </summary>
    public partial class Plateau_Jeu 
    {
        String choixMain;
        public Plateau_Jeu()
        {
            InitializeComponent();
            
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Content = choixMain;
            C1.IsChecked = false;
            C2.IsChecked = false;
            C3.IsChecked = false;
            C4.IsChecked = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        { 
            
            RadioButton choix = sender as RadioButton;
            choixMain=choix.Content.ToString();
        }
    }
}

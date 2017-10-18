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
        public Plateau_Jeu()
        {
            InitializeComponent();
            
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            
               
   
            Button btn = sender as Button;
            int x = btn.X;
            int y = btn.Y;
            MessageBox.Show("test");
        }
    }
}

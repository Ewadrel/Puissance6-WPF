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

namespace S1._01
{
    /// <summary>
    /// Logique d'interaction pour nbrjoueur.xaml
    /// </summary>
    public partial class nbrjoueur : Window
    {
        public nbrjoueur()
        {
            InitializeComponent();
        }

        private void _1joueur_Click(object sender, RoutedEventArgs e)
        {
            Window1 choixCouleur = new Window1();
            choixCouleur.ShowDialog();
        }

        private void _2joueurs_Click(object sender, RoutedEventArgs e)
        {
            Window1 choixCouleur = new Window1();
            choixCouleur.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour règles_du_jeu.xaml
    /// </summary>
    public partial class règles_du_jeu : Window
    {
        public règles_du_jeu()
        {
            InitializeComponent();
        }

        public void ReinitialiseJeu()
        {
            string cheminApplication = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(cheminApplication);
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReinitialiseJeu();
            this.DialogResult = true;
        }
    }
}

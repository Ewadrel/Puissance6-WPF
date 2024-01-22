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
using System.Diagnostics;

namespace S1._01
{
    /// <summary>
    /// Logique d'interaction pour Victoire.xaml
    /// </summary>
    public partial class Victoire : Window
    {
        public Victoire()
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

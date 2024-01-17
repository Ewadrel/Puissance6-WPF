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
    /// Logique d'interaction pour Jouer.xaml
    /// </summary>
    public partial class Jouer : Window
    {
        private ImageBrush titre = new ImageBrush();
        public Jouer()
        {
            InitializeComponent();
            titre.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/puissance6.png"));
            titre1.Fill = titre;
        }

        private void Canvas_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void boutonjouer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

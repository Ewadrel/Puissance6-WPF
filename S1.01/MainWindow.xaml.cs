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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S1._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int choixJeton = ChoixDuJeton(1);
        private ImageBrush jeton1 = new ImageBrush();
        private ImageBrush jeton2 = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private bool TourJoueur1 = true;
        //variable pour le tour du joueur
        public MainWindow()
        {
            InitializeComponent();
            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/.png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/jeton1.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/jeton2.gif"));

            jetonj1.Fill = jeton1;
            jetonj2.Fill = jeton2;

            plateau.Fill = fond;
        }
        public int ChoixDuJeton(int choix)
        {
            return choixJeton;

        }
        private void InitialisationGrille()
        //initialisation grille 
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var bouton = new Button
                    {
                        Content = "",
                        Tag = j,
                        //stockage
                    };
                    bouton.Click += Colonne_Click;
                    Grille.Children.Add(bouton);
                    // lorsqu'une colonne est cliquée
                }
            }
        }
    }
}

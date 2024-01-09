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
        private int[] COORDONNEX =[1, 2, 3, 4, 5, 6, 7, 8, 9];
        private int[] COORDONNEY = [1, 2, 3, 4, 5, 6, 7, 8] ;
        private string CHOIXJETON = ChoixDuJeton(1);
        private ImageBrush jeton1 = new ImageBrush();
        private ImageBrush jeton2 = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private bool TourJoueur1 = true;
        private int VALBONUS = 3;
        //variable pour le tour du joueur
        public MainWindow()
        {
            InitializeComponent();
            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/.png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/rose.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vert.png"));

            jetonj1.Fill = jeton1;
            jetonj2.Fill = jeton2;

            plateau.Fill = fond;
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
        public string ChoixDuJeton(int choix)
        {
            // modifier le type et les valeur if de choix en fonction de ce que renverra la fenêtre de question
            string choixj = " ";
            if (choix = 0)
            {
                choixj= "img/rose.png"
            }
            
            
            return choixj;

        }
        public int[] PointBonus( int[,] tab)
        {
            int[] bonus = [0,0];
            for (int i=0; i < tab.GetLength(0); i++)
            {
                for (int j = 1; j < tab.GetLength(1)-1; j++)
                { 
                    if (tab[i,j]==1 && tab[i+1,j-1]==1 && tab[i + 1, j + 1]==1 && tab[i + 2, j ] == 1)
                    {
                        bonus += VALBONUS;
                    }
                }
            }
            bonus = [0, 0];
            return bonus;
        }
    }
    
}

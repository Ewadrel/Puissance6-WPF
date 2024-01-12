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
        private int tourDuJoueur = 1;
        Point position = new Point(0, 0);
        private readonly double[] COORDONNEX = { 65, 190, 400, 500, 600, 700, 800, 900, 1000 };
        private readonly double[] COORDONNEY = { 65, 190, 400, 500, 600,700, 800, 910 } ;
        private int[,] grille = new int[8,9];
        
        //private string CHOIXJETON = ChoixDuJeton(1);
        private ImageBrush jeton1 = new ImageBrush();
        private ImageBrush jeton2 = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private bool TourJoueur1 = true;
        private int VALBONUS = 3;
        //variable pour le tour du joueur
        public MainWindow()
        {
            InitializeComponent();
            Jouer jouer = new Jouer();
            jouer.ShowDialog();
            Window1 choixCouleur = new Window1();
            choixCouleur.ShowDialog();
            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/puissance4x9x8.png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/violet.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vert.png"));

            
            plateau.Background = fond;
           
            
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
                    //bouton.Click += Colonne_Click;
                    //Grille.Children.Add(bouton);
                    // lorsqu'une colonne est cliquée
                }
            }
        }
        /*public string ChoixDuJeton(int choix)
        {
            //modifier le type et les valeur if de choix en fonction de ce que renverra la fenêtre de question
            string choixj = " ";
            if (choix = 0)
            {
                choixj = "img/rose.png";
            }
            
            
            return choixj;

        }
        */
        public int[] PointBonus( int[,] tab)
        {
            int[] bonus = { 0, 0 };
            for (int i=0; i < tab.GetLength(0)-2; i++)
            {
                for (int j = 1; j < tab.GetLength(1)-1; j++)
                { 
                    if (tab[i,j]==1 && tab[i+1,j-1]==1 && tab[i + 1, j + 1]==1 && tab[i + 2, j ] == 1)
                    {
                        bonus[0] += VALBONUS;
                    }
                    if (tab[i, j] == 2 && tab[i + 1, j - 1] == 2 && tab[i + 1, j + 1] == 2 && tab[i + 2, j] == 2)
                    {
                        bonus[1] += VALBONUS;
                    }
                }
            }
            
            return bonus;
        }
        public bool LIGNE(int[,] tab)
        {
            int g = 0;
            int[] compteur = { 0, 0, 0,0,0 };


            for (int i = 0; i < tab.GetLength(0); i++)
            {

                for (int j = 0; j < tab.GetLength(1) - 3; j++)
                {
                    if (tab[i, j] != 0)
                    {
                        for (int h = 0; h < 6; h++)
                        {
                            if (tab[i, j] == tab[i+h, j ])
                            {
                                compteur[j] += 1;

                            }


                        }

                    }
                }




            }
            for (int i = 0; i < compteur.Length; i++)
            {
                if (compteur[i] == 4)

                    g += 1;

            }
            if (g >= 1)
            {
                return true;
            }
            return false;
           
        }
        public int colonneoccupe(int[,] tab,int indicej)
        {
            int indice = 0;
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                if (tab[i, indicej] == 0)
                {
                    indice += 1;
                }

            }
            indice -= 1;
            return indice;

        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            position = e.GetPosition(plateau);
            double x = position.X;
            double min = x;
            int indice = 0;
            for (int i = 0; i < COORDONNEX.Length; i++)
            {
                if (COORDONNEX[i] - x < min)
                {
                    indice = i;
                    min = COORDONNEX[i] - x;
                }

            }
            grille[colonneoccupe(grille, indice), indice] = tourDuJoueur;

            Rectangle jeton = new Rectangle();
            jeton.Width = 110;
            jeton.Height = 110;
            jeton.Fill = jeton1;

            Canvas.SetTop(jeton, COORDONNEY[colonneoccupe(grille, indice)]);
            Canvas.SetLeft(jeton, COORDONNEX[indice]);
            plateau.Children.Add(jeton);

            MessageBox.Show("jeton");



        }
    }   
}

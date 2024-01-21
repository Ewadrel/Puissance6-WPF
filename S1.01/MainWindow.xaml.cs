using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;

namespace S1._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int compteur = 0;
        private int tourDuJoueur = 1;
        Point position = new Point(0, 0);
        private readonly double[] COORDONNEX = { 10, 134, 258, 382, 506, 630, 754, 878, 1002 };
        private readonly double[] COORDONNEY = { 3, 124, 245, 366, 487, 608, 729, 850 };
        private int[,] grille = new int[9, 9];
        private int nombreGagant = 6;
        private bool testeligne = false;
        private bool testecolonne = false;
        private bool testedigonale = false;
        private int score1 = 0;
        private int score2 = 0;

        private ImageBrush jetonIA = new ImageBrush();
        private ImageBrush jeton1 = new ImageBrush();
        private ImageBrush jeton2 = new ImageBrush();
        private ImageBrush jeton3 = new ImageBrush();
        private ImageBrush fond = new ImageBrush();
        private ImageBrush fm = new ImageBrush();
        private bool TourJoueur1 = true, joueur;
        private int VALBONUS = 1;

        public string[] couleurJoueur;
        public int nombreJoueur;
        private DispatcherTimer timer;
        private DateTime startTime;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            Jouer jouer = new Jouer();
            bool resultatjouer = (bool)jouer.ShowDialog();
            if (resultatjouer == true)
            {
                Nbrjoueur nbrjoueur1 = new Nbrjoueur();
                joueur = (bool)nbrjoueur1.ShowDialog();
                if (joueur == true || joueur == false)
                {
                    couleurJoueur = new string[nombreJoueur];
                    Window1 window1 = new Window1();
                    bool couleur = (bool)window1.ShowDialog();
                }
            }

            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/puissance4x9x8.png"));
            jetonIA.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/bleu (IA).png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/violet.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vert.png"));
            jeton3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/rose.png"));
            fm.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/fm.jpg"));
            main.Background = fm;

            plateau.Fill = fond;
            Canvas.SetZIndex(plateau, 1);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            startTime = DateTime.Now;
            // Démarrer le chronomètre dès que la fenêtre est chargée
            timer.Start();
            Score2.Visibility = Visibility.Collapsed;
            Score2_Copy.Visibility = Visibility.Collapsed;
            violetjeton1.Visibility = Visibility.Collapsed;
            violetjeton2.Visibility = Visibility.Collapsed;
            vertjeton1.Visibility = Visibility.Collapsed;
            vertjeton2.Visibility = Visibility.Collapsed;
            rosejeton1.Visibility = Visibility.Collapsed;
            rosejeton2.Visibility = Visibility.Collapsed;

            if (nombreJoueur == 1)
            {
                Score2.Visibility = Visibility.Visible;
            }
            else
            {
                Score2_Copy.Visibility = Visibility.Visible;
            }
            
        }
        


        public int[] PointBonus(int[,] tab)
        {
            int[] bonus = { 0, 0 };
            for (int i = 0;i<tab.GetLength(0)-2;i++)
            {
                for (int j = 1;j<tab.GetLength(1)-1;j++)
                {
                    if (tab[i, j] == 1 && tab[i + 1, j - 1] == 1 && tab[i + 1, j + 1] == 1 && tab[i + 2, j] == 1 && tab[i + 1, j ]==1)
                    {
                        bonus[0] += VALBONUS;
                    }
                    if (tab[i, j] == 2 && tab[i + 1, j - 1] == 2 && tab[i + 1, j + 1] == 2 && tab[i + 2, j] == 2 && tab[i + 1, j] == 2)
                    {
                        bonus[1] += VALBONUS;
                    }
                }
            }
            
                
            

            return bonus;
        }

        public bool Colonne(int[,] tab, int[] point)
        {
            int compte = 0;
            for (int i = 0; i < tab.GetLength(0) - 5; i++)
            {
                if (tab[i, point[1]] != 0)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (tab[i + j, point[1]] == tab[i, point[1]])
                        {
                            compte++;
                        }
                        if (compte == 6)
                        {
                            return true;
                        }
                    }

                }
                compte = 0;
            }
            return false;

        }

        public bool LIGNE(int[,] tab, int[] point)
        {


            int indice = point[0] + 1;
            int compte = 0;
            for (int i = 0; i < tab.GetLength(1) - 5; i++)
            {
                if (tab[indice, i] != 0)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (tab[indice, i + j] == tab[indice, i])
                        {
                            compte++;
                        }
                        if (compte == 6)
                        {
                            return true;
                        }
                    }

                }
                compte = 0;
            }
            return false;
        }
        public bool Diagmonte(int[,] tab, int[] point)
        {
            int compte = 0;
            int i = 0;
            for (int k = tab.GetLength(0) - 1; k >= nombreGagant - 1; k--)
            {
                for (int l = 0; l < tab.GetLength(1) - 4; l++)
                {


                    if (tab[k, l] != 0)
                    {

                        compte = 0;
                        i = 0;
                        while (i < 6 && tab[k, l] == tab[k - i, l + i])
                        {
                            i++;
                            compte++;
                        }
                        if (compte == nombreGagant)
                        {
                            return true; ;
                        }
                    }


                }
            }

            return false;

        }




        public bool Diagdescend(int[,] tab, int[] point)
        {
            int compte = 0;
            int i = 0;
            for (int k = tab.GetLength(0) - 1; k >= nombreGagant - 1; k--)
            {
                for (int l = nombreGagant - 1; l < tab.GetLength(1); l++)
                {
                    if (tab[k, l] != 0)
                    {
                        compte = 0;
                        i = 0;
                        while (i < nombreGagant && tab[k, l] == tab[k - i, l - i])
                        {
                            i++;
                            compte++;
                        }
                        if (compte == nombreGagant)
                        {
                            return true; ;
                        }
                    }
                }
            }

            return false;

        }

        public int colonneoccupe(int[,] tab, int indicej)
        {
            int indice = 0;
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                if (tab[i, indicej] == 0)
                {
                    indice += 1;
                }

            }
            if (indice == 0)
            {
                indice += 1;
            }
            indice -= 1;
            return indice;

        }
       
        private bool GrillePleine()
        {
            
            for (int j = 0; j < 8; j++)
            {
                if (grille[1, j] == 0)
                {
                    // Si une cellule est vide, la grille n'est pas pleine
                    return false;
                }
            }
            

            // Si on atteint ce point, toutes les cellules sont pleines
            return true;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            // trouver l'indice des colonnes de grille 
            position = e.GetPosition(plateau);
            double x = position.X;
            int indice = 0;
            for (int i = 0; i < COORDONNEX.Length; i++)
            {
                if (COORDONNEX[i] < x)
                {
                    indice = i;

                }

            }
            //dernière ligne du tableau occupé
            if (grille[1, indice] != 0)
            {

                MessageBox.Show("coup impossible");
            }
            else
            {
                Rectangle couleurjeton1 = new Rectangle();
                Rectangle couleurjeton2 = new Rectangle();
                //création du jeton
                Rectangle jeton = new Rectangle();
                jeton.Width = 110;
                jeton.Height = 110;
                Rectangle jetonia = new Rectangle();
                jetonia.Width = 110;
                jetonia.Height = 110;

                switch (nombreJoueur)
                {
                    case 1:
                        
                        if (couleurJoueur[compteur % 2] == "violet")
                        {

                            violetjeton1.Visibility = Visibility.Visible;
                            jeton.Fill = jeton1;
                        }
                        else if (couleurJoueur[compteur % 2] == "vert")
                        {

                            vertjeton1.Visibility = Visibility.Visible;
                            jeton.Fill = jeton2;
                        }
                        else
                        {

                            rosejeton1.Visibility = Visibility.Visible;
                            jeton.Fill = jeton3;
                        }
                        tourDuJoueur = 1;
                        
                        
                        
                        
                        break;
                    case 2:
                        if (compteur % 2 == 0)
                        {
                            if (couleurJoueur[compteur % 2] == "violet")
                            {
                                
                                violetjeton1.Visibility = Visibility.Visible;
                                jeton.Fill = jeton1;
                            }
                            else if (couleurJoueur[compteur % 2] == "vert")
                            {
                                
                                vertjeton1.Visibility = Visibility.Visible;
                                jeton.Fill = jeton2;
                            }
                            else
                            {
                                
                                rosejeton1.Visibility = Visibility.Visible;
                                jeton.Fill = jeton3;
                            }
                            tourDuJoueur = 1;
                        }
                        else
                        {
                            if (couleurJoueur[compteur % 2] == "violet")
                            {
                                
                                violetjeton2.Visibility = Visibility.Visible;
                                jeton.Fill = jeton1;
                            }
                            else if (couleurJoueur[compteur % 2] == "vert")
                            {
                                
                                vertjeton2.Visibility = Visibility.Visible;
                                jeton.Fill = jeton2;
                            }
                            else
                            {
                                
                                rosejeton2.Visibility = Visibility.Visible;
                                jeton.Fill = jeton3;
                            }
                            tourDuJoueur = 2;
                        }
                        break;
                       
                  
                }
                

                //jeton derriere la plateau
                Canvas.SetZIndex(jeton, 0);
                
                //ajoue du pion dans le tableau
                grille[colonneoccupe(grille, indice), indice] = tourDuJoueur;
                //ajoue du pion dans le canvas
                Canvas.SetTop(jeton, COORDONNEY[colonneoccupe(grille, indice)]);
                Canvas.SetLeft(jeton, COORDONNEX[indice]);
                main.Children.Add(jeton);
                compteur += 1;
                //detection coup gagnant
                int[] point = new int[] { colonneoccupe(grille, indice), indice };
                int[] Pi = PointBonus(grille);
                
                if (Pi[0] != 0 || Pi[1]!=0)
                {
                    bonusJ1.Text = Pi[0].ToString();
                    bonusJ2.Text = Pi[1].ToString();
                }
                int[] pointia = new int[] { 11, 11 };
                if (nombreJoueur==1)
                {
                    int ligne= 0;
                    jetonia.Fill = jetonIA;
                    Canvas.SetZIndex(jetonia, 0);
                    grille[colonneoccupe(grille, ligne), ligne] = tourDuJoueur + 1;
                    Canvas.SetTop(jetonia, COORDONNEY[colonneoccupe(grille, ligne)]);
                    Canvas.SetLeft(jetonia, COORDONNEX[ligne]);
                    main.Children.Add(jetonia);    
                    compteur += 1;
                       
                    pointia = new int[] { colonneoccupe(grille, ligne), ligne };


                   
                    
                }
                if (GrillePleine()==true) 
                {
                    Victoire victoire = new Victoire();
                    if (Pi[0] > Pi[1])
                    {
                        victoire.gagne.Text = "Joueur 1";
                    }
                    else
                    { 
                        victoire.gagne.Text = "Joueur 2";
                    }
                    

                    timer.Stop();
                    victoire.ShowDialog();
                }
                int victorieux = 0;

                if (LIGNE(grille, point) == true || Colonne(grille, point) == true || Diagmonte(grille, point) == true || Diagdescend(grille, point) == true)
                {
                    victorieux = 1;
                }
                if    (LIGNE(grille, pointia) == true || Colonne(grille, pointia) == true || Diagmonte(grille, pointia) == true || Diagdescend(grille, pointia) == true)
                { 
                    victorieux = 2; 
                } 
                if (victorieux!=0) 
                {
                    
                    Victoire victoire = new Victoire();
                    if(nombreJoueur==2) 
                    {
                        if (tourDuJoueur == 1)
                        {
                            victoire.gagne.Text = "Joueur 1";
                        }
                        else
                        {
                            victoire.gagne.Text = "Joueur 2";
                        }
                    }
                    else
                    {
                        if (victorieux == 1)
                        {
                            victoire.gagne.Text = "Joueur 2";
                        }
                        else
                        {
                            victoire.gagne.Text = "IA";
                        }
                    }
                    
                    

                    timer.Stop();
                    victoire.ShowDialog();
                }
               

            }


            //affiche la grille
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    Console.Write(grille[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(colonneoccupe(grille, indice));
            Console.WriteLine(indice);
            




        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            UpdateTimerDisplay(elapsed);
        }
        private void UpdateTimerDisplay(TimeSpan elapsed)
        {
            txtChrono.Text = elapsed.ToString(@"hh\:mm\:ss");
        }
    }
}


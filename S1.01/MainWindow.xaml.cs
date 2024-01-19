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
        private int VALBONUS = 3;

        public string[] couleurJoueur;
        public int nombreJoueur;
        private DispatcherTimer timer;
        private DateTime startTime;
        private Random random = new Random();

        public int Score10
        {
            get { return score1; }
            set
            {
                score1 = value;
            }
        }

        public int Score20
        {
            get { return score2; }
            set
            {
                score2 = value;
            }
        }

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
            for (int i = 0; i < tab.GetLength(0) - 2; i++)
            {
                for (int j = 1; j < tab.GetLength(1) - 1; j++)
                {
                    if (tab[i, j] == 1 && tab[i + 1, j - 1] == 1 && tab[i + 1, j + 1] == 1 && tab[i + 2, j] == 1)
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

        static bool EstTableauRempli(int[,] grille)
        {
            int lignes = grille.GetLength(0);
            int trousParLigne = grille.GetLength(1);

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < trousParLigne; j++)
                {
                    if (grille[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static bool DetecterFormeTroisDeux(int[,] grille)
        {
            int compte = 0;
            int lignes = grille.GetLength(0);
            int trousParLigne = grille.GetLength(1);

            // Parcourir chaque ligne et colonne pour détecter une forme (3,2)
            for (int i = 0; i < lignes - 2; i++)
            {
                for (int j = 0; j < trousParLigne - 1; j++)
                {
                    if (grille[i, j] != 0 &&
                        grille[i, j + 1] != 0 &&
                        grille[i + 1, j] != 0 &&
                        grille[i + 1, j + 1] != 0 &&
                        grille[i + 2, j] != 0 &&
                        grille[i + 2, j + 1] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        static bool DetecterFormeDeuxTrois(int[,] grille)
        {
            int lignes = grille.GetLength(0);
            int trousParLigne = grille.GetLength(1);

            // Parcourir chaque ligne et colonne pour détecter une forme (2,3)
            for (int i = 0; i < lignes - 1; i++)
            {
                for (int j = 0; j < trousParLigne - 2; j++)
                {
                    if (grille[i, j] != 0 &&
                        grille[i, j + 1] != 0 &&
                        grille[i, j + 2] != 0 &&
                        grille[i + 1, j] != 0 &&
                        grille[i + 1, j + 1] != 0 &&
                        grille[i + 1, j + 2] != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // trouver l'indice des lignes de grille 

            double y = 9;
            int indice1 = 0;
            for (int i = 0; i < COORDONNEY.Length; i++)
            {
                if (COORDONNEY[i] < y)
                {
                    indice1 = i;

                }

            }
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
                //création du jeton
                Rectangle jeton = new Rectangle();
                jeton.Width = 110;
                jeton.Height = 110;
                if (nombreJoueur == 1)
                {
                    if (jeton.Fill == jeton1 || jeton.Fill == jeton2 || jeton.Fill == jeton3)
                    {
                        int randomColumn;
                        randomColumn = random.Next(0, grille.GetLength(0));
                        jeton.Fill = jetonIA;
                    }
                }
                switch (nombreJoueur)
                {

                    case 2:
                        if (compteur % 2 == 0)
                        {
                            if (couleurJoueur[compteur % 2] == "violet")
                            {
                                jeton.Fill = jeton1;
                            }
                            else if (couleurJoueur[compteur % 2] == "vert")
                            {
                                jeton.Fill = jeton2;
                            }
                            else
                            {
                                jeton.Fill = jeton3;
                            }
                            tourDuJoueur = 1;
                        }
                        else
                        {
                            if (couleurJoueur[compteur % 2] == "violet")
                            {
                                jeton.Fill = jeton1;
                            }
                            else if (couleurJoueur[compteur % 2] == "vert")
                            {
                                jeton.Fill = jeton2;
                            }
                            else
                            {
                                jeton.Fill = jeton3;
                            }
                            tourDuJoueur = 2;
                        }
                        break;
                    case 1:
                        if (couleurJoueur[0] == "violet")
                        {
                            jeton.Fill = jeton1;
                        }
                        else if (couleurJoueur[0] == "vert")
                        {
                            jeton.Fill = jeton2;
                        }
                        else
                        {
                            jeton.Fill = jeton3;
                        }
                        tourDuJoueur = 1;
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

                if (LIGNE(grille, point) == true || Colonne(grille, point) == true || Diagmonte(grille, point) == true || Diagdescend(grille, point) == true)
                {
                    Victoire victoire = new Victoire();
                    if (tourDuJoueur == 1)
                    {
                        victoire.gagne.Text = "Joueur 1";
                    }
                    else if (tourDuJoueur == 2)
                    {
                        victoire.gagne.Text = "Joueur 2";
                    }
                    else { victoire.gagne.Text = "IA"; }

                    timer.Stop();
                    victoire.ShowDialog();
                }
                /* else if (EstTableauRempli(grille) == true)
                 {
                     Victoire victoire = new Victoire();
                     timer.Stop();
                     victoire.ShowDialog();
                 }*/

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

        private void MettreAJourScore(int points, int tourDuJoueur)
        {

            if (tourDuJoueur == 1)
            {
                Score10 += points;
            }
            else

                Score20 += points;
        }
        private void SimulerDetectionForme()
        {
            // Exemple : Vous pouvez appeler cette méthode pour simuler la détection de la forme (2,3)
            if (DetecterFormeDeuxTrois(grille) == true)
            {
                MettreAJourScore(10, tourDuJoueur); // Ajouter 10 points
            }
        }
    }
}


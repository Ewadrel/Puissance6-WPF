﻿using System;
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
        private int compteur=0;
        private int tourDuJoueur = 1;
        Point position = new Point(0, 0);
        private readonly double[] COORDONNEX = { 0,153, 286,419 , 553,686, 819, 952, 1085, 1218 };
        private readonly double[] COORDONNEY = { 11,132.75, 265.5, 398.25,531,663.75,796.5, 929.25 } ;
        private int[,] grille = new int[8,9];
        private bool testeligne=false;
        private bool testecolonne = false;
        private bool testedigonale = false;


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
            /*
            Jouer jouer = new Jouer();
            jouer.ShowDialog();
            nbrjoueur nbrjoueur = new nbrjoueur();
            nbrjoueur.ShowDialog();
            Window1 choixCouleur = new Window1();
            choixCouleur.ShowDialog();
            */
            fond.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/puissance4x9x8.png"));
            jeton1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/violet.png"));
            jeton2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img/vert.png"));
            
            
            plateau.Fill = fond;
            Canvas.SetZIndex(plateau, 1);
           
            
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
                    //grille.Children.Add(bouton);
                    // lorsqu'une colonne est cliquée
                }
            }
        }
        private void Colonne_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int column = (int)button.Tag; // Récupérer la colonne à partir du Tag du bouton
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
        public bool Colonne(int[,] tab, int[]point)
        {
            int compte1 = 0;
            int compte2 = 0;
            for (int i = 0; i < tab.GetLength(0); i++)
            {

                if (tab[i, point[1]] == 1)
                {
                    compte1++;
                }
                if (tab[i, point[0]] == 2)
                {
                    compte2++;
                }
                Console.WriteLine(tab[i, point[0]]);

            }

            if (compte1 == 6 || compte2 == 6)
            {
                return true;;
            }
             return false;
            
            
            
            

        }
            public bool LIGNE(int[,] tab, int[]point)
        {
            

            int compte1 = 0;
            int compte2 = 0;
            for (int i = 0; i < tab.GetLength(1); i++)
            {

                if (tab[point[0], i] == 1)
                {
                    compte1++;
                }
                if (tab[point[0], i] == 2)
                {
                    compte2++;
                }
                Console.WriteLine(tab[point[0], i]);

            }

            if (compte1 == 6 || compte2 == 6)
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
            int indice = 0;
            for (int i = 0; i < COORDONNEX.Length; i++)
            {
                if ( COORDONNEX[i]  < x)
                {
                    indice = i;
                    
                }

            }
            
            if (grille[0,indice]!=0)
            { 
                MessageBox.Show("coup impossible"); 
            }
            else
            {
               
                Rectangle jeton = new Rectangle();
                jeton.Width = 130;
                jeton.Height = 130;
                if (compteur % 2 == 0)
                {
                    jeton.Fill = jeton1;
                    tourDuJoueur = 1;
                }
                else
                {
                    jeton.Fill = jeton2;
                    tourDuJoueur = 2;
                }
                Canvas.SetZIndex(jeton, 0);

                grille[colonneoccupe(grille, indice), indice] = tourDuJoueur;

                Canvas.SetTop(jeton, COORDONNEY[colonneoccupe(grille, indice)]);
                Canvas.SetLeft(jeton, COORDONNEX[indice]);
                main.Children.Add(jeton);
                compteur += 1;
                
            }
            int[]point = new int[] { colonneoccupe(grille, indice), indice};
            if (LIGNE(grille,point) ==true|| Colonne(grille, point) == true)
            {
                MessageBox.Show("coup gagnant");
            }

           


            



        }
    }   
}

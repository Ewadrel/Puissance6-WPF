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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace S1._01
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).nombreJoueur == 1)
            {
                ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "vert";
                this.DialogResult = true;
            } else
            {
                if (((MainWindow)Application.Current.MainWindow).couleurJoueur[0] == null)
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "vert";
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[1] = "vert";
                    this.DialogResult = true;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).nombreJoueur == 1)
            {
                ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "violet";
                this.DialogResult = true;
            }
            else
            {
                if (((MainWindow)Application.Current.MainWindow).couleurJoueur[0] == null)
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "violet";
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[1] = "violet";
                    this.DialogResult = true;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (((MainWindow)Application.Current.MainWindow).nombreJoueur == 1)
            {
                ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "rose";
                this.DialogResult = true;
            }
            else
            {
                if (((MainWindow)Application.Current.MainWindow).couleurJoueur[0] == null)
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[0] = "rose";
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).couleurJoueur[1] = "rose";
                    this.DialogResult = true;
                }
            }
        }
    }
}

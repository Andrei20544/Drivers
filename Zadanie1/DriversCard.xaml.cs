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
using Zadanie1.View;

namespace Zadanie1
{
    /// <summary>
    /// Логика взаимодействия для DriversCard.xaml
    /// </summary>
    public partial class DriversCard : Window
    {
        public DriversCard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListDrive LD = new ListDrive();
            LD.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CardDrive CD = new CardDrive();
            CD.Show();
        }
    }
}

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
using VIN_LIB;
using REG_MARK_LIB;
using Microsoft.Win32;
using System.Threading;

namespace Zadanie1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count;
        public MainWindow()
        {
            InitializeComponent();

            log.Text = "inspector";
            pass.Password = "inspector";

            count = 0;
            System.Diagnostics.Process[] proc;
            proc = System.Diagnostics.Process.GetProcesses();
            string flow = Thread.GetDomain().FriendlyName;
            string name = flow.Split('.')[0];
            int k = 0;
            foreach (System.Diagnostics.Process i in proc)
                if (i.ProcessName.Equals(name))
                {
                    k++;
                    if (k == 2)
                    {
                        this.Close();
                    }
                }

            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string LOG = "inspector";
            
            if (log.Text.Equals(LOG) && pass.Password.Equals(LOG))
            {
                DriversCard DC = new DriversCard();
                DC.ShowDialog();
            }
            else
            {
                count++;
                if (count == 3)
                {
                    MessageBox.Show("Логин или пароль были введены не правильно, ожидайте одну минуту.");
                    Thread.Sleep(1000 * 60);
                    count = 0;
                }
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left) this.DragMove();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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

namespace Zadanie1
{
    /// <summary>
    /// Логика взаимодействия для ListDrive.xaml
    /// </summary>
    public partial class ListDrive : Window
    {

        
        public ListDrive()
        {
            InitializeComponent();
            DataContext = new DriverViewModel();
            DriverViewModel driverViewModel = new DriverViewModel();
            count.Content = driverViewModel.CountObject();

        }

        private void Phot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdatePhoto();
        }

        public void UpdatePhoto()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = " png (*.png)|*.png | jpg (*.jpg)|*.jpg";
                openFileDialog.ShowDialog();

                var path = $@"{AppDomain.CurrentDomain.BaseDirectory}/Assets";
                bool FileExists = new FileInfo(path + "/" + openFileDialog.SafeFileName).Exists;              

                if (FileExists == true && GetFileInfoBool(openFileDialog.FileName) == true)
                {
                    GetBitmap(path, openFileDialog.SafeFileName);
                }
                else if (FileExists == false && GetFileInfoBool(openFileDialog.FileName) == true)
                {                
                    File.Copy($@"{openFileDialog.FileName}", path + $"/{openFileDialog.SafeFileName}");
                    this.Resources.Add("Assets", openFileDialog.SafeFileName);
                    GetBitmap(path, openFileDialog.SafeFileName);
                } 
                else MessageBox.Show("Выбраный файл не подходит.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void GetBitmap(string path, string fileName)
        {
            Uri uri = new Uri($@"{path}/{fileName}");
            BitmapImage bitmap = new BitmapImage(uri);

            Phot.Source = bitmap;
            LabelPath.Text = fileName;
        }

        public bool GetFileInfoBool(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            long fileLength = fileInfo.Length / 1024;

            Uri uri = new Uri($@"{path}");
            BitmapImage bitmap = new BitmapImage(uri);

            if (fileLength <= 2048 && bitmap.Width < bitmap.Height || fileLength <= 2048 && bitmap.Width == bitmap.Height) return true;
            return false;
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            DataContext = new DriverViewModel();
            var list = new List<Drivers>();

            foreach (Drivers driver in ddt.Items)
            {
                if (driver.Name.Contains(DriverName.Text)) list.Add(driver);
            }

            DataContext = new DriverViewModel(list);

            count.Content = list.Count;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!Code.Text.Contains(".") && Code.Text.Length != 0))) 
            {
                e.Handled = true;
            }
        }

        private void AddPhot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdatePhoto();
        }

        public BitmapImage GetBitmapImage(string FileName)
        {
            Uri uri = new Uri($@"{AppDomain.CurrentDomain.BaseDirectory}/Assets/{FileName}C.png");
            BitmapImage bitmapImage = new BitmapImage(uri);

            return bitmapImage;
        }
    }
}

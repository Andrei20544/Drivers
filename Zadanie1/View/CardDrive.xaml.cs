using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using Zadanie1.Model;
using Zadanie1.ViewModel;

namespace Zadanie1.View
{
    /// <summary>
    /// Логика взаимодействия для CardDrive.xaml
    /// </summary>
    public partial class CardDrive : Window
    {
        public CardDrive()
        {
            InitializeComponent();
            DataContext = new DriverCardViewModel();
            DriverCardViewModel driverCardViewModel = new DriverCardViewModel();
            Count.Content = driverCardViewModel.CountObject();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataContext = new DriverCardViewModel();
            var listLic = new List<DriversAndLicences>();

            foreach (DriversAndLicences lic in list.Items)
            {
                if (lic.Name.Contains(LicName.Text)) listLic.Add(lic);
            }

            DataContext = new DriverCardViewModel(listLic);
            Count.Content = listLic.Count;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListDrive listDrive = new ListDrive();
            listDrive.Show();
        }

        private void AddPhot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stats stats = new Stats();
            stats.Show();
        }
    }
}

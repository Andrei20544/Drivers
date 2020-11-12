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

namespace Zadanie1.View
{
    /// <summary>
    /// Логика взаимодействия для Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        public Stats()
        {
            InitializeComponent();
            using (ModelDB db = new ModelDB())
            {
                var Year = db.Licences.Select(p => new
                {
                    Year = p.LicenceDate.Value.Year
                }).ToList().Distinct().OrderBy(p => p.Year);
                foreach (var yr in Year)
                {
                    YearStat.Items.Add(yr.Year);
                }
            }

        }

        private void YearStat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Canv.Children.Clear();
            yearTextBoxAf.Text = "";
            yearTextBoxBe.Text = "";

            int sec = int.Parse(YearStat.SelectedItem.ToString());
            Brush brushes=null;

            using(ModelDB db = new ModelDB())
            {
                for(int m = 1; m <= 12; m++)
                {
                    int Month = db.Licences.Where(p => p.LicenceDate.Value.Year == sec && p.LicenceDate.Value.Month == m).ToList().Count;

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;

                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 80;
                    rectangle.Height = Month * 10;
                    rectangle.Stroke = Brushes.Black;
                    rectangle.StrokeThickness = 2;
                    rectangle.Fill = Brushes.Gray;

                    if (Month == 0)
                    {
                        rectangle.Fill = Brushes.White;
                    }

                    rectangle.VerticalAlignment = VerticalAlignment.Bottom;
                    stackPanel.VerticalAlignment = VerticalAlignment.Bottom;

                    stackPanel.Children.Add(GetLabel(m.ToString() + " Month" + "-" + Month, 80));
                    stackPanel.Children.Add(rectangle);
                    
                    Canv.Children.Add(stackPanel);
                    Canv.Children.Add(GetLabel(""));
                }           
            }
        }

        public Label GetLabel(string Content, int Width = 3)
        {
            Label label = new Label();
            label.Content = Content;
            label.Width = Width;

            return label;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Canv.Children.Clear();

            using (ModelDB db = new ModelDB())
            {
                var Year = db.Licences.Select(p => new
                {
                    Month = p.LicenceDate.Value.Month
                }).ToList().Distinct().OrderBy(p => p.Month);

                int NumYear = int.Parse(yearTextBoxAf.Text) - int.Parse(yearTextBoxBe.Text) + 1;

                for (int m = int.Parse(yearTextBoxBe.Text); m <= int.Parse(yearTextBoxAf.Text); m++)
                {
                    int GiveUdYear = db.Licences.Where(p => p.LicenceDate.Value.Year == m).ToList().Count;

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;

                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = 80;
                    rectangle.Height = GiveUdYear * 2;
                    rectangle.Stroke = Brushes.Black;
                    rectangle.StrokeThickness = 2;
                    rectangle.Fill = Brushes.Gray;

                    if (GiveUdYear == 0)
                    {
                        rectangle.Fill = Brushes.White;
                    }

                    rectangle.VerticalAlignment = VerticalAlignment.Bottom;
                    stackPanel.VerticalAlignment = VerticalAlignment.Bottom;

                    stackPanel.Children.Add(GetLabel(m.ToString() + "-" + GiveUdYear, 80));
                    stackPanel.Children.Add(rectangle);

                    Canv.Children.Add(stackPanel);
                    Canv.Children.Add(GetLabel(""));
                }
            }

            

        }
    }
}

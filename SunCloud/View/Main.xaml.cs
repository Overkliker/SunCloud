using LiveCharts.Wpf;
using LiveCharts;
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
using SunCloud.ViewModel.Helpers;
using SunCloud.Model;

namespace SunCloud
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string globalCity { get; set; }
        public Main(string city)
        {
            InitializeComponent();

            globalCity = city;
            Chart();
            
        }
        
        private void Chart()
        {
            Citi.Text = globalCity;
            Root nowData = Serializer.Get(globalCity);

            List<Hour> hours = nowData.forecast.forecastday[0].hour;
            ChartValues<double> values = new ChartValues<double>();
            List<string> times = new List<string>();
            
            foreach (Hour hour in hours)
            {
                DateTime enteredDate = DateTime.Parse(hour.time);
                string time = enteredDate.TimeOfDay.ToString().Substring(0, 5);
                times.Add(time);
                values.Add(hour.temp_c);
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = values
                }
            };

            Labels = times;
            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        //Тыкаешь на любую область окна и можешь перетаскивать приложение
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

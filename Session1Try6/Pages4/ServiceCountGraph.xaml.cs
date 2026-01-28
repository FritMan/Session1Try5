using LiveCharts;
using LiveCharts.Wpf;
using Session1Try6.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages4
{
    /// <summary>
    /// Логика взаимодействия для ServiceCountGraph.xaml
    /// </summary>
    public partial class ServiceCountGraph : Page
    {
        public ServiceCountGraph()
        {
            InitializeComponent();
            StartDp.SelectedDate = DateTime.Now;
            EndDp.SelectedDate = DateTime.Now.AddDays(1);

            LoadGraph();
            LoadTable();
        }
        private void LoadGraph()
        {
            var start = StartDp.SelectedDate;
            var end = EndDp.SelectedDate;

            var date = start;

            List<int> ints = new List<int>();

            var orders = Db.OrderService.ToList();

            List<OrderService> SortedOrder = new List<OrderService>();

            while (date <= end)
            {
                int count = 0;

                foreach (var item in orders)
                {
                    if (item.Order.DateCreate.Date == date.Value.Date)
                    {
                        count++;
                    }
                }

                ints.Add(count);
                date = date.Value.AddDays(1);
            }

            var series = new SeriesCollection
            {
                new ColumnSeries{Values=new ChartValues<int> { }, Title="Количество оказанных услуг" }
            };

            StolbGraph.Series = series;

            foreach (var item in ints)
            {
                series[0].Values.Add(item);
            }


            CultureInfo culture = new CultureInfo("RU-ru");
            List<string> labels = new List<string>();

            date = start;

            while (date <= end)
            {
                string day = culture.DateTimeFormat.GetAbbreviatedDayName(date.Value.DayOfWeek);
                day += "\n" + date.Value.ToString("dd.MM");

                labels.Add(day);

                date = date.Value.AddDays(1);
            }

            StolbGraph.AxisX.Clear();
            StolbGraph.AxisX.Add(new Axis { Labels = labels, Separator = new LiveCharts.Wpf.Separator { Step = 1 } });
        }

        private class ServiceCount
        {
            public DateTime Date { get; set; }
            public string Name { get; set; }
        }

        private void LoadTable()
        {
            var start = StartDp.SelectedDate;
            var end = EndDp.SelectedDate;

            var date = start;

            var orders = Db.OrderService.ToList();

            List<OrderService> SortedOrder = new List<OrderService>();
            List<ServiceCount> sortedserv = new List<ServiceCount>();

            while(date <= end)
            {

                foreach (var item in orders)
                {
                    if(item.Order.DateCreate.Date == date.Value.Date)
                    {
                       ServiceCount serviceCount = new ServiceCount { Name=item.Service.Name, Date=item.Order.DateCreate};
                       sortedserv.Add(serviceCount);
                    }
                }
                
                date = date.Value.AddDays(1);

                DgGraph.ItemsSource = sortedserv;
            }

            
        }

        private void StartDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TableBtn.IsEnabled == true && GraphBtn.IsSealed == false)
            {
                LoadGraph();
            }
            else if(TableBtn.IsEnabled == false && GraphBtn.IsEnabled == true) 
            {
                LoadTable();
            }
        }

        private void EndDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableBtn.IsEnabled == true && GraphBtn.IsEnabled == false)
            {
                LoadGraph();
            }
            else if (TableBtn.IsEnabled == false && GraphBtn.IsEnabled == true)
            {
                LoadTable();
            }
        }

        private void TableBtn_Click(object sender, RoutedEventArgs e)
        {
            StolbGraph.Visibility = Visibility.Collapsed;
            DgGraph.Visibility = Visibility.Visible;
            GraphBtn.IsEnabled = true;
            TableBtn.IsEnabled = false;
            LoadTable();
        }

        private void GraphBtn_Click(object sender, RoutedEventArgs e)
        {
            StolbGraph.Visibility = Visibility.Visible;
            DgGraph.Visibility = Visibility.Collapsed;
            TableBtn.IsEnabled = true;
            GraphBtn.IsEnabled = false;
            LoadGraph();
        }
    }
}

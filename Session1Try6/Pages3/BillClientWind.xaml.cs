using Microsoft.Win32;
using Session1Try6.Data;
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
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages3
{
    /// <summary>
    /// Логика взаимодействия для BillClientWind.xaml
    /// </summary>
    public partial class BillClientWind : Window
    {
        public BillClientWind()
        {
            InitializeComponent();
            ClientCb.ItemsSource = Db.Client.ToList();
            StartDp.SelectedDate = DateTime.Now;
            EndDp.SelectedDate = DateTime.Now.AddDays(1);
            LoadData();


        }

        private void LoadData()
        {
            var client = ClientCb.SelectedItem as Client;
            List<OrderService> list = new List<OrderService>();


            if (client != null)
            {
                var NoFiltredList = Db.Order.Where(el => el.ClientId == client.Id).ToList();

                foreach (var item in NoFiltredList)
                {
                    if(item.DateCreate >= StartDp.SelectedDate && item.DateCreate <= EndDp.SelectedDate)
                    {
                        list.AddRange(item.OrderService);
                    }
                }

                ServiceDg.ItemsSource = list;
                PhoneTb.Text = client.Phone;

                FinalTb.Text = list.Sum(el => el.Service.Price).ToString();
            }
            
        }

        private void ClientCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void StartDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void EndDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void PdfBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = new System.Printing.PrintQueue(new System.Printing.PrintServer(), "Microsoft Print to PDF");
            printDialog.PrintVisual(BillSp, "");
        }

        private void CsvBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "*.csv|.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                var org = ClientCb.SelectedItem as Client;
                string csv = "ФИО;Телефон;Период оплаты \n";

                csv += $"{org.Name};{org.Phone};C {StartDp.SelectedDate.Value.ToString("d")} по {EndDp.SelectedDate.Value.ToString("d")}\n";

                csv += "Перечень услуг\n";
                csv += "Название;Цена\n";


                foreach (var el in ServiceDg.ItemsSource)
                {
                    csv += $"{(el as OrderService).Service.Name};{(el as OrderService).Service.Price} \n";
                }

                csv += $"Финальная стоимость: {FinalTb.Text}\n";

                File.WriteAllText(saveFileDialog.FileName, csv);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

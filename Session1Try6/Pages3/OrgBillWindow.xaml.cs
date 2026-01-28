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
    /// Логика взаимодействия для OrgBillWindow.xaml
    /// </summary>
    public partial class OrgBillWindow : Window
    {
        public OrgBillWindow()
        {
            InitializeComponent();
            StartDp.SelectedDate = DateTime.Now;
            EndDp.SelectedDate = DateTime.Now.AddDays(1);
            OrgCb.ItemsSource = Db.Organisation.ToList();

            LoadData();
        }

        private void LoadData()
        {
            var com = OrgCb.SelectedItem as Organisation;
            var list = new List<OrderService>();

            if (com != null)
            {
                foreach(var el in Db.Order.Where(el => el.Client.Organisation.Id == com.Id).ToList())
                {
                    if(el.DateCreate.Date >= StartDp.SelectedDate.Value.Date && el.DateCreate.Date <= EndDp.SelectedDate.Value.Date)
                    {
                        list.AddRange(el.OrderService);
                    }
                }

                ServiceDg.ItemsSource = list;
                FinalTb.Text = list.Sum(el => el.Service.Price).ToString();
            }


        }

        private void StartDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void EndDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData() ;
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
                var org = OrgCb.SelectedItem as Organisation;
                string csv = "Организация;Период оплаты \n";

                csv += $"{org.Name};C {StartDp.SelectedDate.Value.ToString("d")} по {EndDp.SelectedDate.Value.ToString("d")}\n";

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

        private void OrgCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

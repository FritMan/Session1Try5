using Microsoft.Win32;
using Session1Try6.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
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

namespace Session1Try6.Pages2
{
    /// <summary>
    /// Логика взаимодействия для PdfOrderWindow.xaml
    /// </summary>
    public partial class PdfOrderWindow : Window
    {
        public PdfOrderWindow(Order order)
        {
            InitializeComponent();
            OrderSp.DataContext = order;
            ServiceDg.ItemsSource = order.OrderService.ToList();
            FinalTb.Text = order.OrderService.Sum(el => el.Service.Price).ToString();

            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF");
            printDialog.PrintVisual(OrderSp, "");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string base64 = "";
            var order = OrderSp.DataContext as Order;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "*.txt|.txt";
            if(saveFileDialog.ShowDialog() == true)
            {
                base64 += "ФИО;Дата рождения;Организация;Дата заказа;Код заказа \n";

                if (order.Client.OrganisationId == null)
                {
                    base64 += $"{order.Client.Fio};{order.Client.BirthDate.ToString("d")};' ';{order.DateCreate.ToString("d")};{order.Code}\n";
                }
                else
                {
                    base64 += $"{order.Client.Fio};{order.Client.BirthDate.ToString("d")};{order.Client.Organisation.Name};{order.DateCreate.ToString("d")};{order.Code}\n";
                }

                    base64 += "Перечень услуг\n";

                    base64 += "Название;Цена\n";

                    foreach (var el in order.OrderService)
                    {
                        base64 += $"{el.Service.Name};{el.Service.Price.ToString()} \n";
                    }

                    base64 += $"Финальная стоимость: {FinalTb.Text}\n";

                    File.WriteAllText(saveFileDialog.FileName, Convert.ToBase64String(Encoding.UTF8.GetBytes(base64)));
                    Close();
                
            }
        }
    }
}

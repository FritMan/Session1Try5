using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Microsoft.Win32;
using Session1Try6.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages2
{
    /// <summary>
    /// Логика взаимодействия для TakeWastePage.xaml
    /// </summary>
    public partial class TakeWastePage : Page
    {
        private ObservableCollection<Data.Service> services = new ObservableCollection<Data.Service>();
        public TakeWastePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ClientCb.ItemsSource = Db.Client.ToList();
            ServiceCb.ItemsSource = Db.Service.ToList();

            ClientCb.SelectedIndex = 0;
            ServiceCb.SelectedIndex = 0;

            ServiceDg.ItemsSource = services;


            var lastorder = Db.Order.OrderByDescending(el => el.Code).FirstOrDefault();

            var neworder = new Order { DateCreate = DateTime.Now, IsDeleted = false, OrderService=new List<OrderService>(), StatusId=2 };

            if (lastorder != null)
            {
                neworder.Code = (int.Parse(lastorder.Code) + 1).ToString();
            }
            else
            {
                neworder.Code ="1";
            }

            OrderSp.DataContext = neworder;
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                QRCodeDecoder qRCodeDecoder = new QRCodeDecoder();
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(openFile.FileName);
                QRCodeBitmapImage qRCodeBitmapImage = new QRCodeBitmapImage(bitmap);
                var res = qRCodeDecoder.Decode(qRCodeBitmapImage);
                MessageBox.Show(res);
            }

        }

        private void SearchClientTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddCientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            LoadData();
        }

        private void ServiceTbSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddBtnService_Click(object sender, RoutedEventArgs e)
        {
            var sel_serv = ServiceCb.SelectedItem as Data.Service;

            if (sel_serv != null)
            {
                services.Add(sel_serv);
                var order = OrderSp.DataContext as Order;
                order.OrderService.Add(new OrderService {ServiceId=sel_serv.Id, StatusId=2});

                FinalTb.Text = services.Sum(el => el.Price).ToString();
                ServiceDg.ItemsSource = services;
                
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = OrderSp.DataContext as Order;

            Db.Order.Add(order);
            Db.SaveChanges();

            PdfOrderWindow pdfOrderWindow = new PdfOrderWindow(order);
            pdfOrderWindow.ShowDialog();

            NavigationService.Navigate(new TakeWastePage());
        }

        private void CodeTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CodeTb.IsReadOnly = true;
                VisSp.Visibility = Visibility.Visible;
            }
        }
    }
}

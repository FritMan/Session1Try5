using Session1Try6.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {

        public AdminPage(User user)
        {
            InitializeComponent();
            UserSp.DataContext = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new Pages3.HistoryPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("https://rpn.gov.ru/fkko/");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages4.GraphMenu());
        }
    }
}

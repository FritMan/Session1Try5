using Session1Try6.Data;
using Session1Try6.Pages3;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuxPage.xaml
    /// </summary>
    public partial class BuxPage : Page
    {

        public BuxPage(User user)
        {
            InitializeComponent();

            UserSp.DataContext = user;
        }

        private void UrBtn_Click(object sender, RoutedEventArgs e)
        {
            OrgBillWindow orgBillWindow = new OrgBillWindow();
            orgBillWindow.Show();
        }

        private void ClBtn_Click(object sender, RoutedEventArgs e)
        {
            BillClientWind billClientWind = new BillClientWind();
            billClientWind.Show();
        }
    }
}

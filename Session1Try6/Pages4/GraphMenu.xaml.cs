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

namespace Session1Try6.Pages4
{
    /// <summary>
    /// Логика взаимодействия для GraphMenu.xaml
    /// </summary>
    public partial class GraphMenu : Page
    {
        public GraphMenu()
        {
            InitializeComponent();
        }

        private void ServiceCountBtn_Click(object sender, RoutedEventArgs e)
        {
            GraphFrame.Content = new ServiceCountGraph();
        }

        private void ServiceListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientCountBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientServiceCountBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

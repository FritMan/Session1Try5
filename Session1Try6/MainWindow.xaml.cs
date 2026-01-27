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
using static Session1Try6.Classes.Helper;

namespace Session1Try6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GlobalTimer.Tick += GlobalTimer_Tick;

        }

        private void GlobalTimer_Tick(object sender, EventArgs e)
        {
            if(GlobalCount == 0)
            {
                MessageBox.Show("Осталась минута");
                GlobalCount++;
            }
            else
            {
                GlobalCount = 0;
                MainFrame.Content = new Pages.AuthPage();
                ExitFlag = true;
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Pages.AuthPage();
        }
    }
}

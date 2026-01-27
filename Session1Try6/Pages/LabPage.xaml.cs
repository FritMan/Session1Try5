using Session1Try6.Data;
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
    /// Логика взаимодействия для LabPage.xaml
    /// </summary>
    public partial class LabPage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
        private DateTime End = DateTime.Now.AddSeconds(121);
        public LabPage(User user)
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;
            UserSp.DataContext = user;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            var res = End - DateTime.Now;
            TimerLab.Content = res.ToString(@"mm\:ss");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalTimer.Start();
            timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            GlobalTimer.Stop();
            timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages2.TakeWastePage());
        }
    }
}

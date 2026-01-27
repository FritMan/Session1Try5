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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int count = 0;
        private bool flag = false;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer BlockTimer = new DispatcherTimer();
        public AuthPage()
        {
            InitializeComponent();
            Generate();
        }

        private void Generate()
        {
            Random random = new Random();
            int x = random.Next(0, 300);
            Canvas.SetLeft(InImg, x);
            SliderCaptch.Value = x;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = Db.User.FirstOrDefault(el => el.Password == PwdPb.Password && el.Login == LoginTb.Text);
            if (count == 0)
            {
                if (user != null)
                {
                    if (user.RoleId == 1)
                    {
                        NavigationService.Navigate(new AdminPage(user));
                    }
                    else if (user.RoleId == 2)
                    {
                        NavigationService.Navigate(new BuxPage(user));
                    }
                    else if (user.RoleId == 3)
                    {
                        NavigationService.Navigate(new LabPage(user));
                    }
                    else if (user.RoleId == 4)
                    {
                        NavigationService.Navigate(new LabResPage(user));
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    CaptchaSp.Visibility = Visibility.Visible;
                    count++;
                }
            }
            else
            {
                if (user != null && flag == true)
                {
                    if (user.RoleId == 1)
                    {
                        NavigationService.Navigate(new AdminPage(user));
                    }
                    else if (user.RoleId == 2)
                    {
                        NavigationService.Navigate(new BuxPage(user));
                    }
                    else if (user.RoleId == 3)
                    {
                        NavigationService.Navigate(new LabPage(user));
                    }
                    else if (user.RoleId == 4)
                    {
                        NavigationService.Navigate(new LabResPage(user));
                    }

                    count = 0;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    dispatcherTimer.Interval = TimeSpan.FromSeconds(10);
                    dispatcherTimer.Tick += DispatcherTimer_Tick;
                    dispatcherTimer.Start();
                    OkBtn.IsEnabled = false;
                }
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OkBtn.IsEnabled = true;
            dispatcherTimer.Stop();
        }

        private void SliderCaptch_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Canvas.SetLeft(InImg, SliderCaptch.Value);

            if(SliderCaptch.Value >= 525 && SliderCaptch.Value <= 555)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(ExitFlag == true)
            {
                OkBtn.IsEnabled = false;
                ExitFlag = true;
                BlockTimer.Interval = TimeSpan.FromSeconds(60);
                BlockTimer.Tick += BlockTimer_Tick;
                BlockTimer.Start();
            }
        }

        private void BlockTimer_Tick(object sender, EventArgs e)
        {
            OkBtn.IsEnabled = true;
            BlockTimer.Stop();
        }
    }
}

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
using static Session1Try6.Classes.Helper;

namespace Session1Try6.Pages3
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            EnterDg.ItemsSource = Db.EnterHistory.ToList();
        }

        private void FilterTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTb.Text))
            {
                LoadData();
            }
            else if(string.IsNullOrEmpty(SortTb.Text))
            {
                EnterDg.ItemsSource = Db.EnterHistory.Where(el => el.User.Login.Contains(FilterTb.Text)).ToList();
            }
            else
            {
              var list = Db.EnterHistory.Where(el => el.User.Login.Contains(FilterTb.Text)).ToList();
                var Sortedlist = new List<EnterHistory>();
                foreach (var el in list)
                {
                    if (el.DateTimeEnter.Date.ToString().Contains(SortTb.Text))
                    {
                        Sortedlist.Add(el);
                    }
                }

                EnterDg.ItemsSource = Sortedlist;
            }
        }

        private void SortTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(SortTb.Text))
            {
                LoadData();
            }
            else if (string.IsNullOrEmpty(FilterTb.Text))
            {
                var list = Db.EnterHistory.ToList();
                var Sortedlist = new List<EnterHistory>();
                foreach (var el in list)
                {
                    if (el.DateTimeEnter.Date.ToString().Contains(SortTb.Text))
                    {
                        Sortedlist.Add(el);
                    }
                }

                EnterDg.ItemsSource = Sortedlist;
            }
            else
            {
                var list = Db.EnterHistory.Where(el => el.User.Login.Contains(FilterTb.Text)).ToList();
                var Sortedlist = new List<EnterHistory>();
                foreach (var el in list)
                {
                    if (el.DateTimeEnter.Date.ToString() == SortTb.Text)
                    {
                        Sortedlist.Add(el);
                    }
                }

                EnterDg.ItemsSource = Sortedlist;
            }
        }

        private void RebutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            SortTb.Text = null;
            FilterTb.Text = null;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

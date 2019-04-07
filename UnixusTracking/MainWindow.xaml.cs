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
using UnixusTracking.Helper.Unixus;
using UnixusTracking.Helper.Unixus.Models;

namespace UnixusTracking
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            error.Visibility = Visibility.Collapsed;
            hawbNumber.Text = "";
            hawbNumber.Focus();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchHAWBStatus();
        }

        private void SearchHAWBStatus()
        {
            var hawbNum = hawbNumber.Text;

            if (!string.IsNullOrEmpty(hawbNum))
            {
                error.Visibility = Visibility.Collapsed;
                progressBar.Visibility = Visibility.Visible;
                try
                {
                    Task.Factory.StartNew(() =>
                    {
                        var (result, err) = UnixusHelper.Instance.GetPackageStatus(hawbNum);
                        Dispatcher.Invoke(() => HandleTrackingResult(result?.ToList(), err));
                    });
                }
                catch (Exception ex)
                {
                    progressBar.Visibility = Visibility.Collapsed;
                    ShowError(ex.Message);
                }
            }
            else
            {
                error.Visibility = Visibility.Visible;
                error.Content = "Please insert HAWB number";
            }
        }

        private void HandleTrackingResult(List<EventListModel> result, string err)
        {
            progressBar.Visibility = Visibility.Collapsed;
            if (result != null && result.Any())
            {
                dataGrid.ItemsSource = result[0].Events.ToList();
            }
            else
            {
                ShowError(err);
            }
        }

        private void ShowError(string errorMessage)
        {
            error.Visibility = Visibility.Visible;
            error.Content = errorMessage;
        }

        private void ToggleLabelHint(object sender, RoutedEventArgs e)
        {
            hint.Visibility = hawbNumber.IsFocused ? Visibility.Collapsed : 
                              !string.IsNullOrEmpty(hawbNumber.Text) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ViewMap(object sender, RoutedEventArgs e)
        {
            string location = ((Button)sender).Tag.ToString();
            MapWindow win2 = new MapWindow();
            win2.SetMapUrl(location);
            win2.Show();
        }

        private void EnterDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchHAWBStatus();
            }
        }
    }
}

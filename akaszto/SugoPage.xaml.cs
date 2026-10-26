using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace akaszto
{
    /// <summary>
    /// Interaction logic for SugoPage.xaml
    /// </summary>
    public partial class SugoPage : Page
    {
        public SugoPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var main = (MainWindow)Application.Current.MainWindow;
            main.mainFrame.Content = null;
            main.mainFrame.Visibility = Visibility.Collapsed;
        }
    }
}

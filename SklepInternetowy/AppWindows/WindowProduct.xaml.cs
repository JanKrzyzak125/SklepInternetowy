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
using System.Windows.Shapes;

namespace SklepInternetowy
{
    /// <summary>
    /// Logika interakcji dla klasy WindowProduct.xaml
    /// </summary>
    public partial class WindowProduct : Window
    {
        private WindowPay _windowPay;
        public WindowProduct()
        {
            InitializeComponent();
        }

        private void Click_Pay(object sender, RoutedEventArgs e) 
        {
            _windowPay =new WindowPay();
            _windowPay.Show();
        }

        void ShowBuyerProduct() 
        {
            EditProduct.Visibility = Visibility.Hidden;
            _windowPay.Visibility = Visibility.Visible;
        }

        void ShowSellerProduct() 
        {
            EditProduct.Visibility = Visibility.Visible;
            _windowPay.Visibility = Visibility.Hidden;
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

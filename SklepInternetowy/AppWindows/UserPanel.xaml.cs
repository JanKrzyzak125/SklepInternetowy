﻿using SklepInternetowy.AppWindows;
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
    /// Logika interakcji dla klasy UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        private WindowProduct windowProducts;
        public UserPanel()
        {
            InitializeComponent();
        }

        private void ClickSellingProduct(object sender, RoutedEventArgs e) 
        {
            windowProducts = new WindowProduct();

        }
        private void ClickBuyedProducts(object sender, RoutedEventArgs e)
        {
            windowProducts = new WindowProduct();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            WindowProduct windowProduct = new WindowProduct();
            windowProduct.Show();
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow();
            newProductWindow.Show();

        }

        private void SelectUser_Click(object sender, RoutedEventArgs e) //Todo: do usunięcia gdy autoryzacja się pojawi
        {

        }
    }
}
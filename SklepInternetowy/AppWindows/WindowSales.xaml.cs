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

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy WindowSales.xaml
	/// </summary>
	public partial class WindowSales : Window
	{
		private object[] actualObject;
		private object[] actualSales;
		private UserPanel userPanel;
		private NewProductWindow windowNewProductWindow;
		public WindowSales()
		{
			InitializeComponent();
			userPanel = null;
			actualObject = null;
			actualSales = null;
			windowNewProductWindow = null;
		}

		public WindowSales(UserPanel windowUserPanel, object[] product)
		{
			InitializeComponent();
			userPanel = windowUserPanel;
			actualObject = product;
			actualSales = null;
			windowUserPanel.IsEnabled = false;
			windowNewProductWindow = new NewProductWindow();
		}

		public WindowSales(UserPanel windowUserPanel,object[] product, object[] sales)
		{
			InitializeComponent();
			userPanel = windowUserPanel;
			actualObject = product;
			actualSales = sales;
			windowUserPanel.IsEnabled = false;
			windowNewProductWindow = new NewProductWindow();
		}

		

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void ButtonProduct_Click(object sender, RoutedEventArgs e)
		{
			if(windowNewProductWindow.IsVisible == false) 
			{
				windowNewProductWindow = new NewProductWindow(actualObject);
				windowNewProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowNewProductWindow.Show();
				windowNewProductWindow.IsEnabled = false;
				windowNewProductWindow.Title = "Podgląd produktu";
				windowNewProductWindow.ButtonProduct.Visibility = Visibility.Hidden;
				windowNewProductWindow.ButtonAddImage.Visibility= Visibility.Hidden;
			}
		}
	}
}

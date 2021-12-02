using SklepInternetowy.Classes;
using System.Windows;
using System.Windows.Controls;
using SklepInternetowy.AppWindows;

namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy WindowProduct.xaml
	/// </summary>
	public partial class WindowProduct : Window
	{
		private WindowPay windowPay;
		private MainWindow mainWindow;
		private Product currentProduct;
		private NewProductWindow newProductWindow;
		public WindowProduct()
		{
			InitializeComponent();
		}

		public WindowProduct(MainWindow tempMainWindow, object[] tempProduct)
		{
			InitializeComponent();
			mainWindow = tempMainWindow;
			newProductWindow = new NewProductWindow();
			currentProduct = new Product(tempProduct);//TODO
		}



		private void Click_Pay(object sender, RoutedEventArgs e)
		{
			windowPay = new WindowPay();
			windowPay.Show();
		}

		void ShowBuyerProduct()
		{
			windowPay.Visibility = Visibility.Visible;
		}

		void ShowSellerProduct()
		{
			windowPay.Visibility = Visibility.Hidden;
		}

		private void ButtonMoreDetails_Click(object sender, RoutedEventArgs e)
		{
			if (newProductWindow.IsVisible == false)
			{
				newProductWindow = new NewProductWindow();
				newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				newProductWindow.Show();
			}
		}
	}
}

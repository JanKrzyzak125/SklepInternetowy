using System.Windows;

namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy WindowProduct.xaml
	/// </summary>
	public partial class WindowProduct : Window
	{
		private WindowPay windowPay;
		public WindowProduct()
		{
			InitializeComponent();
		}

		private void Click_Pay(object sender, RoutedEventArgs e)
		{
			windowPay = new WindowPay();
			windowPay.Show();
		}

		void ShowBuyerProduct()
		{
			EditProduct.Visibility = Visibility.Hidden;
			windowPay.Visibility = Visibility.Visible;
		}

		void ShowSellerProduct()
		{
			EditProduct.Visibility = Visibility.Visible;
			windowPay.Visibility = Visibility.Hidden;
		}

		private void EditProduct_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ChangePhoto_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}

using SklepInternetowy.Classes;
using System.Windows;
using System.Windows.Controls;
using SklepInternetowy.AppWindows;
using System;
using System.Collections.Generic;

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
		private List<int> tempListQuantity;
		private SQLConnect sqlConnect;
		public WindowProduct()
		{
			InitializeComponent();
		}

		public WindowProduct(MainWindow tempMainWindow, object[] tempProduct)
		{
			InitializeComponent();
			windowPay = new WindowPay();
			sqlConnect = new SQLConnect();
			tempListQuantity = new List<int>();
			ComboBoxAvailableQuantity.ItemsSource = tempListQuantity;
			this.Title = "Sprzedaż";
			mainWindow = tempMainWindow;
			newProductWindow = new NewProductWindow();
			currentProduct = new Product(tempProduct);
			fillComboBox();
			DateStart.Text = currentProduct.DateStartSales.ToShortDateString();
			DateStart.IsEnabled = false;
			DateEnding.Text = currentProduct.DateClosing.ToShortDateString();
			DateEnding.IsEnabled = false;
			TextBoxCategory.Text = currentProduct.NameCategory;
			TextBoxCategory.IsEnabled = false;
			TextBoxBrand.Text = currentProduct.NameBrand;
			TextBoxBrand.IsEnabled = false;
			TextBoxNameProduct.Text = currentProduct.Name;
			TextBoxNameProduct.IsEnabled = false;
			TextBoxDescription.Text = currentProduct.Description;
			TextBoxDescription.IsEnabled = false;
			TextBoxVat.Text = currentProduct.Vat_rate + "%";
			TextBoxVat.IsEnabled = false;
			TextBoxVisitors.Text = currentProduct.Visitors.ToString();
			TextBoxVisitors.IsEnabled = false;

			byte[] tempImageData = currentProduct.Image;
			ImageProduct.Source = NewProductWindow.ConvertByteToImage(tempImageData);

			decimal tempPrice = currentProduct.Price;
			tempPrice = tempPrice * (currentProduct.Vat_rate / 100) + tempPrice;
			TextBoxPrice.Text = tempPrice.ToString();
			TextBoxPrice.IsEnabled = false;
			if (Users.LogUser == null)
			{
				ButtonPay.IsEnabled = false;
				ButtonPay.ToolTip = "Zaloguj się by kupić produkt";
			}
			else if (Users.LogUser.Id_User == currentProduct.Id_Seller)
			{
				ButtonPay.IsEnabled = false;
				ButtonPay.ToolTip = "Jesteś sprzedającym więc nie możesz kupić";
			}
			else
			{
				ButtonPay.IsEnabled = true;
				ButtonPay.Content = "Kup produkt";
			}
			DateTime temp = new DateTime();
			if (currentProduct.DateClosed != temp)
			{
				LabelEnd.Visibility = Visibility.Visible;
				DateEnd.Visibility = Visibility.Visible;
				DateEnd.Text = currentProduct.DateClosed.ToShortDateString();
				DateEnd.IsEnabled = false;
				ButtonPay.IsEnabled = false;
				ComboBoxAvailableQuantity.IsEnabled = false;
				ButtonPay.ToolTip = "Zakończona sprzedaż";

			}
		}

		private void fillComboBox()
		{
			int tempIdProduct = currentProduct.Id_Product;
			int selledQuantity = sqlConnect.AvalilableProducts(tempIdProduct, "AvalilableProducts");
			int currentAvalilableQuantity = currentProduct.Quantity - selledQuantity;

			for (int i = 1; i <= currentAvalilableQuantity; i++)
			{
				tempListQuantity.Add(i);
			}

		}

		private void Click_Pay(object sender, RoutedEventArgs e)
		{
			if (windowPay.IsVisible == false)
			{
				windowPay = new WindowPay();
				windowPay.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowPay.Show();
			}
		}

		private void ButtonMoreDetails_Click(object sender, RoutedEventArgs e)
		{
			if (newProductWindow.IsVisible == false)
			{
				newProductWindow = new NewProductWindow(currentProduct);
				newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				newProductWindow.Show();
			}
		}


		private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			decimal tempPrice, tempSum;
			decimal.TryParse(TextBoxPrice.Text, out tempPrice);
			int tempCurrentyQuantity;
			int.TryParse(ComboBoxAvailableQuantity.SelectedValue.ToString(), out tempCurrentyQuantity);
			tempSum = tempPrice * tempCurrentyQuantity;
			TextBoxSumPay.Text = tempSum.ToString();
		}
	}
}

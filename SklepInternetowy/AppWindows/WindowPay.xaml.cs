using SklepInternetowy.AppWindows;
using SklepInternetowy.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy WindowPay.xaml
	/// </summary>
	public partial class WindowPay : Window
	{
		private Product currentProduct;
		private int currentSelectQuantity;
		private List<object[]> tempListPayment;
		private List<string> tempListNamePayment;
		private List<string> tempListNumberPayment;
		private SQLConnect sqlConnect;
		private WindowPayment windowPayment;

		public WindowPay()
		{

		}

		public WindowPay(Product product, int valueSelectQuantity)
		{
			InitializeComponent();
			windowPayment = new WindowPayment();
			sqlConnect = new SQLConnect();
			this.Title = "Kupowanie produktu";
			currentProduct = product;
			TextBoxName.Text = product.Name;
			DateTime today = DateTime.Today;
			TimeSpan duration = new TimeSpan(product.DayDelivery, 0, 0, 0);
			DateTime tempDateDelivery = today.Add(duration);
			DateDelivery.Text = tempDateDelivery.ToShortDateString();
			TextBoxQuantity.Text = valueSelectQuantity.ToString();
			currentSelectQuantity = valueSelectQuantity;
			makeVat();
			makeComboBox();
			currentSelectQuantity = valueSelectQuantity;

		}

		private void makeComboBox() 
		{
			tempListPayment = new List<object[]>();
			tempListNamePayment = new List<string>();
			tempListNumberPayment = new List<string>();

			foreach (DataRow row in sqlConnect.ReadTable("ValuesTypePayment").Rows)
			{
				if ((int)row[2] == 1) 
				{
					tempListNamePayment.Add(row.ItemArray[0].ToString());
				}
			}

			int tempId = Users.LogUser.Id_User;
			foreach (DataRow row in sqlConnect.ShowProduct(tempId, "ViewUserPayment").Rows)
			{
				if ((int)row[5] == 1)
				{
					tempListPayment.Add(row.ItemArray);

				}
			}

			ComboBoxPayment.ItemsSource = tempListNamePayment;
			ComboBoxPayment.SelectedItem = null;

		}

		private void makeVat()
		{
			decimal tempPrice = currentProduct.Price;
			tempPrice = Math.Round(tempPrice, 2);
			TextBoxNetto.Text = tempPrice.ToString();
			decimal tempVat = (currentProduct.Vat_rate * (decimal)0.01) + 1;
			decimal tempBrutto = tempPrice * tempVat * currentSelectQuantity;
			tempBrutto = Math.Round(tempBrutto, 2);
			TextBoxBrutto.Text = tempBrutto.ToString();
		}

		private void ButtonBuy_Click(object sender, RoutedEventArgs e)
		{
			

			
		}


		private void ButtonChangeAddPayment_Click(object sender, RoutedEventArgs e)
		{
			if (!windowPayment.IsVisible) 
			{
				int tempId = Users.LogUser.Id_User;
				windowPayment = new WindowPayment(tempId);
				windowPayment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowPayment.ShowDialog();
				makeComboBox();
			}
		}

		private void ButtonChangePayment_Click(object sender, RoutedEventArgs e)
		{
			if (!windowPayment.IsVisible)
			{
				string tempText = ComboBoxPayment.Text;
				string tempNumber = ComboBoxNumberPayment.Text;
				object[] actualPayment = null;
				for (int i = 0; i < tempListPayment.Count; i++)
				{
					if (tempListPayment[i][6].Equals(tempText) && tempListPayment[i][3].Equals(tempNumber))
					{
						actualPayment = tempListPayment[i];
					}
				}

				if (actualPayment != null)
				{
					windowPayment = new WindowPayment(actualPayment);
					windowPayment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
					windowPayment.ShowDialog();
					makeComboBox();
				}

			}
		}


		private void CheckBoxNewPayment_Checked(object sender, RoutedEventArgs e)
		{
			ButtonChangePayment.Content = "Dodaj";
			ButtonChangePayment.Click -= ButtonChangePayment_Click;
			ButtonChangePayment.Click += ButtonChangeAddPayment_Click;
		}

		private void CheckBoxNewPayment_Unchecked(object sender, RoutedEventArgs e)
		{
			ButtonChangePayment.Content = "Zmień";
			ButtonChangePayment.Click -= ButtonChangeAddPayment_Click;
			ButtonChangePayment.Click += ButtonChangePayment_Click;
		}

		private void ComboBoxPayment_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ComboBox cmb = sender as ComboBox;
			tempListNumberPayment = new List<string>();
			if (e.AddedItems.Count!=0)
			{
				string tempText = e.AddedItems[0].ToString();

				for (int i = 0; i < tempListPayment.Count; i++)
				{
					string tempName = tempListPayment[i][6].ToString();
					if (tempText.Equals(tempName)) 
					{
						tempListNumberPayment.Add(tempListPayment[i][3].ToString());
					}
				}
				ComboBoxNumberPayment.ItemsSource = tempListNumberPayment;
			}
		}

		
	}
}

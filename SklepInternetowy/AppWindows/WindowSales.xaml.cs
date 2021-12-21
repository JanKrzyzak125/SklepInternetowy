using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
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

		private SQLConnect sqlConnect;
		private UserPanel userPanel;
		private NewProductWindow windowNewProductWindow;

		private List<string> tempListDelivery;
		private List<int> tempListQuantity;

		public WindowSales()
		{
			InitializeComponent();
			userPanel = null;
			actualObject = null;
			actualSales = null;
			windowNewProductWindow = null;

		}
		/// <summary>
		/// Constructor for make sales
		/// </summary>
		/// <param name="windowUserPanel"></param>
		/// <param name="product"></param>
		public WindowSales(UserPanel windowUserPanel, object[] product)
		{
			InitializeComponent();

			sqlConnect = new SQLConnect();
			userPanel = windowUserPanel;
			actualObject = product;
			actualSales = null;
			windowUserPanel.IsEnabled = false;
			windowNewProductWindow = new NewProductWindow();
			makeList();
			fillingDate();
			DateTime thisDay = DateTime.Today;
			DateEnding.DataContext = thisDay.Date.ToShortDateString();
			LabelVisitors.Visibility = Visibility.Hidden;
			LabelStatus.Visibility = Visibility.Hidden;
			CheckBoxStatus.Visibility = Visibility.Hidden;
			DateEnd.Visibility = Visibility.Hidden;
			LabelDateEnd.Visibility = Visibility.Hidden;
			TextBoxViews.Visibility = Visibility.Hidden;
			ButtonAccept.Click -= ButtonAcceptEdit_Click;
			ButtonAccept.Click += ButtonAcceptNew_Click;
			this.Title = "Dodaj sprzedaż";
		}

		/// <summary>
		/// Constructor for edit sales
		/// </summary>
		/// <param name="windowUserPanel"></param>
		/// <param name="product"></param>
		/// <param name="sales"></param>
		public WindowSales(UserPanel windowUserPanel, object[] product, object[] sales)
		{
			InitializeComponent();

			sqlConnect = new SQLConnect();
			userPanel = windowUserPanel;
			actualObject = product;
			actualSales = sales;
			windowUserPanel.IsEnabled = false;
			windowNewProductWindow = new NewProductWindow();
			makeList();
			fillingDate2();

			ComboBoxQuantity.Text = actualSales[1].ToString();
			ComboBoxQuantity.IsEnabled = false;

			DateTime temp=new DateTime();
			temp =(DateTime) actualSales[2];
			DateStart.Text = temp.ToShortDateString();
			DateStart.IsEnabled = false;

			temp = (DateTime)actualSales[3];
			DateEnding.Text = temp.ToShortDateString();
			DateEnding.IsEnabled = false;

			TextBoxDaysReturn.Text = actualSales[5].ToString();
			ComboBoxDelivery.Text = actualSales[6].ToString();
			TextBoxDaysDelivery.Text = actualSales[7].ToString();
			TextBoxViews.Text = actualSales[8].ToString();
			TextBoxViews.Visibility = Visibility.Visible;

			DateTime fristDate=new DateTime();
			if ((DateTime)actualSales[4] != fristDate.Date)
			{
				temp = (DateTime)actualSales[4];
				DateEnd.Text = temp.ToShortDateString();
				DateEnd.Visibility = Visibility.Visible;
				LabelDateEnd.Visibility = Visibility.Visible;
				TextBoxDaysReturn.IsEnabled = false;
				ComboBoxDelivery.IsEnabled = false;
				TextBoxDaysDelivery.IsEnabled = false;
				CheckBoxStatus.IsChecked = true;
				CheckBoxStatus.IsEnabled = false;
				this.Title = "Pogląd zakończonej sprzedaży";
				ButtonAccept.IsEnabled = false;
				ButtonAccept.ToolTip = "Nie mozna zaakceptować bo sprzedaż zakończona";
			}
			else 
			{
				DateEnd.Visibility = Visibility.Hidden;
				LabelDateEnd.Visibility = Visibility.Hidden;
				this.Title = "Edytuj sprzedaż";
				ButtonAccept.Click -= ButtonAcceptNew_Click;
				ButtonAccept.Click += ButtonAcceptEdit_Click;
			}
			

		}

		private void makeList()
		{
			tempListDelivery = new List<string>();
			tempListQuantity = new List<int>();

			ComboBoxDelivery.ItemsSource = tempListDelivery;
			ComboBoxQuantity.ItemsSource = tempListQuantity;
		}

		private void fillingDate()
		{
			foreach (DataRow row in sqlConnect.ReadTable("ValuesDelivery").Rows)
			{
				if ((int)row[1] == 1) tempListDelivery.Add(row[0].ToString());
			}

			int length = sqlConnect.ReadQuantity((int)actualObject[0], "ReadQuantity"); //TODO: do sprawdzenia

			for (int i = 0; i <= length; i++)
			{
				tempListQuantity.Add(i);
			}

		}

		private void fillingDate2()
		{
			foreach (DataRow row in sqlConnect.ReadTable("ValuesDelivery").Rows)
			{
				if ((int)row[1] == 1) tempListDelivery.Add(row[0].ToString());
			}

			tempListQuantity.Add((int)actualSales[1] - 1);
			tempListQuantity.Add((int)actualSales[1]);
		}

		//TODO
		private void ButtonAcceptEdit_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			int tempQuantity;

			if (!test(ComboBoxQuantity)) codeFailed++;
			int.TryParse(ComboBoxQuantity.Text, out tempQuantity);

			DateTime tempDateStartSales = (DateTime)DateStart.SelectedDate;

			int tempDaysReturn;
			if (!test(TextBoxDaysReturn)) codeFailed++;
			int.TryParse(TextBoxDaysReturn.Text, out tempDaysReturn);

			DateTime tempDateClosingSales = (DateTime)DateEnding.SelectedDate;

			int tempDaysDelivery;
			if (!test(TextBoxDaysDelivery)) codeFailed++;
			int.TryParse(TextBoxDaysDelivery.Text, out tempDaysDelivery);

			string tempNameDelivery;
			if (!test(ComboBoxDelivery)) codeFailed++;
			tempNameDelivery = ComboBoxDelivery.Text;


			if (codeFailed == 0)
			{
				int tempId = (int)actualObject[0];
				//sqlConnect.UpdateRetailSales(tempId, tempQuantity, tempDateStartSales, tempDateClosingSales,
				//						  tempDaysReturn, tempDaysDelivery, tempNameDelivery, "AddRetailSales");
				MessageBox.Show("Udało się zaktualizować sprzedaż");
				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło=" + codeFailed + " tyle błędów w formularzu");
			}
		}

		private void ButtonAcceptNew_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			int tempQuantity;

			if (!test(ComboBoxQuantity)) codeFailed++;
			int.TryParse(ComboBoxQuantity.Text, out tempQuantity);

			DateTime tempDateStartSales = (DateTime)DateStart.SelectedDate;

			int tempDaysReturn;
			if (!test(TextBoxDaysReturn)) codeFailed++;
			int.TryParse(TextBoxDaysReturn.Text, out tempDaysReturn);

			DateTime tempDateClosingSales = (DateTime)DateEnding.SelectedDate;

			int tempDaysDelivery;
			if (!test(TextBoxDaysDelivery)) codeFailed++;
			int.TryParse(TextBoxDaysDelivery.Text, out tempDaysDelivery);

			string tempNameDelivery;
			if (!test(ComboBoxDelivery)) codeFailed++;
			tempNameDelivery = ComboBoxDelivery.Text;


			if (codeFailed == 0)
			{
				int tempId = (int)actualObject[0];
				sqlConnect.AddRetailSales(tempId, tempQuantity, tempDateStartSales, tempDateClosingSales,
										  tempDaysReturn, tempDaysDelivery, tempNameDelivery, "AddRetailSales");
				MessageBox.Show("Udało się dodać sprzedaż");
				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło=" + codeFailed + " tyle błędów w formularzu");
			}

		}


		private bool test(object tempObject)
		{
			if (tempObject is ComboBox)
			{
				ComboBox tempComboBox;
				tempComboBox = tempObject as ComboBox;
				if (tempComboBox.Text == "")
				{
					tempComboBox.Background = System.Windows.Media.Brushes.Red;
					MessageBox.Show("Brak wybranego pola " + nameBox(tempComboBox.Name));
					return false;
				}
				else
				{
					tempComboBox.Background = System.Windows.Media.Brushes.Green;
					return true;
				}
			}
			else
			if (tempObject is TextBox)
			{
				TextBox tempTextBox;
				tempTextBox = tempObject as TextBox;
				if (tempTextBox.Text == "")
				{
					tempTextBox.Background = System.Windows.Media.Brushes.Red;
					MessageBox.Show("Brak wybranego pola " + nameBox(tempTextBox.Name));
					return false;
				}
				else
				{
					tempTextBox.Background = System.Windows.Media.Brushes.Green;
					return true;
				}
			}
			return false;

		}

		private string nameBox(string temp)
		{
			switch (temp)
			{
				case "ComboBoxQuantity":
					return "ilości produktów";
				case "ComboBoxDelivery":
					return "dostawy";
				case "TextBoxDaysReturn":
					return "dni zwrotu";
				case "TextBoxDaysDelivery":
					return "dni dostarczenia produktu";
				default:
					return "";
			}
		}

		private void OnlyNumeric(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+,");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void ButtonProduct_Click(object sender, RoutedEventArgs e)
		{
			if (windowNewProductWindow.IsVisible == false)
			{
				windowNewProductWindow = new NewProductWindow(actualObject);
				windowNewProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowNewProductWindow.Show();
				windowNewProductWindow.IsEnabled = false;
				windowNewProductWindow.Title = "Podgląd produktu";
				windowNewProductWindow.ButtonProduct.Visibility = Visibility.Hidden;
				windowNewProductWindow.ButtonAddImage.Visibility = Visibility.Hidden;
			}
		}

		private void WindowSales_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (userPanel != null) userPanel.IsEnabled = true;
		}
	}
}

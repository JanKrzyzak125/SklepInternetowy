using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SklepInternetowy.AppWindows;

namespace SklepInternetowy
{
	/// <summary>
	/// MainWindow 
	/// </summary>
	public partial class MainWindow : Window
	{
		private Authentication windowAuthentication;
		private SQLConnect sqlConnect;
		private UserPanel userPanel;
		private AdminPanel adminPanel;
		private WindowProduct windowProduct;
		private List<string> templistName;

		/// <summary>
		/// 
		/// </summary>

		public MainWindow()
		{
			InitializeComponent();
			startApp();
			ButtonLog.Visibility = Visibility.Visible;
			ButtonAdmin.Visibility = Visibility.Hidden;
			ButtonUser.Visibility = Visibility.Hidden;
			
		}

		private void startApp() 
		{
			templistName = new List<string>();
			ComboBoxName.ItemsSource = templistName;
			sqlConnect = new SQLConnect();
			sqlConnect.Refresh(DateTime.Today, "StartApp");
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			windowAuthentication = new Authentication(this);
			userPanel = new UserPanel();
			adminPanel = new AdminPanel();
			windowProduct = new WindowProduct();
			DataTable tempSales = sqlConnect.ReadTable("ViewMainSales");
			MainGrid.ItemsSource = tempSales.DefaultView;
		}


		/// <summary>
		/// Open Window Authentication
		/// </summary>
		/// 
		/// <returns>void</returns>
		private void Log_Open(object sender, RoutedEventArgs e)
		{
			if (windowAuthentication.IsVisible == false)
			{
				windowAuthentication = new Authentication(this);
				windowAuthentication.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowAuthentication.Show();
			}
		}


		private void Log_Close(object sender, RoutedEventArgs e)
		{
			MenuItemLoginOut.IsEnabled = false;
			MenuItemLogin.IsEnabled = true;
			Users.LogUser = null;
			ButtonUser.Visibility = Visibility.Hidden;
			ButtonAdmin.Visibility = Visibility.Hidden;
			ButtonLog.Visibility = Visibility.Visible;

		}

		/// <summary>
		/// Search products by name 
		/// </summary>
		private void Search_Click(object sender, RoutedEventArgs e)
		{
			if (!TextSearch.Text.Equals("") && ComboBoxName.SelectedItem!=null)
			{
				string tempTextSearch = TextSearch.Text;
				string tempName = ComboBoxName.Text;
				DataRow tempDataRow = (DataRow)MainGrid.ItemsSource;
			}
			else 
			{
				if (TextSearch.Text.Equals(""))
				{
					TextSearch.Background = System.Windows.Media.Brushes.Yellow;
				}
				else 
				{
					ComboBoxName.Background = System.Windows.Media.Brushes.Yellow;
				}
				MessageBox.Show("Proszę o wybranie rodzaju oraz nazwy by wyszukać");
			}
			

		}

		private void CloseApp(object sender, RoutedEventArgs e)
		{
			this.Close();
			Application.Current.Shutdown();
		}

		private void Admin_Open(object sender, RoutedEventArgs e)
		{
			if (adminPanel.IsVisible == false)
			{
				adminPanel = new AdminPanel();
				adminPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				adminPanel.Show();
			}
		}

		private void MenuUser_Click(object sender, RoutedEventArgs e)
		{
			if (userPanel.IsVisible == false)
			{
				userPanel = new UserPanel(this);
				userPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				userPanel.Show();
			}
		}


		private void CloseApp(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void mainDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Id_User":
				case "Id_Payment":
				case "Id_TypePayment":
				case "MaxQuantity":
				case "Id_Product":
				case "Image":
				case "StatusProduct":
				case "Status":
				case "StatusTypePayment":
				case "LimitString":
				case "Id_Seller":
				case "Id_RetailSales":
					e.Column.Visibility = Visibility.Hidden;
					break;
				case "Name":
					e.Column.Header = "Nazwa produktu";
					e.Column.HeaderStyle = (Style)Resources[2];
					e.Column.CellStyle= (Style)Resources[0];
					templistName.Add(e.Column.Header.ToString());
					e.Column.DisplayIndex = 0;
					break;
				case "Description":
					e.Column.Header = "Opis";
					templistName.Add(e.Column.Header.ToString());
					e.Column.DisplayIndex = 1;
					break;
				case "Price":
					e.Column.Header = "Cena bez Vatu";
					e.Column.DisplayIndex = 2;
					break;
				case "Vat_rate":
					e.Column.Header = "Procent Vat";
					e.Column.DisplayIndex = 3;
					break;
				case "NameCondition":
					e.Column.Header = "Stan produktu";
					e.Column.DisplayIndex = 4;
					break;
				case "NameCategory":
					e.Column.Header = "Kategoria";
					templistName.Add(e.Column.Header.ToString());
					e.Column.DisplayIndex = 5;
					break;
				case "NameBrand":
					e.Column.Header = "Marka";
					templistName.Add(e.Column.Header.ToString());
					e.Column.DisplayIndex = 6;
					break;

				case "Quantity":
					e.Column.Header = "Ilość wystawiona";
					e.Column.DisplayIndex = 7;
					break;
				case "NameParameter":
					e.Column.Header = "Nazwa parametru dodatkowego";
					e.Column.DisplayIndex = 8;
					break;
				case "Parameter":
					e.Column.Header = "Zawartość dodatkowa parametru";
					e.Column.DisplayIndex = 9;
					break;
				case "TypeWarranty":
					e.Column.Header = "Typ gwarancji";
					e.Column.DisplayIndex = 10;
					break;
				case "WarrantyDays":
					e.Column.Header = "Dni Gwarancji";
					e.Column.DisplayIndex = 11;
					break;
				
				
				case "DateStartSales":
					e.Column.Header = "Data startu sprzedaży";
					e.Column.DisplayIndex = 12;
					break;
				case "DateClosing":
					e.Column.Header = "Data planowa zakończenia";
					e.Column.DisplayIndex = 13;
					break;
				case "DateClosed":
					e.Column.Header = "Data zakończenia";

					int temp= this.Resources.Keys.Count;
					e.Column.HeaderStyle = (Style)Resources[0];


					e.Column.DisplayIndex = 14;
					break;
				case "NameDelivery":
					e.Column.Header = "Sposób Dostawy";
					e.Column.DisplayIndex = 15;
					break;
				case "DayDelivery":
					e.Column.Header = "Dni dostawy";
					e.Column.DisplayIndex = 16;
					break;
				case "DayReturn":
					e.Column.Header = "Czas na zwrot";
					e.Column.DisplayIndex = 17;
					break;
				case "Visitors":
					e.Column.Header = "Ilość odwiedzin";
					e.Column.DisplayIndex = 18;
					break;

			}
		}

		private void MainGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (MainGrid.SelectedItem != null && windowProduct.IsVisible == false)
			{
				DataRowView tempDataRow = MainGrid.SelectedItem as DataRowView;
				object[] tempObject = tempDataRow.Row.ItemArray;
				if (Users.LogUser != null)
				{
					int tempIdUser = Users.LogUser.Id_User;
					int tempIdProduct = (int)tempObject[10];
					sqlConnect.AddVisitor(tempIdUser, tempIdProduct, "AddVisitor");
				}
				windowProduct = new WindowProduct(this, tempObject);
				windowProduct.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowProduct.ShowDialog();
			}
		}

		private BitmapImage ConvertByteToImage(byte[] imageByteArray)
		{
			BitmapImage img = new BitmapImage();
			using (MemoryStream memStream = new MemoryStream(imageByteArray))
			{
				img.BeginInit();
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.StreamSource = memStream;
				img.EndInit();
				img.Freeze();
			}
			return img;
		}

		private void MainGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (MainGrid.SelectedItem != null)
			{
				DataRowView tempDataRow = MainGrid.SelectedItem as DataRowView;
				object[] tempObject = tempDataRow.Row.ItemArray;
				byte[] tempImage = (byte[])tempObject[24];
				ImageProduct.Source = ConvertByteToImage(tempImage);
			}
		}

		private void TextBoxIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;

			if (temp.Text.Equals("Wyszukaj"))
			{
				temp.Text = "";
			}
		}

	}
}

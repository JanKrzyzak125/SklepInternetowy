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
		private DataTable tempSales;

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
			tempSales = sqlConnect.ReadTable("ViewMainSales");


			for (int i = 0; i < tempSales.Rows.Count; i++)
			{
				object[] tempObject = tempSales.Rows[i].ItemArray;
				decimal tempNumber = Math.Round((decimal)tempSales.Rows[i].ItemArray[14], 2);
				tempObject[14] = tempNumber;
				tempSales.Rows[i].ItemArray = tempObject;
			}
			MainGrid.ItemsSource = tempSales.DefaultView;



			for (int i = 0; i < MainGrid.Columns.Count; i++)
			{
				string temp = MainGrid.Columns[i].Header.ToString();
				if (deleteNameColumn(temp))
				{
					templistName.Add(temp);

				}
			}

		}



		private bool deleteNameColumn(string column)
		{
			switch (column)
			{
				case "Nazwa produktu":
				case "Kategoria":
				case "Marka":
				case "Nazwa parametru":
				case "Typ gwarancji":
				case "Dostawa":
					return true;
				default:
					return false;
			}


		}

		private string nameColumnEng(string column)
		{
			return column switch
			{
				"Nazwa produktu" => "Name",
				"Kategoria" => "NameCondition",
				"Marka" => "NameBrand",
				"Nazwa parametru" => "NameParameter",
				"Typ gwarancji" => "TypeWarranty",
				"Dostawa" => "NameDelivery",
				_ => "",
			};
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
				windowAuthentication = new Authentication(this)
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
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
			if (!TextSearch.Equals("") && ComboBoxName.SelectedItem != null)
			{

			}
			else
			{
				ComboBoxName.Background = System.Windows.Media.Brushes.Yellow;
				MessageBox.Show("Proszę o wybranie rodzaju oraz nazwy by wyszukać", "Uwaga!", MessageBoxButton.OK);
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
				adminPanel = new AdminPanel
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				adminPanel.Show();
			}
		}

		private void MenuUser_Click(object sender, RoutedEventArgs e)
		{
			if (userPanel.IsVisible == false)
			{
				userPanel = new UserPanel(this)
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				userPanel.Show();
			}
		}


		private void CloseApp(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
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
				windowProduct = new WindowProduct(this, tempObject)
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
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

		private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;
			DataView tempDataView = MainGrid.ItemsSource as DataView;
			if (ComboBoxName != null)
				if (ComboBoxName.SelectedItem != null && temp.Text.Length > 2)
				{
					string tempSearch = ComboBoxName.Text;
					string tempNameColumn = nameColumnEng(tempSearch);
					tempDataView.RowFilter = tempNameColumn + " LIKE'" + temp.Text + "%'";
					MainGrid.RowBackground = System.Windows.Media.Brushes.Green;
				}
				else
				{
					MainGrid.RowBackground = null;
					tempDataView.RowFilter = null;
				}
		}
	}
}

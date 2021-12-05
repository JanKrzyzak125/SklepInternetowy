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
		private string textSearch;
		private Authentication windowAuthentication;
		private SQLConnect sqlConnect;
		private UserPanel userPanel;
		private AdminPanel adminPanel;
		private WindowProduct windowProduct;

		/// <summary>
		/// 
		/// </summary>

		public MainWindow()
		{
			InitializeComponent();
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			windowAuthentication = new Authentication(this);
			userPanel = new UserPanel();
			sqlConnect = new SQLConnect();
			adminPanel = new AdminPanel();
			windowProduct = new WindowProduct();
			ButtonLog.Visibility = Visibility.Visible;
			ButtonAdmin.Visibility = Visibility.Hidden;
			ButtonUser.Visibility = Visibility.Hidden;

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
			textSearch = TextSearch.Text;
			//TODO: zrobić wyszukiwanie produktów
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
				case "Id_Product":
				case "Image":
				case "StatusTypePayment":
				case "LimitString":
					e.Column.Visibility = Visibility.Hidden;
					break;
				case "Name":
					e.Column.Header = "Nazwa";
					break;
				case "Description":
					e.Column.Header = "Opis";
					break;
				case "Price":
					e.Column.Header = "Cena bez Vatu";
					break;
				case "Vat_rate":
					e.Column.Header = "Procent Vat";
					break;
				case "NameCondition":
					e.Column.Header = "Stan produktu";
					break;
				case "MaxQuantity":
					e.Column.Header = "Maks ilości";
					break;
				case "NameParameter":
					e.Column.Header = "Nazwa parametru dodatkowego";
					break;
				case "Parameter":
					e.Column.Header = "Zawartość dodatkowa parametru";
					break;
				case "TypeWarranty":
					e.Column.Header = "Typ gwarancji";
					break;
				case "WarrantyDays":
					e.Column.Header = "Dni Gwarancji";
					break;
				case "NameBrand":
					e.Column.Header = "Marka";
					break;
				case "NameCategory":
					e.Column.Header = "Kategoria";
					break;
				case "PaymentString":
					e.Column.Header = "Numer";
					break;
				case "NameBank":
					e.Column.Header = "Nazwa Banku";
					break;
				case "TypePayment":
					e.Column.Header = "Typ Płatności";
					break;

			}
		}

		private void MainGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (MainGrid.SelectedItem != null && windowProduct.IsVisible == false)
			{
				DataRowView tempDataRow = MainGrid.SelectedItem as DataRowView;
				object[] tempObject = tempDataRow.Row.ItemArray;
				windowProduct = new WindowProduct(this, tempObject);
				windowProduct.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowProduct.Show();
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
	}
}

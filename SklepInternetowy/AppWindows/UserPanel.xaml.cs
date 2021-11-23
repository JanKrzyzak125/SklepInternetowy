using SklepInternetowy.AppWindows;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy UserPanel.xaml
	/// </summary>
	public partial class UserPanel : Window
	{
		//private WindowProduct windowProducts;
		private List<string> tempListViews;
		private SQLConnect sqlConnect;
		private Registration windowEditProfile;
		private NewProductWindow newProductWindow;
		private RegistrationCompany windowRegistrationCompany;
		private int actualView;
		private WindowSales windowSales;
		private MainWindow windowMainWindow;


		public List<string> TempListViews
		{
			get => tempListViews;
		}

		public UserPanel() 
		{
			InitializeComponent();
		}

		public UserPanel(MainWindow WindowMainWindow)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			windowEditProfile = new Registration((Authentication)null);
			newProductWindow = new NewProductWindow();
			windowRegistrationCompany = new RegistrationCompany();
			windowSales = new WindowSales();
			AddListViews();
			if (Users.LogUser != null)
			{
				if (Users.LogUser.Company == null)
					ButtonCompany.Content = "Dodaj firmę";
				else
				{
					ButtonCompany.Content = "Edytuj firmę";
				}
			}
			actualView = -1;
			windowMainWindow = WindowMainWindow;
			windowMainWindow.IsEnabled = false;
		}

		public void CompanyClick(object sender, RoutedEventArgs e)
		{
			if (windowRegistrationCompany.IsVisible == false)
			{
				if (Users.LogUser.Company != null)
					windowRegistrationCompany = new RegistrationCompany(Users.LogUser.Company.Id_Company);
				else
				{
					windowRegistrationCompany = new RegistrationCompany();
				}
				windowRegistrationCompany.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowRegistrationCompany.Show();
			}
		}

		public void AddListViews()
		{
			tempListViews = new List<string>();
			ComboBoxViews.ItemsSource = tempListViews;

			tempListViews.Add("Widok zakupionych produktów");
			tempListViews.Add("Widok dodanych produktów");
			tempListViews.Add("Widok aktualnych sprzedaży");
			tempListViews.Add("Widok zakończonych sprzedaży");
			tempListViews.Add("Widok kupionych produktów");

		}

		//TODO w jaki sposób zmieniać nazwy taki czy w skrypcie?
		void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Name":
					e.Column.Header = "Nazwa";

					break;
				case "Description":
					e.Column.Header = "Opis";
					break;

			}
		}

		private void ClickChangeViews(object sender, RoutedEventArgs e)
		{
			if (ComboBoxViews.SelectedItem != null)
			{
				string tempViews = ComboBoxViews.SelectedItem.ToString();
				DataTable tempTable;
				int tempidUser = Users.LogUser.Id_User;
				switch (tempViews)
				{
					case "Widok zakupionych produktów":
						actualView = 1;
						break;
					case "Widok dodanych produktów":
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUsersProducts");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						actualView = 2;
						break;
					case "Widok aktualnych sprzedaży":
						actualView = 3;
						break;
					case "Widok zakończonych sprzedaży":
						actualView = 4;
						break;
					case "Widok kupionych produktów":
						actualView = 5;
						break;
					default:
						break;
				}
			}
			else 
			{
				MessageBox.Show("Proszę wybrać z listy widok");
			}
		}

		private void ClickChangeProfile(object sender, RoutedEventArgs e)
		{

			if (windowEditProfile.IsVisible == false)
			{
				windowEditProfile = new Registration();
				windowEditProfile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowEditProfile.Show();
			}
		}

		private void DoubleClickUsersDataGrid(object sender, RoutedEventArgs e)
		{
			switch (actualView)
			{
				case 2:
					if (newProductWindow.IsVisible == false)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						newProductWindow = new NewProductWindow(tempObject);
						newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
						newProductWindow.Show();
					}
					break;
			}

			//windowProducts = new WindowProduct();
		}

		private void NewProduct_Click(object sender, RoutedEventArgs e)
		{
			if (newProductWindow.IsVisible == false)
			{
				newProductWindow = new NewProductWindow();
				newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				newProductWindow.Show();
			}

		}

		private void ButtonAddRetailSales_Click(object sender, RoutedEventArgs e)
		{
			if (UsersDataGrid.SelectedItem != null && actualView == 2)
			{
				DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
				object[] tempObject = temp.Row.ItemArray as object[];
				if (windowSales.IsVisible == false)
				{
					windowSales = new WindowSales(this,tempObject);
					windowSales.WindowStartupLocation = WindowStartupLocation.CenterScreen;
					windowSales.Show();
				}
			}
			else
			{
				MessageBox.Show("Musi być wybrany produkt", "tralalala", MessageBoxButton.OK);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(windowMainWindow!=null) 
				windowMainWindow.IsEnabled = true;
		}
	}
}

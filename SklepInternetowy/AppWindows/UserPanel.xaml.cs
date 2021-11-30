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
		private WindowPayment windowPayment;


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
			windowPayment = new WindowPayment();
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
			tempListViews.Add("Widok sprzedaży");
			tempListViews.Add("Widok form płatności");

		}

		/// <summary>
		/// Methods, which rename columns and hidden 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
					//case "Status":
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
						//tempTable = sqlConnect.ShowProduct(tempidUser, "");
						//UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ComboBoxListBuyed.Visibility = Visibility.Visible;
						ButtonViewListBuyed.Visibility = Visibility.Visible;
						
						ButtonAdd.Click -= AddRetailSales_Click;
						ButtonAdd.Click -= AddPayment_Click;
						ButtonAdd.Click -= AddProduct_Click;
						ButtonAdd.Click += AddInvoice_Click;
						ButtonAdd.Content = "Dodaj Fakturę";
						ButtonAdd.ToolTip = "Musisz wybrać sprzedaż do jakiej chcesz dodać fakturę";
						break;
					case "Widok dodanych produktów":
						actualView = 2;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUsersProducts");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ComboBoxListBuyed.Visibility = Visibility.Hidden;
						ButtonViewListBuyed.Visibility = Visibility.Hidden;
						ButtonAdd.Click -= AddRetailSales_Click;
						ButtonAdd.Click -= AddPayment_Click;
						ButtonAdd.Click -= AddInvoice_Click;
						ButtonAdd.Click += AddProduct_Click;
						ButtonAdd.Content = "Dodaj Produkt";
						ButtonAdd.ToolTip = "Dodaje Produkt";
						break;
					case "Widok sprzedaży":
						actualView = 3;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserSalers");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ComboBoxListBuyed.Visibility = Visibility.Hidden;
						ButtonViewListBuyed.Visibility = Visibility.Hidden;
						ButtonAdd.Click -= AddProduct_Click;
						ButtonAdd.Click -= AddPayment_Click;
						ButtonAdd.Click -= AddInvoice_Click;
						ButtonAdd.Click += AddRetailSales_Click;
						ButtonAdd.Content = "Dodaj Sprzedaż";
						ButtonAdd.ToolTip = "Musisz zaznaczyć jeszcze na widoku jaki produkt chcesz";
						break;
					case "Widok form płatności":
						actualView = 4;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserPayment");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ComboBoxListBuyed.Visibility = Visibility.Hidden;
						ButtonViewListBuyed.Visibility = Visibility.Hidden;
						ButtonAdd.Click -= AddRetailSales_Click;
						ButtonAdd.Click -= AddProduct_Click;
						ButtonAdd.Click -= AddInvoice_Click;
						ButtonAdd.Click += AddPayment_Click;
						ButtonAdd.Content = "Dodaj Płatność";
						ButtonAdd.ToolTip = "Dodaje Płatność";
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
				case 1:


					break;
				case 2:
					if (newProductWindow.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						newProductWindow = new NewProductWindow(tempObject);
						newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
						newProductWindow.Show();
					}
					break;

				case 3:
					if (windowSales.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						object[] tempSales = new object[10];
						object[] tempProduct = new object[(tempObject.Length - 10)];
						for (int i = 0; i < 10; i++)
						{
							tempSales[i] = tempObject[i];
						}

						for (int i = 10, l = 0; i < tempObject.Length; i++, l++)
						{
							tempProduct[l] = tempObject[i];
						}
						windowSales = new WindowSales(this, tempProduct, tempSales);
						windowSales.WindowStartupLocation = WindowStartupLocation.CenterScreen;
						windowSales.Show();
					}
					break;

				case 4:
					if (windowPayment.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						windowPayment = new WindowPayment(this,tempObject);
						windowPayment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
						windowPayment.Show();
					}
					break;
				default:
					MessageBox.Show("Brak wybranego widoku");
					break;
			}


		}

		private void AddProduct_Click(object sender, RoutedEventArgs e)
		{
			if (newProductWindow.IsVisible == false)
			{
				newProductWindow = new NewProductWindow();
				newProductWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				newProductWindow.Show();
			}

		}

		private void AddInvoice_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddPayment_Click(object sender, RoutedEventArgs e)
		{
			if (windowPayment.IsVisible == false)
			{
				int tempId = Users.LogUser.Id_User;
				windowPayment = new WindowPayment(this, tempId);
				windowPayment.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowPayment.Show();
			}
		}


		private void AddRetailSales_Click(object sender, RoutedEventArgs e)
		{
			if (UsersDataGrid.SelectedItem != null && actualView == 2)
			{
				DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
				object[] tempObject = temp.Row.ItemArray as object[];
				if (windowSales.IsVisible == false)
				{
					windowSales = new WindowSales(this, tempObject);
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
			if (windowMainWindow != null)
				windowMainWindow.IsEnabled = true;
		}

		private void ViewListBuyed_Click(object sender, RoutedEventArgs e)
		{
			if (ComboBoxListBuyed.SelectedItem != null ) 
			{

			}
		}
	}
}

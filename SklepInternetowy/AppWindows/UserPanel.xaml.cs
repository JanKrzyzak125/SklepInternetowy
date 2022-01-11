using Sklep.AppWindows;
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
		private List<string> tempListViews;
		private SQLConnect sqlConnect;
		private Registration windowEditProfile;
		private NewProductWindow newProductWindow;
		private RegistrationCompany windowRegistrationCompany;
		private int actualView;
		private WindowSales windowSales;
		private MainWindow windowMainWindow;
		private WindowPayment windowPayment;
		private WindowInvoice windowInvoice;
		private WindowRating windowRating;

		public List<string> TempListViews
		{
			get => tempListViews;
		}

		public UserPanel()
		{
		}

		public UserPanel(MainWindow WindowMainWindow)
		{
			InitializeComponent();
			ButtonAdd.Visibility = Visibility.Hidden;
			ButtonAdd2.Visibility = Visibility.Hidden;
			sqlConnect = new SQLConnect();
			windowInvoice = new WindowInvoice();
			windowEditProfile = new Registration((Authentication)null);
			newProductWindow = new NewProductWindow();
			windowRegistrationCompany = new RegistrationCompany();
			windowRating = new WindowRating();
			windowSales = new WindowSales();
			windowPayment = new WindowPayment();
			AddListViews();
			if (Users.LogUser != null)
			{
				ButtonCompany.Content = Users.LogUser.Company == null ? "Dodaj firmę" : "Edytuj firmę";
			}
			actualView = -1;
			windowMainWindow = WindowMainWindow;
			windowMainWindow.IsEnabled = false;
		}

		public void CompanyClick(object sender, RoutedEventArgs e)
		{
			if (windowRegistrationCompany.IsVisible == false)
			{
				windowRegistrationCompany = Users.LogUser.Company != null ? new RegistrationCompany(Users.LogUser.Company.Id_Company) : new RegistrationCompany();
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
			tempListViews.Add("Widok sprzedanych");

		}

		/// <summary>
		/// Methods, which rename columns and hidden 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Id_User":
				case "Id_Transation":
				case "Id_Payment":
				case "Id_Seller":
				case "Id_TypePayment":
				case "Id_Product":
				case "Image":
				case "StatusTypePayment":
				case "LimitString":
				case "Id_RetailSales":
			    case "Id_ListProductsBuyed":
				case "Id_Invoice":
				case "Id_RetailSalers":
				case "Code_Invoice":
				case "StatusTransation":
				case "StatusInvoice":
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
				case "DateStartSales":
					e.Column.Header = "Data startu sprzedaży";
					break;
				case "DateClosing":
					e.Column.Header = "Data planowa zakończenia";
					break;
				case "DateClosed":
					e.Column.Header = "Data zakończenia";
					break;
				case "DayDelivery":
					e.Column.Header = "Dni dostawy";
					break;
				case "DayReturn":
					e.Column.Header = "Czas na zwrot";
					break;
				case "Visitors":
					e.Column.Header = "Ilość odwiedzin";
					break;
				case "NameDelivery":
					e.Column.Header = "Sposób Dostawy";
					break;
				case "Quantity":
					e.Column.Header = "Ilość wystawiona";
					break;
				case "StatusProduct":
					e.Column.Header = "Status Produktu";
					break;
				case "Date_Transation":
					e.Column.Header = "Data kupienia";
					break;
				case "SumPay":
					e.Column.Header = "Suma Brutto";
					break;
				case "QuantityBuyed":
					e.Column.Header = "Ilość";
					break;
				case "Date_Of_Make_Invoice":
					e.Column.Header = "Data utworzenia faktury";
					break;
				case "Date_Of_Pay":
					e.Column.Header = "Data zapłaty";
					break;
				case "Date_Of_Service":
					e.Column.Header = "Data wykonania";
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
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserBuyed");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ButtonAdd2.Visibility = Visibility.Visible;
						ButtonAdd.Visibility = Visibility.Visible;
						ButtonAdd2.Click -= AddRating_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						DeleteClick();
						ButtonAdd.Visibility = Visibility.Hidden;
						ButtonAdd.Content = "Dodaj Fakturę";
						ButtonAdd.ToolTip = "Musisz wybrać sprzedaż do jakiej chcesz dodać fakturę";
						ButtonAdd2.Click -= AddRetailSales_Click;
						ButtonAdd2.Click += AddRating_Click;
						ButtonAdd2.Content = "Dodaj/edytuj Ocenę";
						ButtonAdd2.ToolTip = "Dodajesz lub edytujesz ocenę";
						break;
					case "Widok dodanych produktów":
						actualView = 2;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUsersProducts");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ButtonAdd2.Visibility = Visibility.Visible;
						ButtonAdd.Visibility = Visibility.Visible;
						DeleteClick();
						ButtonAdd.Click += AddProduct_Click;
						ButtonAdd.Content = "Dodaj Produkt";
						ButtonAdd.ToolTip = "Dodaje Produkt";
						ButtonAdd2.Click -= AddRating_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						ButtonAdd2.Click += AddRetailSales_Click;
						ButtonAdd2.Content = "Dodaj Sprzedaż";
						ButtonAdd2.ToolTip = "Dodajesz nową sprzedaż po wybraniu produktu";

						break;
					case "Widok sprzedaży":
						actualView = 3;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserSalers");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ButtonAdd2.Click -= AddRating_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						ButtonAdd2.Visibility = Visibility.Hidden;
						ButtonAdd.Visibility = Visibility.Hidden;
						break;
					case "Widok form płatności":
						actualView = 4;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserPayment");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ButtonAdd2.Visibility = Visibility.Hidden;
						ButtonAdd.Visibility = Visibility.Visible;
						ButtonAdd2.Click -= AddRating_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						DeleteClick();
						ButtonAdd.Click += AddPayment_Click;
						ButtonAdd.Content = "Dodaj Płatność";
						ButtonAdd.ToolTip = "Dodaje Płatność";
						break;

					case "Widok sprzedanych":
						actualView = 5;
						tempTable = sqlConnect.ShowProduct(tempidUser, "ViewUserSell");
						UsersDataGrid.ItemsSource = tempTable.DefaultView;
						ButtonAdd2.Click -= AddRating_Click;
						ButtonAdd2.Click -= AddRetailSales_Click;
						ButtonAdd2.Visibility = Visibility.Hidden;
						ButtonAdd.Visibility = Visibility.Hidden;
						DeleteClick();
						break;
					default:
						break;
				}
			}
			else
			{
				MessageBox.Show("Proszę wybrać z listy widok","Uwaga!",MessageBoxButton.OK);
			}
		}

		private void DeleteClick()
		{
			ButtonAdd.Click -= AddRetailSales_Click;
			ButtonAdd.Click -= AddProduct_Click;
			ButtonAdd.Click -= AddPayment_Click;

		}
		private void ClickChangeProfile(object sender, RoutedEventArgs e)
		{
			if (windowEditProfile.IsVisible == false)
			{
				windowEditProfile = new Registration
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				windowEditProfile.ShowDialog();
			}
		}

		private void DoubleClickUsersDataGrid(object sender, RoutedEventArgs e)
		{
			switch (actualView)
			{
				case 1:
					if (windowInvoice.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						windowInvoice = new WindowInvoice(tempObject, false, temp)
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						windowInvoice.ShowDialog();
					}

					break;
				case 2:
					if (newProductWindow.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						newProductWindow = new NewProductWindow(tempObject)
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						newProductWindow.ShowDialog();
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
						windowSales = new WindowSales(this, tempProduct, tempSales)
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						windowSales.ShowDialog();
					}
					break;

				case 4:
					if (windowPayment.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						windowPayment = new WindowPayment(this, tempObject)
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						windowPayment.ShowDialog();
					}
					break;
				case 5:
					if (windowInvoice.IsVisible == false && UsersDataGrid.SelectedItem != null)
					{
						DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
						object[] tempObject = temp.Row.ItemArray as object[];
						windowInvoice = new WindowInvoice(tempObject, true, temp)
						{
							WindowStartupLocation = WindowStartupLocation.CenterScreen
						};
						windowInvoice.ShowDialog();
					}
					break;
				default:
					MessageBox.Show("Brak wybranego widoku","Uwaga!",MessageBoxButton.OK);
					break;
			}


		}

		private void AddProduct_Click(object sender, RoutedEventArgs e)
		{
			if (newProductWindow.IsVisible == false)
			{
				newProductWindow = new NewProductWindow
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				newProductWindow.ShowDialog();
			}

		}

		private void AddRating_Click(object sender, RoutedEventArgs e)
		{
			if (windowRating.IsVisible == false && UsersDataGrid.SelectedItem != null)
			{
				DataRowView temp = UsersDataGrid.SelectedItem as DataRowView;
				object[] tempObject = temp.Row.ItemArray as object[];
				int tempIdProduct = (int)tempObject[16];
				int tempIdUser = Users.LogUser.Id_User;
				List<object[]> tempComment = sqlConnect.Show(tempIdUser, "ValueComment");
				windowRating = tempComment.Count == 0
					? new WindowRating(tempIdProduct, tempIdUser)
					: new WindowRating(tempIdProduct, tempIdUser, tempComment[0]);
				windowRating.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				windowRating.ShowDialog();
			}
			else
			{
				MessageBox.Show("Musisz wybrać sprzedaż, która chcesz ocenić", "Uwaga!", MessageBoxButton.OK);
			}

		}

		private void AddPayment_Click(object sender, RoutedEventArgs e)
		{
			if (windowPayment.IsVisible == false)
			{
				int tempId = Users.LogUser.Id_User;
				windowPayment = new WindowPayment(this, tempId)
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				windowPayment.ShowDialog();
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
					windowSales = new WindowSales(this, tempObject)
					{
						WindowStartupLocation = WindowStartupLocation.CenterScreen
					};
					windowSales.ShowDialog();
				}
			}
			else
			{
				MessageBox.Show("Musi być wybrany produkt", "Uwaga!", MessageBoxButton.OK);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (windowMainWindow != null)
			{
				windowMainWindow.IsEnabled = true;
			}
		}

		private void MenuItemClose_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}

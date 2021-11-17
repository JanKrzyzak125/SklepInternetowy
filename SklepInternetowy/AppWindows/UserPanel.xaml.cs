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
		private WindowProduct windowProducts;
		private List<string> tempListViews;
		private SQLConnect sqlConnect;
		private Registration windowEditProfile;
		private NewProductWindow newProductWindow;
		private RegistrationCompany windowRegistrationCompany;

		public List<string> TempListViews
		{
			get => tempListViews;
		}

		public UserPanel()
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			windowEditProfile = new Registration((Authentication)null);
			newProductWindow = new NewProductWindow();
			windowRegistrationCompany = new RegistrationCompany();
			AddListViews();
			if (Users.LogUser!=null)
			{
				if (Users.LogUser.Company == null) 
					ButtonCompany.Content = "Dodaj firmę";
				else
				{
					ButtonCompany.Content = "Edytuj firmę";
				}
			}
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
			string tempViews = ComboBoxViews.SelectedItem.ToString();
			DataTable tempTable;
			switch (tempViews)
			{
				case "Widok zakupionych produktów":

					break;
				case "Widok dodanych produktów":
					tempTable = sqlConnect.ShowProduct(0, "ViewUsersProducts");
					UsersDataGrid.ItemsSource = tempTable.DefaultView;

					break;
				case "Widok aktualnych sprzedaży":

					break;
				case "Widok zakończonych sprzedaży":

					break;
				case "Widok kupionych produktów":

					break;
				default:
					break;
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
			windowProducts = new WindowProduct();
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

	}
}

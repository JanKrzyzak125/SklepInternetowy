using System.Windows;
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
			ButtonLog.Visibility = Visibility.Visible;
			ButtonAdmin.Visibility = Visibility.Hidden;
			ButtonUser.Visibility = Visibility.Hidden;
			//TODO: PRZENIEŚĆ WSZYSTKIE DODAWANIA DO ADMINISTARTORA POZA DODAWANIEM PRODUKTU 
			//sqlConnect.ShowProduct(this,15); //zmienić całkowicie metodę
			//  sqlConnect.AddImage();//dodawanie zdjęcia do produktu TODO: przenieść oraz dodać do innego skryptu patrz dodawanie produkut
			// sqlConnect.Add(26,"AddVat"); //dodawanie wartości VAT TODO: przenieść  
			//sqlConnect.Add("lata","AddWarranty");
			// sqlConnect.Add("Zegarki","AddCategory");
			// sqlConnect.Add("Po naprawie","AddCondition"); 
			//sqlConnect.Delete(3, "DeleteBrand");
			//sqlConnect.Add("Genesis","AddBrand");
			//sqlConnect.Add("EA", "AddBrand");
			//sqlConnect.Add("Intel", "AddBrand");
			/*
            string tempStringBank="54165465645644651454662465";
            int tempLimitString = sqlConnect.valueStringBank(0,"valueMaxStringBank");
            if (tempStringBank.Length==tempLimitString) 
            {

                sqlConnect.AddPayment(0,0, tempStringBank, "BankPolski", "AddPayment");
                MessageBox.Show("udalo sie");
            }
            else
            {
                MessageBox.Show("nie udalo sie");
            }
            */
			//sqlConnect.Add("Konto bankowe",26,"AddTypePayment");
			//sqlConnect.Add("Kurier", "AddDelivery");
			//sqlConnect.Add("Użytkownik","AddTypeUser");
			/*
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == true)
            {
                byte[] imageData = File.ReadAllBytes(OpenDialog.FileName);
                sqlConnect.AddProduct(0, "Komputer", "Do grania", 124.15, 4, 1, 10, "", 
                                      "", 0, 10, 0, 0, imageData, "AddProduct");
            }
            */
			//sqlConnect.AddCompany("Kebab","8888888888","ktotam.wp.pl",888888,"ul.Slaska 25a","Opole",0,"AddCompany");//TODO: Zrobić dodawanie do konkretnego zalogowanego! usera
			//sqlConnect.AddUser("125Kowalski", makeHash("test"), "Jan", "Kowalski", "pa@wp.pl", "8888888", "Zielona 14a", "Opole", "AddUser");
			//sqlConnect.Delete(0, "DeleteCompany");
			//sqlConnect.Delete(0, "DeleteProduct");
			//sqlConnect.Update(1,"SONY XPERIA","UpdateBrand");//TODO CZY TO POTRZEBNE?
			//sqlConnect.Update(4,25,"UpdateVat");
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
				userPanel = new UserPanel();
				userPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				userPanel.Show();
			}
		}


		private void CloseApp(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}

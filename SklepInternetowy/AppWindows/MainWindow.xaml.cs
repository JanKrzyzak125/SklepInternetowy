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
	}
}

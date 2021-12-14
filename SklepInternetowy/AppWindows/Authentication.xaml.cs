using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy Authentication.xaml
	/// </summary>
	public partial class Authentication : Window
	{
		private Registration registrationWindow;
		private SQLConnect sqlConnect;
		private MainWindow mainWindow;

		private readonly string[] namePersmision = { "Użytkownik", "Administrator" };
		public Authentication(MainWindow tempMainWindow)
		{
			InitializeComponent();
			mainWindow = tempMainWindow;
			registrationWindow = new Registration(this);
			sqlConnect = new SQLConnect();
		}

		/// <summary>
		/// This methods send SQLTaks to verify identity
		/// </summary>
		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string tempNick = NickText.Text;
			byte[] tempHash = makeHash(PasswordBox.Password);
			object[] tempUsers = sqlConnect.VerLogin(tempNick, tempHash, "Login");
			if (tempUsers != null)
			{
				Users tempUser = new Users(tempUsers);
				if (Users.LogUser.IsActive == 0)
				{
					MessageBox.Show("Konto nieaktywne. Spróbuj założyć nowe konto lub skonsultować się z administracja");
				}
				else
				{
					MessageBox.Show("Udało się zalogować użytkownikowi" + Users.LogUser.Nick);
					List<object> tempPermission = sqlConnect.ReadUserPermision(Users.LogUser.Id_User, "ValueUserPermision");
					makePermision(tempPermission);


					mainWindow.ButtonLog.Visibility = Visibility.Hidden;
					mainWindow.MenuItemLogin.IsEnabled = false;
					mainWindow.MenuItemLoginOut.IsEnabled = true;

					this.Close();
				}
			}
			else
			{
				MessageBox.Show("Spróbuj ponownie wpisać dane");
			}
		}

		public void makePermision(List<object> tempPermission)
		{
			mainWindow.ButtonUser.Visibility = Visibility.Hidden;
			mainWindow.ButtonAdmin.Visibility = Visibility.Hidden;

			foreach (object[] temp in tempPermission)
			{
				string tempValuePermission = temp[0] as string;
				if (tempValuePermission.Equals(namePersmision[0]))
				{
					mainWindow.ButtonUser.Visibility = Visibility.Visible;
				}
				if (tempValuePermission.Equals(namePersmision[1]))
				{
					mainWindow.ButtonAdmin.Visibility = Visibility.Visible;
				}

			}
		}

		public byte[] makeHash(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				return bytes;
			}
		}

		/// <summary>
		/// Open Window Registration
		/// </summary>
		private void Registration_Open(object sender, RoutedEventArgs e)
		{

			if (registrationWindow.IsVisible == false)
			{
				registrationWindow = new Registration(this);
				registrationWindow.Show();
				this.IsEnabled = false;
			}
		}

		private void PasswordChange_Open(object sender, RoutedEventArgs e)
		{
			string tempNick = NickText.Text;
			byte[] tempHash = makeHash(PasswordBox.Password);
			int okey = sqlConnect.IsUserNick(tempNick,tempHash, "IsUserNick");

			if (registrationWindow.IsVisible == false && okey == 1)
			{
				registrationWindow = new Registration(tempNick, this);
				registrationWindow.Show();
			}
			else
			{
				MessageBox.Show("Nie ma takiego nicku lub złe hasło podane");
			}
		}

		public void Password_Check(object sender, RoutedEventArgs e)
		{
			LoginButton.Click -= LoginButton_Click;
			LoginButton.Click += PasswordChange_Open;
			LoginButton.Content = "Zmiana hasła";
		}

		public void Password_Uncheck(object sender, RoutedEventArgs e)
		{
			LoginButton.Click -= PasswordChange_Open;
			LoginButton.Click += LoginButton_Click;
			LoginButton.Content = "Zalogować";
		}

		private void TextBoxAuthenticationIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;

			if (temp.Text.Equals("Nick") || temp.Text.Equals("Telefon"))
			{
				temp.Text = "";
			}
		}

	}
}
using SklepInternetowy.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepInternetowy
{
	/// <summary>
	/// Logika interakcji dla klasy Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{
		private Authentication windowAuthentication;
		private SQLConnect sqlConnect;
		private string message="";

		/// <summary>
		/// Constructor for edit user data
		/// </summary>
		public Registration()
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			PasswordBox1.IsEnabled = false;
			PasswordBox2.IsEnabled = false;
			PasswordBox1.ToolTip = "Edycja hasła odbywa się w okienku logowania";
			PasswordBox2.ToolTip = "Edycja hasła odbywa się w okienku logowania";
			RegisterButton.Click -= RegisterButton_Click;
			RegisterButton.Click -= EditPassword_Click;
			RegisterButton.Click += EditButton_Click;
			CheckBoxIsActive.Visibility = Visibility.Visible;

			TextBoxNick.Text = Users.LogUser.Nick;
			TextBoxName.Text = Users.LogUser.Name;
			TextBoxSurname.Text = Users.LogUser.Surname;
			TextBoxEmail.Text = Users.LogUser.Email;
			TextBoxPhone.Text = Users.LogUser.Phone.ToString();
			TextBoxAdress.Text = Users.LogUser.Adress;
			TextBoxCity.Text = Users.LogUser.City;
			this.Title = "Zmiana danych użytkownika";
			RegisterButton.Content = "Edytuj";
		}

		/// <summary>
		/// constructor for add new user
		/// </summary>
		/// <param name="authentication"></param>
		public Registration(Authentication authentication)
		{
			InitializeComponent();
			windowAuthentication = authentication;
			sqlConnect = new SQLConnect();
			CheckBoxIsActive.Visibility = Visibility.Hidden;
			RegisterButton.Click -= EditButton_Click;
			RegisterButton.Click -= EditPassword_Click;
			RegisterButton.Click += RegisterButton_Click;
			this.Title = "Rejestracja";
			RegisterButton.Content = "Zarejestruj";
		}

		public Registration(string valueNick, Authentication authentication)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			windowAuthentication = authentication;
			CheckBoxIsActive.Visibility = Visibility.Hidden;
			TextBoxNick.Text = valueNick;
			TextBoxNick.IsEnabled = false;
			RegisterButton.Click -= EditButton_Click;
			RegisterButton.Click -= RegisterButton_Click;
			RegisterButton.Click += EditPassword_Click;
			this.Title = "Zmiana hasła";
			RegisterButton.Content = "Zmień Hasło";
			TextBoxAdress.IsEnabled = false;
			TextBoxCity.IsEnabled = false;
			TextBoxEmail.IsEnabled = false;
			TextBoxName.IsEnabled = false;
			TextBoxPhone.IsEnabled = false;
			TextBoxSurname.IsEnabled = false;
		}

		private void EditPassword_Click(object sender, RoutedEventArgs e)
		{
			
			string tempHash1 = PasswordBox1.Password;
			string tempHash2 = PasswordBox2.Password;
			string tempNick = TextBoxNick.Text;

			if (tempHash1.Equals(tempHash2) && (tempHash1.Length > 3))
			{
				PasswordBox1.Background = System.Windows.Media.Brushes.Green;
				PasswordBox2.Background = System.Windows.Media.Brushes.Green;
				byte[] tempHash = windowAuthentication.makeHash(tempHash1);
				sqlConnect.NewPassword(tempNick, tempHash, "NewPassword");
				MessageBox.Show("Udalo się zmienić hasło","Powiadomienie",MessageBoxButton.OK);
				this.Close();
			}
			else
			{
				PasswordBox1.Background = System.Windows.Media.Brushes.Red;
				PasswordBox2.Background = System.Windows.Media.Brushes.Red;
				MessageBox.Show("Hasła się róznią lub za krótkie hasło", "Uwaga!", MessageBoxButton.OK!);
			}
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			message = "";
			int codeFailed = 0;
			string tempNick = TextBoxNick.Text;
			if (!testName(TextBoxNick)) codeFailed++;

			string tempName = TextBoxName.Text;
			if (!testName(TextBoxName)) codeFailed++;

			string tempSurname = TextBoxSurname.Text;
			if (!testName(TextBoxSurname)) codeFailed++;

			string tempEmail = TextBoxEmail.Text;
			if (!testName(TextBoxEmail)) codeFailed++;

			long tempPhone;
			if (!long.TryParse(TextBoxPhone.Text, out tempPhone))
			{
				message += "Zły numer Telefonu\n";
				TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				if (TextBoxPhone.Text.Length == 0)
				{
					message += "brak numeru telefonu\n";
					TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				}
				else
				{
					TextBoxPhone.Background = System.Windows.Media.Brushes.Green;
				}
			}

			string tempAdress = TextBoxAdress.Text;
			if (!testName(TextBoxAdress)) codeFailed++;

			string tempCity = TextBoxCity.Text;
			if (!testName(TextBoxCity)) codeFailed++;


			if (codeFailed == 0)
			{
				int tempId = Users.LogUser.Id_User;
				Int16 tempIsActive = 1;
				if (CheckBoxIsActive.IsChecked == true) tempIsActive = 0;
				int tempCountVisitors = Users.LogUser.CountVisitors;
				Company tempCompany = Users.LogUser.Company;

				if (tempCompany == null)
					sqlConnect.UpdateUser(tempId, tempIsActive, tempNick, tempName, tempSurname, tempEmail, tempPhone,
								  tempAdress, tempCity, tempCountVisitors, "UpdateUsers2");
				else
					sqlConnect.UpdateUser(tempId, tempIsActive, tempNick, tempName, tempSurname, tempEmail, tempPhone,
							  tempAdress, tempCity, tempCountVisitors, tempCompany, "UpdateUsers");
				Users.LogUser = new Users(sqlConnect.RefreshUser(tempId, "RefreshUser"));
				MessageBox.Show("Udalo się zmienić dane");
				this.Close();
			}
			else
			{
				codeFailed++;
				MessageBox.Show("Wystąpiło w formularzu=" + codeFailed + " błędów:\n"+message,"Uwaga!",MessageBoxButton.OK);
			}

		}


		/// <summary>
		/// Click to verification date and making new account to database 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			message = "";
			int codeFailed = 0;
			string tempNick = TextBoxNick.Text;
			if (!testName(TextBoxNick)) codeFailed++;

			string tempHash1 = PasswordBox1.Password;
			string tempHash2 = PasswordBox2.Password;

			string tempName = TextBoxName.Text;
			if (!testName(TextBoxName)) codeFailed++;

			string tempSurname = TextBoxSurname.Text;
			if (!testName(TextBoxSurname)) codeFailed++;

			string tempEmail = TextBoxEmail.Text;
			if (!testName(TextBoxEmail)) codeFailed++;

			long tempPhone;
			if (!long.TryParse(TextBoxPhone.Text, out tempPhone))
			{
				message += "Zły numer telefonu\n";
				TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				if (TextBoxPhone.Text.Length == 0)
				{
					message += "brak numeru telefonu\n";
					TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				}
				else
				{
					TextBoxPhone.Background = System.Windows.Media.Brushes.Green;
				}
			}

			string tempAdress = TextBoxAdress.Text;
			if (!testName(TextBoxAdress)) codeFailed++;

			string tempCity = TextBoxCity.Text;
			if (!testName(TextBoxCity)) codeFailed++;


			if (tempHash1.Equals(tempHash2) && (codeFailed == 0))
			{
				PasswordBox1.Background = System.Windows.Media.Brushes.Green;
				PasswordBox2.Background = System.Windows.Media.Brushes.Green;
				byte[] tempHash = windowAuthentication.makeHash(tempHash1);
				sqlConnect.AddUser(tempNick, tempHash, tempName, tempSurname, tempEmail, tempPhone,
							  tempAdress, tempCity, "AddUser");
				windowAuthentication.IsEnabled = true;
				MessageBox.Show("Udalo się zajestrować");
				this.Close();
			}
			else
			{
				codeFailed++;
				PasswordBox1.Background = System.Windows.Media.Brushes.Red;
				PasswordBox2.Background = System.Windows.Media.Brushes.Red;
				MessageBox.Show("Wystąpiło w formularzu=" + codeFailed + " błędów:\n" + message, "Uwaga!", MessageBoxButton.OK);
			}
		}



		private bool testName(TextBox tempTextBox)
		{
			string tempString = tempTextBox.Text;
			if (tempString.Length < 2 || tempString.Equals("Imię") || tempString.Equals("Nazwisko") ||
				tempString.Equals("Nick") || tempString.Equals("Email") || tempString.Equals("Adres") ||
				tempString.Equals("Miasto"))
			{
				message += "Złe " + nameTextBox(tempTextBox.Name)+"\n";
				tempTextBox.Background = System.Windows.Media.Brushes.Red;
				return false;
			}
			else
			{
				tempTextBox.Background = System.Windows.Media.Brushes.Green;
				return true;
			}
		}


		private string nameTextBox(string temp)
		{
			return temp switch
			{
				"TextBoxName" => "Imię",
				"TextBoxNick" => "Nick",
				"TextBoxSurname" => "Nazwisko",
				"TextBoxEmail" => "Poczta Email",
				"TextBoxAdress" => "Adres",
				"TextBoxCity" => "Miasto",
				_ => "",
			};
		}

		private void NameChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;
			temp.CharacterCasing = temp.Text.Length < 1 ? CharacterCasing.Upper : CharacterCasing.Lower;
		}

		private void TextBoxNameIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;
			if (temp.Text.Equals("Imię") || temp.Text.Equals("Nazwisko") ||
				temp.Text.Equals("Nick") || temp.Text.Equals("Email") ||
				temp.Text.Equals("Adres") || temp.Text.Equals("Miasto") ||
				temp.Text.Equals("Telefon"))
			{
				temp.Text = "";
			}
		}
		private void OnlyNumeric(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (windowAuthentication != null)
			{
				windowAuthentication.IsEnabled = true;
			}
		}
	}
}

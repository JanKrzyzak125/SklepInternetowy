using System.Drawing;
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
		private bool failed=false;

		public Registration(Authentication authentication)
		{
			InitializeComponent();
			windowAuthentication = authentication;
			sqlConnect = new SQLConnect();
		}

		/// <summary>
		/// Click to verification date and making new account to database 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			string tempNick = TextBoxNick.Text;
			if (tempNick.Length < 3|| tempNick.Equals("Nick")) 
			{
				TextBoxNick.Background = System.Windows.Media.Brushes.Red;
				MessageBox.Show("Zły nick");
				codeFailed++;
			}
			else 
			{
				TextBoxNick.Background = System.Windows.Media.Brushes.Green;
			}

			string tempHash1 = PasswordBox1.Password;
			string tempHash2 = PasswordBox2.Password;

			string tempName = TextBoxName.Text;
			if (tempName.Length<2 || tempName.Equals("Imię")) 
			{
				MessageBox.Show("Złe imię");
				TextBoxName.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxName.Background = System.Windows.Media.Brushes.Green;
			}

			string tempSurname = TextBoxSurname.Text;
			if (tempSurname.Length < 2 || tempSurname.Equals("Nazwisko"))
			{
				MessageBox.Show("Złe nazwisko");
				TextBoxSurname.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxSurname.Background = System.Windows.Media.Brushes.Green;
			}

			string tempEmail = TextBoxEmail.Text;
			if (tempEmail.Length<3|| tempEmail.Equals("Poczta Email")) 
			{
				MessageBox.Show("Zły adres Email");
				TextBoxEmail.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxEmail.Background = System.Windows.Media.Brushes.Green;
			}

			int tempPhone;
			if (!int.TryParse(TextBoxPhone.Text, out tempPhone))
			{
				MessageBox.Show("Zły numer Telefonu");
				TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;

			}
			else
			{
				TextBoxPhone.Background = System.Windows.Media.Brushes.Green;
			}

			string tempAdress = TextBoxAdress.Text;
			if (tempAdress.Length < 3|| tempAdress.Equals("Adres")) 
			{
				MessageBox.Show("Zły Adres");
				TextBoxAdress.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxAdress.Background = System.Windows.Media.Brushes.Green;
			}

			string tempCity = TextBoxCity.Text;
			if (tempCity.Length < 3|| tempCity.Equals("Miasto"))
			{
				MessageBox.Show("Złe miasto");
				TextBoxCity.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxCity.Background = System.Windows.Media.Brushes.Green;
			}


			if (tempHash1.Equals(tempHash2) && (codeFailed == 0))
			{
				PasswordBox1.Background = System.Windows.Media.Brushes.Green;
				PasswordBox2.Background = System.Windows.Media.Brushes.Green;
				byte[] tempHash = windowAuthentication.makeHash(tempHash1);
				sqlConnect.AddUser(tempNick, tempHash, tempName, tempSurname, tempEmail, tempPhone,
							  tempAdress, tempCity, "AddUser");

				MessageBox.Show("Udalo się zajestrować");
				this.Close();
			}
			else 
			{
				codeFailed++;
				PasswordBox1.Background = System.Windows.Media.Brushes.Red;
				PasswordBox2.Background = System.Windows.Media.Brushes.Red;

			}
			MessageBox.Show("Wystąpiło w formularzu=" + codeFailed+" błędów");
		}

		private void NameChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;
			if (temp.Text.Length < 1) 
			{
				temp.CharacterCasing = CharacterCasing.Upper;
			}
			else 
			{
				temp.CharacterCasing = CharacterCasing.Lower;
			}
		}

		private void TextBoxNameIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox temp= sender as TextBox;
			if (temp.Text.Equals("Imię") || temp.Text.Equals("Nazwisko") ||
				temp.Text.Equals("Nick") || temp.Text.Equals("Poczta Email") ||
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

	}
}

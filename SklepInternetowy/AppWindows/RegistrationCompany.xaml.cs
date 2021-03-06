using SklepInternetowy.Classes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy RegistrationCompany.xaml
	/// </summary>
	public partial class RegistrationCompany : Window
	{
		private SQLConnect sqlConnect;
		private int actualIdCompany;

		public RegistrationCompany()
		{
			InitializeComponent();
			CheckBoxIsActive.IsEnabled = false;
			this.Title = "Dodaj Firmę";
			RegisterButton.Click -= EditButton_Click;
			RegisterButton.Click += RegisterButton_Click;
			sqlConnect = new SQLConnect();
		}

		public RegistrationCompany(int valueId)
		{
			InitializeComponent();
			CheckBoxIsActive.IsEnabled = true;
			this.Title = "Edycja Firmy";
			RegisterButton.Click -= RegisterButton_Click;
			RegisterButton.Click += EditButton_Click;
			sqlConnect = new SQLConnect();
			actualIdCompany = valueId;
			Company tempCompany = Users.LogUser.Company;
			TextBoxPhone.Text = tempCompany.Phone.ToString();
			TextBoxNameCompany.Text = tempCompany.NameCompany;
			TextBoxNIP.Text = tempCompany.NIP;
			TextBoxCity.Text = tempCompany.City;
			TextBoxAdress.Text = tempCompany.Adress;
			TextBoxEmail.Text = tempCompany.Email;
			if (tempCompany.Status == 0)
			{
				CheckBoxIsActive.IsChecked = true;
			}
		}


		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			string tempNameCompany = TextBoxNameCompany.Text;
			if (!testName(TextBoxNameCompany)) codeFailed++;

			string tempAdress = TextBoxAdress.Text;
			if (!testName(TextBoxAdress)) codeFailed++;

			string tempCity = TextBoxCity.Text;
			if (!testName(TextBoxCity)) codeFailed++;

			long tempPhone;
			if (!long.TryParse(TextBoxPhone.Text, out tempPhone))
			{
				MessageBox.Show("Zły numer Telefonu");
				TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else TextBoxPhone.Background = System.Windows.Media.Brushes.Green;


			string tempNIP = TextBoxNIP.Text;
			if (!testName(TextBoxNIP)) codeFailed++;


			string tempEmail = TextBoxEmail.Text;
			if (!testName(TextBoxEmail)) codeFailed++;


			if (codeFailed == 0)
			{
				int tempIsActive = 1;
				int tempId = Users.LogUser.Id_User;
				if (CheckBoxIsActive.IsChecked == true) tempIsActive = 0;
				sqlConnect.UpdateCompany(actualIdCompany, tempNameCompany, tempAdress, tempCity, tempPhone,
										 tempEmail, tempNIP, tempIsActive, "UpdateCompany");
				Users.LogUser= new Users( sqlConnect.RefreshUser(tempId, "RefreshUser"));
				MessageBox.Show("Udało się zaktualizować dane o firmie");
				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło w formularzu=" + codeFailed + " błędów", "Uwaga!", MessageBoxButton.OK);
			}

		}


		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			string tempNameCompany = TextBoxNameCompany.Text;
			if (!testName(TextBoxNameCompany)) codeFailed++;

			string tempAdress = TextBoxAdress.Text;
			if (!testName(TextBoxAdress)) codeFailed++;

			string tempCity = TextBoxCity.Text;
			if (!testName(TextBoxCity)) codeFailed++;

			long tempPhone;
			if (!long.TryParse(TextBoxPhone.Text, out tempPhone))
			{
				MessageBox.Show("Zły numer Telefonu");
				TextBoxPhone.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else TextBoxPhone.Background = System.Windows.Media.Brushes.Green;


			string tempNIP = TextBoxNIP.Text;
			if (!testName(TextBoxNIP)) codeFailed++;


			string tempEmail = TextBoxEmail.Text;
			if (!testName(TextBoxEmail)) codeFailed++;

			if (codeFailed == 0)
			{
				int tempIdUser = Users.LogUser.Id_User;
				sqlConnect.AddCompany(tempNameCompany, tempNIP, tempEmail, tempPhone,
									  tempAdress, tempCity, tempIdUser, "AddCompany");
				Users.LogUser = new Users(sqlConnect.RefreshUser(tempIdUser, "RefreshUser"));
				MessageBox.Show("Udało się dodać dane o firmie");

				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło w formularzu=" + codeFailed + " błędów", "Uwaga!", MessageBoxButton.OK);
			}
		}
		private bool testName(TextBox tempTextBox)
		{
			string tempString = tempTextBox.Text;
			if (tempString.Length < 3 || tempString.Equals("Nazwa firmy") || tempString.Equals("NIP") ||
				tempString.Equals("Email") || tempString.Equals("Adres") || tempString.Equals("Miasto"))
			{
				MessageBox.Show("Złe " + nameTextBox(tempTextBox.Name), "Uwaga!", MessageBoxButton.OK);
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
				"TextBoxNameCompany" => "Nazwa firmy",
				"TextBoxNIP" => "NIP",
				"TextBoxEmail" => "Email",
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
			if (temp.Text.Equals("Nazwa Firmy") || temp.Text.Equals("Email") ||
				temp.Text.Equals("Adres") || temp.Text.Equals("Miasto") ||
				temp.Text.Equals("Telefon") || temp.Text.Equals("NIP"))
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy RegistrationCompany.xaml
	/// </summary>
	public partial class RegistrationCompany : Window
	{
		public RegistrationCompany()
		{
			InitializeComponent();
		}

		public RegistrationCompany(int valueId)
		{
			InitializeComponent();
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{

		}


		private void RegisterButton_Click(object sender, RoutedEventArgs e)
		{

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
			TextBox temp = sender as TextBox;
			if (temp.Text.Equals("Nazwa Firmy") || temp.Text.Equals("Poczta Email") ||
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

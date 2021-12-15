using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy WindowPayment.xaml
	/// </summary>
	public partial class WindowPayment : Window
	{
		private int currentUser;
		private object[] currentPayment;
		private UserPanel windowUserPanel;
		private SQLConnect sqlConnect;
		private List<string> tempListTypePayment;
		private List<object[]> tempTypePayment;

		public WindowPayment()
		{
			currentPayment = null;
			sqlConnect = null;
		}

		public WindowPayment(int valueId) 
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentUser = valueId;
			FillCombo();
			this.Title = "Dodaj nową płatność";
			ButtonAccept.Click -= EditPayment_Click;
			ButtonAccept.Click += NewPayment_Click;
			LabelStatus.Visibility = Visibility.Hidden;
			CheckBoxStatus.Visibility = Visibility.Hidden;
		}

		public WindowPayment(object[] tempPayment)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentPayment = tempPayment;
			FillCombo();
			this.Title = "Zmień płatność";
			ComboBoxTypePayment.Text = currentPayment[6].ToString();
			TextBoxNameBank.Text = currentPayment[4].ToString();
			TextBoxPaymentString.Text = currentPayment[3].ToString();
			if ((int)currentPayment[5] == 0)
			{
				CheckBoxStatus.IsChecked = true;
			}
			ButtonAccept.Click -= NewPayment_Click;
			ButtonAccept.Click += EditPayment_Click;
			LabelStatus.Visibility = Visibility.Visible;
			CheckBoxStatus.Visibility = Visibility.Visible;
		}

		public WindowPayment(UserPanel tempWindowUserPanel, int valueId)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentUser = valueId;
			windowUserPanel = tempWindowUserPanel;
			FillCombo();
			this.Title = "Dodaj nową płatność";
			tempWindowUserPanel.IsEnabled = false;

			ButtonAccept.Click -= EditPayment_Click;
			ButtonAccept.Click += NewPayment_Click;
			LabelStatus.Visibility = Visibility.Hidden;
			CheckBoxStatus.Visibility = Visibility.Hidden;
		}

		public WindowPayment(UserPanel tempWindowUserPanel, object[] tempPayment)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentPayment = tempPayment;
			windowUserPanel = tempWindowUserPanel;
			FillCombo();
			this.Title = "Zmień płatność";
			tempWindowUserPanel.IsEnabled = false;
			ComboBoxTypePayment.Text = currentPayment[6].ToString();
			TextBoxNameBank.Text = currentPayment[4].ToString();
			TextBoxPaymentString.Text = currentPayment[3].ToString();
			if ((int)currentPayment[5] == 0)
			{
				CheckBoxStatus.IsChecked = true;
			}
			ButtonAccept.Click -= NewPayment_Click;
			ButtonAccept.Click += EditPayment_Click;
			LabelStatus.Visibility = Visibility.Visible;
			CheckBoxStatus.Visibility = Visibility.Visible;
		}

		private void FillCombo()
		{
			tempListTypePayment = new List<string>();
			tempTypePayment = new List<object[]>();
			ComboBoxTypePayment.ItemsSource = tempListTypePayment;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesTypePayment").Rows)
			{
				if ((int)row[2] == 1)
				{
					tempListTypePayment.Add(row[0].ToString());
					tempTypePayment.Add(row.ItemArray);
				}
			}

		}

		private void EditPayment_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			if (ComboBoxTypePayment.SelectedItem == null)
			{
				codeFailed++;
				MessageBox.Show("Nie wybrano typu płatności");
				ComboBoxTypePayment.Foreground = Brushes.Red;

			}
			else
			{
				ComboBoxTypePayment.Foreground = Brushes.Green;
			}
			string tempTypePayment = ComboBoxTypePayment.Text;
			string tempNameBank = TextBoxNameBank.Text;
			TextBoxNameBank.Background = Brushes.Green;
			string tempPaymentString = TextBoxPaymentString.Text;

			int tempLimit = searchLimit(tempTypePayment);
			if (tempPaymentString.Length != tempLimit)
			{
				codeFailed++;
				MessageBox.Show("Numer musi być długości=" + tempLimit);
				TextBoxPaymentString.Background = Brushes.Red;
			}
			else
			{
				TextBoxPaymentString.Background = Brushes.Green;
			}
			if (codeFailed == 0)
			{
				int tempId = (int)currentPayment[1];
				int tempStatus;
				if (CheckBoxStatus.IsChecked == true) tempStatus = 0;
				else tempStatus = 1;
				sqlConnect.UpdatePayment(tempId, tempTypePayment, tempPaymentString, tempNameBank, tempStatus, "UpdatePayment2");
				MessageBox.Show("Udało się zmienić płatność");
				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło=" + codeFailed + " tyle błędów w formularzu");
			}
		}

		private void NewPayment_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			if (ComboBoxTypePayment.SelectedItem == null)
			{
				codeFailed++;
				MessageBox.Show("Nie wybrano typu płatności");

				ComboBoxTypePayment.Foreground = Brushes.Red;

			}
			else
			{
				ComboBoxTypePayment.Foreground = Brushes.Green;
			}
			string tempTypePayment = ComboBoxTypePayment.Text;
			string tempNameBank = TextBoxNameBank.Text;
			TextBoxNameBank.Background = Brushes.Green;
			string tempPaymentString = TextBoxPaymentString.Text;

			int tempLimit = searchLimit(tempTypePayment);
			if (tempPaymentString.Length != tempLimit)
			{
				codeFailed++;
				MessageBox.Show("Numer musi być długości=" + tempLimit);
				TextBoxPaymentString.Background = Brushes.Red;
			}
			else
			{
				TextBoxPaymentString.Background = Brushes.Green;
			}
			if (codeFailed == 0)
			{
				sqlConnect.AddPayment(currentUser, tempTypePayment, tempPaymentString, tempNameBank, "AddPayment");
				MessageBox.Show("Udało się dodać płatność");
				this.Close();
			}
			else
			{
				MessageBox.Show("Wystąpiło=" + codeFailed + " tyle błędów w formularzu");
			}
		}

		private int searchLimit(string tempType)
		{
			int tempLimit = 0;
			foreach (object[] item in tempTypePayment)
			{
				string temp = item[0].ToString();
				if (temp.Equals(tempType)) tempLimit = (int)item[1];
			}
			return tempLimit;
		}

		private void OnlyNumeric(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]");
			e.Handled = regex.IsMatch(e.Text);
		}


		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (windowUserPanel != null) windowUserPanel.IsEnabled = true;
		}
	}
}

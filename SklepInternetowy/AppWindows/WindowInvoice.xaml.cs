using SklepInternetowy;
using SklepInternetowy.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Sklep.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy WindowInvoice.xaml
	/// </summary>
	public partial class WindowInvoice : Window
	{
		private object[] currentSeller;
		private object[] currentUser2;
		private Users currentUser;
	 	private Product currentProduct;
		private object[] currrentInvoice;
		private bool isSeller;
		private SQLConnect sqlConnect;
		private readonly string[] tempName = { "Imię: ", "Nazwisko: ","Email: ","Telefon: ","Adres: ","Miasto: ","Nazwa Firmy: ",
											  "Email Firmy: ","Telefon Firmy: ","Adres Firmy: ","Miasto Firmy: ","NIP: "};

		public WindowInvoice()
		{
			InitializeComponent();
		}

		public WindowInvoice(object[] tempSellerUser, Product product, object[] invoice) 
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentSeller = tempSellerUser;
			currentUser = Users.LogUser;
			currrentInvoice = invoice;
			currentProduct = product;
			this.Name = "Podgląd faktury";
		}



		public WindowInvoice(object[] invoice, bool tempIsSeller,DataRowView dataRowView)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currrentInvoice = invoice;
			isSeller = tempIsSeller;
			currentUser = Users.LogUser;
			int tempIdUser2 = isSeller ? (int)invoice[2] : (int)invoice[15];
			currentUser2 = sqlConnect.Value(tempIdUser2,"ValueUser");
			this.Name = "Podgląd faktury";

			string temp="";
			for (int i = 0; i < currentUser2.Length; i++)
			{
				temp += tempName[i] + currentUser2[i].ToString()+"\n";
			}

			string temp2 = "";
			temp2 += tempName[0] + currentUser.Name.ToString() + "\n";
			temp2 += tempName[1] + currentUser.Surname.ToString() + "\n";
			temp2 += tempName[2] + currentUser.Email.ToString() + "\n";
			temp2 += tempName[3] + currentUser.Phone.ToString() + "\n";
			temp2 += tempName[4] + currentUser.Adress.ToString() + "\n";
			temp2 += tempName[5] + currentUser.City.ToString() + "\n";
			if (currentUser.Company != null) 
			{
				temp2 += tempName[6] + currentUser.Company.NameCompany.ToString() + "\n";
				temp2 += tempName[7] + currentUser.Company.Email.ToString() + "\n";
				temp2 += tempName[8] + currentUser.Company.Phone.ToString() + "\n";
				temp2 += tempName[9] + currentUser.Company.Adress.ToString() + "\n";
				temp2 += tempName[10] + currentUser.Company.City.ToString() + "\n";
				temp2 += tempName[11] + currentUser.Company.NIP.ToString() + "\n";
			}
			TextBoxSeller.Text = isSeller ? temp2 : temp;
			TextBoxBuyed.Text = isSeller ? temp : temp2;

			DateTime tempTime = (DateTime)invoice[10];
			string tempStringTime =tempTime.Day+"/"+tempTime.Month+"/"+ tempTime.Year;
			TextBoxNameInvoice.Text += invoice[0]+"/"+ tempStringTime;

			tempStringTime = tempTime.Day + "-" + tempTime.Month + "-" + tempTime.Year;
			TextBoxDateMade.Text += tempStringTime;

			tempTime = (DateTime)invoice[12];
			tempStringTime = tempTime.Day + "-" + tempTime.Month + "-" + tempTime.Year;
			TextBoxDateMaking.Text += tempStringTime;

			string tempPayment = TextBoxPayment.Text;
			string[] tempTabPayment=tempPayment.Split(':',4);

			int tempIdUser = (int)invoice[2];
			List<object[]> tempListPayment= sqlConnect.Show(tempIdUser, "ViewUserPayment");
			
			int tempIdPayment = (int) invoice[1];
			object[] position=null;
			foreach(object[] item in tempListPayment) 
			{
				if ((int)item[1] == tempIdPayment) 
				{
					position = item;
				}
			}

			tempTabPayment[0]+=": "+ position[6]+"\n";
			tempTabPayment[2]+= position[3].ToString().Length != 0 ? ": " + position[3] + "\n": ": " + "brak" + "\n";

			tempTime = (DateTime)invoice[11];
			tempStringTime = tempTime.Day + "-" + tempTime.Month + "-" + tempTime.Year;
			tempTabPayment[1] += ": " + tempStringTime + "\n";

			TextBoxPayment.Text = tempTabPayment[0] + tempTabPayment[1] + tempTabPayment[2];
			decimal tempSumPay =(decimal) invoice[4];
			tempSumPay = Math.Round(tempSumPay, 2);
			TextBoxPay.Text += " " +tempSumPay+"zł";

			for (int i = 0; i < dataRowView.DataView.Count; i++)
			{
				object[] tempObject = dataRowView.Row.ItemArray;
				decimal tempNumber = Math.Round((decimal)dataRowView.Row.ItemArray[4], 2);
				dataRowView.Row.AcceptChanges();
				tempObject[4] = tempNumber;
				tempNumber = Math.Round((decimal)dataRowView.Row.ItemArray[18], 2);
				tempObject[18] = tempNumber;
				dataRowView.Row.ItemArray = tempObject;
			}
			DataGridProducts.ItemsSource = dataRowView.DataView;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ButtonPrint.Visibility = Visibility.Hidden;
			PrintDialog dlg = new PrintDialog();
			Window currentMainWindow = Application.Current.MainWindow;
			Application.Current.MainWindow = this;

			if ((bool)dlg.ShowDialog().GetValueOrDefault())
			{
				Application.Current.MainWindow = currentMainWindow; // do it early enough if the 'if' is entered
				dlg.PrintVisual(this, "Certificate");
			}

			ButtonPrint.Visibility = Visibility.Visible;
		}
	}
}

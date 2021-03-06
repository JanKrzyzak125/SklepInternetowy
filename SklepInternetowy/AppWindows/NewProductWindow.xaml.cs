using Microsoft.Win32;
using SklepInternetowy.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy NewProductWindow.xaml
	/// </summary>
	/// 
	public partial class NewProductWindow : Window
	{
		private SQLConnect sqlConnect = new SQLConnect();
		private List<string> tempListVat;
		private List<string> tempListCondition;
		private List<string> tempListBrand;
		private List<string> tempListCategory;
		private List<string> tempListWarranty;
		private byte[] tempImageData;
		private object[] actualProduct;
		private Product currentProduct;
		private WindowRating windowRating;

		public List<string> TempListVat
		{
			get => tempListVat;
		}

		public List<string> TempListCondition
		{
			get => tempListCondition;
		}

		public List<string> TempListBrand
		{
			get => tempListBrand;
		}

		public List<string> TempListCategory
		{
			get => tempListCategory;
		}

		private List<string> TempListWarranty
		{
			get => tempListWarranty;
		}


		public NewProductWindow(object[] Product)
		{
			InitializeComponent();
			makeList();
			FillingDate();
			windowRating = new WindowRating();
			this.Title = "Edycja produktu";
			ButtonProduct.Click -= NewButton_Click;
			ButtonProduct.Click += EditButton_Click;
			ButtonProduct.Content = "Edytuj produkt";
			actualProduct = Product;
			TextBoxNameProduct.Text = Product[1].ToString();
			TextBoxDescription.Text = Product[2].ToString();
			TextBoxNetto.Text = Product[3].ToString();
			ComboVAT.Text = Product[4].ToString() + "%";
			ComboCondition.Text = Product[5].ToString();
			TextBoxQuantity.Text = Product[6].ToString();
			TextBoxOptionalName.Text = Product[7].ToString();
			TextBoxOptionalDescription.Text = Product[8].ToString();
			ComboWarranty.Text = Product[9].ToString();
			TextBoxDaysWarranty.Text = Product[10].ToString();
			ComboBrand.Text = Product[11].ToString();
			ComboCategory.Text = Product[12].ToString();
			tempImageData = (byte[])Product[13];
			ImageProduct.Source = ConvertByteToImage(tempImageData);
			LabelRating.Visibility = Visibility.Visible;
			ButtonRating.Visibility = Visibility.Visible;
		}

		public NewProductWindow(Product CurrentProduct)
		{
			InitializeComponent();
			makeList();
			FillingDate();
			windowRating = new WindowRating(CurrentProduct.Id_Product);
			this.Title = "Podgląd produktu";
			ButtonProduct.Click -= NewButton_Click;
			ButtonProduct.Click += EditButton_Click;
			ButtonProduct.Content = "Edytuj produkt";
			ButtonProduct.Visibility = Visibility.Hidden;
			ButtonAddImage.Visibility = Visibility.Hidden;
			currentProduct = CurrentProduct;
			TextBoxNameProduct.Text = CurrentProduct.Name.ToString();
			TextBoxNameProduct.IsEnabled = false;
			TextBoxDescription.Text = CurrentProduct.Description.ToString();
			TextBoxDescription.IsEnabled = false;
			decimal tempNetto = CurrentProduct.Price;
			tempNetto = Math.Round(tempNetto, 2);
			TextBoxNetto.Text = tempNetto.ToString();
			TextBoxNetto.IsEnabled = false;
			ComboVAT.Text = CurrentProduct.Vat_rate.ToString() + "%";
			ComboVAT.IsEnabled = false;
			ComboCondition.Text = CurrentProduct.NameCondition.ToString();
			ComboCondition.IsEnabled = false;
			TextBoxQuantity.Text = CurrentProduct.Quantity.ToString();
			TextBoxQuantity.IsEnabled = false;
			TextBoxOptionalName.Text = CurrentProduct.NameParameter.ToString();
			TextBoxOptionalName.IsEnabled = false;
			TextBoxOptionalDescription.Text = CurrentProduct.Parameter.ToString();
			TextBoxOptionalDescription.IsEnabled = false;
			ComboWarranty.Text = CurrentProduct.TypeWarranty.ToString();
			ComboWarranty.IsEnabled = false;
			TextBoxDaysWarranty.Text = CurrentProduct.WarrantyDays.ToString();
			TextBoxDaysWarranty.IsEnabled = false;
			ComboBrand.Text = CurrentProduct.NameBrand.ToString();
			ComboBrand.IsEnabled = false;
			ComboCategory.Text = CurrentProduct.NameCategory.ToString();
			ComboCategory.IsEnabled = false;
			tempImageData = CurrentProduct.Image;
			ImageProduct.Source = ConvertByteToImage(tempImageData);
			LabelRating.Visibility = Visibility.Visible;
			ButtonRating.Visibility = Visibility.Visible;
		}

		public NewProductWindow()
		{
			InitializeComponent();
			makeList();
			FillingDate();

			this.Title = "Dodaj nowy produkt";
			ButtonProduct.Click -= EditButton_Click;
			ButtonProduct.Click += NewButton_Click;
			ButtonProduct.Content = "Dodaj produkt";
			LabelRating.Visibility = Visibility.Hidden;
			ButtonRating.Visibility = Visibility.Hidden;
			tempImageData = File.ReadAllBytes("./Picture/Brak.PNG");
			ImageProduct.Source = ConvertByteToImage(tempImageData);
		}

		private void makeList()
		{
			tempListVat = new List<string>();
			tempListCondition = new List<string>();
			tempListBrand = new List<string>();
			tempListCategory = new List<string>();
			tempListWarranty = new List<string>();

			ComboVAT.ItemsSource = tempListVat;
			ComboBrand.ItemsSource = tempListBrand;
			ComboCondition.ItemsSource = tempListCondition;
			ComboCategory.ItemsSource = tempListCategory;
			ComboWarranty.ItemsSource = tempListWarranty;
		}


		public void FillingDate()
		{
			foreach (DataRow row in sqlConnect.ReadTable("ValuesVat").Rows)
			{
				if ((int)row[1] == 1) tempListVat.Add(row[0].ToString() + "%");
			}
			ComboVAT.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesCondition").Rows)
			{
				if ((int)row[1] == 1) tempListCondition.Add(row[0].ToString());
			}
			ComboCondition.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesBrand").Rows)
			{
				if ((int)row[1] == 1) tempListBrand.Add(row[0].ToString());
			}
			ComboBrand.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesCategory").Rows)
			{
				if ((int)row[1] == 1) tempListCategory.Add(row[0].ToString());
			}
			ComboCategory.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesWarranty").Rows)
			{
				if ((int)row[1] == 1) tempListWarranty.Add(row[0].ToString());
			}
			ComboWarranty.SelectedIndex = 0;

		}

		/// <summary>
		/// Convert Byte[] to BitmapImage
		/// </summary>
		/// <param name="imageByteArray"></param>
		/// <returns></returns>
		public static BitmapImage ConvertByteToImage(byte[] imageByteArray)
		{
			BitmapImage img = new BitmapImage();
			using (MemoryStream memStream = new MemoryStream(imageByteArray))
			{
				img.BeginInit();
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.StreamSource = memStream;
				img.EndInit();
				img.Freeze();
			}
			return img;
		}

		


		private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog OpenDialog = new OpenFileDialog();
			if (OpenDialog.ShowDialog() == true)
			{
				tempImageData = File.ReadAllBytes(OpenDialog.FileName);

			}
			ImageProduct.Source = ConvertByteToImage(tempImageData);
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{

			int codeFailed = 0;
			string tempNameProduct = TextBoxNameProduct.Text;
			if (!testName(TextBoxNameProduct)) codeFailed++;
			string tempBrand = ComboBrand.Text;
			string tempCategory = ComboCategory.Text;
			string tempCondition = ComboCondition.Text;
			string tempTypeWarranty = ComboWarranty.Text;
			int tempDayWarranty = testNumeric(TextBoxDaysWarranty);
			if (tempDayWarranty == -1)
			{
				MessageBox.Show("Zła liczba podana czasu Gwarancji");
				codeFailed++;
			}
			string tempVAT = ComboVAT.Text;
			tempVAT = tempVAT.Substring(0, tempVAT.Length - 1);
			int tempVAT2 = int.Parse(tempVAT);
			decimal tempNetto;
			if (!decimal.TryParse(TextBoxNetto.Text, out tempNetto))
			{
				MessageBox.Show("Zły czas dostawy");
				TextBoxNetto.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxNetto.Background = System.Windows.Media.Brushes.Green;
			}


			int tempQuantity = testNumeric(TextBoxQuantity);
			if (tempQuantity == -1)
			{
				MessageBox.Show("Zła liczba podana ilości");
				codeFailed++;
			}
			string tempOptionalName = TextBoxOptionalName.Text;
			string tempOptionalDescription = TextBoxOptionalDescription.Text;
			string tempDescription = TextBoxDescription.Text;

			if (codeFailed == 0)
			{
				int tempId = (int)actualProduct[0];
				int tempUser = Users.LogUser.Id_User;
				int tempStatus = (int)actualProduct[14];
				sqlConnect.UpdateProduct2(tempId, tempUser, tempNameProduct, tempDescription, tempNetto,
									tempVAT2, tempCondition, tempQuantity, tempOptionalName,
									tempOptionalDescription, tempTypeWarranty, tempDayWarranty,
									tempBrand, tempCategory, tempImageData, tempStatus, "UpdateProduct2");
				MessageBox.Show("Udało się dodać nowy produkt");
				this.Close();
			}
		}

		private void NewButton_Click(object sender, RoutedEventArgs e)
		{
			int codeFailed = 0;
			string tempNameProduct = TextBoxNameProduct.Text;
			if (!testName(TextBoxNameProduct)) codeFailed++;
			string tempBrand = ComboBrand.Text;
			string tempCategory = ComboCategory.Text;
			string tempCondition = ComboCondition.Text;
			string tempTypeWarranty = ComboWarranty.Text;
			int tempDayWarranty = testNumeric(TextBoxDaysWarranty);
			if (tempDayWarranty == -1)
			{
				MessageBox.Show("Zła liczba podana czasu Gwarancji");
				codeFailed++;
			}
			string tempVAT = ComboVAT.Text;
			tempVAT = tempVAT.Substring(0, tempVAT.Length - 1);
			int tempVAT2 = int.Parse(tempVAT);
			decimal tempNetto;
			if (!decimal.TryParse(TextBoxNetto.Text, out tempNetto))
			{
				MessageBox.Show("Zły czas dostawy");
				TextBoxNetto.Background = System.Windows.Media.Brushes.Red;
				codeFailed++;
			}
			else
			{
				TextBoxNetto.Background = System.Windows.Media.Brushes.Green;
			}


			int tempQuantity = testNumeric(TextBoxQuantity);
			if (tempQuantity == -1)
			{
				MessageBox.Show("Zła liczba podana ilości");
				codeFailed++;
			}
			string tempOptionalName = TextBoxOptionalName.Text;
			string tempOptionalDescription = TextBoxOptionalDescription.Text;
			string tempDescription = TextBoxDescription.Text;

			if (codeFailed == 0)
			{
				int tempUser = Users.LogUser.Id_User;
				sqlConnect.AddUserProduct(tempUser, tempNameProduct, tempDescription, tempNetto,
									tempVAT2, tempCondition, tempQuantity, tempOptionalName,
									tempOptionalDescription, tempTypeWarranty, tempDayWarranty,
									tempBrand, tempCategory, tempImageData, "AddUserProduct");
				MessageBox.Show("Udało się dodać nowy produkt");
				this.Close();
			}
		}

		private void OnlyNumeric(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+,");
			e.Handled = regex.IsMatch(e.Text);
		}

		private bool testName(TextBox tempTextBox)
		{
			string tempString = tempTextBox.Text;
			if (tempString.Length < 2)
			{
				MessageBox.Show("Złe " + nameTextBox(tempTextBox.Name));
				tempTextBox.Background = System.Windows.Media.Brushes.Red;
				return false;
			}
			else
			{
				tempTextBox.Background = System.Windows.Media.Brushes.Green;
				return true;
			}
		}

		private int testNumeric(TextBox tempTextBox)
		{
			if (!int.TryParse(tempTextBox.Text, out int temp))
			{
				MessageBox.Show("Zły czas dostawy");
				tempTextBox.Background = System.Windows.Media.Brushes.Red;
				return -1;
			}
			else
			{
				tempTextBox.Background = System.Windows.Media.Brushes.Green;
				return temp;
			}
		}
		private void MakeBrutto(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			decimal tempPrice, tempBrutto;
			decimal.TryParse(TextBoxNetto.Text, out tempPrice);
			string tempVAT = ComboVAT.Text;
			tempVAT = tempVAT.Substring(0, tempVAT.Length - 1);
			decimal tempVAT2 = int.Parse(tempVAT);
			tempVAT2 /= 100;
			tempVAT2 += 1;
			tempBrutto = tempPrice * tempVAT2;
			tempBrutto = Math.Round(tempBrutto, 2);
			TextBoxBrutto.Text = tempBrutto.ToString();
		}

		private string nameTextBox(string temp)
		{
			return temp switch
			{
				"TextBoxNameProduct" => "Nazwa Produktu",
				"TextBoxOptionalName" => "Opcjonalny Parametr",
				"TextBoxOptionalDescription" => "Opcjonalny opis",
				_ => "",
			};
		}

		private void NameChange(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			TextBox temp = sender as TextBox;
			if (temp.Text.Length < 1)
				temp.CharacterCasing = CharacterCasing.Upper;
			else
			{
				temp.CharacterCasing = CharacterCasing.Lower;
			}
		}

		private void Rating_Click(object sender, RoutedEventArgs e)
		{
			if (windowRating.IsVisible == false)
			{
				int tempId;
				if (actualProduct != null) tempId = (int)actualProduct[0];
				else
				{
					tempId = currentProduct.Id_Product;
				}
				windowRating = new WindowRating(tempId)
				{
					WindowStartupLocation = WindowStartupLocation.CenterScreen
				};
				windowRating.Show();
			}
		}
	}
}

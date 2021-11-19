using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
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
		private List<string> tempListDelivery;
		private List<string> tempListWarranty;
		private byte[] tempImageData;

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
		private List<string> TempListDelivery
		{
			get => tempListDelivery;
		}

		private List<string> TempListWarranty
		{
			get => tempListWarranty;
		}



		public NewProductWindow()
		{
			InitializeComponent();
			makeList();
			FillingDate();
			defaultImage();
		}

		private void makeList()
		{
			tempListVat = new List<string>();
			tempListCondition = new List<string>();
			tempListBrand = new List<string>();
			tempListCategory = new List<string>();
			tempListDelivery = new List<string>();
			tempListWarranty = new List<string>();

			ComboVAT.ItemsSource = tempListVat;
			ComboBrand.ItemsSource = tempListBrand;
			ComboCondition.ItemsSource = tempListCondition;
			ComboCategory.ItemsSource = tempListCategory;
			ComboDelivery.ItemsSource = tempListDelivery;
			ComboWarranty.ItemsSource = tempListWarranty;
		}


		public void FillingDate()
		{
			foreach (DataRow row in sqlConnect.ReadTable("ValuesVat").Rows)
			{
				tempListVat.Add(row[0].ToString() + "%");
			}
			ComboVAT.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesCondition").Rows)
			{
				tempListCondition.Add(row[0].ToString());
			}
			ComboCondition.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesBrand").Rows)
			{
				tempListBrand.Add(row[0].ToString());
			}
			ComboBrand.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesCategory").Rows)
			{
				tempListCategory.Add(row[0].ToString());
			}
			ComboCategory.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesDelivery").Rows)
			{
				tempListDelivery.Add(row[0].ToString());
			}
			ComboDelivery.SelectedIndex = 0;

			foreach (DataRow row in sqlConnect.ReadTable("ValuesWarranty").Rows)
			{
				tempListWarranty.Add(row[0].ToString());
			}
			ComboWarranty.SelectedIndex = 0;





		}

		/// <summary>
		/// Convert Byte[] to BitmapImage
		/// </summary>
		/// <param name="imageByteArray"></param>
		/// <returns></returns>
		private BitmapImage ConvertByteToImage(byte[] imageByteArray)
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

		private void defaultImage() 
		{
			//todo domyślne zrobić lub brak
		}

		private void ButtonAddImage_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog OpenDialog = new OpenFileDialog();
			if (OpenDialog.ShowDialog() == true)
			{
				tempImageData = File.ReadAllBytes(OpenDialog.FileName);

			}
			ImageProduct.Source= ConvertByteToImage(tempImageData);

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}

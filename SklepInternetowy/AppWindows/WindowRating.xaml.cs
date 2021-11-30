using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
	/// Logika interakcji dla klasy WindowRating.xaml
	/// </summary>
	public partial class WindowRating : Window
	{
		private int currentIdProduct;
		private int currentIdUser;
		private object[] currentComment;
		private SQLConnect sqlConnect;
		private WindowProduct windowProduct;

		public WindowRating()
		{

		}

		public WindowRating(int valueIdProduct)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			DataGridRating.Visibility = Visibility.Visible;
			HiddenElements();
			FillingDataGrid();
		}

		public WindowRating(WindowProduct tempWindowProduct, int valueIdProduct, int valueIdUser)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			currentIdUser = valueIdUser;
			windowProduct = tempWindowProduct;
			DataGridRating.Visibility = Visibility.Hidden;
			ShowElements();
			tempWindowProduct.IsEnabled = false;
			ButtonComment.Click -= ButonEditComment_Click;
			ButtonComment.Click += ButtonAddComment_Click;
		}

		public WindowRating(WindowProduct tempWindowProduct, int valueIdProduct, int valueIdUser, object[] tempComment)
		{
			InitializeComponent();
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			currentIdUser = valueIdUser;
			windowProduct = tempWindowProduct;
			DataGridRating.Visibility = Visibility.Hidden;
			ShowElements();
			tempWindowProduct.IsEnabled = false;
			currentComment = tempComment;
			ButtonComment.Click -= ButtonAddComment_Click;
			ButtonComment.Click += ButonEditComment_Click;
		}

		private void HiddenElements()
		{
			LabelComment.Visibility = Visibility.Hidden;
			LabelStars.Visibility = Visibility.Hidden;
			LabelStatus.Visibility = Visibility.Hidden;
			LabelAccept.Visibility = Visibility.Hidden;
			TextBlockComment.Visibility = Visibility.Hidden;
			CheckBoxStatus.Visibility = Visibility.Hidden;
			ButtonComment.Visibility = Visibility.Hidden;
		}

		private void ShowElements()
		{
			LabelComment.Visibility = Visibility.Visible;
			LabelStars.Visibility = Visibility.Visible;
			LabelStatus.Visibility = Visibility.Visible;
			LabelAccept.Visibility = Visibility.Visible;
			TextBlockComment.Visibility = Visibility.Visible;
			CheckBoxStatus.Visibility = Visibility.Visible;
			ButtonComment.Visibility = Visibility.Visible;
		}

		private void FillingDataGrid()
		{
			DataTable tempTable = sqlConnect.ShowRating(currentIdProduct, "ViewProductRating");
			DataGridRating.ItemsSource = tempTable.DefaultView;
		}

		private void dataGridRating_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "Id_User":
				case "Id_Comment":
					e.Column.Visibility = Visibility.Hidden;
					break;
				case "Comment":
					e.Column.Header = "Komentarz";
					break;
			}
		}

		private void ButtonAddComment_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ButonEditComment_Click(object sender, RoutedEventArgs e)
		{
		}


		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (windowProduct != null) windowProduct.IsEnabled = true;
		}
	}
}

using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

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
		private string tempStars;
		private int currentIdComment;

		public WindowRating()
		{
		}

		public WindowRating(int valueIdProduct)
		{
			InitializeComponent();
			Title = "Lista komentarzy";
			tempStars = TextBoxStars.Text;
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			DataGridRating.Visibility = Visibility.Visible;
			HiddenElements();
			FillingDataGrid();
		}

		public WindowRating(int valueIdProduct, int valueIdUser)
		{
			InitializeComponent();
			Title = "Dodaj komentarz";
			tempStars = TextBoxStars.Text;
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			currentIdUser = valueIdUser;
			DataGridRating.Visibility = Visibility.Hidden;
			ShowElements();
			ButtonComment.Click -= ButonEditComment_Click;
			ButtonComment.Click += ButtonAddComment_Click;

		}

		public WindowRating(int valueIdProduct, int valueIdUser, object[] tempComment)
		{
			InitializeComponent();
			Title = "Edytuj komentarz";
			tempStars = TextBoxStars.Text;
			sqlConnect = new SQLConnect();
			currentIdProduct = valueIdProduct;
			currentIdUser = valueIdUser;
			DataGridRating.Visibility = Visibility.Hidden;
			ShowElements();
			currentComment = tempComment;
			ButtonComment.Click -= ButtonAddComment_Click;
			ButtonComment.Click += ButonEditComment_Click;
			CheckBoxStatus.IsEnabled = true;
			currentIdComment = (int)tempComment[0];
			currentIdProduct = (int)tempComment[1];
			TextBoxComment.Text = (string)tempComment[2];
			TextBoxComment.ToolTip = "Stary komentarz wyglądał tak= \n" + (string)tempComment[2];
			TextBoxStars.Text = tempStars + (Int16)tempComment[3];
			TextBoxStars.ToolTip = "Stara ocena=" + (Int16)tempComment[3];
			CheckBoxStatus.IsChecked = (int)tempComment[4] == 0 ? true : false;
		}

		private void HiddenElements()
		{
			LabelComment.Visibility = Visibility.Hidden;
			LabelStars.Visibility = Visibility.Hidden;
			LabelStatus.Visibility = Visibility.Hidden;
			LabelAccept.Visibility = Visibility.Hidden;
			TextBoxComment.Visibility = Visibility.Hidden;
			CheckBoxStatus.Visibility = Visibility.Hidden;
			ButtonComment.Visibility = Visibility.Hidden;
			CheckBox1.Visibility = Visibility.Hidden;
			CheckBox2.Visibility = Visibility.Hidden;
			CheckBox3.Visibility = Visibility.Hidden;
			CheckBox4.Visibility = Visibility.Hidden;
			CheckBox5.Visibility = Visibility.Hidden;
			TextBoxStars.Visibility = Visibility.Hidden;
		}

		private void ShowElements()
		{
			LabelComment.Visibility = Visibility.Visible;
			LabelStars.Visibility = Visibility.Visible;
			LabelStatus.Visibility = Visibility.Visible;
			LabelAccept.Visibility = Visibility.Visible;
			TextBoxComment.Visibility = Visibility.Visible;
			CheckBoxStatus.Visibility = Visibility.Visible;
			ButtonComment.Visibility = Visibility.Visible;
			CheckBox1.Visibility = Visibility.Visible;
			CheckBox2.Visibility = Visibility.Visible;
			CheckBox3.Visibility = Visibility.Visible;
			CheckBox4.Visibility = Visibility.Visible;
			CheckBox5.Visibility = Visibility.Visible;
			TextBoxStars.Visibility = Visibility.Visible;
		}

		private void FillingDataGrid()
		{
			DataTable tempTable = sqlConnect.ShowRating(currentIdProduct, "ViewProductRating");
			DataGridRating.ItemsSource = tempTable.DefaultView;
		}


		private void ButtonAddComment_Click(object sender, RoutedEventArgs e)
		{
			string tempComment = TextBoxComment.Text;
			string tempStar = TextBoxStars.Text;
			Int16 tempNumberStar = Int16.Parse(tempStar[tempStar.Length - 1].ToString());
			int tempIdUser = Users.LogUser.Id_User;
			sqlConnect.AddComment(tempComment, tempNumberStar, tempIdUser, currentIdProduct, "AddComment");
			MessageBox.Show("Udało się dodać komentarz");
			this.Close();
		}

		private void ButonEditComment_Click(object sender, RoutedEventArgs e)
		{
			int tempIdComment = 0;
			string tempComment = TextBoxComment.Text;
			string tempStar = TextBoxStars.Text;
			Int16 tempNumberStar = Int16.Parse(tempStar[tempStar.Length - 1].ToString());
			int tempStatus = CheckBoxStatus.IsChecked == true ? 1 : 0;
			sqlConnect.UpdateComment(tempIdComment, tempComment, tempNumberStar, tempStatus, "UpdateComment");
			MessageBox.Show("Udało się zmienić komentarz");
			this.Close();
		}

		private void CheckBoxStars_Checked(object sender, RoutedEventArgs e)
		{
			CheckBox tempCheckBox = sender as CheckBox;
			if (tempCheckBox.Name != "CheckBoxStatus")
			{
				string tempName = tempCheckBox.Name;
				int tempLocation = tempName.Length - 1;
				int tempNumeric = int.Parse(tempName[tempLocation].ToString());
				string tempStars2 = tempStars;

				switch (tempNumeric)
				{
					case 1:
						tempStars2 += 1;
						break;
					case 2:
						tempStars2 += 2;
						CheckBox1.IsChecked = true;
						break;
					case 3:
						tempStars2 += 3;
						CheckBox1.IsChecked = true;
						CheckBox2.IsChecked = true;
						break;
					case 4:
						tempStars2 += 4;
						CheckBox1.IsChecked = true;
						CheckBox2.IsChecked = true;
						CheckBox3.IsChecked = true;
						break;
					case 5:
						tempStars2 += 5;
						CheckBox1.IsChecked = true;
						CheckBox2.IsChecked = true;
						CheckBox3.IsChecked = true;
						CheckBox4.IsChecked = true;
						break;
					default:
						break;
				}
				TextBoxStars.Text = tempStars2;
			}
		}

		private void CheckBoxStars_Unchecked(object sender, RoutedEventArgs e)
		{
			CheckBox tempCheckBox = sender as CheckBox;
			if (tempCheckBox.Name != "CheckBoxStatus")
			{
				string tempName = tempCheckBox.Name;
				int tempLocation = tempName.Length - 1;
				int tempNumeric = int.Parse(tempName[tempLocation].ToString());
				string tempStars2 = tempStars;
				switch (tempNumeric)
				{
					case 1:
						CheckBox2.IsChecked = false;
						CheckBox3.IsChecked = false;
						CheckBox4.IsChecked = false;
						CheckBox5.IsChecked = false;
						tempStars2 += 0;
						break;
					case 2:
						CheckBox3.IsChecked = false;
						CheckBox4.IsChecked = false;
						CheckBox5.IsChecked = false;
						tempStars2 += 1;
						break;
					case 3:
						CheckBox4.IsChecked = false;
						CheckBox5.IsChecked = false;
						tempStars2 += 2;
						break;
					case 4:
						CheckBox5.IsChecked = false;
						tempStars2 += 3;
						break;
					case 5:
						tempStars2 += 4;
						break;
					default:
						break;

				}
				TextBoxStars.Text = tempStars2;
			}
		}
	}
}

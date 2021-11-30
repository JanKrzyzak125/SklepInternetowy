using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SklepInternetowy.AppWindows
{
	/// <summary>
	/// Class of window Admin
	/// </summary>
	public partial class AdminPanel : Window
	{
		private List<string> tempListTabs;
		private List<string> tempListViews;

		private SQLConnect sqlConnect;
		private bool isChangeDataGridAdmin;
		private string currentTab;
		private DataTable currentDataTable;
		private int oldTabCount;

		public List<string> TempListTabs
		{
			get => tempListTabs;
		}

		public List<string> TempListViews
		{
			get => tempListViews;
		}

		public AdminPanel()
		{

			InitializeComponent();

			isChangeDataGridAdmin = false;
			sqlConnect = new SQLConnect();

			tempListTabs = sqlConnect.listTables();
			ComboBoxTabs.ItemsSource = tempListTabs;

			tempListViews = sqlConnect.listViews();
			ComboBoxViews.ItemsSource = tempListViews;

		}

		public void ChangeCommandClick(object sender, RoutedEventArgs e)
		{
			if (isChangeDataGridAdmin)
			{
				SelectUpdateDataBase();
				isChangeDataGridAdmin = false;
			}

			currentTab = ComboBoxTabs.Text;
			string tempCommand = "values" + currentTab;
			currentDataTable = sqlConnect.ReadTable(tempCommand);
			oldTabCount = currentDataTable.Rows.Count;
			DataGridAdmin.ItemsSource = currentDataTable.DefaultView;
		}

		private void ChangeViewsClick(object sender, RoutedEventArgs e)
		{
			string tempCommand = "View" + ComboBoxViews.Text;
			DataGridAdmin.ItemsSource = sqlConnect.ReadTable(tempCommand).DefaultView;
		}

		private void DataGridAdminCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			isChangeDataGridAdmin = true;
		}

		private void DataGridAdminMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int temp = DataGridAdmin.SelectedIndex;
			MessageBox.Show("Element zawiera=" + DataGridAdmin);
			//TODO: dla niektórych widoku pokazać szczegóły jak produkty,Faktury

		}

		private void SelectUpdateDataBase()
		{
			switch (currentTab)
			{
				case "Users":
					UpdateUsers();
					break;

				case "ProductsBuyed":
					UpdateProductsBuyed();
					break;

				case "Payment":
					UpdatePayment();
					break;
				case "Company":
					UpdateCompany();
					break;

				case "Comment":
					UpdateComment();
					break;

				case "RetailSales":
					UpdateRetailSales();
					break;

				case "Transation":
					UpdateTransation();
					break;

				case "Product":
					UpdateProduct();
					break;

				case "Invoice":
					UpdateInvoice();
					break;

				case "TypePayment":
					UpdateTypePayment();
					break;

				case "UserPermission":
					AddUserPermission();
					break;
				case "UsersPayment":
					UpdateUsersPayment();
					break;
				case "Rating":
					UpdateRating();
					break;
				default:
					UpdateDataBase();
					break;
			}
		}

		private void AddUserPermission()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}
			int i = 0;

			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i >= oldTabCount)
				{
					sqlConnect.AddUserPermission((string)item[0], (string)item[1], "Add" + currentTab);
				}
				i++;
			}
		}

		private void UpdateRetailSales()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateRetailSales(i, (int)item[0], (int)item[1], (DateTime)item[2],
									(DateTime)item[3], (DateTime)item[4], (int)item[5], (int)item[6],
									(int)item[7], (int)item[8], (int)item[9], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać Sprzedaży, możesz w panelu użytkownika");
				}
				i++;
			}
		}

		private void UpdateUsers()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateUser(i, (Int16)item[1], (string)item[0], (string)item[2],
									(string)item[3], (string)item[4], (int)item[5], (string)item[6],
									(string)item[7], (int)item[8], item[9], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać uzytkowników");
				}
				i++;
			}
		}

		private void UpdateProduct()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateProduct(i, (int)item[0], (string)item[1], (string)item[2], (decimal)item[3],
											(int)item[4], (int)item[5], (int)item[6], (string)item[7],
											(string)item[8], (int)item[9], (int)item[10], (int)item[11],
											(int)item[12], (byte[])item[13], (int)item[14], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać Produktów, możesz dodać w zakładce panel użytkownika");
				}
				i++;
			}
		}

		private void UpdatePayment()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdatePaymentAdmin(i, (int)item[0], (string)item[1], (string)item[2], (int)item[3],
											 "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać płatności");
				}
				i++;
			}
		}


		private void UpdateCompany()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateCompany(i, item[0].ToString(), (string)item[1], (string)item[2], (int)item[3]
											 , (string)item[4], (string)item[5], (int)item[6], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać firm");
				}
				i++;
			}

		}

		private void UpdateComment()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateComment(i, item[0].ToString(), (Int16)item[1], (int)item[3], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie może dodawać Komentarzy");
				}
				i++;
			}
		}

		private void UpdateInvoice()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateInvoice(i, (DataSetDateTime)item[1], (DataSetDateTime)item[2], (DataSetDateTime)item[3],
											(int)item[4], (int)item[5], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administartor nie moze dodawać nowych Faktur");
				}
				i++;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void UpdateTypePayment()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.Update(i, item[0].ToString(), (int)item[1], (int)item[2], "Update" + currentTab);
				}
				else
				{
					sqlConnect.Add(item[0].ToString(), (int)item[1], "Add" + currentTab);
				}
				i++;
			}
		}
		private void UpdateProductsBuyed()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateProductsBuyed(i, (int)item[0], (int)item[1], (int)item[2], (int)item[3], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administrator nie może dodawać nowych zakupionych produktów");
				}
				i++;
			}
		}

		private void UpdateTransation()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.UpdateTransation(i, (int)item[0], (int)item[1], (decimal)item[2], (int)item[3], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administrator nie może dodawać nowych zakupionych produktów");
				}
				i++;
			}
		}



		/// <summary>
		/// 
		/// </summary>
		private void UpdateDataBase()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{

					if (item[0] is string)
					{
						sqlConnect.Update(i, item[0].ToString(), (int)item[1], "Update" + currentTab);
					}
					else if (item[0] is int)
					{
						sqlConnect.Update(i, (int)item[0], (int)item[1], "Update" + currentTab);
					}

				}
				else
				{
					if (item[0] is string)
					{
						sqlConnect.Add(item[0].ToString(), "Add" + currentTab);
					}
					else if (item[0] is int)
					{
						sqlConnect.Add((int)item[0], "Add" + currentTab);
					}

				}
				i++;
			}
		}

		private void UpdateRating()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.Update((int)item[1], (string)item[3], (int)item[4], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administrator nie może dodawać nowych komenatrzy/ocen");
				}
				i++;
			}
		}

		private void UpdateUsersPayment()
		{
			DataView tempChange = DataGridAdmin.ItemsSource as DataView;
			DataTable tempDataTable = tempChange.ToTable();
			List<object> tempListObjects = new List<object>();

			foreach (DataRow item in tempDataTable.Rows)
			{
				tempListObjects.Add(item.ItemArray);
			}

			int i = 0;
			foreach (object[] item in tempListObjects.ToArray())
			{
				if (i < oldTabCount)
				{
					sqlConnect.Update((int)item[1], (string)item[2], (string)item[4], (int)item[5], "Update" + currentTab);
				}
				else
				{
					MessageBox.Show("Administrator nie może dodawać nowych płatności użytkownikowi");
				}
				i++;
			}
		}

		private void WindowAdminClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (isChangeDataGridAdmin)
			{
				SelectUpdateDataBase();
				isChangeDataGridAdmin = false;
			}
		}
	}
}

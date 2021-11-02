using SklepInternetowy.Classes;
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
                    
                    break;
                    
                case "Company":
                    
                    break;
                case "RetailSales":

                    break;
                case "Transation":

                    break;
                case "Product":

                    break;

                case "Invoice":

                    break;

                case "TypePayment":
                    UpdateTypePayment();
                    break;
                default:
                    UpdateDataBase();
                    break;
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

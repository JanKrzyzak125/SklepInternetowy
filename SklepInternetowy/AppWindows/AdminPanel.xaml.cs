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
        private List<object> tempListShow;
        private List<string> tempListViews;

        private SQLConnect sqlConnect;
        private bool isChangeDataGridAdmin;
        private string currentTab;
        private DataTable oldTable;
        private DataTable currentDataTable;

        private List<string> tempListCommands = new List<string>();
        public List<string> TempListTabs
        {
            get => tempListTabs;
        }

        public List<object> TempListShow
        {
            get => tempListShow;
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

            tempListShow = new List<object>();
            tempListShow = sqlConnect.ReadTable2(tempCommand);
            currentDataTable = sqlConnect.ReadTable(tempCommand);
            oldTable = currentDataTable.Copy();
            DataGridAdmin.ItemsSource = currentDataTable.DefaultView;
            ComboBoxShow.SelectedIndex = 0;
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

        }

        private void SelectUpdateDataBase()
        {
            switch (currentTab)
            {
                case "Users":
                case "Company":
                    MessageBox.Show("Nie możesz zmieniać tabeli "+currentTab);
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
                if (i < tempListShow.Count)
                {
                    if (!item.Equals(tempListShow[i]))
                    {
                        sqlConnect.Update(i, item[0].ToString(), (int)item[1], "Update" + currentTab);
                    }
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
                if (i < tempListShow.Count)
                {
                    if (!item.Equals(tempListShow[i]))
                    {
                        if (item[0] is string)
                        {
                            if (item[0].Equals(""))
                            {
                                if(sqlConnect.IsCanBeDeleted(i, "IsCanBeDeleted" + currentTab) == 1)
                                    sqlConnect.Delete(i,"Delete"+currentTab);
                            }
                            else
                            {
                                sqlConnect.Update(i, item[0].ToString(), "Update" + currentTab);
                            }
                        }
                        else if (item[0] is int)
                        {
                            sqlConnect.Update(i, (int)item[0], "Update" + currentTab);
                        }
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

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
        private List<string> tempListShow;
        private List<string> tempListViews;
        private SQLConnect sqlConnect;

        public List<string> TempListTabs
        {
            get => tempListTabs;
        }

        public List<string> TempListShow
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
            sqlConnect = new SQLConnect();

            tempListTabs = sqlConnect.listTables();
            ComboBoxTabs.ItemsSource = tempListTabs;

            tempListViews = sqlConnect.listViews();
            ComboBoxViews.ItemsSource = tempListViews;
        }

        public void ChangeCommandClick(object sender, RoutedEventArgs e)
        {
            tempListShow = new List<string>();
            string tempCommand = TextBoxCommand.Text+ ComboBoxTabs.Text;
            foreach (DataRow row in sqlConnect.ReadTable(tempCommand).Rows)
            {
                tempListShow.Add(row[0].ToString());
            }
            ComboBoxShow.ItemsSource = tempListShow;
            DataGridAdmin.ItemsSource = sqlConnect.ReadTable(tempCommand).DefaultView;
            ComboBoxShow.SelectedIndex = 0;
            
        }

        private void ChangeViewsClick(object sender, RoutedEventArgs e) 
        {
            string tempCommand = TextBoxCommand.Text + ComboBoxViews.Text;
            DataGridAdmin.ItemsSource = sqlConnect.ReadTable(tempCommand).DefaultView;
        }

    }
}

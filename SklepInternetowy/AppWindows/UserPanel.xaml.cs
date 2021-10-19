using SklepInternetowy.AppWindows;
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

namespace SklepInternetowy
{
    /// <summary>
    /// Logika interakcji dla klasy UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        private WindowProduct windowProducts;
        private List<string> tempListViews;
        private SQLConnect sqlConnect;
        

        public List<string> TempListViews
        {
            get => tempListViews;
        }

        public UserPanel()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();
            AddListViews();
        }

        public void AddListViews()
        {
            tempListViews = new List<string>();
            ComboBoxViews.ItemsSource = tempListViews;

            tempListViews.Add("Widok zakupionych produktów");
            tempListViews.Add("Widok dodanych produktów");
            tempListViews.Add("Widok aktualnych sprzedaży");
            tempListViews.Add("Widok zakończonych sprzedaży");
            tempListViews.Add("Widok kupionych produktów");

        }

        //TODO w jaki sposób zmieniać nazwy taki czy w skrypcie?
        void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.PropertyName) 
            {
                case "Name":
                    e.Column.Header = "Nazwa";
                    break;
                case "Description":
                    e.Column.Header = "Opis";
                    break;

            }
        }

        private void ClickChangeViews(object sender, RoutedEventArgs e)
        {
            string tempViews = ComboBoxViews.SelectedItem.ToString();
            DataTable tempTable;
            switch (tempViews)
            {
                case "Widok zakupionych produktów":

                    break;
                case "Widok dodanych produktów":
                    tempTable =sqlConnect.ShowProduct(0, "ViewUsersProducts");
                    UsersDataGrid.ItemsSource = tempTable.DefaultView;

                    break;
                case "Widok aktualnych sprzedaży":

                    break;
                case "Widok zakończonych sprzedaży":

                    break;
                case "Widok kupionych produktów":

                    break;
                default:
                    break;
            }
        }

        private void DoubleClickUsersDataGrid(object sender, RoutedEventArgs e)
        {
            windowProducts = new WindowProduct();
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow();
            newProductWindow.Show();
        }

    }
}

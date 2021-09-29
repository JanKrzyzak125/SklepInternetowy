using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.Data.SqlClient;
using System.Data;
using SklepInternetowy.AppWindows;
using System.Security.Cryptography;

namespace SklepInternetowy
{
    /// <summary>
    /// MainWindow 
    /// </summary>
    public partial class MainWindow : Window
    {
        private string textSearch;
        private Authentication windowAuthentication;
        private SQLConnect sqlConnect;
        private UserPanel userPanel;
        private AdminPanel adminPanel;
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            windowAuthentication = new Authentication();
            userPanel = new UserPanel();
            sqlConnect = new SQLConnect();
            adminPanel = new AdminPanel();

            //TODO: PRZENIEŚĆ WSZYSTKIE DODAWANIA DO ADMINISTARTORA POZA DODAWANIEM PRODUKTU 
            //sqlConnect.ShowProduct(this,15); //zmienić całkowicie metodę
            //  sqlConnect.AddImage();//dodawanie zdjęcia do produktu TODO: przenieść oraz dodać do innego skryptu patrz dodawanie produkut
            // sqlConnect.Add(26,"AddVat"); //dodawanie wartości VAT TODO: przenieść  
            //sqlConnect.Add("lata","AddWarranty");
            // sqlConnect.Add("Zegarki","AddCategory");
            // sqlConnect.Add("Po naprawie","AddCondition"); 
            //sqlConnect.Delete(3, "DeleteBrand");
            //sqlConnect.Add("Genesis","AddBrand");
            //sqlConnect.Add("EA", "AddBrand");
            //sqlConnect.Add("Intel", "AddBrand");
            //sqlConnect.Add("Konto bankowe",26,"AddTypePayment");

            sqlConnect.AddUser("125Kowalski", makeHash("test"), "Jan", "Kowalski", "pa@wp.pl", "8888888", "Zielona 14a", "Opole", "AddUser");

        }

        byte[] makeHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                return bytes;
            }
        }

        /// <summary>
        /// Open Window Authentication
        /// </summary>
        /// 
        /// <returns>void</returns>
        private void Log_Open(object sender, RoutedEventArgs e)
        {
            if (windowAuthentication.IsVisible == false)
            {
                windowAuthentication = new Authentication();
                windowAuthentication.Show();
            }

        }

        /// <summary>
        /// Search products by name 
        /// </summary>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            textSearch = TextSearch.Text;
            //TODO: zrobić wyszukiwanie produktów
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            this.Close();
            sqlConnect.Con.Close();
            Application.Current.Shutdown();
        }

        private void Admin_Open(object sender, RoutedEventArgs e)
        {
            if (adminPanel.IsVisible == false)
            {
                adminPanel = new AdminPanel();
                adminPanel.Show();
            }
        }

        private void MenuUser_Click(object sender, RoutedEventArgs e)
        {
            if (userPanel.IsVisible == false)
            {
                userPanel = new UserPanel();
                userPanel.Show();
            }
        }

        private void CloseApp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

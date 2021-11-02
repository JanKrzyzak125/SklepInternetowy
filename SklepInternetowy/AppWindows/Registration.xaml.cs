using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Authentication windowAuthentication;
        private SQLConnect sqlConnect;

        public Registration(Authentication authentication)
        {
            InitializeComponent();
            windowAuthentication = authentication;
            sqlConnect = new SQLConnect();
        }

        /// <summary>
        /// Click to verification date and making new account to database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            int codeFailed=0;
            string tempNick = TextBoxNick.Text;

            string tempHash1 = PasswordBox1.Password;
            string tempHash2 = PasswordBox2.Password;
            
            

            string tempName = TextBoxName.Text;
            string tempSurname= TextBoxSurname.Text;

            string tempEmail = TextBoxEmail.Text;

            int tempPhone;
            if (!int.TryParse(TextBoxPhone.Text, out tempPhone))
            {
             
            }

            string tempAdress = TextBoxAdress.Text;
            string tempCity = TextBoxCity.Text;

            if (tempHash1.Equals(tempHash2) && (codeFailed==0))
            {
                byte[] tempHash = windowAuthentication.makeHash(tempHash1);
                sqlConnect.AddUser(tempNick, tempHash, tempName, tempSurname, tempEmail, tempPhone,
                              tempAdress, tempCity, "AddUser");

                MessageBox.Show("Udalo się zajestrować");
                this.Close();
            }

        }
    }
}

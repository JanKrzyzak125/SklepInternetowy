using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SklepInternetowy
{
    /// <summary>
    /// Logika interakcji dla klasy Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        private Registration registrationWindow;
        private SQLConnect sqlConnect;
        public Authentication()
        {
            InitializeComponent();
            registrationWindow = new Registration(this);
            sqlConnect = new SQLConnect();
        }

        /// <summary>
        /// This methods send SQLTaks to verify identity
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string tempNick=NickText.Text;
            byte[] tempHash= makeHash(PasswordBox.Password);
            object[] tempUsers=sqlConnect.VerLogin(tempNick,tempHash,"Login");
            if (tempUsers != null)
            {
                Users tempUser = new Users(tempUsers);
                MessageBox.Show("Udało się zalogować użytkownikowi" + Users.LogUser.Nick);
                this.Close();
            }
            else
            {
                MessageBox.Show("Spróbuj ponownie wpisać dane");
            }
        }

        public byte[] makeHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                return bytes;
            }
        }

        /// <summary>
        /// Open Window Registration
        /// </summary>
        private void Registration_Open(object sender, RoutedEventArgs e)
        {
            if(registrationWindow.IsVisible == false)
            {
                registrationWindow = new Registration(this);
                registrationWindow.Show();
            }
        }
    }
}

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
    /// Logika interakcji dla klasy Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        private Registration _registrationWindow;
        public Authentication()
        {
            InitializeComponent();
            _registrationWindow = new Registration();
        }

        /// <summary>
        /// This methods send SQLTaks to verify identity
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(NickText.Text);

        }
        /// <summary>
        /// Open Window Registration
        /// </summary>
        private void Registration_Open(object sender, RoutedEventArgs e)
        {
            if(_registrationWindow.IsVisible == false)
            {
                _registrationWindow = new Registration();
                _registrationWindow.Show();
            }
        }
    }
}

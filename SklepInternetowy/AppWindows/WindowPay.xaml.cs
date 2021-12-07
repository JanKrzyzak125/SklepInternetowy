using SklepInternetowy.Classes;
using System.Windows;

namespace SklepInternetowy
{
    /// <summary>
    /// Logika interakcji dla klasy WindowPay.xaml
    /// </summary>
    public partial class WindowPay : Window
    {
        private Product currentProduct;
        private int currentSelectQuantity;
        
        public WindowPay()
        {
          
        }

        public WindowPay(Product product,int valueSelectQuantity)
		{
            InitializeComponent();
            currentProduct = product;
            currentSelectQuantity = valueSelectQuantity;

        }
    }
}

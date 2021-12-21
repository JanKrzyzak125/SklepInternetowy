using SklepInternetowy.Classes;
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

namespace Sklep.AppWindows
{
	/// <summary>
	/// Logika interakcji dla klasy WindowInvoice.xaml
	/// </summary>
	public partial class WindowInvoice : Window
	{
		private object[] currentSeller;
	 	private Product currentProduct;
		private object[] currrentInvoice;

		public WindowInvoice()
		{
			InitializeComponent();
		}

		public WindowInvoice(object[] tempSellerUser, Product product, object[] invoice) 
		{
			InitializeComponent();
			currentSeller = tempSellerUser;
			currrentInvoice = invoice;

		}

		public WindowInvoice(int invoice)
		{
			InitializeComponent();
		}
	}
}

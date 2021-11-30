using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SklepInternetowy.Classes
{
	class Product
	{
		public Product(object[] tempProduct)
		{
			TempProduct = tempProduct;
		}

		public object[] TempProduct { get; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SklepInternetowy.Classes
{
	public class Product
	{
		private int id_Product;
		private int id_Seller;
		private int id_RetailSales;
		private int quantity;
		private DateTime dateStartSales;
		private DateTime dateClosing;
		private DateTime dateClosed;
		private int dayDelivery;
		private int dayReturn;
		private int visitors;
		private int status;
		private string nameDelivery;
		private string name;
		private string description;
		private decimal price;
		private int vat_rate;
		private string nameCondition;
		private int maxQuantity;
		private string nameParameter;
		private string parameter;
		private string typeWarranty;
		private int warrantyDays;
		private string nameBrand;
		private string nameCategory;
		private byte[] image;
		private int statusProduct;
		private object[] currentProduct;
		private object[] objectProduct;

		public int Id_Product
		{
			get => id_Product;
			set => id_Product = value;
		}

		public int Id_Seller
		{
			get => id_Seller;
			set => id_Seller = value;
		}

		public int Id_RetailSales
		{
			get => id_RetailSales;
			set => id_RetailSales = value;
		}

		public int Quantity
		{
			get => quantity;
			set => quantity = value;
		}

		public DateTime DateStartSales
		{
			get => dateStartSales;
			set => dateStartSales = value;
		}

		public DateTime DateClosing
		{
			get => dateClosing;
			set => dateClosing = value;
		}

		public DateTime DateClosed
		{
			get => dateClosed;
			set => dateClosed = value;
		}

		public int DayDelivery
		{
			get => dayDelivery;
			set => dayDelivery = value;
		}

		public int DayReturn
		{
			get => dayReturn;
			set => dayReturn = value;
		}

		public int Visitors
		{
			get => visitors;
			set => visitors = value;
		}

		public int Status
		{
			get => status;
			set => status = value;
		}

		public string NameDelivery
		{
			get => nameDelivery;
			set => nameDelivery = value;
		}

		public string Name
		{
			get => name;
			set => name = value;
		}

		public string Description
		{
			get => description;
			set => description = value;
		}

		public decimal Price
		{
			get => price;
			set => price = value;
		}

		public int Vat_rate
		{
			get => vat_rate;
			set => vat_rate = value;
		}

		public string NameCondition
		{
			get => nameCondition;
			set => nameCondition = value;
		}

		public int MaxQuantity
		{
			get => maxQuantity;
			set => maxQuantity = value;
		}

		public string NameParameter
		{
			get => nameParameter;
			set => nameParameter = value;
		}

		public string Parameter
		{
			get => parameter;
			set => parameter = value;
		}

		public string TypeWarranty
		{
			get => typeWarranty;
			set => typeWarranty = value;
		}

		public int WarrantyDays
		{
			get => warrantyDays;
			set => warrantyDays = value;
		}

		public string NameBrand
		{
			get => nameBrand;
			set => nameBrand = value;
		}

		public string NameCategory
		{
			get => nameCategory;
			set => nameCategory = value;
		}

		public byte[] Image
		{
			get => image;
			set => image = value;
		}

		public int StatusProduct
		{
			get => statusProduct;
			set => statusProduct = value;
		}

		public object[] CurrentProduct
		{
			get => currentProduct;

		}

		public object[] ObjectProduct 
		{
			get => objectProduct;
		}

		public Product(object[] tempProduct)
		{
			currentProduct = tempProduct;
			id_Product = (int)tempProduct[10];
			id_Seller = (int)tempProduct[11];
			id_RetailSales = (int)tempProduct[0];
			quantity = (int)tempProduct[1];
			dateStartSales = (DateTime)tempProduct[2];
			dateClosing = (DateTime)tempProduct[3];
			if (tempProduct[4] == null) dateClosed = (DateTime)tempProduct[4];
			dayDelivery = (int)tempProduct[5];
			dayReturn = (int)tempProduct[6];
			visitors = (int)tempProduct[7];
			status = (int)tempProduct[8];
			nameDelivery = (string)tempProduct[9];
			name = (string)tempProduct[12];
			description = (string)tempProduct[13];
			price = (decimal)tempProduct[14];
			vat_rate = (int)tempProduct[15];
			nameCondition = (string)tempProduct[16];
			maxQuantity = (int)tempProduct[17];
			nameParameter = (string)tempProduct[18];
			parameter = (string)tempProduct[19];
			typeWarranty = (string)tempProduct[20];
			warrantyDays = (int)tempProduct[21];
			nameBrand = (string)tempProduct[22];
			nameCategory = (string)tempProduct[23];
			image = (byte[])tempProduct[24];
			statusProduct = (int)tempProduct[25];
		}


	}
}

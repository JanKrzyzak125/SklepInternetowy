using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
    /// Logika interakcji dla klasy NewProductWindow.xaml
    /// </summary>
    /// 

    public partial class NewProductWindow : Window
    {
        private SQLConnect sqlConnect=new SQLConnect();
        private List<string> tempListVat;
        private List<string> tempListCondition;
        private List<string> tempListBrand;
        private List<string> tempListCategory;
        private List<string> tempListDelivery;
        private List<string> tempListWarranty;


        public List<string> TempListVat 
        {
            get => tempListVat;
        }

        public List<string> TempListCondition 
        {
            get => tempListCondition;
        }

        public List<string> TempListBrand 
        {
            get => tempListBrand;
        }

        public List<string> TempListCategory 
        {
            get => tempListCategory;
        }
        private List<string> TempListDelivery 
        {
            get => tempListDelivery;
        }

        private List<string> TempListWarranty 
        {
            get => tempListWarranty;
        }



        public NewProductWindow()
        {
            InitializeComponent();
            makeList();
            FillingDate();
        }

        private void makeList() 
        {
            tempListVat = new List<string>();
            tempListCondition = new List<string>();
            tempListBrand = new List<string>();
            tempListCategory = new List<string>();
            tempListDelivery = new List<string>();
            tempListWarranty = new List<string>();

            ComboVAT.ItemsSource = tempListVat;
            ComboBrand.ItemsSource = tempListBrand;
            ComboCondition.ItemsSource = tempListCondition;
            ComboCategory.ItemsSource = tempListCategory;
            ComboDelivery.ItemsSource = tempListDelivery;
            ComboWarranty.ItemsSource = tempListWarranty;
        }


        public void FillingDate()
        {
            foreach (DataRow row in sqlConnect.ReadTable("Vat_rate","VAT","ValuesOne").Rows)
            {
                tempListVat.Add(row[0].ToString() + "%");
            }
            ComboVAT.SelectedIndex=0;

            foreach (DataRow row in sqlConnect.ReadTable("Condition", "Condition", "ValuesOne").Rows)
            {
                tempListCondition.Add(row[0].ToString());
            }
            ComboCondition.SelectedIndex = 0;

            foreach (DataRow row in sqlConnect.ReadTable("NameBrand", "Brand", "ValuesOne").Rows)
            {
                tempListBrand.Add(row[0].ToString());
            }
            ComboBrand.SelectedIndex = 0;

            foreach (DataRow row in sqlConnect.ReadTable("NameCategory", "Category", "ValuesOne").Rows)
            {
                tempListCategory.Add(row[0].ToString());
            }
            ComboCategory.SelectedIndex = 0;

            foreach (DataRow row in sqlConnect.ReadTable("NameDelivery", "Delivery", "ValuesOne").Rows)
            {
                tempListDelivery.Add(row[0].ToString());
            }
            ComboDelivery.SelectedIndex = 0;

            foreach (DataRow row in sqlConnect.ReadTable("TypeWarranty", "Warranty", "ValuesOne").Rows)
            {
                tempListWarranty.Add(row[0].ToString());
            }
            ComboWarranty.SelectedIndex = 0;

            



        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Data.Sql;
using Microsoft.Win32;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace SklepInternetowy
{
    class SQLConnect
    {
        private SqlConnection _con;
        private string sqlConnection = @"Data source=GŁÓWNY\SQLEXPRESS;Initial Catalog=StoreDatabase;Integrated Security=True;";

        public SqlConnection Con
        {
            get => _con;
        }


        public SQLConnect()
        {
            _con = new SqlConnection(sqlConnection);//czy jest to w tym miejscu potrzebne?
        }


        public DataTable ReadTable(string commandText)
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = _con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        DataTable ds = new DataTable(); //conn is opened by dataadapter
                        da.Fill(ds);
                        _con.Close();
                        return ds;
                    }
                }
            }
        }
        /*
        public DataTable ReadTable(string nameColumns, string nameTable,string commandText)
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = _con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@NameValue", nameColumns);
                        da.SelectCommand.Parameters.AddWithValue("@Table", nameTable);
                        DataTable ds = new DataTable(); //conn is opened by dataadapter
                        da.Fill(ds);
                        _con.Close();
                        return ds;
                    }
                }
                
            }
        }
        */
        public DataTable ShowProduct(int valueIdUser, string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = _con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        da.SelectCommand.Parameters.AddWithValue("@valueId", valueIdUser);
                        DataTable ds = new DataTable(); //conn is opened by dataadapter
                        da.SelectCommand.Parameters.Add("@Table_Var", new SqlDbType());
                        da.SelectCommand.Parameters["@Table_Var"].Direction = ParameterDirection.ReturnValue;
                        var result = da.SelectCommand.ExecuteReader();
                       // da.Fill(ds);
                        _con.Close();
                        return ds;
                    }
                }
            }
            
        }


        public void ShowProduct(MainWindow window, int Id_Product) // TODO: narazie to były różne próby i trzeba zrobić wpierw dodawanie
        {


            /* 
       
                    byte[] img = (byte[])tempImage.ItemArray[1];
                    
                    using (MemoryStream stream = new MemoryStream(img))
                    
                        window.ImageProduct.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        
                
            }
            */
        }

        /// <summary>
        /// universal methods that add to database
        /// </summary>
        /// <param name="valueName">String with name</param>
        /// <param name="commandText">Name of Command</param>

        public void Add(string valueName, string commandText)
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.ExecuteNonQuery();

                }
                _con.Close();
            }
        }

        /// <summary>
        /// universal methods that add to database
        /// </summary>
        /// <param name="value">int with value</param>
        /// <param name="commandText">Name of Command</param>

        public void Add(int value, string commandText)
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }
        /// <summary>
        /// universal methods that add to database
        /// </summary>
        /// <param name="valueName">String with name</param>
        /// <param name="value">int with value</param>
        /// <param name="commandText">Name of Command</param>
        public void Add(string valueName, int value, string commandText) 
        {

            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }

        public void AddCompany(string valueName,string valueNIP,string valueEmail, int valuePhone, 
                               string valueAdress, string valueCity,int valueUser,string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@valueAdress", valueAdress);
                    cmd.Parameters.AddWithValue("@valueCity", valueCity);
                    cmd.Parameters.AddWithValue("@valuePhone", valuePhone);
                    cmd.Parameters.AddWithValue("@valueEmail", valueEmail);
                    cmd.Parameters.AddWithValue("@valueNip", valueNIP);
                    cmd.Parameters.AddWithValue("@valueUser", valueUser);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }


        public void AddUser(string valueNick,byte[]valueHash,string valueName,string valueSurname,
                            string valueEmail,string numberPhone,string valueAdress,string valueCity,
                            string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueNick", valueNick);
                    cmd.Parameters.AddWithValue("@valueHash", valueHash);
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@valueSurname", valueSurname);
                    cmd.Parameters.AddWithValue("@valueEmail", valueEmail);
                    cmd.Parameters.AddWithValue("@numberPhone", numberPhone);
                    cmd.Parameters.AddWithValue("@valueAdress", valueAdress);
                    cmd.Parameters.AddWithValue("@valueCity", valueCity);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }



        public void AddProduct(int valueSeller, string valueName, string valueDescription,
                               double valuePrice, int valueVat, int valueCondition, 
                               int valueMaxQuantity,string valueNameParameter,
                               string valueParameter,int valueWarranty, int valueWarrantyDays,
                               int valueBrand, int valueCategory,byte[] valueImage, string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueSeller", valueSeller);
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@valueDescription", valueDescription);
                    cmd.Parameters.AddWithValue("@valuePrice", valuePrice);
                    cmd.Parameters.AddWithValue("@valueVat", valueVat);
                    cmd.Parameters.AddWithValue("@valueCondition", valueCondition);
                    cmd.Parameters.AddWithValue("@valueMaxQuantity", valueMaxQuantity);
                    cmd.Parameters.AddWithValue("@valueNameParameter", valueNameParameter);
                    cmd.Parameters.AddWithValue("@valueParameter", valueParameter);
                    cmd.Parameters.AddWithValue("@valueWarranty", valueWarranty);
                    cmd.Parameters.AddWithValue("@valueWarrantyDays", valueWarrantyDays);
                    cmd.Parameters.AddWithValue("@valueBrand", valueBrand);
                    cmd.Parameters.AddWithValue("@valueCategory", valueCategory);
                    cmd.Parameters.AddWithValue("@valueImage", valueImage);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }

        //TODO: czy aktualizacje robić dla tych mniejszych tabel czy tylko i wyłącznie do produktów/użytkownika/firmy ?

        public void Update(int valueId, string valueName, string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.Parameters.AddWithValue("valueName", valueName);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }

       
        public void Update(int valueId,int value, string commandText)
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.Parameters.AddWithValue("value", value);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }

        public void Delete(int valueId,string commandText) 
        {
            using (_con = new SqlConnection(sqlConnection))
            {
                _con.Open();
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.ExecuteNonQuery();
                }
                _con.Close();
            }
        }

        /* TODO dodać dodawanie zdjęcia do samego produktu oraz pomoc przy tworzeniu połączeń
        public void AddImage()
        {
            using (_con)
            {
                int number = 0;
                string text = "DyskAdata";//TODO: dodać pole do wprowadzania 
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = "ImageCount";
                    cmd.CommandType = CommandType.StoredProcedure;
                    var retValParam = new SqlParameter("RetVal", SqlDbType.Int)
                    {
                        //Set this property as return value
                        Direction = ParameterDirection.ReturnValue
                    };
                    cmd.Parameters.Add(retValParam);
                    cmd.ExecuteScalar();
                    number = (int)retValParam.Value + 1;

                    cmd.CommandText = "Add_Image";
                    cmd.CommandType = CommandType.StoredProcedure;

                    OpenFileDialog OpenDialog = new OpenFileDialog();
                    if (OpenDialog.ShowDialog() == true)
                    {
                        byte[] imageData = File.ReadAllBytes(OpenDialog.FileName);
                        cmd.Parameters.AddWithValue("@IdImage", number);
                        cmd.Parameters.AddWithValue("@NameImage", text);
                        cmd.Parameters.AddWithValue("@ByteImage", imageData);
                        cmd.ExecuteNonQuery();

                    }

                }
            }
        }
        */

    }
}

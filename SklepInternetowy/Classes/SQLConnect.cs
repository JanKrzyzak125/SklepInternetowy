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
        //private SqlCommand _com;
        private string sqlConnection = @"Data source=GŁÓWNY\SQLEXPRESS;Initial Catalog=StoreDatabase;Integrated Security=True;";

        private List<Users> listUsers = new List<Users>();

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
                        return ds;
                    }
                }
                _con.Close();
            }
        }
        
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
                        return ds;
                    }
                }
                _con.Close();
            }
        }

        /* TODO: zastanowić się co lepsze to czy rozwiązanie bez <T>
        public DataTable Read1<T>(string query) where T : IDbConnection, new()
        {
            using (var conn = new T())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection.ConnectionString = sqlConnection;


                    cmd.Connection.Open();
                    DataTable table = new DataTable();
                    table.Load(cmd.ExecuteReader());
                    return table;
                }
            }
        }

        */

        public void ShowProduct(MainWindow window, int Id_Product) // TODO: narazie to były różne próby i trzeba zrobić wpierw dodawanie
        {


            /*
            using (var cmd = _con.CreateCommand())
            {
                cmd.CommandText = "TCupom";
                cmd.CommandType = CommandType.StoredProcedure;

                List<SqlParameter> sqlParams = new List<SqlParameter>();

                Random rand = new Random();
                for (int i = 0; i < 1; i++)
                {
                    SqlParameter code = new SqlParameter("@cupom", SqlDbType.Int);
                    code.Value = rand.Next();
                    sqlParams.Add(code);
                }

                foreach (var cmdParam in sqlParams)
                {
                    cmd.Parameters.Add(cmdParam);
                }


                var retValParam = new SqlParameter("RetVal", SqlDbType.Int)
                {
                    //Set this property as return value
                    Direction = ParameterDirection.ReturnValue
                };

                cmd.Parameters.Add(retValParam);
                cmd.ExecuteScalar();

                var retVal = retValParam.Value;
                MessageBox.Show("Wynik" + retVal);
            }

            */

            /* 
            using (var cmd = _con.CreateCommand())
            {
                cmd.CommandText = "ViewImages";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Name,Image From Image", _con);

                DataSet image = new DataSet();

                adapter.Fill(image, "Image");
                //window.MainGrid.ItemsSource = image.Tables["Image"].DefaultView;

                int i = 0;

                foreach (DataRow tempImage in image.Tables["Image"].Rows)
                {
                   

                    byte[] img = (byte[])tempImage.ItemArray[1];
                    
                    using (MemoryStream stream = new MemoryStream(img))
                    {
                        window.ImageProduct.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        
                    }

                    i++;
                }

                window.MainGrid.ItemsSource = image.Tables["Image"].DefaultView;

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


        /*
        /// <summary>
        /// Methods 
        /// </summary>
        public void AddBrand(string brand) 
        {
            using (_con)
            {
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = "AddBrand";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", brand);
                    cmd.ExecuteNonQuery();
                }

            }
        }


        public void AddCondition(string condition)
        {
            using (_con)
            {
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = "AddCondition";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@condition", condition);
                    cmd.ExecuteNonQuery();
                }

            }
        }


        public void AddCategory(string nameCategory) 
        {
            using (_con) 
            {
                using(var cmd = _con.CreateCommand()) 
                {
                    cmd.CommandText = "AddCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", nameCategory);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AddWarranty(string typeWarranty)//TODO: usunąć dni w dodatku zmienic zapytanie w sql
        {
            using (_con)
            {
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = "AddWarranty";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueType", typeWarranty);
                    cmd.ExecuteNonQuery();
                }

            }
        }


        public void AddVatValue(int valueVat) 
        {
            using (_con) 
            {
                using (var cmd = _con.CreateCommand()) 
                {
                    cmd.CommandText = "AddVat";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Value",valueVat);
                    cmd.ExecuteNonQuery();
                }

            }
        }
        */

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

    }
}

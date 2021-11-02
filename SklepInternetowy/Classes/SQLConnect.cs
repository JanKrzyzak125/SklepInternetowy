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
    /// <summary>
    /// Main class to connect with Database store
    /// </summary>
    class SQLConnect
    {
        private SqlConnection con;
        private string sqlConnection = @"Data source=GŁÓWNY\SQLEXPRESS;Initial Catalog=StoreDatabase;Integrated Security=True;";

        public SqlConnection Con
        {
            get => con;
        }

        /// <summary>
        /// Constructor class of SQLConnect
        /// </summary>
        public SQLConnect()
        {
            //con = new SqlConnection(sqlConnection);//czy jest to w tym miejscu potrzebne?
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>

        public object[] VerLogin(string valueNick, byte[] valueHash, string commandText)
        {
            object[] tempObject;
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@valueNick",valueNick);
                        da.SelectCommand.Parameters.AddWithValue("@valueHash",valueHash);
                        DataTable ds = new DataTable(); 
                        da.Fill(ds);
                        if (ds.Rows.Count == 0) 
                        {
                            return null;
                        }
                        tempObject=ds.Rows[0].ItemArray;
                        con.Close();
                        return tempObject;
                    }
                }
            }
        }

        public DataTable ReadTable(string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        DataTable ds = new DataTable(); //conn is opened by dataadapter
                        da.Fill(ds);
                        con.Close();
                        return ds;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public List<object> ReadTable2(string commandText)
        {
            List<object> tempList = new List<object>();
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        DataTable ds = new DataTable(); 
                        da.Fill(ds);
                        foreach (DataRow item in ds.Rows)
                        {
                            tempList.Add(item.ItemArray);

                        }

                        con.Close();
                        return tempList;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueIdUser"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable ShowProduct(int valueIdUser, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@valueId", valueIdUser);
                        DataTable ds = new DataTable(); 
                        da.Fill(ds);
                        con.Close();
                        return ds;
                    }
                }
            }

        }
        /// <summary>
        /// universal methods that add to database
        /// </summary>
        /// <param name="valueName">String with name</param>
        /// <param name="commandText">Name of Command</param>

        public void Add(string valueName, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.ExecuteNonQuery();

                }
                con.Close();
            }
        }

        /// <summary>
        /// universal methods that add to database
        /// </summary>
        /// <param name="value">int with value</param>
        /// <param name="commandText">Name of Command</param>

        public void Add(int value, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
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

            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void AddPayment(int valueIdUser, int valueIdType, string valueString, string valueName, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueIdUser", valueIdUser);
                    cmd.Parameters.AddWithValue("@valueIdType", valueIdType);
                    cmd.Parameters.AddWithValue("@valueString", valueString);
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="valueNIP"></param>
        /// <param name="valueEmail"></param>
        /// <param name="valuePhone"></param>
        /// <param name="valueAdress"></param>
        /// <param name="valueCity"></param>
        /// <param name="valueUser"></param>
        /// <param name="commandText"></param>
        public void AddCompany(string valueName, string valueNIP, string valueEmail, int valuePhone,
                               string valueAdress, string valueCity, int valueUser, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
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
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueNick"></param>
        /// <param name="valueHash"></param>
        /// <param name="valueName"></param>
        /// <param name="valueSurname"></param>
        /// <param name="valueEmail"></param>
        /// <param name="numberPhone"></param>
        /// <param name="valueAdress"></param>
        /// <param name="valueCity"></param>
        /// <param name="commandText"></param>
        public void AddUser(string valueNick, byte[] valueHash, string valueName, string valueSurname,
                            string valueEmail, int numberPhone, string valueAdress, string valueCity,
                            string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
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
                con.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueSeller"></param>
        /// <param name="valueName"></param>
        /// <param name="valueDescription"></param>
        /// <param name="valuePrice"></param>
        /// <param name="valueVat"></param>
        /// <param name="valueCondition"></param>
        /// <param name="valueMaxQuantity"></param>
        /// <param name="valueNameParameter"></param>
        /// <param name="valueParameter"></param>
        /// <param name="valueWarranty"></param>
        /// <param name="valueWarrantyDays"></param>
        /// <param name="valueBrand"></param>
        /// <param name="valueCategory"></param>
        /// <param name="valueImage"></param>
        /// <param name="commandText"></param>
        public void AddProduct(int valueSeller, string valueName, string valueDescription,
                               double valuePrice, int valueVat, int valueCondition,
                               int valueMaxQuantity, string valueNameParameter,
                               string valueParameter, int valueWarranty, int valueWarrantyDays,
                               int valueBrand, int valueCategory, byte[] valueImage, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
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
                con.Close();
            }
        }

        public int valueStringBank(int valueId, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    var valueResult = new SqlParameter("valueResult", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    cmd.Parameters.Add(valueResult);
                    cmd.ExecuteScalar();
                    con.Close();
                    return (int)valueResult.Value;
                }

            }

        }

        public object[] valueCompany(int valueIdCompany,string commandText) 
        {
            object[] tempObject;
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    using (da.SelectCommand = con.CreateCommand())
                    {
                        da.SelectCommand.CommandText = commandText;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@valueIdCompany", valueIdCompany);
                        DataTable ds = new DataTable();
                        da.Fill(ds);
                        tempObject = ds.Rows[0].ItemArray;
                        con.Close();
                        return tempObject;
                    }
                }
            }

        }

        //TODO: czy aktualizacje robić dla tych mniejszych tabel czy tylko i wyłącznie do produktów/użytkownika/firmy ?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueId"></param>
        /// <param name="valueName"></param>
        /// <param name="commandText"></param>
        public void Update(int valueId, string valueName, int valueStatus, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueId"></param>
        /// <param name="value"></param>
        /// <param name="commandText"></param>
        public void Update(int valueId, int value, int valueStatus, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("valueStatus", valueStatus);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void Update(int valueId, string valueName, int value, int valueStatus, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.Parameters.AddWithValue("@valueName", valueName);
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueId"></param>
        /// <param name="commandText"></param>
        public void Delete(int valueId, string commandText)
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@valueId", valueId);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public List<string> listTables()
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                List<string> tempTables = new List<string>();
                DataTable dataTables = con.GetSchema("Tables");
                foreach (DataRow row in dataTables.Rows)
                {
                    string tableName = (string)row[2];
                    string temp = (string)row[3];
                    if (temp.Equals("BASE TABLE") & !tableName.Equals("sysdiagrams")) tempTables.Add(tableName);
                }

                tempTables.Sort();
                con.Close();
                return tempTables;
            }
        }

        public List<string> listViews()
        {
            using (con = new SqlConnection(sqlConnection))
            {
                con.Open();
                List<string> tempViews = new List<string>();
                DataTable dataTables = con.GetSchema("Views");
                foreach (DataRow row in dataTables.Rows)
                {
                    string tablename = (string)row[2];
                    tempViews.Add(tablename);
                }
                con.Close();
                return tempViews;
            }
        }
        /* TODO dodać dodawanie zdjęcia do samego produktu oraz pomoc przy tworzeniu połączeń
        public void AddImage()
        {
            using (con)
            {
                int number = 0;
                string text = "DyskAdata";//TODO: dodać pole do wprowadzania 
                using (var cmd = con.CreateCommand())
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
        /*
       public void ShowProduct(MainWindow window, int Id_Product) // TODO: narazie to były różne próby i trzeba zrobić wpierw dodawanie
       {
           byte[] img = (byte[])tempImage.ItemArray[1];
           using (MemoryStream stream = new MemoryStream(img))
               window.ImageProduct.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
       }

       */
    }
}

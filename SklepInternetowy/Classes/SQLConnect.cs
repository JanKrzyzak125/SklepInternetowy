using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using SklepInternetowy.Classes;

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
		}


		public List<object> ReadUserPermision(int valueId, string commandText)
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
						da.SelectCommand.CommandType = CommandType.StoredProcedure;
						da.SelectCommand.Parameters.AddWithValue("@valueId", valueId);
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
						da.SelectCommand.Parameters.AddWithValue("@valueNick", valueNick);
						da.SelectCommand.Parameters.AddWithValue("@valueHash", valueHash);
						DataTable ds = new DataTable();
						da.Fill(ds);
						if (ds.Rows.Count == 0)
						{
							return null;
						}
						tempObject = ds.Rows[0].ItemArray;
						con.Close();
						return tempObject;
					}
				}
			}
		}

		public int IsUserNick(string valueNick, string commandText)
		{
			int resultVar;
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueNick", valueNick);
					var retValParam = new SqlParameter("resultVar", SqlDbType.Int)
					{
						Direction = ParameterDirection.ReturnValue
					};
					cmd.Parameters.Add(retValParam);
					cmd.ExecuteScalar();
					resultVar = (int)retValParam.Value;
				}
				con.Close();
				return resultVar;
			}
		}

		public int ReadQuantity(int valueId, string commandText)
		{
			int resultVar;
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					var retValParam = new SqlParameter("resultVar", SqlDbType.Int)
					{
						Direction = ParameterDirection.ReturnValue
					};
					cmd.Parameters.Add(retValParam);
					cmd.ExecuteScalar();
					resultVar = (int)retValParam.Value;
				}
				con.Close();
				return resultVar;
			}
		}

		public void NewPassword(string valueNick, byte[] valuePassword, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueNick", valueNick);
					cmd.Parameters.AddWithValue("@valuePassword", valuePassword);
					cmd.ExecuteNonQuery();
				}
				con.Close();
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
						DataTable ds = new DataTable();
						da.Fill(ds);
						con.Close();
						return ds;
					}
				}
			}
		}


		public DataTable ShowRating(int valueId, string commandText)
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
						da.SelectCommand.Parameters.AddWithValue("@valueId", valueId);
						DataTable ds = new DataTable();
						da.Fill(ds);
						con.Close();
						return ds;
					}
				}
			}
		}

		public object[] RefreshUser(int valueId, string commandText)
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
						da.SelectCommand.Parameters.AddWithValue("@valueIdUser", valueId);
						DataTable ds = new DataTable();
						da.Fill(ds);
						if (ds.Rows.Count == 0)
						{
							return null;
						}
						tempObject = ds.Rows[0].ItemArray;
						con.Close();
						return tempObject;
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

		public int AvalilableProducts(int valueId,string commandText) 
		{
			int resultVar;
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					var retValParam = new SqlParameter("resultVar", SqlDbType.Int)
					{
						Direction = ParameterDirection.ReturnValue
					};
					cmd.Parameters.Add(retValParam);
					cmd.ExecuteScalar();
					resultVar = (int)retValParam.Value;
				}
				con.Close();
				return resultVar;
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

		public void AddVisitor(int valueId,int valueIdProduct, string commandText) 
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIdProduct", valueIdProduct);
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

		public void AddUserPermission(string valueNick, string valueNameTypeUser, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueNick", valueNick);
					cmd.Parameters.AddWithValue("@valueNameTypeUser", valueNameTypeUser);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void AddComment(string valueComment, int valueStars, int valueIdUser, int valueIdProduct, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueComment", valueComment);
					cmd.Parameters.AddWithValue("@valueStars", valueStars);
					cmd.Parameters.AddWithValue("@valueIdUser", valueIdUser);
					cmd.Parameters.AddWithValue("@valueIdProduct", valueIdProduct);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void AddPayment(int valueIdUser, string valueNameType, string valueString, string valueName, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueIdUser", valueIdUser);
					cmd.Parameters.AddWithValue("@valueNameType", valueNameType);
					cmd.Parameters.AddWithValue("@valueString", valueString);
					cmd.Parameters.AddWithValue("@valueName", valueName);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}


		public void AddInvoice(int valueIdTransation, DataSetDateTime valueDateOfMakeInvoice,
							   DataSetDateTime valueDateOfPay, DataSetDateTime valueDateOfService,
								int valueCodeInvoice, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueIdTransation", valueIdTransation);
					cmd.Parameters.AddWithValue("@valueDateOfMakeInvoice", valueDateOfMakeInvoice);
					cmd.Parameters.AddWithValue("@valueDateOfPay", valueDateOfPay);
					cmd.Parameters.AddWithValue("@valueDateOfService", valueDateOfService);
					cmd.Parameters.AddWithValue("@valueCodeInvoice", valueCodeInvoice);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
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
		public void AddCompany(string valueName, string valueNIP, string valueEmail, long valuePhone,
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
							string valueEmail, long numberPhone, string valueAdress, string valueCity,
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

		public void AddUserProduct(int valueSeller, string valueName, string valueDescription,
								   decimal valuePrice, int valueVat, string valueCondition,
								   int valueMaxQuantity, string valueNameParameter,
								   string valueParameter, string valueWarranty,
								   int valueWarrantyDays, string valueBrand, string valueCategory,
								   byte[] valueImage, string commandText)
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
		public void AddRetailSales(int valueIdProduct, int valueQuantity, DateTime valueDateStartSales,
								   DateTime valueDateClosing, int valueDayReturn, int valueDayDelivery,
								   string valueNameDelivery, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueIdProduct", valueIdProduct);
					cmd.Parameters.AddWithValue("@valueQuantity", valueQuantity);
					cmd.Parameters.AddWithValue("@valueDateStartSales", valueDateStartSales);
					cmd.Parameters.AddWithValue("@valueDateClosing", valueDateClosing);
					cmd.Parameters.AddWithValue("@valueDayReturn", valueDayReturn);
					cmd.Parameters.AddWithValue("@valueDayDelivery", valueDayDelivery);
					cmd.Parameters.AddWithValue("@valueNameDelivery", valueNameDelivery);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}


		public void AddProductsBuyed(int valueIdRetailSalers, int valueIdTransation, int valueQuantity,
									string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueIdRetailSalers", valueIdRetailSalers);
					cmd.Parameters.AddWithValue("@valueIdTransation", valueIdTransation);
					cmd.Parameters.AddWithValue("@valueQuantity", valueQuantity);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void AddTransation(int valuePayment, int valueIdUser, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valuePayment", valuePayment);
					cmd.Parameters.AddWithValue("@valueIdUser", valueIdUser);
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

		public object[] valueCompany(int valueIdCompany, string commandText)
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

		public void UpdateComment(int valueIdComment, string valueComment, Int16 valueStars,
								 int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueIdComment", valueIdComment);
					cmd.Parameters.AddWithValue("@valueCommenty", valueComment);
					cmd.Parameters.AddWithValue("@valueStars", valueStars);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateCompany(int valueId, string valueNameCompany, string valueAdress,
								 string valueCity, long valuePhone, string valueEmail, string valueNIP,
								 int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueNameCompany", valueNameCompany);
					cmd.Parameters.AddWithValue("@valueAdress", valueAdress);
					cmd.Parameters.AddWithValue("@valueCity", valueCity);
					cmd.Parameters.AddWithValue("@valuePhone", valuePhone);
					cmd.Parameters.AddWithValue("@valueEmail", valueEmail);
					cmd.Parameters.AddWithValue("@valueNIP", valueNIP);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdatePayment(int valueId, string valueNameTypePayment, string valuePaymentString,
								 string valueNameBank, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueNameTypePayment", valueNameTypePayment);
					cmd.Parameters.AddWithValue("@valuePaymentString", valuePaymentString);
					cmd.Parameters.AddWithValue("@valueNameBank", valueNameBank);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdatePaymentAdmin(int valueId, int valueIdTypePayment, string valuePaymentString,
								 string valueNameBank, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIdTypePayment", valueIdTypePayment);
					cmd.Parameters.AddWithValue("@valuePaymentString", valuePaymentString);
					cmd.Parameters.AddWithValue("@valueNameBank", valueNameBank);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateProductsBuyed(int valueId, int valueIdRetailSalers, int valueTransation, int valueQuantity,
										int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIdRetailSalers", valueIdRetailSalers);
					cmd.Parameters.AddWithValue("@valueTransation", valueTransation);
					cmd.Parameters.AddWithValue("@valueQuantity", valueQuantity);
					cmd.Parameters.AddWithValue("valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateInvoice(int valueId, DataSetDateTime valueDateOfMakeInvoice,
							 DataSetDateTime valueDateOfPay, DataSetDateTime valueDateOfService, int valueStatus,
							 int valueCodeInvoice, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueDateOfMakeInvoice", valueDateOfMakeInvoice);
					cmd.Parameters.AddWithValue("@valueDateOfPay", valueDateOfPay);
					cmd.Parameters.AddWithValue("@valueDateOfService", valueDateOfService);
					cmd.Parameters.AddWithValue("valueCodeInvoice", valueCodeInvoice);
					cmd.Parameters.AddWithValue("valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateProduct(int valueId, int valueSeller, string valueName, string valueDescription,
							  decimal valuePrice, int valueVat, int valueCondition,
							  int valueMaxQuantity, string valueNameParameter,
							  string valueParameter, int valueWarranty, int valueWarrantyDays,
							  int valueBrand, int valueCategory, byte[] valueImage, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
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
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateRetailSales(int valueId, int valueIdProduct, int valueQuantity, DateTime valueDateStartSales,
								   DateTime valueDateClosing, DateTime valueDateClosed, int valueDayReturn,
								   int valueDayDelivery, int valueIdDelivery, int valueVisitors, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIdProduct", valueIdProduct);
					cmd.Parameters.AddWithValue("@valueQuantity", valueQuantity);
					cmd.Parameters.AddWithValue("@valueDateStartSales", valueDateStartSales);
					cmd.Parameters.AddWithValue("@valueDateClosing", valueDateClosing);
					cmd.Parameters.AddWithValue("@valueDateClosed", valueDateClosed);
					cmd.Parameters.AddWithValue("@valueDayReturn", valueDayReturn);
					cmd.Parameters.AddWithValue("@valueDayDelivery", valueDayDelivery);
					cmd.Parameters.AddWithValue("@valueDayDelivery", valueIdDelivery);
					cmd.Parameters.AddWithValue("@valueVisitors", valueVisitors);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateUser(int valueId, Int16 valueIsActive, string valueNick, string valueName,
						   string valueSurname, string valueEmail, long numberPhone, string valueAdress, string valueCity,
						   int valueCountVisitors, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIsActive", valueIsActive);
					cmd.Parameters.AddWithValue("@valueNick", valueNick);
					cmd.Parameters.AddWithValue("@valueName", valueName);
					cmd.Parameters.AddWithValue("@valueSurname", valueSurname);
					cmd.Parameters.AddWithValue("@valueEmail", valueEmail);
					cmd.Parameters.AddWithValue("@numberPhone", numberPhone);
					cmd.Parameters.AddWithValue("@valueAdress", valueAdress);
					cmd.Parameters.AddWithValue("@valueCity", valueCity);
					cmd.Parameters.AddWithValue("@valueCountVisitors", valueCountVisitors);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateUser(int valueId, Int16 valueIsActive, string valueNick, string valueName,
						   string valueSurname, string valueEmail, long numberPhone, string valueAdress, string valueCity,
						   int valueCountVisitors, Company valueCompany, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valueIsActive", valueIsActive);
					cmd.Parameters.AddWithValue("@valueNick", valueNick);
					cmd.Parameters.AddWithValue("@valueName", valueName);
					cmd.Parameters.AddWithValue("@valueSurname", valueSurname);
					cmd.Parameters.AddWithValue("@valueEmail", valueEmail);
					cmd.Parameters.AddWithValue("@numberPhone", numberPhone);
					cmd.Parameters.AddWithValue("@valueAdress", valueAdress);
					cmd.Parameters.AddWithValue("@valueCity", valueCity);
					cmd.Parameters.AddWithValue("@valueCountVisitors", valueCountVisitors);
					int valueIdCompany = valueCompany.Id_Company;
					cmd.Parameters.AddWithValue("@valueIdCompany", (int)valueIdCompany);


					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}
		public void Update(int valueId, string valuePaymentString, string valueNameBank, int valueStatus,
						   string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valuePaymentString", valuePaymentString);
					cmd.Parameters.AddWithValue("@valueNameBank", valueNameBank);
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateProduct2(int valueId, int valueSeller, string valueName, string valueDescription,
							  decimal valuePrice, int valueVat, string valueCondition,
							  int valueMaxQuantity, string valueNameParameter,
							  string valueParameter, string valueWarranty, int valueWarrantyDays,
							  string valueBrand, string valueCategory, byte[] valueImage, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
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
					cmd.Parameters.AddWithValue("@valueStatus", valueStatus);
					cmd.ExecuteNonQuery();
				}
				con.Close();
			}
		}

		public void UpdateTransation(int valueId, int valuePayment, int valueIdUser, decimal valueSumPay, int valueStatus, string commandText)
		{
			using (con = new SqlConnection(sqlConnection))
			{
				con.Open();
				using (var cmd = con.CreateCommand())
				{
					cmd.CommandText = commandText;
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@valueId", valueId);
					cmd.Parameters.AddWithValue("@valuePayment", valuePayment);
					cmd.Parameters.AddWithValue("@valueIdUser", valueIdUser);
					cmd.Parameters.AddWithValue("@valueSumPay", valueSumPay);
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

		public object[] valueProduct(int valueId, string valueName, string commandText)
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

						da.SelectCommand.Parameters.AddWithValue("@valueId", valueId);
						da.SelectCommand.Parameters.AddWithValue("@valueName", valueName);
						DataTable ds = new DataTable();
						da.Fill(ds);
						if (ds.Rows.Count == 0)
						{
							return null;
						}
						tempObject = ds.Rows[0].ItemArray;
						con.Close();
						return tempObject;
					}
				}

			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SklepInternetowy
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static string sqlConnection = @"Data source=GŁÓWNY\SQLEXPRESS;Initial Catalog=StoreDatabase;Integrated Security=True;";

		public static string SQLConnection
		{
			get => sqlConnection;
		}
	}
}

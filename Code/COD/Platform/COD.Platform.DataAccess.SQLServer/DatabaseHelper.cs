using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace COD.Platform.DataAccess.SQLServer
{
	public static class DatabaseHelper
	{

        [DebuggerStepThrough]
        public static DataSet CreateDataSet(this IDataReader reader)
		{
			DataSet set = new DataSet();
			do
			{
				set.Tables.Add(CreateDataTable(reader));
			} while (reader.NextResult());
			return set;
		}

        [DebuggerStepThrough]
        public static DataSet CreateAndPopulateDataSet(this IDataReader reader)
		{
			DataSet set = new DataSet();
			do
			{
				set.Tables.Add(CreateAndPopulateDataTable(reader));
			} while (reader.NextResult());
			return set;
		}

		public static DataTable CreateDataTable(this IDataReader reader)
		{

			var schemaTable = reader.GetSchemaTable();

			DataTable dataTable = new DataTable();
			for (int x = 0; x < schemaTable.Rows.Count; x++)
			{

				DataRow dataRow = schemaTable.Rows[x];

				string columnName = dataRow["ColumnName"].ToString();

				DataColumn column = new DataColumn(columnName, dataRow["DataType"] as Type);

				dataTable.Columns.Add(column);

			}

			return dataTable;
		}

		public static DataTable CreateAndPopulateDataTable(this IDataReader reader)
		{
			DataTable dataTable = CreateDataTable(reader);

			while (reader.Read())
			{
				var row = dataTable.NewRow();
				for (int x = 0; x < dataTable.Columns.Count; x++)
				{
					if (reader.IsDBNull(x))
						row[x] = DBNull.Value;
					else
						row[x] = reader.GetValue(x);
				}
				dataTable.Rows.Add(row);
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

        /// <summary>
        /// Builds a connection string that you can re-use
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="dbName"></param>
        /// <param name="integratedSecurity"></param>
        /// <param name="appName"></param>
        /// <returns></returns>

        [DebuggerStepThrough]
        public static string BuildConnectionString(string server, string username, string password, string dbName, bool integratedSecurity, string appName)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                WorkstationID = Environment.MachineName,
                IntegratedSecurity = integratedSecurity,
                Password = password,
                UserID = username,
                PersistSecurityInfo = false,
                DataSource = server,
                InitialCatalog = dbName,
                ApplicationName = appName
            };

           return  builder.ConnectionString ?? string.Empty;

        }

        public static SqlConnection GetDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

		public static SqlConnection GetDbConnection(string server, string username, string password, string dbName, bool integratedSecurity, string appName)
		{
			return GetDbConnection(BuildConnectionString(server, username, password, dbName, integratedSecurity, appName));
		}
		

		


		public static DataSet RunStoredProcedure(SqlConnection connection, string spName, params (string, object)[] parameters)
		{
			if (connection.State != ConnectionState.Open)
				connection.Open();

			using (var command = connection.CreateCommand())
			{
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = spName;

				foreach (var param in parameters)
				{
					command.Parameters.AddWithValue(param.Item1, param.Item2);
				}

				using (var reader = command.ExecuteReader())
				{
					return CreateAndPopulateDataSet(reader);
				}
			}
		}


		public static DataTable CreateAndPopulateDataTable(SqlConnection sqlConnection, string tableName, string whereClause)
		{
			if (sqlConnection.State == ConnectionState.Closed)
			{
				sqlConnection.Open();
			}
			else if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Close();
				sqlConnection.Open();
			}

			using (var command = sqlConnection.CreateCommand())
			{
				command.CommandText = "Select * from " + tableName;

				if (!string.IsNullOrEmpty(whereClause))
					command.CommandText += " WHERE " + whereClause;

				using (var reader = command.ExecuteReader())
				{
					return reader.CreateAndPopulateDataTable();
				}
			}
		}


		private static readonly SqlParameter[] emptyParamArray = new SqlParameter[0];
		/// <summary>
		/// Executes a query and returns a IDataReader
		/// </summary>
		/// <param name="dbCon">Database Connection</param>
		/// <param name="sql">Query string</param>
		/// <returns>The IDataReader</returns>
		public static IDataReader ExecuteReader(SqlConnection dbCon, string sql, CommandType commandType)
		{
			return ExecuteReader(dbCon, sql, commandType, emptyParamArray);
		}

		/// <summary>
		/// Executes a query and returns a IDataReader
		/// </summary>
		/// <param name="dbCon">Database Connection</param>
		/// <param name="sql">Query string</param>
		/// <param name="commandType">Type of the command.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		/// The IDataReader
		/// </returns>
		public static IDataReader ExecuteReader(SqlConnection dbCon, string sql, CommandType commandType, params (string, object)[] parameters)
		{
			var p = new SqlParameter[parameters.Length];
			for (int x = 0; x < parameters.Length; x++)
			{
				var param = parameters[x];
				p[x] = new SqlParameter { ParameterName = EnsureParameterNameIsCorrectFormat(param.Item1), Value = param.Item2 };
			}

			return ExecuteReader(dbCon, sql, commandType, p);
		}

        private static string EnsureParameterNameIsCorrectFormat(string parameterName)
        {
            return parameterName[0] == '@' ? parameterName : "@" + parameterName;
        }

        /// <summary>
        /// Executes a query and returns a IDataReader
        /// </summary>
        /// <param name="dbCon">Database Connection</param>
        /// <param name="sql">Query string</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The IDataReader
        /// </returns>
        public static IDataReader ExecuteReader(SqlConnection dbCon, string sql, CommandType commandType, params SqlParameter[] parameters)
		{
			if (dbCon.State == ConnectionState.Closed)
			{
				dbCon.Open();
			}

			IDataReader reader = null;
			using (var command = CreateCommand(dbCon))
			{
				command.CommandText = sql;

				foreach (var param in parameters)
				{
					command.Parameters.Add(param);
				}
				command.CommandType = commandType;
				reader = command.ExecuteReader();
			}
			return reader;
		}


		private static SqlCommand CreateCommand(SqlConnection db)
		{
			return CreateCommand(db, DefaultCommandTimeout);
		}
		private static SqlCommand CreateCommand(SqlConnection db, int timeout)
		{
			var command = db.CreateCommand();
			command.CommandTimeout = timeout;
			return command;
		}



		/// <summary>
		/// This timeout will be applied to all commands executed in this class
		/// </summary>
		static public int DefaultCommandTimeout
		{
			get { return ourDefaultCommandTimeout; }
			set { ourDefaultCommandTimeout = value; }
		}

		static private int ourDefaultCommandTimeout = 600;



		static public int ExecuteNonQuery(SqlConnection connection, string sql, params object[] arguments)
		{
			if (connection == null)
				throw new ArgumentNullException(nameof(connection));

			using (SqlCommand cmd = CreateCommand(connection))
			{
				if (-1 != ourDefaultCommandTimeout) cmd.CommandTimeout = ourDefaultCommandTimeout;
				cmd.CommandText = sql;
				BuildParameters(cmd, arguments);
				return cmd.ExecuteNonQuery();
			}
		}


		static public void BuildParameters(SqlCommand command, params object[] arguments)
		{
			if (command == null)
				throw new ArgumentNullException(nameof(command));

			foreach (object arg in arguments)
			{
				var param = command.CreateParameter();
				param.Value = arg;
				command.Parameters.Add(param);
			}
		}


		/// <summary>
		/// Drops the table.
		/// </summary>
		/// <param name="db">The db.</param>
		/// <param name="tableName">Name of the table.</param>
		public static void DropTable(SqlConnection connection, string tableName)
		{
			ExecuteNonQuery(connection, "DROP TABLE " + tableName);
		}



	}
}

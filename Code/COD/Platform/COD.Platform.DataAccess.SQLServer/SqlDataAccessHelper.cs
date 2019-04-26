using COD.Platform.Configuration.Core;
using COD.Platform.Logging.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace COD.Platform.DataAccess.SQLServer
{
    public class SqlDataAccessHelper : IDataAccessHelper
    {
        private ILog log;
        private IConfiguration config;
        private ILoggingService loggingService;
        private string connectionString;


        public SqlDataAccessHelper(ILoggingService loggingService, IConfiguration configuration, IDatabaseConnectionInfo dbInfo)
            : this(loggingService, configuration, DatabaseHelper.BuildConnectionString(dbInfo.Server,
                                                            dbInfo.Username,
                                                            dbInfo.Password,
                                                            dbInfo.DBName, false,
                                                            dbInfo.FullApplicationName))
        {
        }

        public SqlDataAccessHelper(ILoggingService loggingService, IConfiguration configuration, string connectionString)
        {
            this.log = loggingService.GetCurrentClassLogger();
            this.config = configuration;
            this.loggingService = loggingService;
            this.connectionString = connectionString;
        }

        private static readonly string TempFilePathConfigKey = "SQLHELPER_TEMPFILEPATH";
        private static readonly string CacheDatabaseOutputsConfigKey = "SQLHELPER_CACHEDATABASEOUTPUTS";

        private bool? _SaveDataSets;
        public bool SaveDataSets
        {
            get
            {
                if (!_SaveDataSets.HasValue)
                {
                    _SaveDataSets = config.GetBool(CacheDatabaseOutputsConfigKey, false);
                }
                return _SaveDataSets.Value;
            }
            set
            {
                _SaveDataSets = value;
            }
        }

        public IDataReader RunStoredProcedure(string spName, params (string, object)[] parameters)
        {

#if DEBUG
            if (SaveDataSets)
            {
                var reader2 = InnerRunStoredProcedure(connectionString, spName, parameters);
                DataSet ds = DatabaseHelper.CreateAndPopulateDataSet(reader2);
                ds.WriteXml(GetCacheFilePath(spName), XmlWriteMode.WriteSchema);

                return ds.CreateDataReader();
            }

#endif
            var reader = InnerRunStoredProcedure(connectionString, spName, parameters);
            return reader;
        }


        string cacheFolderPath = null;
        private string GetCacheFilePath(string spName)
        {
            if (cacheFolderPath is null)
            {
                cacheFolderPath = config.GetString(TempFilePathConfigKey, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
            return Path.Combine(cacheFolderPath, spName + ".datasets");
        }

        private  IDataReader InnerRunStoredProcedure(string connectionString, string spName, params (string, object)[] parameters)
        {

            log.Debug(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Running stored procedure ").Append(spName).Append(" on connection ").Append(connectionString);
                sb.AppendLine();
                foreach (var param in parameters)
                {
                    sb.Append("Parameter: ").Append(param.Item1).Append(", Value: ").Append(param.Item2).AppendLine();
                }
                return sb.ToString();
            });

            var connection = DatabaseHelper.GetDbConnection(connectionString);


            return new OneTimeUseDataReader(DatabaseHelper.ExecuteReader(connection, spName, CommandType.StoredProcedure, parameters), connection);


        }

        public IDataReader RunStoredProcedure(string spName, params SqlParameter[] parameters)
        {

            log.Debug(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Running stored procedure ").Append(spName).Append(" on connection ").Append(connectionString);
                sb.AppendLine();
                foreach (var param in parameters)
                {
                    sb.Append("Parameter: ").Append(param.ParameterName).Append(", Type:").Append(param.DbType).Append(", Value: ").Append(param.Value).AppendLine();
                }
                return sb.ToString();
            });


            var reader = InnerRunStoredProcedure(connectionString, spName, parameters);
#if DEBUG
            if (SaveDataSets)
            {
                //copy the parameters as they cant be attached to two seperate db calls;

                DataSet ds = DatabaseHelper.CreateAndPopulateDataSet(reader);
                ds.WriteXml(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), spName + "." + CachedDataSetDataAccessHelper.CachedDataSetExtension), XmlWriteMode.WriteSchema);

                return ds.CreateDataReader();
            }
#endif
            return reader;
        }

        private IDataReader InnerRunStoredProcedure(string connectionString, string spName, params SqlParameter[] parameters)
        {
            log.Debug(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Running stored procedure ").Append(spName).Append(" on connection ").Append(connectionString);
                sb.AppendLine();
                foreach (var param in parameters)
                {
                    sb.Append("Parameter: ").Append(param.ParameterName).Append(", Type:").Append(param.DbType).Append(", Value: ").Append(param.Value).AppendLine();
                }
                return sb.ToString();
            });

            var connection = DatabaseHelper.GetDbConnection(connectionString);


            return new OneTimeUseDataReader(DatabaseHelper.ExecuteReader(connection, spName, CommandType.StoredProcedure, parameters), connection);


        }


        public IDataReader RunStoredProcedure(string spName)
        {
            var connection = DatabaseHelper.GetDbConnection(connectionString);

#if DEBUG
            if (SaveDataSets)
            {
                var reader2 = DatabaseHelper.ExecuteReader(connection, spName, CommandType.StoredProcedure);
                DataSet ds = DatabaseHelper.CreateAndPopulateDataSet(reader2);
                ds.WriteXml(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), spName + ".mocksql"), XmlWriteMode.WriteSchema);
                reader2.Close();
            }
#endif

            return new OneTimeUseDataReader(DatabaseHelper.ExecuteReader(connection, spName, CommandType.StoredProcedure), connection);

        }




        public void BulkLoadTableToDatabase(string tableName, DataTable table)
        {

            log.Trace("SqlDataAccessHelper.BulkLoadTableToDatabase - {0}, {1} rows", tableName, table?.Rows?.Count);

            if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));
            if (table is null) throw new ArgumentNullException(nameof(table));


            table.TableName = tableName;

            var connection = DatabaseHelper.GetDbConnection(connectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            SqlBulkCopy copy = new SqlBulkCopy(connection);
            copy.DestinationTableName = tableName;
            copy.WriteToServer(table);

            log.Trace("SqlDataAccessHelper.BulkLoadTableToDatabase - Complete");
        }
    }
}

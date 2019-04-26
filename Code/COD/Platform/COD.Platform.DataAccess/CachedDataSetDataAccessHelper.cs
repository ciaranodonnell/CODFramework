using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace COD.Platform.DataAccess
{
    public class CachedDataSetDataAccessHelper : IDataAccessHelper
    {

        public static readonly string CachedDataSetExtension = "datasets";

        public Dictionary<string, DataSet> datasets = new Dictionary<string, DataSet>();


        public Dictionary<string, DataSet> DataSets { get { return datasets; } }



        public CachedDataSetDataAccessHelper()
        {
        }

        public void LoadFilesToDataSets(string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            var files = di.GetFiles("*.datasets", SearchOption.AllDirectories);

            try
            {
                foreach (var file in files)
                {
                    string spName = Path.GetFileNameWithoutExtension(file.Name).ToUpper();
                    if (!datasets.ContainsKey(spName))
                    {
                        DataSet ds = new DataSet();
                        ds.ReadXml(file.FullName, XmlReadMode.ReadSchema);
                        datasets.Add(spName, ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IDataReader RunStoredProcedure(string procedure)
        {
            var e = new RunStoredProcedureCalledEventArgs { SpName = procedure, Parameters = null, OverrideSPNameToDifferentCacheLookupValue = null };
            RunStoredProcedureCalled?.Invoke(this, e);

            var cacheKey = (e.OverrideSPNameToDifferentCacheLookupValue ?? procedure).ToUpperInvariant();

            return datasets[cacheKey].CreateDataReader();


        }


        public IDataReader RunStoredProcedure(string procedure, params (string, object)[] parameters)
        {
            var e = new RunStoredProcedureCalledEventArgs { SpName = procedure, Parameters = null, OverrideSPNameToDifferentCacheLookupValue = null };
            RunStoredProcedureCalled?.Invoke(this, e);

            var cacheKey = (e.OverrideSPNameToDifferentCacheLookupValue ?? procedure).ToUpperInvariant();

            return datasets[cacheKey].CreateDataReader();

        }

        public void BulkLoadTableToDatabase(string tableName, DataTable table)
        {
            
        }

        public event Action<object, RunStoredProcedureCalledEventArgs> RunStoredProcedureCalled;


        public class RunStoredProcedureCalledEventArgs : EventArgs
        {

            public IDatabaseConnectionInfo ConnectionInfo { get; internal set; }

            public string SpName { get; internal set; }

            public (string, object)[] Parameters { get; internal set; }

            /// <summary>
            /// Setting this string to a non-null value allows the test to (based on the parameters) choose the cached dataset explicitly by name
            /// </summary>
            public string OverrideSPNameToDifferentCacheLookupValue { get; set; }
        }

    }
}

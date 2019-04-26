using System;
using System.Data;

namespace COD.Platform.DataAccess
{
    public interface IDataAccessHelper
    {

        IDataReader RunStoredProcedure(string spName);
        IDataReader RunStoredProcedure(string spName, params (string, object)[] parameters);

        void BulkLoadTableToDatabase(string tableName, DataTable table);
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace COD.Platform.DataAccess
{
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Extension method that uses reflection to convert sqldatareader to list of specified type
        /// typed object properties must match db table fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<T> MapDataToEntities<T>(this IDataReader dataReader) where T : new()
        {

            Type businessEntityType = typeof(T);
            List<T> entities = new List<T>();
            Dictionary<string, PropertyInfo> hashtable = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] properties = businessEntityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }

            while (dataReader.Read())
            {
                entities.Add(GetNewObjectFromReader<T>(dataReader, hashtable));
            }

            return entities;
        }


        /// <summary>
        /// Extension method that uses reflection to convert an IDataReader to list of specified type
        /// typed object properties must match db table fields
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static Dictionary<TForeignKey, TEntity> MapDataToEntitiesWithFK<TEntity, TForeignKey>(this IDataReader dataReader, string fkName) where TEntity : new()
        {

            Type businessEntityType = typeof(TEntity);
            var entities = new Dictionary<TForeignKey, TEntity>();
            Dictionary<string, PropertyInfo> hashtable = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] properties = businessEntityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }

            while (dataReader.Read())
            {
                var key = dataReader.GetValue(dataReader.GetOrdinal(fkName));

                entities.Add((TForeignKey)key, GetNewObjectFromReader<TEntity>(dataReader, hashtable));
            }

            return entities;
        }

        private static T GetNewObjectFromReader<T>(IDataReader dataReader, Dictionary<string, PropertyInfo> hashtable) where T : new()
        {
            T newObject = new T();

            for (int index = 0; index < dataReader.FieldCount; index++)
            {
                string columnName = dataReader.GetName(index).ToUpper();
                if (hashtable.ContainsKey(columnName))
                {
                    PropertyInfo propertyInfo = hashtable[dataReader.GetName(index).ToUpper()];

                    if (!(propertyInfo is null) && propertyInfo.CanWrite)
                    {
                        Type propertyType = propertyInfo.PropertyType;
                        var readValue = dataReader.GetValue(index);

                        if (readValue is DBNull)
                            propertyInfo.SetValue(newObject, null, null);
                        else if (readValue is DateTime)
                        {
                            if (propertyType == typeof(UInt64))
                                propertyInfo.SetValue(newObject, ((DateTime)readValue), null);
                            else if (propertyType == typeof(UInt32))
                                propertyInfo.SetValue(newObject, ((DateTime)readValue), null);
                            else if (propertyType == typeof(DateTime))
                                propertyInfo.SetValue(newObject, readValue, null);
                        }
                        else if (readValue is TimeSpan)
                            propertyInfo.SetValue(newObject, Convert.ToUInt64(((TimeSpan)readValue).TotalSeconds), null);
                        else if (propertyType.IsPrimitive)
                            propertyInfo.SetValue(newObject, Convert.ChangeType(readValue, propertyType), null);
                        else
                            propertyInfo.SetValue(newObject, readValue, null);
                    }
                }
            }

            return newObject;
        }
    }
}


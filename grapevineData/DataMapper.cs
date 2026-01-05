using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData
{
    public static class DataMapper
    {
        public static List<T> MapToList<T>(this IDataReader reader) where T : new()
        {
            var result = new List<T>();
            var props = typeof(T).GetProperties();

            while (reader.Read())
            {
                var obj = new T();
                foreach (var p in props)
                {
                    if (!reader.HasColumn(p.Name) || reader[p.Name] == DBNull.Value)
                        continue;

                    p.SetValue(obj, reader[p.Name]);
                }
                result.Add(obj);
            }
            return result;
        }

        public static bool HasColumn(this IDataRecord r, string col)
        {
            for (int i = 0; i < r.FieldCount; i++)
                if (r.GetName(i).Equals(col, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
    
}

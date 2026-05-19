using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Tajnned.Infrastructure.Dapper
{
    public static class Helper
    {
        public static DynamicParameters ToDynamicParams<T>(this T obj)
        {
            var parameters = new DynamicParameters();

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                if (value is IEnumerable<int> intList && prop.PropertyType != typeof(string))
                {
                    parameters.Add("@" + prop.Name,
                        intList.ToTvp("dbo." + prop.Name + "TVP"));
                }
                else
                {
                    parameters.Add("@" + prop.Name, value);
                }
            }

            return parameters;
        }


        public static SqlMapper.ICustomQueryParameter ToTvp(
                  this IEnumerable<int> values,
                  string tvpTypeName)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));

            foreach (var value in values)
                table.Rows.Add(value);

            return table.AsTableValuedParameter(tvpTypeName);
        }

    }
    
      
     
}

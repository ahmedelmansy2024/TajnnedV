using Dapper;
using System;
using System.Collections;
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

                if (prop.PropertyType == typeof(string))
                {
                    parameters.Add("@" + prop.Name, value);
                    continue;
                }

                parameters.Add("@" + prop.Name, value);
            }

            return parameters;
        }


        public static DynamicParameters ToDynamicParam<T>(this T obj)
        {
            var parameters = new DynamicParameters();

            if (obj == null)
                return parameters;

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                if (value == null)
                {
                    parameters.Add("@" + prop.Name, null);
                    continue;
                }

                // skip string (because it's IEnumerable<char>)
                if (prop.PropertyType == typeof(string))
                {
                    parameters.Add("@" + prop.Name, value);
                    continue;
                }

                // TVP case (List<T>)
                if (value is IEnumerable enumerable &&
                    prop.PropertyType.IsGenericType)
                {
                    var elementType = prop.PropertyType.GetGenericArguments()[0];

                    // int TVP
                    if (elementType == typeof(int))
                    {
                        parameters.Add("@" + prop.Name,
                            ((IEnumerable<int>)value)
                                .ToTvp("dbo." + prop.Name + "TVP"));
                        continue;
                    }

                    // string TVP
                    if (elementType == typeof(string))
                    {
                        parameters.Add("@" + prop.Name,
                            ((IEnumerable<string>)value)
                                .ToTvp("dbo." + prop.Name + "TVP"));
                        continue;
                    }
                }

                // normal scalar
                parameters.Add("@" + prop.Name, value);
            }

            return parameters;
        }


        public static DynamicParameters ToDynamicParamsWithTvp<T>(this T obj)
        {
            var parameters = new DynamicParameters();

            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                if (value == null)
                {
                    parameters.Add("@" + prop.Name, null);
                    continue;
                }

                // TVP case (List<int>)
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

        public static SqlMapper.ICustomQueryParameter ToTvp<T>(
       this IEnumerable<T> values,
       string tvpTypeName)
        {
            var table = new DataTable();
            table.Columns.Add("Value", typeof(T));

            foreach (var value in values)
                table.Rows.Add(value);

            return table.AsTableValuedParameter(tvpTypeName);
        }

        public static SqlMapper.ICustomQueryParameter ToTvpINT(
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

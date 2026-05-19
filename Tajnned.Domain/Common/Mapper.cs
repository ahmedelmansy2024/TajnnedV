namespace Tajnned.Domain.Common
{
    public static class Mapper
    {
        public static List<T> Map<T>(List<dynamic> table) where T : new()
        {
            var result = new List<T>();

            foreach (IDictionary<string, object> row in table)
            {
                var obj = new T();
                var props = typeof(T).GetProperties();

                foreach (var prop in props)
                {
                    if (row.ContainsKey(prop.Name) && row[prop.Name] != null)
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }

                result.Add(obj);
            }

            return result;
        }
        public static List<T> Mapp<T>(List<dynamic> table)
        {
            return table.Select(x =>
            {
                var dict = (IDictionary<string, object>)x;

                var json = System.Text.Json.JsonSerializer.Serialize(dict);
                return System.Text.Json.JsonSerializer.Deserialize<T>(json);
            }).ToList();
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Linq;

namespace JoshCodes.Web.Serialization
{
    public static class NameValueCollectionSerializer
    {
        private static object DeserializeRecursive(NameValueCollection collection, Type entityType, string prefix)
        {
            // TODO: If no constructor throw exception
            var entityConstructor = entityType.GetConstructor(Type.EmptyTypes);
            if (entityConstructor == null)
            {
                throw new Exception(String.Format("Unserializable type:{0}", entityType.Name));
            }
            var entity = entityConstructor.Invoke(null);

            foreach (var propInfo in entityType.GetProperties())
            {
                var propName = propInfo.Name;
                var dataMemberAttr = (System.Runtime.Serialization.DataMemberAttribute)propInfo.GetCustomAttributes(
                    typeof(System.Runtime.Serialization.DataMemberAttribute), false).FirstOrDefault();
                if (dataMemberAttr != null)
                {
                    propName = dataMemberAttr.Name;
                }

                var propStringValue = collection[prefix + propName];

                if (propInfo.PropertyType.GUID == typeof(string).GUID)
                {
                    propInfo.SetValue(entity, propStringValue);
                }
                else if (propInfo.PropertyType.GUID == typeof(Guid).GUID)
                {
                    Guid result;
                    if (Guid.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(double).GUID)
                {
                    double result;
                    if (double.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(bool).GUID)
                {
                    bool result;
                    if (bool.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(decimal).GUID)
                {
                    decimal result;
                    if (decimal.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(Int32).GUID)
                {
                    Int32 result;
                    if (Int32.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(Uri).GUID)
                {
                    Uri result;
                    if (Uri.TryCreate(propStringValue, UriKind.RelativeOrAbsolute, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.GUID == typeof(DateTime).GUID)
                {
                    DateTime result;
                    if (DateTime.TryParse(propStringValue, out result))
                    {
                        propInfo.SetValue(entity, result);
                    }
                }
                else if (propInfo.PropertyType.IsArray)
                {
                    // var arrayItems = collection.AllKeys.Where((key) => key.StartsWith(prefix + propName, StringComparison.OrdinalIgnoreCase));
                    // TODO: use these items to populate an array
                    propInfo.SetValue(entity, null);
                }
                else if (propInfo.PropertyType.IsInterface)
                {
                    propInfo.SetValue(entity, null);
                }
                else
                {
                    var subEntity = DeserializeRecursive(collection, propInfo.PropertyType, prefix + propName + ".");
                    propInfo.SetValue(entity, subEntity);
                }
            }

            return entity;
        }

        public static TEntity Deserialize<TEntity>(this NameValueCollection collection)
        {
            var entityType = typeof(TEntity);
            return (TEntity)DeserializeRecursive(collection, entityType, String.Empty);
        }
    }
}

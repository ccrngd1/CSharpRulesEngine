using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace RulesEngine.DAL
{
    public static class Utils
    {
        public static DataTable ConvertToDataTable<T>(List<T> data)
        {
            if (data.Count == 0) return new DataTable();
            var dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(data[0]);
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            var values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}

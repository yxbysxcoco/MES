using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SQ_Render.Models.Common
{
    public class TableHeader 
    {
        public string GetDataUrl { get; set; }

        public List<Field> fields { get; set; }

        public  TableHeader(IEnumerable<PropertyInfo> propertyInfos)
        {
            fields = new List<Field>();
            foreach (var property in propertyInfos)
            {
                Field field = new Field()
                {
                    FiledName = property.Name,
                    Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                    Length = property.SetLength()
                };
                fields.Add(field);
            }
        }
    }
}
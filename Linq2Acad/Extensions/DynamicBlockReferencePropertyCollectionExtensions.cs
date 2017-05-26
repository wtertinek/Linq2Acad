using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public static class DynamicBlockReferencePropertyCollectionExtensions
  {
    public static bool Contains(this DynamicBlockReferencePropertyCollection properties, string name)
    {
      return properties.Cast<DynamicBlockReferenceProperty>()
                       .Any(p => p.PropertyName == name);
    }

    public static object GetValue(this DynamicBlockReferencePropertyCollection properties, string name)
    {
      var property = properties.Cast<DynamicBlockReferenceProperty>()
                               .FirstOrDefault(p => p.PropertyName == name);

      if (property == null)
      {
        throw Error.KeyNotFound(name);
      }
      else
      {
        return property.Value;
      }
    }

    public static void SetValue(this DynamicBlockReferencePropertyCollection properties, string name, object value)
    {
      var property = properties.Cast<DynamicBlockReferenceProperty>()
                               .FirstOrDefault(a => a.PropertyName == name);

      if (property == null)
      {
        throw Error.KeyNotFound(name);
      }
      else
      {
        property.Value = value;
      }
    }
  }
}

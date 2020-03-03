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
      Require.ParameterNotNull(properties, nameof(properties));
      Require.StringNotEmpty(name, nameof(name));

      return properties.Cast<DynamicBlockReferenceProperty>()
                       .Any(p => p.PropertyName == name);
    }

    public static object GetValue(this DynamicBlockReferencePropertyCollection properties, string name)
    {
      Require.ParameterNotNull(properties, nameof(properties));
      Require.StringNotEmpty(name, nameof(name));

      var property = properties.Cast<DynamicBlockReferenceProperty>()
                               .FirstOrDefault(p => p.PropertyName == name);

      Require.ObjectNotNull(property, $"No {nameof(DynamicBlockReferenceProperty)} with name '{name}' found");

      return property.Value;
    }

    public static void SetValue(this DynamicBlockReferencePropertyCollection properties, string name, object value)
    {
      Require.ParameterNotNull(properties, nameof(properties));
      Require.StringNotEmpty(name, nameof(name));

      var property = properties.Cast<DynamicBlockReferenceProperty>()
                               .FirstOrDefault(a => a.PropertyName == name);

      Require.ObjectNotNull(property, $"No {nameof(DynamicBlockReferenceProperty)} with name '{name}' found");

      property.Value = value;
    }
  }
}

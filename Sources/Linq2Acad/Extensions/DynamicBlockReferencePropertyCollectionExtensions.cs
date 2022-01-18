using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for DynamicBlockReferencePropertyCollections.
  /// </summary>
  public static class DynamicBlockReferencePropertyCollectionExtensions
  {
    /// <summary>
    /// Checks the given DynamicBlockReferencePropertyCollection for a DynamicBlockReferenceProperty with the given name.
    /// </summary>
    /// <param name="properties">The DynamicBlockReferencePropertyCollection.</param>
    /// <param name="name">The name to look for.</param>
    /// <returns>True if the DynamicBlockReferencePropertyCollection contains a DynamicBlockReferenceProperty with the given name, otherwise false.</returns>
    public static bool Contains(this DynamicBlockReferencePropertyCollection properties, string name)
    {
      Require.ParameterNotNull(properties, nameof(properties));
      Require.StringNotEmpty(name, nameof(name));

      return properties.Cast<DynamicBlockReferenceProperty>()
                       .Any(p => p.PropertyName == name);
    }

    /// <summary>
    /// Gets the value of the DynamicBlockReferencePropertywith the given name.
    /// </summary>
    /// <param name="properties">The DynamicBlockReferencePropertyCollection.</param>
    /// <param name="name">The name to look for.</param>
    public static object GetValue(this DynamicBlockReferencePropertyCollection properties, string name)
    {
      Require.ParameterNotNull(properties, nameof(properties));
      Require.StringNotEmpty(name, nameof(name));

      var property = properties.Cast<DynamicBlockReferenceProperty>()
                               .FirstOrDefault(p => p.PropertyName == name);

      Require.ObjectNotNull(property, $"No {nameof(DynamicBlockReferenceProperty)} with name '{name}' found");

      return property.Value;
    }

    /// <summary>
    /// Sets the value of the DynamicBlockReferencePropertywith the given name.
    /// </summary>
    /// <param name="properties">The DynamicBlockReferencePropertyCollection.</param>
    /// <param name="name">The name to look for.</param>
    /// <param name="value">The value to set.</param>
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

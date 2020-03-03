using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public static class AttributeCollectionExtensions
  {
    public static bool Contains(this AttributeCollection attributes, string tag)
    {
      Require.ParameterNotNull(attributes, nameof(attributes));

      return attributes.Cast<AttributeReference>()
                       .Any(a => a.Tag == tag);
    }

    public static string GetValue(this AttributeCollection attributes, string tag)
    {
      Require.ParameterNotNull(attributes, nameof(attributes));

      var attribute = attributes.Cast<AttributeReference>()
                                .FirstOrDefault(a => a.Tag == tag);

      Require.ObjectNotNull(attribute, $"No {nameof(AttributeReference)} with Tag '{tag}' found");

      return attribute.TextString;
    }

    public static void SetValue(this AttributeCollection attributes, string tag, string value)
    {
      Require.ParameterNotNull(attributes, nameof(attributes));

      var attribute = attributes.Cast<AttributeReference>()
                                .FirstOrDefault(a => a.Tag == tag);

      Require.ObjectNotNull(attribute, $"No {nameof(AttributeReference)} with Tag '{tag}' found");

      attribute.TextString = value;
    }
  }
}

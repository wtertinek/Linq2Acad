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
      return attributes.Cast<AttributeReference>()
                       .Any(a => a.Tag == tag);
    }

    public static string GetValue(this AttributeCollection attributes, string tag)
    {
      var attribute = attributes.Cast<AttributeReference>()
                                .FirstOrDefault(a => a.Tag == tag);

      if (attribute == null)
      {
        throw Error.KeyNotFound(tag);
      }
      else
      {
        return attribute.TextString;
      }
    }

    public static void SetValue(this AttributeCollection attributes, string tag, string value)
    {
      var attribute = attributes.Cast<AttributeReference>()
                                .FirstOrDefault(a => a.Tag == tag);

      if (attribute == null)
      {
        throw Error.KeyNotFound(tag);
      }
      else
      {
        attribute.TextString = value;
      }
    }
  }
}

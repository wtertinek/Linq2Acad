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
      return GetAttributeReferences(attributes, OpenMode.ForRead)
             .Any(a => a.Tag == tag);
    }

    public static string GetValue(this AttributeCollection attributes, string tag)
    {
      var attribute = GetAttributeReference(attributes, tag, OpenMode.ForRead);
      return attribute.TextString;
    }

    public static void SetValue(this AttributeCollection attributes, string tag, string value)
    {
      var attribute = GetAttributeReference(attributes, tag, OpenMode.ForWrite);
      attribute.TextString = value;
    }

    public static void CleanValues(this AttributeCollection attributes)
    {
      GetAttributeReferences(attributes, OpenMode.ForWrite)
        .ForEach(ar => ar.TextString = "");
    }

    private static AttributeReference GetAttributeReference(AttributeCollection attributes, string tag, OpenMode openMode)
    {
      var attribute = GetAttributeReferences(attributes, openMode)
                      .FirstOrDefault(a => a.Tag == tag);

      Require.ObjectNotNull(attribute, $"No {nameof(AttributeReference)} with Tag '{tag}' found");

      return attribute;
    }

    private static IEnumerable<AttributeReference> GetAttributeReferences(AttributeCollection attributes, OpenMode openMode)
    {
      Require.ParameterNotNull(attributes, nameof(attributes));

      if (attributes.Count > 0)
      {
        if (attributes[0].Database.TransactionManager.TopTransaction == null)
        {
          throw new InvalidOperationException("No transaction available");
        }

        var transaction = attributes[0].Database.TransactionManager.TopTransaction;
        return attributes.Cast<ObjectId>()
                         .Select(id => (AttributeReference)transaction.GetObject(id, openMode));
      }
      else
      {
        return Array.Empty<AttributeReference>();
      }
    }
  }
}

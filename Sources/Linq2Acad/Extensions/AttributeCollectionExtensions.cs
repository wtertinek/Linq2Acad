using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for AttributeCollections.
  /// </summary>
  public static class AttributeCollectionExtensions
  {
    /// <summary>
    /// Checks the given AttributeCollection for an AttributeReference with the given tag.
    /// </summary>
    /// <param name="attributes">The AttributeCollection.</param>
    /// <param name="tag">The tag to look for.</param>
    /// <returns>True if the AttributeCollection contains an AttributeReference with the given tag, otherwise false.</returns>
    public static bool Contains(this AttributeCollection attributes, string tag)
    {
      return GetAttributeReferences(attributes, OpenMode.ForRead)
             .Any(a => a.Tag == tag);
    }

    /// <summary>
    /// Gets the value of the AttributeReference with the given tag.
    /// </summary>
    /// <param name="attributes">The AttributeCollection.</param>
    /// <param name="tag">The tag to look for.</param>
    public static string GetValue(this AttributeCollection attributes, string tag)
    {
      var attribute = GetAttributeReference(attributes, tag, OpenMode.ForRead);
      return attribute.TextString;
    }

    /// <summary>
    /// Sets the value of the AttributeReference with the given tag.
    /// </summary>
    /// <param name="attributes">The AttributeCollection.</param>
    /// <param name="tag">The tag to look for.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this AttributeCollection attributes, string tag, string value)
    {
      var attribute = GetAttributeReference(attributes, tag, OpenMode.ForWrite);
      attribute.TextString = value;
    }

    /// <summary>
    /// Resets the values of all AttributeReferences in the AttributeCollection.
    /// </summary>
    /// <param name="attributes">The AttributeCollection.</param>
    public static void ClearValues(this AttributeCollection attributes)
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
        return new AttributeReference[0];
      }
    }
  }
}

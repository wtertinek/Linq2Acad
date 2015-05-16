using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DBDictionaryHelpers
  {
    public static T GetItem<T>(IEnumerable<T> source, Func<DBDictionary, ObjectId> getID) where T : DBObject
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerable)
      {
        var enumerable = source as IAcadEnumerable;
        var dict = (DBDictionary)L2ADatabase.Transaction.Value.GetObject(enumerable.ContainerID, OpenMode.ForRead);
        return (T)L2ADatabase.Transaction.Value.GetObject(getID(dict), OpenMode.ForRead);
      }
      else
      {
        throw new InvalidOperationException();
      }
    }

    public static bool Contains<T>(IEnumerable<T> source, Func<DBDictionary, bool> has, Func<T, bool> altPredicate) where T : DBObject
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerable)
      {
        var enumerable = source as IAcadEnumerable;
        var dict = (DBDictionary)L2ADatabase.Transaction.Value.GetObject(enumerable.ContainerID, OpenMode.ForRead);
        return has(dict);
      }
      else
      {
        return source.Any(altPredicate);
      }
    }

    public static ObjectId Set<T>(IEnumerable<T> source, string name, T item) where T : DBObject
    {
      return SetRange<T>(source, new[] { name }, new[] { item }).First();
    }

    public static IEnumerable<ObjectId> SetRange<T>(this IEnumerable<T> source, IEnumerable<string> names, IEnumerable<T> items) where T : DBObject
    {
      Helpers.CheckTransaction();

      var a_names = names.ToArray();
      var a_items = items.ToArray();

      if (a_names.Length != a_items.Length)
      {
        throw new ArgumentException();
      }

      if (source is IAcadEnumerable)
      {
        var enumerable = source as IAcadEnumerable;
        var dict = (DBDictionary)L2ADatabase.Transaction.Value.GetObject(enumerable.ContainerID, OpenMode.ForWrite);

        for (int i = 0; i < a_items.Length; i++)
        {
          var id = dict.SetAt(a_names[i], a_items[i]);
          L2ADatabase.Transaction.Value.AddNewlyCreatedDBObject(a_items[i], true);
          yield return id;
        }
      }
      else
      {
        throw new InvalidOperationException();
      }
    }
  }
}

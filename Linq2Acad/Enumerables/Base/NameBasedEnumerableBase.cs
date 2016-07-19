using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class NameBasedEnumerableBase<T> : ContainerEnumerableBase<T> where T : DBObject
  {
    protected NameBasedEnumerableBase(Database database, Transaction transaction,
                                      ObjectId containerID, Func<object, ObjectId> getID)
      : base(database, transaction, containerID, getID)
    {
    }

    public abstract bool Contains(string name);

    public abstract T Element(string name);

    public abstract T Element(string name, bool forWrite);

    public abstract T ElementOrDefault(string name);

    public abstract T ElementOrDefault(string name, bool forWrite);

    protected abstract T CreateNew();

    protected virtual void SetName(T item, string name) { }

    protected abstract void AddRangeInternal(IEnumerable<T> items, IEnumerable<string> names);

    public T Create(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.Generic("An object with name " + name + " already exists");

      try
      {
        return CreateInternal(name);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    protected virtual T CreateInternal(string name)
    {
      var item = CreateNew();
      AddRangeInternal(new[] { item }, new[] { name });
      SetName(item, name);
      return item;
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      if (names == null) throw Error.ArgumentNull("names");
      var invalidName = names.FirstOrDefault(n => !Helpers.IsNameValid(n));
      if (invalidName != null) throw Error.InvalidName(invalidName);
      var existingName = names.FirstOrDefault(n => Contains(n));
      if (existingName != null) throw Error.Generic("An object with name " + existingName + " already exists");

      try
      {
        var tmpNames = names.ToArray();
        var items = new T[tmpNames.Length];

        for (int i = 0; i < items.Length; i++)
        {
          items[i] = CreateNew();
        }

        AddRangeInternal(items, names);

        for (int i = 0; i < items.Length; i++)
        {
          SetName(items[i], tmpNames[i]);
        }

        return items;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}

using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class DBVisualStyleContainer : DBDictionaryEnumerable<DBVisualStyle>
  {
    internal DBVisualStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DBVisualStyle CreateNew()
    {
      return new DBVisualStyle();
    }

    public DBVisualStyle Create(string name)
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

    public void Add(DBVisualStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<DBVisualStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class DetailViewStyleContainer : DBDictionaryEnumerable<DetailViewStyle>
  {
    internal DetailViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DetailViewStyle CreateNew()
    {
      return new DetailViewStyle();
    }

    public DetailViewStyle Create(string name)
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

    public void Add(DetailViewStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<DetailViewStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class GroupContainer : DBDictionaryEnumerable<Group>
  {
    internal GroupContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Group CreateNew()
    {
      return new Group();
    }

    public Group Create(string name)
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

    public Group Create(string name, IEnumerable<Entity> entities)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.Generic("An object with name " + name + " already exists");

      try
      {
        var group = CreateInternal(name);

        if (entities.Any())
        {
          group.Append(new ObjectIdCollection(entities.Select(e => e.ObjectId)
                                                      .ToArray()));
        }

        return group;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Add(Group item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<Group> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class LayoutContainer : DBDictionaryEnumerable<Layout>
  {
    internal LayoutContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Layout CreateNew()
    {
      throw new NotImplementedException();
    }

    protected override Layout CreateInternal(string name)
    {
      return (Layout)transaction.GetObject(LayoutManager.Current.CreateLayout(name), OpenMode.ForWrite);
    }

    public Layout Create(string name)
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

    public void Add(Layout item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.LayoutName)) throw Error.InvalidName(item.LayoutName);
      if (Contains(item.LayoutName)) throw Error.Generic("An object with name " + item.LayoutName + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.LayoutName });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<Layout> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.LayoutName)) throw Error.InvalidName(item.LayoutName);
        if (Contains(item.LayoutName)) throw Error.Generic("An object with name " + item.LayoutName + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.LayoutName));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class MaterialContainer : DBDictionaryEnumerable<Material>
  {
    internal MaterialContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Material CreateNew()
    {
      return new Material();
    }

    public Material Create(string name)
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

    public void Add(Material item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<Material> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class MLeaderStyleContainer : DBDictionaryEnumerable<MLeaderStyle>
  {
    internal MLeaderStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override MLeaderStyle CreateNew()
    {
      return new MLeaderStyle();
    }

    public MLeaderStyle Create(string name)
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

    public void Add(MLeaderStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<MLeaderStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class MlineStyleContainer : DBDictionaryEnumerable<MlineStyle>
  {
    internal MlineStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override MlineStyle CreateNew()
    {
      return new MlineStyle();
    }

    public MlineStyle Create(string name)
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

    public void Add(MlineStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<MlineStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class PlotSettingsContainer : DBDictionaryEnumerable<PlotSettings>
  {
    private bool modelType;

    internal PlotSettingsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override PlotSettings CreateNew()
    {
      return new PlotSettings(modelType);
    }

    public PlotSettings Create(string name, bool modelType)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.Generic("An object with name " + name + " already exists");

      this.modelType = modelType;
      
      try
      {
        return CreateInternal(name);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Add(PlotSettings item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.PlotSettingsName)) throw Error.InvalidName(item.PlotSettingsName);
      if (Contains(item.PlotSettingsName)) throw Error.Generic("An object with name " + item.PlotSettingsName + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.PlotSettingsName });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<PlotSettings> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.PlotSettingsName)) throw Error.InvalidName(item.PlotSettingsName);
        if (Contains(item.PlotSettingsName)) throw Error.Generic("An object with name " + item.PlotSettingsName + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.PlotSettingsName));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class SectionViewStyleContainer : DBDictionaryEnumerable<SectionViewStyle>
  {
    internal SectionViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override SectionViewStyle CreateNew()
    {
      return new SectionViewStyle();
    }

    public SectionViewStyle Create(string name)
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

    public void Add(SectionViewStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<SectionViewStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class TableStyleContainer : DBDictionaryEnumerable<TableStyle>
  {
    internal TableStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override TableStyle CreateNew()
    {
      return new TableStyle();
    }

    public TableStyle Create(string name)
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

    public void Add(TableStyle item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      try
      {
        AddRangeInternal(new[] { item }, new[] { item.Name });
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void AddRange(IEnumerable<TableStyle> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      try
      {
        AddRangeInternal(items, items.Select(i => i.Name));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}

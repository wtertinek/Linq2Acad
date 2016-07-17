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

    protected override DBVisualStyle CreateNew(string name)
    {
      return new DBVisualStyle();
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

    protected override DetailViewStyle CreateNew(string name)
    {
      return new DetailViewStyle() { Name = name };
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

    protected override Group CreateNew(string name)
    {
      return new Group() { Name = name };
    }

    public Group Create(string name, IEnumerable<Entity> entities)
    {
      var group = Create(name);
      group.Append(new ObjectIdCollection(entities.Select(e => e.ObjectId)
                                                  .ToArray()));
      return group;
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

    protected override Layout CreateNew(string name)
    {
      return new Layout() { LayoutName = name };
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

    protected override Material CreateNew(string name)
    {
      return new Material() { Name = name };
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

    protected override MLeaderStyle CreateNew(string name)
    {
      return new MLeaderStyle() { Name = name };
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

    protected override MlineStyle CreateNew(string name)
    {
      return new MlineStyle() { Name = name };
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
    internal PlotSettingsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override PlotSettings CreateNew(string name)
    {
      // TODO: Select correct type
      return new PlotSettings(true) { PlotSettingsName = name };
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

    protected override SectionViewStyle CreateNew(string name)
    {
      return new SectionViewStyle() { Name = name };
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

    protected override TableStyle CreateNew(string name)
    {
      return new TableStyle() { Name = name };
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

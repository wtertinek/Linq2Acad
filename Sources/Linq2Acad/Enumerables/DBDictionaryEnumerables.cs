using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public sealed class DBVisualStyleContainer : DBDictionaryEnumerable<DBVisualStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<DBVisualStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(DBVisualStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<DBVisualStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<DBVisualStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<DBVisualStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class DetailViewStyleContainer : DBDictionaryEnumerable<DetailViewStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<DetailViewStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(DetailViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<DetailViewStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<DetailViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<DetailViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class GroupContainer : DBDictionaryEnumerable<Group>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<Group>(Contains(name), name);

      return CreateInternal(name);
    }

    public Group Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<Group>(Contains(name), name);

      var group = CreateInternal(name);

      if (entities.Any())
      {
        using (var idCollection = new ObjectIdCollection(entities.Select(e => e.ObjectId)
                                                                  .ToArray()))
        {
          group.Append(idCollection);
        }
      }

      return group;
    }

    public void Add(Group item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<Group>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<Group> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<Group>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class LayoutContainer : DBDictionaryEnumerable<Layout>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<Layout>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(Layout item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.LayoutName, nameof(item.LayoutName));
      Require.NameDoesNotExists<Layout>(Contains(item.LayoutName), item.LayoutName);

      AddRangeInternal(new[] { item }, new[] { item.LayoutName });
    }

    public void AddRange(IEnumerable<Layout> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.LayoutName, nameof(item.LayoutName));
        Require.NameDoesNotExists<Layout>(Contains(item.LayoutName), item.LayoutName);
      }

      AddRangeInternal(items, items.Select(i => i.LayoutName));
    }
  }

  public sealed class MaterialContainer : DBDictionaryEnumerable<Material>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<Material>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(Material item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<Material>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<Material> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<Material>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class MLeaderStyleContainer : DBDictionaryEnumerable<MLeaderStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<MLeaderStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(MLeaderStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<MLeaderStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<MLeaderStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<MLeaderStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class MlineStyleContainer : DBDictionaryEnumerable<MlineStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<MlineStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(MlineStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<MlineStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<MlineStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<MlineStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class PlotSettingsContainer : DBDictionaryEnumerable<PlotSettings>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<PlotSettings>(Contains(name), name);

      this.modelType = modelType;
      
      return CreateInternal(name);
    }

    public void Add(PlotSettings item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
      Require.NameDoesNotExists<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);

      AddRangeInternal(new[] { item }, new[] { item.PlotSettingsName });
    }

    public void AddRange(IEnumerable<PlotSettings> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
        Require.NameDoesNotExists<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);
      }

      AddRangeInternal(items, items.Select(i => i.PlotSettingsName));
    }
  }

  public sealed class SectionViewStyleContainer : DBDictionaryEnumerable<SectionViewStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<SectionViewStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(SectionViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<SectionViewStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<SectionViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<SectionViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }

  public sealed class TableStyleContainer : DBDictionaryEnumerable<TableStyle>
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
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<TableStyle>(Contains(name), name);

      return CreateInternal(name);
    }

    public void Add(TableStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<TableStyle>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<TableStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<TableStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }
}

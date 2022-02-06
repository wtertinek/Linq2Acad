using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the elements of the DBVisualStyle ditionary.
  /// </summary>
  public sealed class DBVisualStyleContainer : DBDictionaryEnumerable<DBVisualStyle>
  {
    internal DBVisualStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new DBVisualStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DBVisualStyle element.</param>
    public DBVisualStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<DBVisualStyle>(Contains(name), name);

      return AddInternal(new DBVisualStyle(), name);
    }

    /// <summary>
    /// Adds a newly created DBVisualStyle element.
    /// </summary>
    /// <param name="item">The DBVisualStyle element to add.</param>
    public void Add(DBVisualStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<DBVisualStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created DBVisualStyle elements.
    /// </summary>
    /// <param name="items">The DBVisualStyle elements to add.</param>
    public void AddRange(IEnumerable<DBVisualStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<DBVisualStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the DetailViewStyle ditionary.
  /// </summary>
  public sealed class DetailViewStyleContainer : DBDictionaryEnumerable<DetailViewStyle>
  {
    internal DetailViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new DetailViewStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DetailViewStyle element.</param>
    public DetailViewStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<DetailViewStyle>(Contains(name), name);

      return AddInternal(new DetailViewStyle(), name);
    }

    /// <summary>
    /// Adds a newly created DetailViewStyle element.
    /// </summary>
    /// <param name="item">The DetailViewStyle element to add.</param>
    public void Add(DetailViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<DetailViewStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created DetailViewStyle elements.
    /// </summary>
    /// <param name="items">The DetailViewStyle elements to add.</param>
    public void AddRange(IEnumerable<DetailViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<DetailViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Group ditionary.
  /// </summary>
  public sealed class GroupContainer : DBDictionaryEnumerable<Group>
  {
    internal GroupContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Group element.
    /// </summary>
    /// <param name="name">The unique name of the Group element.</param>
    public Group Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Group>(Contains(name), name);

      return AddInternal(new Group(), name);
    }

    /// <summary>
    /// Creates a new DBVisualStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DBVisualStyle element.</param>
    /// <param name="entities">The entities to be added to the group.</param>
    public Group Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Group>(Contains(name), name);

      var group = AddInternal(new Group(), name);

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

    /// <summary>
    /// Adds a newly created Group element.
    /// </summary>
    /// <param name="item">The Group element to add.</param>
    public void Add(Group item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<Group>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created Group elements.
    /// </summary>
    /// <param name="items">The Group elements to add.</param>
    public void AddRange(IEnumerable<Group> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<Group>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Layout ditionary.
  /// </summary>
  public sealed class LayoutContainer : DBDictionaryEnumerable<Layout>
  {
    internal LayoutContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Layout element.
    /// </summary>
    /// <param name="name">The unique name of the Layout element.</param>
    public Layout Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Layout>(Contains(name), name);

      return AddInternal((Layout)transaction.GetObject(LayoutManager.Current.CreateLayout(name), OpenMode.ForWrite), name);
    }

    /// <summary>
    /// Adds a newly created Layout element.
    /// </summary>
    /// <param name="item">The Layout to element add.</param>
    public void Add(Layout item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.LayoutName, nameof(item.LayoutName));
      Require.NameDoesNotExist<Layout>(Contains(item.LayoutName), item.LayoutName);

      AddRangeInternal(new[] { (item, item.LayoutName) });
    }

    /// <summary>
    /// Adds a collection of newly created Layout elements.
    /// </summary>
    /// <param name="items">The Layout elements to add.</param>
    public void AddRange(IEnumerable<Layout> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.LayoutName, nameof(item.LayoutName));
        Require.NameDoesNotExist<Layout>(Contains(item.LayoutName), item.LayoutName);
      }

      AddRangeInternal(items.Select(i => (i, i.LayoutName)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Material ditionary.
  /// </summary>
  public sealed class MaterialContainer : DBDictionaryEnumerable<Material>
  {
    internal MaterialContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Material element.
    /// </summary>
    /// <param name="name">The unique name of the Material element.</param>
    public Material Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Material>(Contains(name), name);

      return AddInternal(new Material(), name);
    }

    /// <summary>
    /// Adds a newly created Material element.
    /// </summary>
    /// <param name="item">The Material to element add.</param>
    public void Add(Material item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<Material>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created Material elements.
    /// </summary>
    /// <param name="items">The Material elements to add.</param>
    public void AddRange(IEnumerable<Material> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<Material>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the MLeaderStyle ditionary.
  /// </summary>
  public sealed class MLeaderStyleContainer : DBDictionaryEnumerable<MLeaderStyle>
  {
    internal MLeaderStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new MLeaderStyle element.
    /// </summary>
    /// <param name="name">The unique name of the MLeaderStyle element.</param>
    public MLeaderStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<MLeaderStyle>(Contains(name), name);

      return AddInternal(new MLeaderStyle(), name);
    }

    /// <summary>
    /// Adds a newly created MLeaderStyle element.
    /// </summary>
    /// <param name="item">The MLeaderStyle to element add.</param>
    public void Add(MLeaderStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<MLeaderStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created MLeaderStyle elements.
    /// </summary>
    /// <param name="items">The MLeaderStyle elements to add.</param>
    public void AddRange(IEnumerable<MLeaderStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<MLeaderStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the MLineStyle ditionary.
  /// </summary>
  public sealed class MlineStyleContainer : DBDictionaryEnumerable<MlineStyle>
  {
    internal MlineStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new MlineStyle element.
    /// </summary>
    /// <param name="name">The unique name of the MlineStyle element.</param>
    public MlineStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<MlineStyle>(Contains(name), name);

      return AddInternal(new MlineStyle(), name);
    }

    /// <summary>
    /// Adds a newly created MlineStyle element.
    /// </summary>
    /// <param name="item">The MlineStyle to element add.</param>
    public void Add(MlineStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<MlineStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created MlineStyle elements.
    /// </summary>
    /// <param name="items">The MlineStyle elements to add.</param>
    public void AddRange(IEnumerable<MlineStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<MlineStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the PlottSettings ditionary.
  /// </summary>
  public sealed class PlotSettingsContainer : DBDictionaryEnumerable<PlotSettings>
  {
    private bool modelType;

    internal PlotSettingsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new PlotSettings element.
    /// </summary>
    /// <param name="name">The unique name of the PlotSettings element.</param>
    /// <param name="modelType">Determines the plot setup type.</param>
    public PlotSettings Create(string name, bool modelType)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<PlotSettings>(Contains(name), name);

      this.modelType = modelType;
      
      return AddInternal(new PlotSettings(modelType), name);
    }

    /// <summary>
    /// Adds a newly created PlotSettings element.
    /// </summary>
    /// <param name="item">The PlotSettings to element add.</param>
    public void Add(PlotSettings item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
      Require.NameDoesNotExist<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);

      AddRangeInternal(new[] { (item , item.PlotSettingsName) });
    }

    /// <summary>
    /// Adds a collection of newly created PlotSettings elements.
    /// </summary>
    /// <param name="items">The PlotSettings elements to add.</param>
    public void AddRange(IEnumerable<PlotSettings> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
        Require.NameDoesNotExist<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);
      }

      AddRangeInternal(items.Select(i => (i, i.PlotSettingsName)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the SectionViewStyle ditionary.
  /// </summary>
  public sealed class SectionViewStyleContainer : DBDictionaryEnumerable<SectionViewStyle>
  {
    internal SectionViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new SectionViewStyle element.
    /// </summary>
    /// <param name="name">The unique name of the SectionViewStyle element.</param>
    public SectionViewStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<SectionViewStyle>(Contains(name), name);

      return AddInternal(new SectionViewStyle(), name);
    }

    /// <summary>
    /// Adds a newly created SectionViewStyle element.
    /// </summary>
    /// <param name="item">The SectionViewStyle to element add.</param>
    public void Add(SectionViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<SectionViewStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created SectionViewStyle elements.
    /// </summary>
    /// <param name="items">The SectionViewStyle elements to add.</param>
    public void AddRange(IEnumerable<SectionViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<SectionViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the TableStyle ditionary.
  /// </summary>
  public sealed class TableStyleContainer : DBDictionaryEnumerable<TableStyle>
  {
    internal TableStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new TableStyle element.
    /// </summary>
    /// <param name="name">The unique name of the TableStyle element.</param>
    public TableStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<TableStyle>(Contains(name), name);

      return AddInternal(new TableStyle(), name);
    }

    /// <summary>
    /// Adds a newly created TableStyle element.
    /// </summary>
    /// <param name="item">The TableStyle to element add.</param>
    public void Add(TableStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<TableStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created TableStyle elements.
    /// </summary>
    /// <param name="items">The TableStyle elements to add.</param>
    public void AddRange(IEnumerable<TableStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<TableStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }
}

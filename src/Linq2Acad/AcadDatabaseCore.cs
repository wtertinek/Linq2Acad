using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// The main class that provides access to the drawing database.
  /// </summary>
  public abstract class AcadDatabaseCore
  {
    protected readonly Transaction transaction;
    private readonly AcadSummaryInfo summaryInfo;

    protected AcadDatabaseCore(Database database, Transaction transaction)
    {
      Database = database;
      this.transaction = transaction;
      summaryInfo = new AcadSummaryInfo(database);
    }

    /// <summary>
    /// The drawing database in use.
    /// </summary>
    public Database Database { get; }

    /// <summary>
    /// Provies access to the summary info.
    /// </summary>
    public AcadSummaryInfo SummaryInfo
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return summaryInfo;
      }
    }

    /// <summary>
    /// Provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.
    /// </summary>
    public DbObjectContainer DbObjects
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new DbObjectContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to all style related tables and dictionaries.
    /// </summary>
    public StylesContainer Styles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new StylesContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to all XRef elements and methods to attach, overlay, resolve, reload and unload XRefs.
    /// </summary>
    public XRefContainer XRefs
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new XRefContainer(Database, transaction);
      }
    }

    #region Tables

    /// <summary>
    /// Provides access to the elements of the Block table and methods to create, add and import BlockTableRecords.
    /// </summary>
    public BlockContainer Blocks
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new BlockContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Layer table and methods to create, add and import LayerTableRecords.
    /// </summary>
    public LayerContainer Layers
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new LayerContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Linetype table and methods to create, add and import LinetypeTableRecords.
    /// </summary>
    public LinetypeContainer Linetypes
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new LinetypeContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the RegApp table and methods to create, add and import RegAppTableRecords.
    /// </summary>
    public RegAppContainer RegApps
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new RegAppContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Ucs table and methods to create, add and import UcsTableRecords.
    /// </summary>
    public UcsContainer Ucss
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new UcsContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Viewport table and methods to create, add and import ViewportTableRecords.
    /// </summary>
    public ViewportContainer Viewports
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new ViewportContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the View table and methods to create, add and import ViewTableRecords.
    /// </summary>
    public ViewContainer Views
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new ViewContainer(Database, transaction);
      }
    }

    #endregion

    #region Dictionaries

    /// <summary>
    /// Provides access to the elements of the Layout dictionary and methods to create, add and import Layouts.
    /// </summary>
    public LayoutContainer Layouts
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new LayoutContainer(Database, transaction, Database.LayoutDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Group dictionary and methods to create, add and import Groups.
    /// </summary>
    public GroupContainer Groups
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new GroupContainer(Database, transaction, Database.GroupDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Material dictionary and methods to create, add and import Materials.
    /// </summary>
    public MaterialContainer Materials
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new MaterialContainer(Database, transaction, Database.MaterialDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the PlotSettings dictionary and methods to create, add and import PlotSettings.
    /// </summary>
    public PlotSettingsContainer PlotSettings
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new PlotSettingsContainer(Database, transaction, Database.PlotSettingsDictionaryId);
      }
    }

    #endregion

    #region CurrentSpace | ModelSpace | PaperSpace

    /// <summary>
    /// Provides access to the entities of the currently active space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
    /// </summary>
    public EntityContainer CurrentSpace
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);
        return new EntityContainer(Database, transaction, Database.CurrentSpaceId);
      }
    }

    /// <summary>
    /// Provides access to the entities of the model space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
    /// </summary>
    public EntityContainer ModelSpace
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);

        var modelSpaceId = ((BlockTable)transaction.GetObject(Database.BlockTableId, OpenMode.ForRead))[BlockTableRecord.ModelSpace];
        return new EntityContainer(Database, transaction, modelSpaceId);
      }
    }

    /// <summary>
    /// Provides access to the entities of the paper space layouts.
    /// </summary>
    public IEnumerable<PaperSpaceEntityContainer> PaperSpace
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);

        return new PaperSpaceLayoutContainer(Database, transaction);
      }
    }

    #endregion

    #region Overrides to remove methods from IntelliSense

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Equals(object obj)
      => base.Equals(obj);

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int GetHashCode()
      => base.GetHashCode();

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override string ToString()
      => base.ToString();

    #endregion
  }
}

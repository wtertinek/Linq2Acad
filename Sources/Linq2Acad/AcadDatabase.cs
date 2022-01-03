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
  /// DWG version enum.
  /// </summary>
  public enum SaveAsDwgVersion
  {
    /// <summary>
    /// This is the default value
    /// </summary>
    DontChange,
    DWG2004,
    DWG2007,
    DWG2010,
    DWG2013,
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022
    DWG2018,
#endif
    NewestAvailable
  }

  public class CreateOptions
  {
    /// <summary>
    /// If specified, path to save the database to.
    /// </summary>
    public string SaveFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Defaults to that used by the running AutoCAD version. 
    /// </summary>
    public SaveAsDwgVersion SaveDwgVersion { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }

  public class OpenReadOnlyOptions
  {
    public string Password { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }

  public class OpenForEditOptions
  {
    public string SaveAsFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Keeps the current version by default.
    /// </summary>
    public SaveAsDwgVersion DwgVersion { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }

    public string Password { get; set; }
  }

  /// <summary>
  /// The main class that provides access to the drawing database.
  /// </summary>
  public sealed class AcadDatabase : IDisposable
  {
    private readonly bool commitTransaction;
    private readonly bool disposeTransaction;
    private readonly bool disposeDatabase;
    private readonly bool saveOnCommit;
    private readonly string saveAsFileName;
    private readonly SaveAsDwgVersion dwgVersion;
    private Transaction transaction;
    private readonly AcadSummaryInfo summaryInfo;

    /// <summary>
    /// Creates a new instance of AcadDatabase.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <param name="saveOnCommit">True, if the database should be saved during the commit. False, if the database should not be saved.</param>
    /// <param name="outFileName">Path to which save the database if saveOnCommit is True.</param>
    /// <param name="dwgVersion">DWG version to use when saving the database.</param>
    private AcadDatabase(Database database, bool keepOpen, bool saveOnCommit, string outFileName, SaveAsDwgVersion dwgVersion)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
      this.saveOnCommit = saveOnCommit;
      saveAsFileName = outFileName;
      this.dwgVersion = dwgVersion;
      disposeDatabase = !keepOpen;
    }

    /// <summary>
    /// Creates a new instance of AcadDatabase.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    private AcadDatabase(Database database, bool keepOpen)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
      disposeDatabase = !keepOpen;
      saveOnCommit = false;
    }

    /// <summary>
    /// Creates a new instance of AcadDatabase.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commitTransaction">True, if the transaction in use should be committed when this instance is disposed of.</param>
    /// <param name="disposeTransaction">True, if the transaction in use should be disposed of when this instance is disposed of.</param>
    private AcadDatabase(Database database, Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      Database = database;
      this.transaction = transaction;
      this.commitTransaction = commitTransaction;
      this.disposeTransaction = disposeTransaction;
      summaryInfo = new AcadSummaryInfo(database);
    }

    /// <summary>
    /// The darawing database in use.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public Database Database { get; }

    /// <summary>
    /// Immediately discards all changes and the underlying transaction.
    /// </summary>
    public void DiscardChanges()
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      if (!transaction.IsDisposed)
      {
        transaction.Abort();
        transaction.Dispose();
      }

      transaction = Database.TransactionManager.StartTransaction();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      if (commitTransaction)
      {
        transaction.Commit();

        if (SummaryInfo.Changed)
        {
          SummaryInfo.Commit();
        }

        if (saveOnCommit)
        {
          Database.SaveAs(saveAsFileName, GetDwgVersion());
        }
      }

      if (disposeTransaction)
      {
        transaction.Dispose();
      }

      if (disposeDatabase)
      {
        Database.Dispose();
      }
    }

    /// <summary>
    /// Convert the SaveAsDwgVersion enum to Autodesk's DwgVersion.
    /// </summary>
    /// <returns>Autodesk DwgVersion.</returns>
    private DwgVersion GetDwgVersion()
    {
      switch (dwgVersion)
      {
        case SaveAsDwgVersion.DWG2004:
          return DwgVersion.AC1015;
        case SaveAsDwgVersion.DWG2007:
          return DwgVersion.AC1021;
        case SaveAsDwgVersion.DWG2010:
          return DwgVersion.AC1024;
        case SaveAsDwgVersion.DWG2013:
          return DwgVersion.AC1027;
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022
        case SaveAsDwgVersion.DWG2018:
          return DwgVersion.AC1032;
#endif
        case SaveAsDwgVersion.NewestAvailable:
          return DwgVersion.Newest;
        case SaveAsDwgVersion.DontChange:
        default:
          return Database.OriginalFileSavedByVersion;
       }
    }

    /// <summary>
    /// Adds the given object to the underlaying transaction. This is only needed for objects that are not stored in containers (e.g. AttributeReference).
    /// </summary>
    /// <param name="obj">The object to add to the transaction.</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public void AddNewlyCreatedDBObject(DBObject obj)
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(obj, nameof(obj));

      transaction.AddNewlyCreatedDBObject(obj, true);
    }

    /// <summary>
    /// Accesses the database's summary info.
    /// </summary>
    public AcadSummaryInfo SummaryInfo
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return summaryInfo;
      }
    }

    #region Element | Elements

    /// <summary>
    /// Returns the database object with the given ObjectId. The object is readonly.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.IsValid(id, nameof(id));

      return ElementInternal<T>(id, false);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.IsValid(id, nameof(id));

      return ElementInternal<T>(id, forWrite);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId, or a default value if the element does not exist. The object is readonly.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T ElementOrDefault<T>(ObjectId id) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        return ElementInternal<T>(id, false);
      }
    }

    /// <summary>
    /// Returns the database object with the given ObjectId, or a default value if the element does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T ElementOrDefault<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        return ElementInternal<T>(id, forWrite);
      }
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <returns>The object with the given ObjectId.</returns>
    private T ElementInternal<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds. The objects are readonly.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ElementsValid(ids, nameof(ids));

      return ElementsInternal<T>(ids, false);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ElementsValid(ids, nameof(ids));

      return ElementsInternal<T>(ids, forWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds. The objects are readonly.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(ObjectIdCollection ids) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids.Cast<ObjectId>(), nameof(ids));

      return ElementsInternal<T>(ids.Cast<ObjectId>(), false);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(ObjectIdCollection ids, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids.Cast<ObjectId>(), nameof(ids));

      return ElementsInternal<T>(ids.Cast<ObjectId>(), forWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <returns>The objects with the given ObjectIds.</returns>
    private IEnumerable<T> ElementsInternal<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      var openMode = forWrite ? OpenMode.ForWrite : OpenMode.ForRead;

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, openMode);
      }
    }

    #endregion

    #region Tables

    /// <summary>
    /// Provides access to the elements of the Block table.
    /// </summary>
    public BlockContainer Blocks
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new BlockContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Layer table.
    /// </summary>
    public LayerContainer Layers
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new LayerContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the DimStyle table.
    /// </summary>
    public DimStyleContainer DimStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new DimStyleContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Linetype table.
    /// </summary>
    public LinetypeContainer Linetypes
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new LinetypeContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the RegApp table.
    /// </summary>
    public RegAppContainer RegApps
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new RegAppContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the TextStyle table.
    /// </summary>
    public TextStyleContainer TextStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new TextStyleContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Ucs table.
    /// </summary>
    public UcsContainer Ucss
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new UcsContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Viewport table.
    /// </summary>
    public ViewportContainer Viewports
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new ViewportContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the elements of the View table.
    /// </summary>
    public ViewContainer Views
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new ViewContainer(Database, transaction);
      }
    }

    /// <summary>
    /// Provides access to the XRef elements.
    /// </summary>
    public XRefContainer XRefs
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new XRefContainer(Database, transaction);
      }
    }

    #endregion

    #region Dictionaries

    /// <summary>
    /// Provides access to the elements of the Layout dictionary.
    /// </summary>
    public LayoutContainer Layouts
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new LayoutContainer(Database, transaction, Database.LayoutDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Group dictionary.
    /// </summary>
    public GroupContainer Groups
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new GroupContainer(Database, transaction, Database.GroupDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the MLeaderStyle dictionary.
    /// </summary>
    public MLeaderStyleContainer MLeaderStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new MLeaderStyleContainer(Database, transaction, Database.MLeaderStyleDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the MlineStyle dictionary.
    /// </summary>
    public MlineStyleContainer MlineStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new MlineStyleContainer(Database, transaction, Database.MLStyleDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the Material dictionary.
    /// </summary>
    public MaterialContainer Materials
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new MaterialContainer(Database, transaction, Database.MaterialDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the DBVisualStyle dictionary.
    /// </summary>
    public DBVisualStyleContainer DBVisualStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new DBVisualStyleContainer(Database, transaction, Database.VisualStyleDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the PlotSetting dictionary.
    /// </summary>
    public PlotSettingsContainer PlotSettings
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new PlotSettingsContainer(Database, transaction, Database.PlotSettingsDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the TableStyle dictionary.
    /// </summary>
    public TableStyleContainer TableStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new TableStyleContainer(Database, transaction, Database.TableStyleDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the SectionViewStyle dictionary.
    /// </summary>
    public SectionViewStyleContainer SectionViewStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new SectionViewStyleContainer(Database, transaction, Database.SectionViewStyleDictionaryId);
      }
    }

    /// <summary>
    /// Provides access to the elements of the DetailViewStyle dictionary.
    /// </summary>
    public DetailViewStyleContainer DetailViewStyles
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new DetailViewStyleContainer(Database, transaction, Database.DetailViewStyleDictionaryId);
      }
    }

    #endregion

    #region CurrentSpace | ModelSpace | PaperSpace

    /// <summary>
    /// Provides access to the entities of the currently active space.
    /// </summary>
    public EntityContainer CurrentSpace
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        return new EntityContainer(Database, transaction, Database.CurrentSpaceId);
      }
    }

    /// <summary>
    /// Provides access to the model space entities.
    /// </summary>
    public EntityContainer ModelSpace
    {
      get
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        var modelSpaceId = ((BlockTable)transaction.GetObject(Database.BlockTableId, OpenMode.ForRead))[BlockTableRecord.ModelSpace];
        return new EntityContainer(Database, transaction, modelSpaceId);
      }
    }

    /// <summary>
    /// Provides access to the entities of all layouts. Each element provides access to the entities of one layout.
    /// The elements are in the order of the AutoCAD layout tabs.
    /// </summary>
    /// <returns>An IEnumerable&lt;EntityContainer&gt;. Each EntityContainer provides access to the entities of one layout.</returns>
    public IEnumerable<EntityContainer> PaperSpace()
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      foreach (var layout in Layouts.Where(l => !l.ModelType)
                                    .OrderBy(l => l.TabOrder))
      {
        yield return new EntityContainer(Database, transaction, layout.BlockTableRecordId);
      }
    }

    /// <summary>
    /// Provides access to the entities of the layout with the given tab index.
    /// </summary>
    /// <param name="index">The zero-based tab index of the layout.</param>
    /// <returns>An EntityContainer to access the layout's entities.</returns>
    public EntityContainer PaperSpace(int index)
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

      var paperSpaceLayouts = Layouts.Where(l => !l.ModelType)
                                     .OrderBy(l => l.TabOrder)
                                     .ToArray();

      Require.ValidArrayIndex(index, paperSpaceLayouts.Length, nameof(index));

      return new EntityContainer(Database, transaction, paperSpaceLayouts.ElementAt(index)
                                                               .BlockTableRecordId);
    }

    /// <summary>
    /// Provides access to the entities of the layout with the given name.
    /// </summary>
    /// <param name="name">The name of the layout.</param>
    /// <returns>An EntityContainer to access the layout's entities.</returns>
    public EntityContainer PaperSpace(string name)
    {
      Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(name, nameof(name));
      Require.NameExists<Layout>(Layouts.Contains(name), nameof(name));

      var layout = Layouts.Element(name);

      Require.IsTrue(layout.ModelType, $"{name} is not a paper space layout");

      return new EntityContainer(Database, transaction, layout.BlockTableRecordId);
    }

    #endregion

    #region Factory methods

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when creating the drawing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create()
    {
      return Create(new CreateOptions()
      {
        KeepDatabaseOpen = false,
        SaveDwgVersion = SaveAsDwgVersion.NewestAvailable
      });
    }

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <param name="options">Database create options.</param>
    /// <exception cref="System.Exception">Thrown when creating the drawing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create(CreateOptions options)
    {
      bool saveOnCommit = !string.IsNullOrEmpty(options.SaveFileName);
      return new AcadDatabase(new Database(true, true), options.KeepDatabaseOpen, saveOnCommit, options.SaveFileName, options.SaveDwgVersion);
    }

    /// <summary>
    /// Provides access to the drawing database of the active document.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when no active document is available.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Active()
    {
      Require.ObjectNotNull(Application.DocumentManager.MdiActiveDocument, "No active document");

      return new AcadDatabase(Application.DocumentManager.MdiActiveDocument.Database, true);
    }

    /// <summary>
    /// Provides access to the drawing database of the active document.
    /// </summary>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commitTransaction">True, if the transaction in use should be committed when this instance is disposed of.</param>
    /// <param name="disposeTransaction">True, if the transaction in use should be disposed of when this instance is disposed of.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>transaction</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when no active document is available or the transaction is invalid.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public static AcadDatabase Active(Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      Require.ObjectNotNull(Application.DocumentManager.MdiActiveDocument, "No active document");
      Require.ParameterNotNull(transaction, nameof(transaction));
      Require.NotDisposed(transaction.IsDisposed, nameof(Transaction), nameof(transaction));

      return new AcadDatabase(Application.DocumentManager.MdiActiveDocument.Database, transaction, commitTransaction, disposeTransaction);
    }

    /// <summary>
    /// Provides access to the given drawing database.
    /// </summary>
    /// <param name="database">The darwing database to use.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>database</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the database is invalid.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Use(Database database)
    {
      Require.ParameterNotNull(database, nameof(database));
      Require.NotDisposed(database.IsDisposed, nameof(Database), nameof(database));

      return new AcadDatabase(database, true);
    }

    /// <summary>
    /// Provides access to the given drawing database.
    /// </summary>
    /// <param name="database">The darwing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commitTransaction">True, if the transaction in use should be committed when this instance is disposed of.</param>
    /// <param name="disposeTransaction">True, if the transaction in use should be disposed of when this instance is disposed of.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>database</i> or <i>transaction</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the database or the transaction is invalid.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public static AcadDatabase Use(Database database, Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      Require.ParameterNotNull(database, nameof(database));
      Require.NotDisposed(database.IsDisposed, nameof(Database), nameof(database));
      Require.ParameterNotNull(transaction, nameof(transaction));
      Require.NotDisposed(transaction.IsDisposed, nameof(transaction));

      return new AcadDatabase(database, transaction, commitTransaction, disposeTransaction);
    }

    /// <summary>
    /// Provides read only access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenReadOnly(string fileName)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      return OpenInternal(fileName, null, false);
    }

    /// <summary>
    /// Provides read only access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="options">Options for opening the database.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenReadOnly(string fileName, OpenReadOnlyOptions options)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      return OpenInternal(fileName, options.Password, options.KeepDatabaseOpen);
    }

    /// <summary>
    /// Provides read/write access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenForEdit(string fileName)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));
      return OpenForEdit(fileName, new OpenForEditOptions()
      {
        SaveAsFileName = fileName,
        KeepDatabaseOpen = false,
        DwgVersion = SaveAsDwgVersion.DontChange,
      });
    }

    /// <summary>
    /// Provides read/write access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="options">Options for opening and closing the database.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenForEdit(string fileName, OpenForEditOptions options)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      Database database = GetDatabase(fileName, false, options.Password);
      var outFileName = options.SaveAsFileName ?? fileName;
      return new AcadDatabase(database, options.KeepDatabaseOpen, true, outFileName, options.DwgVersion);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <returns>The AcadDatabase instance.</returns>
    private static AcadDatabase OpenInternal(string fileName, string password, bool keepOpen)
    {
      Database database = GetDatabase(fileName, true, password);
      return new AcadDatabase(database, keepOpen);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="readOnly">If true open the drawing in read only mode. If false, open it in read/write mode.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <returns>The Autocad Database instance.</returns>
    private static Database GetDatabase(string fileName, bool readOnly, string password)
    {
      var database = new Database(false, true);
      database.ReadDwgFile(fileName, readOnly ? FileOpenMode.OpenForReadAndReadShare : FileOpenMode.OpenForReadAndWriteNoShare, false, password);
      database.CloseInput(true);
      return database;
    }

    #endregion

    #region Overrides to remove methods from IntelliSense

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public new Type GetType()
    {
      return base.GetType();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override string ToString()
    {
      return base.ToString();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static new bool Equals(object objA, object objB)
    {
      return Object.Equals(objA, objB);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static new bool ReferenceEquals(object objA, object objB)
    {
      return Object.ReferenceEquals(objA, objB);
    }

    #endregion
  }
}

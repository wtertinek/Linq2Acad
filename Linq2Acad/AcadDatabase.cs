using Autodesk.AutoCAD.ApplicationServices;
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
  public class AcadDatabase : IDisposable
  {
    private Transaction transaction;
    private bool commitTransaction;
    private bool disposeTransaction;
    private bool disposeDatabase;
    private bool abort;

    /// <summary>
    /// Creates a new instance of AcadDatabase.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    private AcadDatabase(Database database, bool keepOpen)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
      disposeDatabase = !keepOpen;
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
    }

    /// <summary>
    /// The darawing database in use.
    /// </summary>
    public Database Database { get; private set; }

    /// <summary>
    /// Discards all changes on the underlying transaction.
    /// </summary>
    public void DiscardChanges()
    {
      abort = true;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      try
      {
        DisposeInternal(false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="force">True, if the drawing database and the transaction should be disposed of, even if keepOpen was used.</param>
    public void Dispose(bool force)
    {
      try
      {
        DisposeInternal(force);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="force">True, if the drawing database and the transaction should be disposed of, even if keepOpen was used.</param>
    private void DisposeInternal(bool force)
    {
      if (!transaction.IsDisposed)
      {
        if (abort)
        {
          transaction.Abort();
        }
        else
        {
          if (commitTransaction)
          {
            transaction.Commit();
          }

          if (disposeTransaction || force)
          {
            transaction.Dispose();
          }
        }
      }

      if ((disposeDatabase || force) &&
          !Database.IsDisposed)
      {
        Database.Dispose();
      }
    }

    /// <summary>
    /// Saves the drawing database to the file with the given name. The newest DWG version is used.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when saving the drawing database throws an exception.</exception>
    /// <param name="fileName">The name of the file.</param>
    public void SaveAs(string fileName)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");

      try
      {
        Database.SaveAs(fileName, DwgVersion.Newest);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e, "Error saving drawing database to file " + fileName);
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
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");
      
      try
      {
        return ElementInternal<T>(id, false);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");

      try
      {
        return ElementInternal<T>(id, forWrite);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        try
        {
          return ElementInternal<T>(id, false);
        }
        catch (InvalidCastException e)
        {
          throw e;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
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
      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        try
        {
          return ElementInternal<T>(id, forWrite);
        }
        catch (InvalidCastException e)
        {
          throw e;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
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
      if (ids == null) throw Error.ArgumentNull("ids");
      
      try
      {
        return ElementsInternal<T>(ids, false);
      }
      catch (InvalidObjectException e)
      {
        throw Error.Generic(e.Message);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      if (ids == null) throw Error.ArgumentNull("ids");

      try
      {
        return ElementsInternal<T>(ids, forWrite);
      }
      catch (InvalidObjectException e)
      {
        throw Error.Generic(e.Message);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      if (ids == null) throw Error.ArgumentNull("ids");
      
      try
      {
        return ElementsInternal<T>(ids.Cast<ObjectId>(), false);
      }
      catch (InvalidObjectException e)
      {
        throw Error.Generic(e.Message);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      if (ids == null) throw Error.ArgumentNull("ids");

      try
      {
        return ElementsInternal<T>(ids.Cast<ObjectId>(), forWrite);
      }
      catch (InvalidObjectException e)
      {
        throw Error.Generic(e.Message);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
        if (!id.IsValid) throw Error.InvalidObject("ObjectId");
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
      get { return new BlockContainer(Database, transaction, Database.BlockTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the Layer table.
    /// </summary>
    public LayerContainer Layers
    {
      get { return new LayerContainer(Database, transaction, Database.LayerTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the DimStyle table.
    /// </summary>
    public DimStyleContainer DimStyles
    {
      get { return new DimStyleContainer(Database, transaction, Database.DimStyleTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the Linetype table.
    /// </summary>
    public LinetypeContainer Linetypes
    {
      get { return new LinetypeContainer(Database, transaction, Database.LinetypeTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the RegApp table.
    /// </summary>
    public RegAppContainer RegApps
    {
      get { return new RegAppContainer(Database, transaction, Database.RegAppTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the TextStyle table.
    /// </summary>
    public TextStyleContainer TextStyles
    {
      get { return new TextStyleContainer(Database, transaction, Database.TextStyleTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the Ucs table.
    /// </summary>
    public UcsContainer Ucss
    {
      get { return new UcsContainer(Database, transaction, Database.UcsTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the Viewport table.
    /// </summary>
    public ViewportContainer Viewports
    {
      get { return new ViewportContainer(Database, transaction, Database.ViewportTableId); }
    }

    /// <summary>
    /// Provides access to the elements of the View table.
    /// </summary>
    public ViewContainer Views
    {
      get { return new ViewContainer(Database, transaction, Database.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    /// <summary>
    /// Provides access to the elements of the Layout dictionary.
    /// </summary>
    public LayoutContainer Layouts
    {
      get { return new LayoutContainer(Database, transaction, Database.LayoutDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the Group dictionary.
    /// </summary>
    public GroupContainer Groups
    {
      get { return new GroupContainer(Database, transaction, Database.GroupDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the MLeaderStyle dictionary.
    /// </summary>
    public MLeaderStyleContainer MLeaderStyles
    {
      get { return new MLeaderStyleContainer(Database, transaction, Database.MLeaderStyleDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the MlineStyle dictionary.
    /// </summary>
    public MlineStyleContainer MlineStyles
    {
      get { return new MlineStyleContainer(Database, transaction, Database.MLStyleDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the Material dictionary.
    /// </summary>
    public MaterialContainer Materials
    {
      get { return new MaterialContainer(Database, transaction, Database.MaterialDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the DBVisualStyle dictionary.
    /// </summary>
    public DBVisualStyleContainer DBVisualStyles
    {
      get { return new DBVisualStyleContainer(Database, transaction, Database.VisualStyleDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the PlotSetting dictionary.
    /// </summary>
    public PlotSettingsContainer PlotSettings
    {
      get { return new PlotSettingsContainer(Database, transaction, Database.PlotSettingsDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the TableStyle dictionary.
    /// </summary>
    public TableStyleContainer TableStyles
    {
      get { return new TableStyleContainer(Database, transaction, Database.TableStyleDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the SectionViewStyle dictionary.
    /// </summary>
    public SectionViewStyleContainer SectionViewStyles
    {
      get { return new SectionViewStyleContainer(Database, transaction, Database.SectionViewStyleDictionaryId); }
    }

    /// <summary>
    /// Provides access to the elements of the DetailViewStyle dictionary.
    /// </summary>
    public DetailViewStyleContainer DetailViewStyles
    {
      get { return new DetailViewStyleContainer(Database, transaction, Database.DetailViewStyleDictionaryId); }
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
        try
        {
          return new EntityContainer(Database, transaction, Database.CurrentSpaceId);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    /// <summary>
    /// Provides access to the model space entities.
    /// </summary>
    public EntityContainer ModelSpace
    {
      get
      {
        try
        {
          var modelSpaceId = ((BlockTable)transaction.GetObject(Database.BlockTableId, OpenMode.ForRead))[BlockTableRecord.ModelSpace];
          return new EntityContainer(Database, transaction, modelSpaceId);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    /// <summary>
    /// Provides access to the entities of all layouts. Each element provides access to the entities of one layout.
    /// The elements are in the order of the AutoCAD layout tabs.
    /// </summary>
    /// <returns>An IEnumerable&lt;EntityContainer&gt;. Each EntityContainer provides access to the entities of one layout.</returns>
    public IEnumerable<EntityContainer> PaperSpace()
    {
      foreach (var layout in Layouts.OrderBy(l => l.TabOrder))
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
      var layouts = Layouts.ToArray();
      if (index <= 0 || index >= layouts.Length) throw Error.IndexOutOfRange("index", layouts.Length);
      
      var blockId = Layouts.OrderBy(l => l.TabOrder)
                           .ElementAt(index)
                           .BlockTableRecordId;
      try
      {
        return new EntityContainer(Database, transaction, blockId);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Provides access to the entities of the layout with the given name.
    /// </summary>
    /// <param name="name">The name of the layout.</param>
    /// <returns>An EntityContainer to access the layout's entities.</returns>
    public EntityContainer PaperSpace(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");

      try
      {
        var layout = Layouts.Element(name);
        return new EntityContainer(Database, transaction, layout.BlockTableRecordId);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
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
      Database db = null;

      try
      {
        db = new Database(true, true);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }

      return new AcadDatabase(db, false);
    }

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <exception cref="System.Exception">Thrown when creating the drawing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create(bool keepOpen)
    {
      Database db = null;

      try
      {
        db = new Database(true, true);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }

      return new AcadDatabase(db, keepOpen);
    }

    /// <summary>
    /// Provides access to the drawing database of the active document.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when no active document is available.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Active()
    {
      if (Application.DocumentManager.MdiActiveDocument == null) Error.NoActiveDocument();
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
    public static AcadDatabase Active(Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      if (Application.DocumentManager.MdiActiveDocument == null) Error.NoActiveDocument();
      if (transaction == null) throw Error.ArgumentNull("transaction");
      if (transaction.IsDisposed) throw Error.InvalidObject("Transaction");
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
      if (database == null) throw Error.ArgumentNull("database");
      if (database.IsDisposed) throw Error.InvalidObject("Database");
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
    public static AcadDatabase Use(Database database, Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      if (database == null) throw Error.ArgumentNull("database");
      if (database.IsDisposed) throw Error.InvalidObject("Database");
      if (transaction == null) throw Error.ArgumentNull("transaction");
      if (transaction.IsDisposed) throw Error.InvalidObject("Transaction");
      return new AcadDatabase(database, transaction, commitTransaction, disposeTransaction);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!File.Exists(fileName)) throw Error.FileNotFound(fileName);

      try
      {
        return OpenInternal(fileName, openMode, null, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e, "Error opening drawing file " + fileName);
      }
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, bool keepOpen)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!File.Exists(fileName)) throw Error.FileNotFound(fileName);
      
      try
      {
        return OpenInternal(fileName, openMode, null, keepOpen);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e, "Error opening drawing file " + fileName);
      }
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, string password)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!File.Exists(fileName)) throw Error.FileNotFound(fileName);
      
      try
      {
        return OpenInternal(fileName, openMode, password, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e, "Error opening drawing file " + fileName);
      }
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="System.Exception">Thrown when opening the darwing database throws an exception.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, string password, bool keepOpen)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!File.Exists(fileName)) throw Error.FileNotFound(fileName);

      try
      {
        return OpenInternal(fileName, openMode, password, keepOpen);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e, "Error opening drawing file " + fileName);
      }
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <returns>The AcadDatabase instance.</returns>
    private static AcadDatabase OpenInternal(string fileName, DwgOpenMode openMode, string password, bool keepOpen)
    {
      var database = new Database(false, true);
      database.ReadDwgFile(fileName, openMode == DwgOpenMode.ReadWrite ? FileOpenMode.OpenForReadAndWriteNoShare : FileOpenMode.OpenForReadAndReadShare, false, password);
      database.CloseInput(true);
      return new AcadDatabase(database, keepOpen);
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

    #endregion
  }
}

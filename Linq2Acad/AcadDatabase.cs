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
    /// Discards all changes to the underlying transaction.
    /// </summary>
    public void DiscardChanges()
    {
      abort = true;
    }

    /// <summary>
    /// Frees all resources.
    /// </summary>
    public void Dispose()
    {
      Dispose(false);
    }

    /// <summary>
    /// Frees all resources forcefully.
    /// </summary>
    /// <param name="force">True, if the drawing database and the transaction should be disposed of, even if keepOpen was used.</param>
    public void Dispose(bool force)
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
    /// Returns the database object with the given ObjectId. The object is readonly.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id) where T : DBObject
    {
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");
      return ElementInternal<T>(id, false);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");
      return ElementInternal<T>(id, forWrite);
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
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      if (ids == null) throw Error.ArgumentNull("ids");
      return ElementsInternal<T>(ids, false);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      if (ids == null) throw Error.ArgumentNull("ids");
      return ElementsInternal<T>(ids, forWrite);
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
      if (ids == null) throw Error.ArgumentNull("ids");
      var openMode = forWrite ? OpenMode.ForWrite : OpenMode.ForRead;

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, openMode);
      }
    }

    /// <summary>
    /// Saves the drawing database to the file with the given name. The newest DWG version is used.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <param name="fileName">The name of the file.</param>
    public void SaveAs(string fileName)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      Database.SaveAs(fileName, DwgVersion.Newest);
    }

    #region Tables

    public BlockContainer Blocks
    {
      get { return new BlockContainer(Database, transaction, Database.BlockTableId); }
    }

    public LayerContainer Layers
    {
      get { return new LayerContainer(Database, transaction, Database.LayerTableId); }
    }

    public DimStyleContainer DimStyles
    {
      get { return new DimStyleContainer(Database, transaction, Database.DimStyleTableId); }
    }

    public LinetypeContainer Linetypes
    {
      get { return new LinetypeContainer(Database, transaction, Database.LinetypeTableId); }
    }

    public RegAppContainer RegApps
    {
      get { return new RegAppContainer(Database, transaction, Database.RegAppTableId); }
    }

    public TextStyleContainer TextStyles
    {
      get { return new TextStyleContainer(Database, transaction, Database.TextStyleTableId); }
    }

    public UcsContainer Ucss
    {
      get { return new UcsContainer(Database, transaction, Database.UcsTableId); }
    }

    public ViewportContainer Viewports
    {
      get { return new ViewportContainer(Database, transaction, Database.ViewportTableId); }
    }

    public ViewContainer Views
    {
      get { return new ViewContainer(Database, transaction, Database.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    public LayoutContainer Layouts
    {
      get { return new LayoutContainer(Database, transaction, Database.LayoutDictionaryId); }
    }

    public GroupContainer Groups
    {
      get { return new GroupContainer(Database, transaction, Database.GroupDictionaryId); }
    }

    public MLeaderStyleContainer MLeaderStyles
    {
      get { return new MLeaderStyleContainer(Database, transaction, Database.MLeaderStyleDictionaryId); }
    }

    public MlineStyleContainer MlineStyles
    {
      get { return new MlineStyleContainer(Database, transaction, Database.MLStyleDictionaryId); }
    }

    public MaterialContainer Materials
    {
      get { return new MaterialContainer(Database, transaction, Database.MaterialDictionaryId); }
    }

    public DBVisualStyleContainer DBVisualStyles
    {
      get { return new DBVisualStyleContainer(Database, transaction, Database.VisualStyleDictionaryId); }
    }

    public PlotSettingsContainer PlotSettings
    {
      get { return new PlotSettingsContainer(Database, transaction, Database.PlotSettingsDictionaryId); }
    }

    public TableStyleContainer TableStyles
    {
      get { return new TableStyleContainer(Database, transaction, Database.TableStyleDictionaryId); }
    }

    public SectionViewStyleContainer SectionViewStyles
    {
      get { return new SectionViewStyleContainer(Database, transaction, Database.SectionViewStyleDictionaryId); }
    }

    public DetailViewStyleContainer DetailViewStyles
    {
      get { return new DetailViewStyleContainer(Database, transaction, Database.DetailViewStyleDictionaryId); }
    }

    #endregion

    #region Model/Paper space

    public EntityContainer CurrentSpace
    {
      get { return new EntityContainer(Database, transaction, Database.CurrentSpaceId); }
    }

    public EntityContainer ModelSpace
    {
      get { return GetSpace(BlockTableRecord.ModelSpace); }
    }
    public EntityContainer PaperSpace
    {
      get { return GetSpace(BlockTableRecord.PaperSpace); }
    }

    private EntityContainer GetSpace(string name)
    {
      if (name == null) { throw Error.ArgumentNull("name"); }
      var spaceID = ((BlockTable)transaction.GetObject(Database.BlockTableId, OpenMode.ForRead))[name];
      return new EntityContainer(Database, transaction, spaceID);
    }

    #endregion

    #region Factory methods

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create()
    {
      return Create(false);
    }

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create(bool keepOpen)
    {
      return new AcadDatabase(new Database(true, true), keepOpen);
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
      if (transaction == null) { throw Error.ArgumentNull("transaction"); }
      if (transaction.IsDisposed) { throw Error.InvalidObject("Transaction"); }
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
      if (database == null) { throw Error.ArgumentNull("database"); }
      if (database.IsDisposed) { throw Error.InvalidObject("Database"); }
      return new AcadDatabase(database, true);
    }

    /// <summary>
    /// Provides access to the given drawing database.
    /// </summary>
    /// <param name="database">The darwing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="commitTransaction">True, if the transaction in use should be committed when this instance is disposed of.</param>
    /// <param name="disposeTransaction">True, if the transaction in use should be disposed of when this instance is disposed of.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>database</i> or <i>transaction</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the database is invalid.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Use(Database database, Transaction transaction, bool commitTransaction, bool disposeTransaction)
    {
      if (database == null) { throw Error.ArgumentNull("database"); }
      if (database.IsDisposed) { throw Error.InvalidObject("Database"); }
      if (transaction == null) { throw Error.ArgumentNull("transaction"); }
      return new AcadDatabase(database, transaction, commitTransaction, disposeTransaction);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode)
    {
      if (fileName == null) { throw Error.ArgumentNull("fileName"); }
      if (!File.Exists(fileName)) { throw Error.FileNotFound(fileName); }
      return OpenInternal(fileName, openMode, null, false);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="keepOpen">True, if the database should be kept open after it has been used. False, if the database should be closed.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, bool keepOpen)
    {
      if (fileName == null) { throw Error.ArgumentNull("fileName"); }
      if (!File.Exists(fileName)) { throw Error.FileNotFound(fileName); }
      return OpenInternal(fileName, openMode, null, keepOpen);
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="openMode">The mode in which the drawing database should be opened.</param>
    /// <param name="password">The password for the darwing database.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, string password)
    {
      if (fileName == null) { throw Error.ArgumentNull("fileName"); }
      if (!File.Exists(fileName)) { throw Error.FileNotFound(fileName); }
      return OpenInternal(fileName, openMode, password, false);
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
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Open(string fileName, DwgOpenMode openMode, string password, bool keepOpen)
    {
      if (fileName == null) { throw Error.ArgumentNull("fileName"); }
      if (!File.Exists(fileName)) { throw Error.FileNotFound(fileName); }
      return OpenInternal(fileName, openMode, password, keepOpen);
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
    /// <returns>The AcadDatabase instance.</returns>
    private static AcadDatabase OpenInternal(string fileName, DwgOpenMode openMode, string password, bool keepOpen)
    {
      var database = new Database(false, true);
      database.ReadDwgFile(fileName, openMode == DwgOpenMode.ReadWrite ? FileOpenMode.OpenForReadAndWriteNoShare : FileOpenMode.OpenForReadAndReadShare, false, password);
      database.CloseInput(true);
      return new AcadDatabase(database, keepOpen);
    }

    #endregion
  }
}

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
  public class AcadDatabase : IDisposable
  {
    private Transaction transaction;
    private bool commit;
    private bool dispose;
    private bool disposeDatabase;
    private bool abort;

    private AcadDatabase(Database database, bool keepOpen)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
      disposeDatabase = !keepOpen;
    }

    private AcadDatabase(Database database, Transaction transaction, bool commit, bool dispose)
    {
      if (database == null) { throw new ArgumentNullException("database"); }
      if (transaction == null) { throw new ArgumentNullException("transaction"); }

      Database = database;
      this.transaction = transaction;
      this.commit = commit;
      this.dispose = dispose;
    }

    public Database Database { get; private set; }

    public void DiscardChanges()
    {
      abort = true;
    }

    public void Dispose()
    {
      if (!transaction.IsDisposed)
      {
        if (abort)
        {
          transaction.Abort();
        }
        else
        {
          if (commit)
          {
            transaction.Commit();
          }

          if (dispose)
          {
            transaction.Dispose();
          }
        }
      }

      if (disposeDatabase)
      {
        Database.Dispose();
      }
    }

    public T Element<T>(ObjectId id) where T : DBObject
    {
      return Element<T>(id, false);
    }

    public T Element<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      return Elements<T>(ids, false);
    }

    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      var openMode = forWrite ? OpenMode.ForWrite : OpenMode.ForRead;

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, openMode);
      }
    }

    public void SaveAs(string fileName)
    {
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

    public EntityContainer GetSpace(string name)
    {
      var spaceID = ((BlockTable)transaction.GetObject(Database.BlockTableId, OpenMode.ForRead))[name];
      return new EntityContainer(Database, transaction, spaceID);
    }

    #endregion

    #region Factory methods

    public static AcadDatabase CreateNew(bool keepOpen = false)
    {
      return CreateNew(false, keepOpen);
    }

    public static AcadDatabase CreateNew(bool createDocument, bool keepOpen = false)
    {
      return new AcadDatabase(new Database(true, !createDocument), keepOpen);
    }

    public static AcadDatabase FromActiveDocument()
    {
      return new AcadDatabase(Application.DocumentManager.MdiActiveDocument.Database, true);
    }

    public static AcadDatabase FromActiveDocument(Transaction tr, bool commit, bool dispose)
    {
      return new AcadDatabase(Application.DocumentManager.MdiActiveDocument.Database, tr, commit, dispose);
    }

    public static AcadDatabase FromOpenDatabase(Database database)
    {
      return new AcadDatabase(database, true);
    }

    public static AcadDatabase FromOpenDatabase(Database database, Transaction tr, bool commit, bool dispose)
    {
      return new AcadDatabase(database, tr, commit, dispose);
    }

    public static AcadDatabase FromFile(string fileName, bool keepOpen = false)
    {
      return FromFile(fileName, false, null, keepOpen);
    }

    public static AcadDatabase FromFile(string fileName, string password, bool keepOpen = false)
    {
      return FromFile(fileName, false, password, keepOpen);
    }

    public static AcadDatabase FromFile(string fileName, bool forWrite, bool keepOpen = false)
    {
      return FromFile(fileName, forWrite, null, keepOpen);
    }

    public static AcadDatabase FromFile(string fileName, bool forWrite, string password, bool keepOpen = false)
    {
      if (!File.Exists(fileName)) { throw new FileNotFoundException(); }

      var database = new Database(false, true);
      database.ReadDwgFile(fileName, forWrite ? FileOpenMode.OpenForReadAndWriteNoShare : FileOpenMode.OpenForReadAndAllShare, false, password);
      database.CloseInput(true);
      return new AcadDatabase(database, keepOpen);
    }

    #endregion
  }
}

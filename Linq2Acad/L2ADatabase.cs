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
  public class L2ADatabase : IDisposable
  {
    private Transaction transaction;
    private bool commit;
    private bool dispose;
    private bool disposeDatabase;
    private bool abort;

    private L2ADatabase(Database database, bool keepOpen)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
      disposeDatabase = !keepOpen;
    }

    private L2ADatabase(Database database, Transaction transaction, bool commit, bool dispose)
    {
      if (database == null) { throw new ArgumentNullException("database"); }
      if (transaction == null) { throw new ArgumentNullException("transaction"); }

      AcadDatabase = database;
      this.transaction = transaction;
      this.commit = commit;
      this.dispose = dispose;
    }

    public Database AcadDatabase { get; private set; }

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
        AcadDatabase.Dispose();
      }
    }

    public T Item<T>(ObjectId id) where T : DBObject
    {
      return Item<T>(id, false);
    }

    public T Item<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public IEnumerable<T> Items<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      return Items<T>(ids, false);
    }

    public IEnumerable<T> Items<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      var openMode = forWrite ? OpenMode.ForWrite : OpenMode.ForRead;

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, openMode);
      }
    }

    public void SaveAs(string fileName)
    {
      AcadDatabase.SaveAs(fileName, DwgVersion.Newest);
    }

    #region Tables

    public BlockContainer Blocks
    {
      get { return new BlockContainer(AcadDatabase, transaction, AcadDatabase.BlockTableId); }
    }

    public LayerContainer Layers
    {
      get { return new LayerContainer(AcadDatabase, transaction, AcadDatabase.LayerTableId); }
    }

    public DimStyleContainer DimStyles
    {
      get { return new DimStyleContainer(AcadDatabase, transaction, AcadDatabase.DimStyleTableId); }
    }

    public LinetypeContainer Linetypes
    {
      get { return new LinetypeContainer(AcadDatabase, transaction, AcadDatabase.LinetypeTableId); }
    }

    public RegAppContainer RegApps
    {
      get { return new RegAppContainer(AcadDatabase, transaction, AcadDatabase.RegAppTableId); }
    }

    public TextStyleContainer TextStyles
    {
      get { return new TextStyleContainer(AcadDatabase, transaction, AcadDatabase.TextStyleTableId); }
    }

    public UcsContainer Ucss
    {
      get { return new UcsContainer(AcadDatabase, transaction, AcadDatabase.UcsTableId); }
    }

    public ViewportContainer Viewports
    {
      get { return new ViewportContainer(AcadDatabase, transaction, AcadDatabase.ViewportTableId); }
    }

    public ViewContainer Views
    {
      get { return new ViewContainer(AcadDatabase, transaction, AcadDatabase.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    public LayoutContainer Layouts
    {
      get { return new LayoutContainer(AcadDatabase, transaction, AcadDatabase.LayoutDictionaryId); }
    }

    public GroupContainer Groups
    {
      get { return new GroupContainer(AcadDatabase, transaction, AcadDatabase.GroupDictionaryId); }
    }

    public MLeaderStyleContainer MLeaderStyles
    {
      get { return new MLeaderStyleContainer(AcadDatabase, transaction, AcadDatabase.MLeaderStyleDictionaryId); }
    }

    public MlineStyleContainer MlineStyles
    {
      get { return new MlineStyleContainer(AcadDatabase, transaction, AcadDatabase.MLStyleDictionaryId); }
    }

    public MaterialContainer Materials
    {
      get { return new MaterialContainer(AcadDatabase, transaction, AcadDatabase.MaterialDictionaryId); }
    }

    public DBVisualStyleContainer DBVisualStyles
    {
      get { return new DBVisualStyleContainer(AcadDatabase, transaction, AcadDatabase.VisualStyleDictionaryId); }
    }

    public PlotSettingsContainer PlotSettings
    {
      get { return new PlotSettingsContainer(AcadDatabase, transaction, AcadDatabase.PlotSettingsDictionaryId); }
    }

    public TableStyleContainer TableStyles
    {
      get { return new TableStyleContainer(AcadDatabase, transaction, AcadDatabase.TableStyleDictionaryId); }
    }

    public SectionViewStyleContainer SectionViewStyles
    {
      get { return new SectionViewStyleContainer(AcadDatabase, transaction, AcadDatabase.SectionViewStyleDictionaryId); }
    }

    public DetailViewStyleContainer DetailViewStyles
    {
      get { return new DetailViewStyleContainer(AcadDatabase, transaction, AcadDatabase.DetailViewStyleDictionaryId); }
    }

    #endregion

    #region Model/Paper space

    public EntityContainer CurrentSpace
    {
      get { return new EntityContainer(AcadDatabase, transaction, AcadDatabase.CurrentSpaceId); }
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
      var spaceID = ((BlockTable)transaction.GetObject(AcadDatabase.BlockTableId, OpenMode.ForRead))[name];
      return new EntityContainer(AcadDatabase, transaction, spaceID);
    }

    #endregion

    #region Factory methods

    public static L2ADatabase CreateNew(bool keepOpen = false)
    {
      return CreateNew(false, keepOpen);
    }

    public static L2ADatabase CreateNew(bool createDocument, bool keepOpen = false)
    {
      return new L2ADatabase(new Database(true, !createDocument), keepOpen);
    }

    public static L2ADatabase Active()
    {
      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database, true);
    }

    public static L2ADatabase Active(Transaction tr, bool commit, bool dispose)
    {
      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database, tr, commit, dispose);
    }

    public static L2ADatabase Use(Database database)
    {
      return new L2ADatabase(database, true);
    }

    public static L2ADatabase Use(Database database, Transaction tr, bool commit, bool dispose)
    {
      return new L2ADatabase(database, tr, commit, dispose);
    }

    public static L2ADatabase Open(string fileName, bool keepOpen = false)
    {
      return Open(fileName, false, null, keepOpen);
    }

    public static L2ADatabase Open(string fileName, string password, bool keepOpen = false)
    {
      return Open(fileName, false, password, keepOpen);
    }

    public static L2ADatabase Open(string fileName, bool forWrite, bool keepOpen = false)
    {
      return Open(fileName, forWrite, null, keepOpen);
    }

    public static L2ADatabase Open(string fileName, bool forWrite, string password, bool keepOpen = false)
    {
      if (!File.Exists(fileName)) { throw new FileNotFoundException(); }

      var database = new Database(false, true);
      database.ReadDwgFile(fileName, forWrite ? FileOpenMode.OpenForReadAndWriteNoShare : FileOpenMode.OpenForReadAndAllShare, false, password);
      database.CloseInput(true);
      return new L2ADatabase(database, keepOpen);
    }

    #endregion
  }
}

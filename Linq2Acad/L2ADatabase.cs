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
    private bool commit;
    private bool dispose;
    private bool abort;
    private Transaction transaction;

    private L2ADatabase(Database database)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
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
    }

    public T GetObject<T>(ObjectId id) where T : DBObject
    {
      return (T)transaction.GetObject(id, OpenMode.ForRead);
    }

    public IEnumerable<T> GetObjects<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
    }

    #region Tables

    public Blocks Blocks
    {
      get { return new Blocks(AcadDatabase, transaction, AcadDatabase.BlockTableId); }
    }

    public Layers Layers
    {
      get { return new Layers(AcadDatabase, transaction, AcadDatabase.LayerTableId); }
    }

    public DimStyles DimStyles
    {
      get { return new DimStyles(AcadDatabase, transaction, AcadDatabase.DimStyleTableId); }
    }

    public Linetypes Linetypes
    {
      get { return new Linetypes(AcadDatabase, transaction, AcadDatabase.LinetypeTableId); }
    }

    public RegApps RegApps
    {
      get { return new RegApps(AcadDatabase, transaction, AcadDatabase.RegAppTableId); }
    }

    public TextStyles TextStyles
    {
      get { return new TextStyles(AcadDatabase, transaction, AcadDatabase.TextStyleTableId); }
    }

    public Ucss Ucss
    {
      get { return new Ucss(AcadDatabase, transaction, AcadDatabase.UcsTableId); }
    }

    public Viewports Viewports
    {
      get { return new Viewports(AcadDatabase, transaction, AcadDatabase.ViewportTableId); }
    }

    public Views Views
    {
      get { return new Views(AcadDatabase, transaction, AcadDatabase.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    public Layouts Layouts
    {
      get { return new Layouts(AcadDatabase, transaction, AcadDatabase.LayoutDictionaryId); }
    }

    public Groups Groups
    {
      get { return new Groups(AcadDatabase, transaction, AcadDatabase.GroupDictionaryId); }
    }

    public MLeaderStyles MLeaderStyles
    {
      get { return new MLeaderStyles(AcadDatabase, transaction, AcadDatabase.MLeaderStyleDictionaryId); }
    }

    public MlineStyles MlineStyles
    {
      get { return new MlineStyles(AcadDatabase, transaction, AcadDatabase.MLStyleDictionaryId); }
    }

    public Materials Materials
    {
      get { return new Materials(AcadDatabase, transaction, AcadDatabase.MaterialDictionaryId); }
    }

    public DBVisualStyles DBVisualStyles
    {
      get { return new DBVisualStyles(AcadDatabase, transaction, AcadDatabase.VisualStyleDictionaryId); }
    }

    public PlotSettingss PlotSettings
    {
      get { return new PlotSettingss(AcadDatabase, transaction, AcadDatabase.PlotSettingsDictionaryId); }
    }

    public TableStyles TableStyles
    {
      get { return new TableStyles(AcadDatabase, transaction, AcadDatabase.TableStyleDictionaryId); }
    }

    public SectionViewStyles SectionViewStyles
    {
      get { return new SectionViewStyles(AcadDatabase, transaction, AcadDatabase.SectionViewStyleDictionaryId); }
    }

    public DetailViewStyles DetailViewStyles
    {
      get { return new DetailViewStyles(AcadDatabase, transaction, AcadDatabase.DetailViewStyleDictionaryId); }
    }

    #endregion

    #region Model/Paper space

    public Block CurrentSpace
    {
      get { return new Block(AcadDatabase, transaction, AcadDatabase.CurrentSpaceId); }
    }

    public Block ModelSpace
    {
      get { return GetSpace(BlockTableRecord.ModelSpace); }
    }
    public Block PaperSpace
    {
      get { return GetSpace(BlockTableRecord.PaperSpace); }
    }

    private Block GetSpace(string name)
    {
      var spaceID = ((BlockTable)transaction.GetObject(AcadDatabase.BlockTableId, OpenMode.ForRead))[name];
      return new Block(AcadDatabase, transaction, spaceID);
    }

    #endregion

    #region Factory methods

    public static L2ADatabase ActiveDatabase()
    {
      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database);
    }

    public static L2ADatabase ActiveDatabase(Transaction tr, bool commit, bool dispose)
    {
      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database, tr, commit, dispose);
    }

    public static L2ADatabase Use(Database database)
    {
      return new L2ADatabase(database);
    }

    public static L2ADatabase Use(Database database, Transaction tr, bool commit, bool dispose)
    {
      return new L2ADatabase(database, tr, commit, dispose);
    }

    public static L2ADatabase Open(string fileName)
    {
      return Open(fileName, false, null);
    }

    public static L2ADatabase Open(string fileName, string password)
    {
      return Open(fileName, false, password);
    }

    public static L2ADatabase Open(string fileName, bool forWrite)
    {
      return Open(fileName, forWrite, null);
    }

    public static L2ADatabase Open(string fileName, bool forWrite, string password)
    {
      if (!File.Exists(fileName)) { throw new FileNotFoundException(); }

      var database = new Database(false, true);
      database.ReadDwgFile(fileName, forWrite ? FileOpenMode.OpenForReadAndWriteNoShare : FileOpenMode.OpenForReadAndAllShare, false, password);
      database.CloseInput(true);
      return new L2ADatabase(database);
    }

    #endregion
  }
}

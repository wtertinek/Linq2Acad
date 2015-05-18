using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class L2ADatabase : IDisposable
  {
    private bool commit;
    private bool dispose;
    private Database database;
    private Transaction transaction;

    private L2ADatabase(Database database)
      : this(database, database.TransactionManager.StartTransaction(), true, true)
    {
    }

    private L2ADatabase(Database database, Transaction transaction, bool commit, bool dispose)
    {
      if (database == null)
      {
        throw new InvalidOperationException("No database available");
      }

      AcadDatabase = database;
      this.commit = commit;
      this.dispose = dispose;
      this.database = database;
      this.transaction = transaction;
    }

    public Database AcadDatabase { get; private set; }

    public void DiscardChanges()
    {
      commit = false;
    }

    public void Dispose()
    {
      if (!transaction.IsDisposed)
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
      get { return new Blocks(database, transaction, database.BlockTableId); }
    }

    public Layers Layers
    {
      get { return new Layers(database, transaction, database.LayerTableId); }
    }

    public DimStyles DimStyles
    {
      get { return new DimStyles(database, transaction, database.DimStyleTableId); }
    }

    public Linetypes Linetypes
    {
      get { return new Linetypes(database, transaction, database.LinetypeTableId); }
    }

    public RegApps RegApps
    {
      get { return new RegApps(database, transaction, database.RegAppTableId); }
    }

    public TextStyles TextStyles
    {
      get { return new TextStyles(database, transaction, database.TextStyleTableId); }
    }

    public Ucss Ucss
    {
      get { return new Ucss(database, transaction, database.UcsTableId); }
    }

    public Viewports Viewports
    {
      get { return new Viewports(database, transaction, database.ViewportTableId); }
    }

    public Views Views
    {
      get { return new Views(database, transaction, database.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    public Layouts Layouts
    {
      get { return new Layouts(database, transaction, database.LayoutDictionaryId); }
    }

    public Groups Groups
    {
      get { return new Groups(database, transaction, database.GroupDictionaryId); }
    }

    public MLeaderStyles MLeaderStyles
    {
      get { return new MLeaderStyles(database, transaction, database.MLeaderStyleDictionaryId); }
    }

    public MlineStyles MlineStyles
    {
      get { return new MlineStyles(database, transaction, database.MLStyleDictionaryId); }
    }

    public Materials Materials
    {
      get { return new Materials(database, transaction, database.MaterialDictionaryId); }
    }

    public DBVisualStyles DBVisualStyles
    {
      get { return new DBVisualStyles(database, transaction, database.VisualStyleDictionaryId); }
    }

    public PlotSettingss PlotSettings
    {
      get { return new PlotSettingss(database, transaction, database.PlotSettingsDictionaryId); }
    }

    public TableStyles TableStyles
    {
      get { return new TableStyles(database, transaction, database.TableStyleDictionaryId); }
    }

    public SectionViewStyles SectionViewStyles
    {
      get { return new SectionViewStyles(database, transaction, database.SectionViewStyleDictionaryId); }
    }

    public DetailViewStyles DetailViewStyles
    {
      get { return new DetailViewStyles(database, transaction, database.DetailViewStyleDictionaryId); }
    }

    #endregion

    #region Model/Paper space

    public Block CurrentSpace
    {
      get { return new Block(database, transaction, database.CurrentSpaceId); }
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
      return new Block(database, transaction, spaceID);
    }

    #endregion

    #region Factory methods

    public static L2ADatabase ActiveDatabase()
    {
      if (Application.DocumentManager.MdiActiveDocument == null)
      {
        throw new InvalidOperationException("No document available");
      }

      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database);
    }

    public static L2ADatabase ActiveDatabase(Transaction tr, bool commit, bool dispose)
    {
      if (Application.DocumentManager.MdiActiveDocument == null)
      {
        throw new InvalidOperationException("No document available");
      }

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

    #endregion
  }
}

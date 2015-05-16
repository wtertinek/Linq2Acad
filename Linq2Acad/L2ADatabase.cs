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

    private L2ADatabase()
    {
      CheckTransaction();
    }

    private L2ADatabase(Database database)
    {
      CheckTransaction();

      if (database != null)
      {
        Database = database;
        commit = true;
        dispose = true;
        Transaction = new Lazy<Transaction>(Database.TransactionManager.StartTransaction);
        IsValid = true;
      }
    }

    private L2ADatabase(Database database, Transaction tr, bool commit, bool dispose)
    {
      CheckTransaction();

      if (database != null)
      {
        Database = database;
        this.commit = commit;
        this.dispose = dispose;
        Transaction = new Lazy<Transaction>(() => tr);
        IsValid = true;
      }
    }

    public bool IsValid { get; private set; }

    private void CheckTransaction()
    {
      if (Transaction != null)
      {
        throw new InvalidOperationException("Can't create nested instance of ActiveDatabase");
      }
    }

    internal static Lazy<Transaction> Transaction { get; private set; }

    public Database Database { get; private set; }

    public void DiscardChanges()
    {
      commit = false;
    }

    public void Dispose()
    {
      if (Transaction.IsValueCreated &&
          !Transaction.Value.IsDisposed)
      {
        if (commit)
        {
          Transaction.Value.Commit();
        }

        if (dispose)
        {
          Transaction.Value.Dispose();
        }
      }

      Transaction = null;
    }

    public IEnumerable<BlockTableRecord> Blocks
    {
      get { return AcdbEnumerable<BlockTableRecord>.Create(Transaction, Database.BlockTableId); }
    }

    public IEnumerable<LayerTableRecord> Layers
    {
      get { return AcdbEnumerable<LayerTableRecord>.Create(Transaction, Database.LayerTableId); }
    }

    public IEnumerable<DimStyleTableRecord> DimStyles
    {
      get { return AcdbEnumerable<DimStyleTableRecord>.Create(Transaction, Database.DimStyleTableId); }
    }

    public IEnumerable<LinetypeTableRecord> Linetypes
    {
      get { return AcdbEnumerable<LinetypeTableRecord>.Create(Transaction, Database.LinetypeTableId); }
    }

    public IEnumerable<RegAppTableRecord> RegApps
    {
      get { return AcdbEnumerable<RegAppTableRecord>.Create(Transaction, Database.RegAppTableId); }
    }

    public IEnumerable<TextStyleTableRecord> TextStyles
    {
      get { return AcdbEnumerable<TextStyleTableRecord>.Create(Transaction, Database.TextStyleTableId); }
    }

    public IEnumerable<UcsTableRecord> Ucss
    {
      get { return AcdbEnumerable<UcsTableRecord>.Create(Transaction, Database.UcsTableId); }
    }

    public IEnumerable<ViewportTableRecord> Viewports
    {
      get { return AcdbEnumerable<ViewportTableRecord>.Create(Transaction, Database.ViewportTableId); }
    }

    public ViewportTableRecord CurrentViewport
    {
      get
      {
        Helpers.CheckTransaction();
        return (ViewportTableRecord)Transaction.Value.GetObject(Database.CurrentViewportTableRecordId, OpenMode.ForRead);
      }
    }

    public IEnumerable<ViewTableRecord> Views
    {
      get { return AcdbEnumerable<ViewTableRecord>.Create(Transaction, Database.ViewTableId); }
    }

    public IEnumerable<Layout> Layouts
    {
      get { return AcdbEnumerable<Layout>.Create(Transaction, Database.LayoutDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<Group> Groups
    {
      get { return AcdbEnumerable<Group>.Create(Transaction, Database.GroupDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<MLeaderStyle> MLeaderStyles
    {
      get { return AcdbEnumerable<MLeaderStyle>.Create(Transaction, Database.MLeaderStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<Material> Materials
    {
      get { return AcdbEnumerable<Material>.Create(Transaction, Database.MaterialDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<DBVisualStyle> DBVisualStyles
    {
      get { return AcdbEnumerable<DBVisualStyle>.Create(Transaction, Database.VisualStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<PlotSettings> PlotSettings
    {
      get { return AcdbEnumerable<PlotSettings>.Create(Transaction, Database.PlotSettingsDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<TableStyle> TableStyles
    {
      get { return AcdbEnumerable<TableStyle>.Create(Transaction, Database.TableStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<SectionViewStyle> SectionViewStyles
    {
      get { return AcdbEnumerable<SectionViewStyle>.Create(Transaction, Database.SectionViewStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<DetailViewStyle> DetailViewStyles
    {
      get { return AcdbEnumerable<DetailViewStyle>.Create(Transaction, Database.DetailViewStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public BlockTableRecord CurrentSpace
    {
      get
      {
        Helpers.CheckTransaction();
        return (BlockTableRecord)Transaction.Value.GetObject(Database.CurrentSpaceId, OpenMode.ForRead);
      }
    }

    public BlockTableRecord ModelSpace
    {
      get { return GetSpace(BlockTableRecord.ModelSpace); }
    }
    public BlockTableRecord PaperSpace
    {
      get { return GetSpace(BlockTableRecord.PaperSpace); }
    }

    private BlockTableRecord GetSpace(string name)
    {
      Helpers.CheckTransaction();
      var modelSpaceId = ((BlockTable)Transaction.Value.GetObject(Database.BlockTableId, OpenMode.ForRead))[name];
      return (BlockTableRecord)Transaction.Value.GetObject(modelSpaceId, OpenMode.ForRead);
    }

    public static L2ADatabase ActiveDatabase()
    {
      if (Application.DocumentManager.MdiActiveDocument == null)
      {
        return new L2ADatabase();
      }

      return new L2ADatabase(Application.DocumentManager.MdiActiveDocument.Database);
    }

    public static L2ADatabase ActiveDatabase(Transaction tr, bool commit, bool dispose)
    {
      if (Application.DocumentManager.MdiActiveDocument == null)
      {
        return new L2ADatabase();
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
  }
}

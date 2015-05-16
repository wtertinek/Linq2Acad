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

    private L2ADatabase(Database database)
    {
      CheckTransaction();
      CheckDatabase(database);

      AcadDatabase = database;
      commit = true;
      dispose = true;
      Transaction = new Lazy<Transaction>(AcadDatabase.TransactionManager.StartTransaction);
    }

    private L2ADatabase(Database database, Transaction tr, bool commit, bool dispose)
    {
      CheckTransaction();
      CheckDatabase(database);

      AcadDatabase = database;
      this.commit = commit;
      this.dispose = dispose;
      Transaction = new Lazy<Transaction>(() => tr);
    }

    private void CheckTransaction()
    {
      if (Transaction != null)
      {
        throw new InvalidOperationException("Can't create nested instance of ActiveDatabase");
      }
    }

    private void CheckDatabase(Database database)
    {
      if (database == null)
      {
        throw new InvalidOperationException("No database available");
      }
    }

    internal static Lazy<Transaction> Transaction { get; private set; }

    public Database AcadDatabase { get; private set; }

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

    #region Properties

    public IEnumerable<BlockTableRecord> Blocks
    {
      get { return AcdbEnumerable<BlockTableRecord>.Create(Transaction, AcadDatabase.BlockTableId); }
    }

    public IEnumerable<LayerTableRecord> Layers
    {
      get { return AcdbEnumerable<LayerTableRecord>.Create(Transaction, AcadDatabase.LayerTableId); }
    }

    public IEnumerable<DimStyleTableRecord> DimStyles
    {
      get { return AcdbEnumerable<DimStyleTableRecord>.Create(Transaction, AcadDatabase.DimStyleTableId); }
    }

    public IEnumerable<LinetypeTableRecord> Linetypes
    {
      get { return AcdbEnumerable<LinetypeTableRecord>.Create(Transaction, AcadDatabase.LinetypeTableId); }
    }

    public IEnumerable<RegAppTableRecord> RegApps
    {
      get { return AcdbEnumerable<RegAppTableRecord>.Create(Transaction, AcadDatabase.RegAppTableId); }
    }

    public IEnumerable<TextStyleTableRecord> TextStyles
    {
      get { return AcdbEnumerable<TextStyleTableRecord>.Create(Transaction, AcadDatabase.TextStyleTableId); }
    }

    public IEnumerable<UcsTableRecord> Ucss
    {
      get { return AcdbEnumerable<UcsTableRecord>.Create(Transaction, AcadDatabase.UcsTableId); }
    }

    public IEnumerable<ViewportTableRecord> Viewports
    {
      get { return AcdbEnumerable<ViewportTableRecord>.Create(Transaction, AcadDatabase.ViewportTableId); }
    }

    public ViewportTableRecord CurrentViewport
    {
      get
      {
        Helpers.CheckTransaction();
        return (ViewportTableRecord)Transaction.Value.GetObject(AcadDatabase.CurrentViewportTableRecordId, OpenMode.ForRead);
      }
    }

    public IEnumerable<ViewTableRecord> Views
    {
      get { return AcdbEnumerable<ViewTableRecord>.Create(Transaction, AcadDatabase.ViewTableId); }
    }

    public IEnumerable<Layout> Layouts
    {
      get { return AcdbEnumerable<Layout>.Create(Transaction, AcadDatabase.LayoutDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<Group> Groups
    {
      get { return AcdbEnumerable<Group>.Create(Transaction, AcadDatabase.GroupDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<MLeaderStyle> MLeaderStyles
    {
      get { return AcdbEnumerable<MLeaderStyle>.Create(Transaction, AcadDatabase.MLeaderStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<Material> Materials
    {
      get { return AcdbEnumerable<Material>.Create(Transaction, AcadDatabase.MaterialDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<DBVisualStyle> DBVisualStyles
    {
      get { return AcdbEnumerable<DBVisualStyle>.Create(Transaction, AcadDatabase.VisualStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<PlotSettings> PlotSettings
    {
      get { return AcdbEnumerable<PlotSettings>.Create(Transaction, AcadDatabase.PlotSettingsDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<TableStyle> TableStyles
    {
      get { return AcdbEnumerable<TableStyle>.Create(Transaction, AcadDatabase.TableStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<SectionViewStyle> SectionViewStyles
    {
      get { return AcdbEnumerable<SectionViewStyle>.Create(Transaction, AcadDatabase.SectionViewStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<DetailViewStyle> DetailViewStyles
    {
      get { return AcdbEnumerable<DetailViewStyle>.Create(Transaction, AcadDatabase.DetailViewStyleDictionaryId, o => (ObjectId)((DictionaryEntry)o).Value); }
    }

    public IEnumerable<Entity> CurrentSpace
    {
      get { return AcdbEnumerable<Entity>.Create(Transaction, AcadDatabase.CurrentSpaceId); }
    }

    public IEnumerable<Entity> ModelSpace
    {
      get { return GetSpace(BlockTableRecord.ModelSpace); }
    }
    public IEnumerable<Entity> PaperSpace
    {
      get { return GetSpace(BlockTableRecord.PaperSpace); }
    }

    private IEnumerable<Entity> GetSpace(string name)
    {
      Helpers.CheckTransaction();
      var modelSpaceId = ((BlockTable)Transaction.Value.GetObject(AcadDatabase.BlockTableId, OpenMode.ForRead))[name];
      return AcdbEnumerable<Entity>.Create(Transaction, modelSpaceId);
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

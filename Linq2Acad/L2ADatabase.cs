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
      Database = database;
    }

    private L2ADatabase(Database database, Transaction tr, bool commit, bool dispose)
    {
      CheckTransaction();
      CheckDatabase(database);

      AcadDatabase = database;
      this.commit = commit;
      this.dispose = dispose;
      Transaction = new Lazy<Transaction>(() => tr);
      Database = database;
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

    internal static Database Database { get; private set; }

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
      Database = null;
    }

    #region TableRecords

    public IEnumerable<BlockTableRecord> Blocks
    {
      get { return new TableEnumerable<BlockTableRecord>(Transaction, AcadDatabase.BlockTableId); }
    }

    public IEnumerable<LayerTableRecord> Layers
    {
      get { return new TableEnumerable<LayerTableRecord>(Transaction, AcadDatabase.LayerTableId); }
    }

    public IEnumerable<DimStyleTableRecord> DimStyles
    {
      get { return new TableEnumerable<DimStyleTableRecord>(Transaction, AcadDatabase.DimStyleTableId); }
    }

    public IEnumerable<LinetypeTableRecord> Linetypes
    {
      get { return new TableEnumerable<LinetypeTableRecord>(Transaction, AcadDatabase.LinetypeTableId); }
    }

    public IEnumerable<RegAppTableRecord> RegApps
    {
      get { return new TableEnumerable<RegAppTableRecord>(Transaction, AcadDatabase.RegAppTableId); }
    }

    public IEnumerable<TextStyleTableRecord> TextStyles
    {
      get { return new TableEnumerable<TextStyleTableRecord>(Transaction, AcadDatabase.TextStyleTableId); }
    }

    public IEnumerable<UcsTableRecord> Ucss
    {
      get { return new TableEnumerable<UcsTableRecord>(Transaction, AcadDatabase.UcsTableId); }
    }

    public IEnumerable<ViewportTableRecord> Viewports
    {
      get { return new TableEnumerable<ViewportTableRecord>(Transaction, AcadDatabase.ViewportTableId); }
    }

    public IEnumerable<ViewTableRecord> Views
    {
      get { return new TableEnumerable<ViewTableRecord>(Transaction, AcadDatabase.ViewTableId); }
    }

    #endregion

    #region DBDictionaries

    public IEnumerable<Layout> Layouts
    {
      get { return new DBDictionaryEnumerable<Layout>(Transaction, AcadDatabase.LayoutDictionaryId); }
    }

    public IEnumerable<Group> Groups
    {
      get { return new DBDictionaryEnumerable<Group>(Transaction, AcadDatabase.GroupDictionaryId); }
    }

    public IEnumerable<MLeaderStyle> MLeaderStyles
    {
      get { return new DBDictionaryEnumerable<MLeaderStyle>(Transaction, AcadDatabase.MLeaderStyleDictionaryId); }
    }

    public IEnumerable<MlineStyle> MlineStyles
    {
      get { return new DBDictionaryEnumerable<MlineStyle>(Transaction, AcadDatabase.MLStyleDictionaryId); }
    }

    public IEnumerable<Material> Materials
    {
      get { return new DBDictionaryEnumerable<Material>(Transaction, AcadDatabase.MaterialDictionaryId); }
    }

    public IEnumerable<DBVisualStyle> DBVisualStyles
    {
      get { return new DBDictionaryEnumerable<DBVisualStyle>(Transaction, AcadDatabase.VisualStyleDictionaryId); }
    }

    public IEnumerable<PlotSettings> PlotSettings
    {
      get { return new DBDictionaryEnumerable<PlotSettings>(Transaction, AcadDatabase.PlotSettingsDictionaryId); }
    }

    public IEnumerable<TableStyle> TableStyles
    {
      get { return new DBDictionaryEnumerable<TableStyle>(Transaction, AcadDatabase.TableStyleDictionaryId); }
    }

    public IEnumerable<SectionViewStyle> SectionViewStyles
    {
      get { return new DBDictionaryEnumerable<SectionViewStyle>(Transaction, AcadDatabase.SectionViewStyleDictionaryId); }
    }

    public IEnumerable<DetailViewStyle> DetailViewStyles
    {
      get { return new DBDictionaryEnumerable<DetailViewStyle>(Transaction, AcadDatabase.DetailViewStyleDictionaryId); }
    }

    public IEnumerable<Entity> CurrentSpace
    {
      get { return new TableEnumerable<Entity>(Transaction, AcadDatabase.CurrentSpaceId); }
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
      return new TableEnumerable<Entity>(Transaction, modelSpaceId);
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

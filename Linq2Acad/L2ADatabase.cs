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

    #region Tables

    public L2ABlockTable Blocks
    {
      get { return new L2ABlockTable(Transaction, AcadDatabase.BlockTableId); }
    }

    public L2ALayerTable Layers
    {
      get { return new L2ALayerTable(Transaction, AcadDatabase.LayerTableId); }
    }

    public L2ADimStyleTable DimStyles
    {
      get { return new L2ADimStyleTable(Transaction, AcadDatabase.DimStyleTableId); }
    }

    public L2ALinetypeTable Linetypes
    {
      get { return new L2ALinetypeTable(Transaction, AcadDatabase.LinetypeTableId); }
    }

    public L2ARegAppTable RegApps
    {
      get { return new L2ARegAppTable(Transaction, AcadDatabase.RegAppTableId); }
    }

    public L2ATextStyleTable TextStyles
    {
      get { return new L2ATextStyleTable(Transaction, AcadDatabase.TextStyleTableId); }
    }

    public L2AUcsTable Ucss
    {
      get { return new L2AUcsTable(Transaction, AcadDatabase.UcsTableId); }
    }

    public L2AViewportTable Viewports
    {
      get { return new L2AViewportTable(Transaction, AcadDatabase.ViewportTableId); }
    }

    public L2AViewTable Views
    {
      get { return new L2AViewTable(Transaction, AcadDatabase.ViewTableId); }
    }

    #endregion

    #region Dictionaries

    public L2ALayoutDictionary Layouts
    {
      get { return new L2ALayoutDictionary(Transaction, AcadDatabase.LayoutDictionaryId); }
    }

    public L2AGroupDictionary Groups
    {
      get { return new L2AGroupDictionary(Transaction, AcadDatabase.GroupDictionaryId); }
    }

    public L2AMLeaderStyleDictionary MLeaderStyles
    {
      get { return new L2AMLeaderStyleDictionary(Transaction, AcadDatabase.MLeaderStyleDictionaryId); }
    }

    public L2AMlineStyleDictionary MlineStyles
    {
      get { return new L2AMlineStyleDictionary(Transaction, AcadDatabase.MLStyleDictionaryId); }
    }

    public L2AMaterialDictionary Materials
    {
      get { return new L2AMaterialDictionary(Transaction, AcadDatabase.MaterialDictionaryId); }
    }

    public L2ADBVisualStyleDictionary DBVisualStyles
    {
      get { return new L2ADBVisualStyleDictionary(Transaction, AcadDatabase.VisualStyleDictionaryId); }
    }

    public L2APlotSettingsDictionary PlotSettings
    {
      get { return new L2APlotSettingsDictionary(Transaction, AcadDatabase.PlotSettingsDictionaryId); }
    }

    public L2ATableStyleDictionary TableStyles
    {
      get { return new L2ATableStyleDictionary(Transaction, AcadDatabase.TableStyleDictionaryId); }
    }

    public L2ASectionViewStyleDictionary SectionViewStyles
    {
      get { return new L2ASectionViewStyleDictionary(Transaction, AcadDatabase.SectionViewStyleDictionaryId); }
    }

    public L2ADetailViewStyleDictionary DetailViewStyles
    {
      get { return new L2ADetailViewStyleDictionary(Transaction, AcadDatabase.DetailViewStyleDictionaryId); }
    }

    #endregion

    #region Model/Paper space

    public EntityEnumerable CurrentSpace
    {
      get { return new EntityEnumerable(Transaction, AcadDatabase.CurrentSpaceId); }
    }

    public EntityEnumerable ModelSpace
    {
      get { return GetSpace(BlockTableRecord.ModelSpace); }
    }
    public EntityEnumerable PaperSpace
    {
      get { return GetSpace(BlockTableRecord.PaperSpace); }
    }

    private EntityEnumerable GetSpace(string name)
    {
      Helpers.CheckTransaction();
      var spaceID = ((BlockTable)Transaction.Value.GetObject(AcadDatabase.BlockTableId, OpenMode.ForRead))[name];
      return new EntityEnumerable(Transaction, spaceID);
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

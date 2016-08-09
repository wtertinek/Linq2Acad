using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
  public class AssertThat
  {
    private Database db;
    private StringBuilder builder;

    internal AssertThat(Database db)
    {
      this.db = db;
      this.builder = new StringBuilder();
    }

    #region Tables

    public TableAssertions<BlockTable> BlockTable
    {
      get { return new TableAssertions<BlockTable>(db, db.BlockTableId, builder); }
    }

    public TableAssertions<DimStyleTable> DimStyleTable
    {
      get { return new TableAssertions<DimStyleTable>(db, db.DimStyleTableId, builder); }
    }

    public TableAssertions<LayerTable> LayerTable
    {
      get { return new TableAssertions<LayerTable>(db, db.LayerTableId, builder); }
    }

    public TableAssertions<LinetypeTable> LinetypeTable
    {
      get { return new TableAssertions<LinetypeTable>(db, db.LinetypeTableId, builder); }
    }

    public TableAssertions<RegAppTable> RegAppTable
    {
      get { return new TableAssertions<RegAppTable>(db, db.RegAppTableId, builder); }
    }

    public TableAssertions<TextStyleTable> TextStyleTable
    {
      get { return new TableAssertions<TextStyleTable>(db, db.TextStyleTableId, builder); }
    }

    public TableAssertions<UcsTable> UcsTable
    {
      get { return new TableAssertions<UcsTable>(db, db.UcsTableId, builder); }
    }

    public TableAssertions<ViewTable> ViewTable
    {
      get { return new TableAssertions<ViewTable>(db, db.ViewTableId, builder); }
    }

    public TableAssertions<ViewportTable> ViewportTable
    {
      get { return new TableAssertions<ViewportTable>(db, db.ViewportTableId, builder); }
    }

    #endregion

    #region Dictionaries

    public DictionaryAssertions DBVisualStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.VisualStyleDictionaryId, "VisualStyleDictionary", builder); }
    }

    public DictionaryAssertions DetailViewStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.DetailViewStyleDictionaryId, "DetailViewStyleDictionary", builder); }
    }

    public DictionaryAssertions GroupDictionary
    {
      get { return new DictionaryAssertions(db, db.GroupDictionaryId, "GroupDictionary", builder); }
    }

    public DictionaryAssertions LayoutDictionary
    {
      get { return new DictionaryAssertions(db, db.LayoutDictionaryId, "LayoutDictionary", builder); }
    }

    public DictionaryAssertions MaterialDictionary
    {
      get { return new DictionaryAssertions(db, db.MaterialDictionaryId, "MaterialDictionary", builder); }
    }

    public DictionaryAssertions MLeaderStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.MLeaderStyleDictionaryId, "MLeaderStyleDictionary", builder); }
    }

    public DictionaryAssertions MlineStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.MLStyleDictionaryId, "MLStyleDictionary", builder); }
    }

    public DictionaryAssertions PlotSettingsDictionary
    {
      get { return new DictionaryAssertions(db, db.PlotSettingsDictionaryId, "PlotSettingsDictionary", builder); }
    }

    public DictionaryAssertions SectionViewStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.SectionViewStyleDictionaryId, "SectionViewStyleDictionary", builder); }
    }

    public DictionaryAssertions TableStyleDictionary
    {
      get { return new DictionaryAssertions(db, db.TableStyleDictionaryId, "TableStyleDictionary", builder); }
    }

    #endregion
  }
}

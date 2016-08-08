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

    public TableAssertions<BlockTable> BlockTable()
    {
      return new TableAssertions<BlockTable>(db, db.BlockTableId, builder);
    }

    public TableAssertions<DimStyleTable> DimStyleTable()
    {
      return new TableAssertions<DimStyleTable>(db, db.DimStyleTableId, builder);
    }

    public TableAssertions<LayerTable> LayerTable()
    {
      return new TableAssertions<LayerTable>(db, db.LayerTableId, builder);
    }

    public TableAssertions<LinetypeTable> LinetypeTable()
    {
      return new TableAssertions<LinetypeTable>(db, db.LinetypeTableId, builder);
    }

    public TableAssertions<RegAppTable> RegAppTable()
    {
      return new TableAssertions<RegAppTable>(db, db.RegAppTableId, builder);
    }

    public TableAssertions<TextStyleTable> TextStyleTable()
    {
      return new TableAssertions<TextStyleTable>(db, db.TextStyleTableId, builder);
    }

    public TableAssertions<UcsTable> UcsTable()
    {
      return new TableAssertions<UcsTable>(db, db.UcsTableId, builder);
    }

    public TableAssertions<ViewTable> ViewTable()
    {
      return new TableAssertions<ViewTable>(db, db.ViewTableId, builder);
    }

    public TableAssertions<ViewportTable> ViewportTable()
    {
      return new TableAssertions<ViewportTable>(db, db.ViewportTableId, builder);
    }

    #endregion

    #region Dictionaries

    public DictionaryAssertions DBVisualStyleDictionary()
    {
      return new DictionaryAssertions(db, db.VisualStyleDictionaryId, "VisualStyleDictionary", builder);
    }

    public DictionaryAssertions DetailViewStyleDictionary()
    {
      return new DictionaryAssertions(db, db.DetailViewStyleDictionaryId, "DetailViewStyleDictionary", builder);
    }

    public DictionaryAssertions GroupDictionary()
    {
      return new DictionaryAssertions(db, db.GroupDictionaryId, "GroupDictionary", builder);
    }

    public DictionaryAssertions LayoutDictionary()
    {
      return new DictionaryAssertions(db, db.LayoutDictionaryId, "LayoutDictionary", builder);
    }

    public DictionaryAssertions MaterialDictionary()
    {
      return new DictionaryAssertions(db, db.MaterialDictionaryId, "MaterialDictionary", builder);
    }

    public DictionaryAssertions MLeaderStyleDictionary()
    {
      return new DictionaryAssertions(db, db.MLeaderStyleDictionaryId, "MLeaderStyleDictionary", builder);
    }

    public DictionaryAssertions MlineStyleDictionary()
    {
      return new DictionaryAssertions(db, db.MLStyleDictionaryId, "MLStyleDictionary", builder);
    }

    public DictionaryAssertions PlotSettingsDictionary()
    {
      return new DictionaryAssertions(db, db.PlotSettingsDictionaryId, "PlotSettingsDictionary", builder);
    }

    public DictionaryAssertions SectionViewStyleDictionary()
    {
      return new DictionaryAssertions(db, db.SectionViewStyleDictionaryId, "SectionViewStyleDictionary", builder);
    }

    public DictionaryAssertions TableStyleDictionary()
    {
      return new DictionaryAssertions(db, db.TableStyleDictionaryId, "TableStyleDictionary", builder);
    }

    #endregion
  }
}

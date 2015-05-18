using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class DBVisualStyles : DBDictionaryEnumerable<DBVisualStyle>
  {
    internal DBVisualStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DBVisualStyle CreateNew()
    {
      return new DBVisualStyle();
    }
  }

  public class DetailViewStyles : DBDictionaryEnumerable<DetailViewStyle>
  {
    internal DetailViewStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DetailViewStyle CreateNew()
    {
      return new DetailViewStyle();
    }
  }

  public class Groups : DBDictionaryEnumerable<Group>
  {
    internal Groups(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Group CreateNew()
    {
      return new Group();
    }

    public Group Create(string name, IEnumerable<Entity> entities)
    {
      var group = Create(name);
      group.Append(new ObjectIdCollection(entities.Select(e => e.ObjectId)
                                                  .ToArray()));
      return group;
    }
  }

  public class Layouts : DBDictionaryEnumerable<Layout>
  {
    internal Layouts(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Layout CreateNew()
    {
      return new Layout();
    }
  }

  public class Materials : DBDictionaryEnumerable<Material>
  {
    internal Materials(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override Material CreateNew()
    {
      return new Material();
    }
  }

  public class MLeaderStyles : DBDictionaryEnumerable<MLeaderStyle>
  {
    internal MLeaderStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override MLeaderStyle CreateNew()
    {
      return new MLeaderStyle();
    }
  }

  public class MlineStyles : DBDictionaryEnumerable<MlineStyle>
  {
    internal MlineStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override MlineStyle CreateNew()
    {
      return new MlineStyle();
    }
  }

  public class PlotSettingss : DBDictionaryEnumerable<PlotSettings>
  {
    internal PlotSettingss(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override PlotSettings CreateNew()
    {
      // TODO: Select correct type
      return new PlotSettings(true);
    }
  }

  public class SectionViewStyles : DBDictionaryEnumerable<SectionViewStyle>
  {
    internal SectionViewStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override SectionViewStyle CreateNew()
    {
      return new SectionViewStyle();
    }
  }

  public class TableStyles : DBDictionaryEnumerable<TableStyle>
  {
    internal TableStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override TableStyle CreateNew()
    {
      return new TableStyle();
    }
  }
}

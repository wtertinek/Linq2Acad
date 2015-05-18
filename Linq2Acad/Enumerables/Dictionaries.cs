using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class L2ADBVisualStyleDictionary : DBDictionaryEnumerableBase<DBVisualStyle>
  {
    internal L2ADBVisualStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override DBVisualStyle CreateNew()
    {
      return new DBVisualStyle();
    }
  }

  public class L2ADetailViewStyleDictionary : DBDictionaryEnumerableBase<DetailViewStyle>
  {
    internal L2ADetailViewStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override DetailViewStyle CreateNew()
    {
      return new DetailViewStyle();
    }
  }

  public class L2AGroupDictionary : DBDictionaryEnumerableBase<Group>
  {
    internal L2AGroupDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override Group CreateNew()
    {
      return new Group();
    }

    public Group Create(string name, IEnumerable<ObjectId> ids)
    {
      var group = Create(name);
      group.Append(new ObjectIdCollection(ids.ToArray()));
      return group;
    }
  }

  public class L2ALayoutDictionary : DBDictionaryEnumerableBase<Layout>
  {
    internal L2ALayoutDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override Layout CreateNew()
    {
      return new Layout();
    }
  }

  public class L2AMaterialDictionary : DBDictionaryEnumerableBase<Material>
  {
    internal L2AMaterialDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override Material CreateNew()
    {
      return new Material();
    }
  }

  public class L2AMLeaderStyleDictionary : DBDictionaryEnumerableBase<MLeaderStyle>
  {
    internal L2AMLeaderStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override MLeaderStyle CreateNew()
    {
      return new MLeaderStyle();
    }
  }

  public class L2AMlineStyleDictionary : DBDictionaryEnumerableBase<MlineStyle>
  {
    internal L2AMlineStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override MlineStyle CreateNew()
    {
      return new MlineStyle();
    }
  }

  public class L2APlotSettingsDictionary : DBDictionaryEnumerableBase<PlotSettings>
  {
    internal L2APlotSettingsDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override PlotSettings CreateNew()
    {
      // TODO: Select correct type
      return new PlotSettings(true);
    }
  }

  public class L2ASectionViewStyleDictionary : DBDictionaryEnumerableBase<SectionViewStyle>
  {
    internal L2ASectionViewStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override SectionViewStyle CreateNew()
    {
      return new SectionViewStyle();
    }
  }

  public class L2ATableStyleDictionary : DBDictionaryEnumerableBase<TableStyle>
  {
    internal L2ATableStyleDictionary(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override TableStyle CreateNew()
    {
      return new TableStyle();
    }
  }
}

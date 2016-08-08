using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public abstract class ContainerAssertions
  {
    private Database db;
    private ObjectId containerId;
    private StringBuilder builder;

    protected ContainerAssertions(Database db, ObjectId containerId, string containerName, StringBuilder builder)
    {
      this.db = db;
      this.containerId = containerId;
      this.builder = builder;
      builder.Append(containerName + " ");
    }

    protected abstract bool ContainsInteral(Transaction tr, ObjectId containerId, string elementName);

    protected abstract bool ContainsInteral(Transaction tr, ObjectId containerId, ObjectId objectId);
    
    public void Contains(string blockName)
    {
      using (var tr = db.TransactionManager.StartTransaction())
      {
        if (!ContainsInteral(tr, containerId, blockName))
        {
          builder.Append(" does not contain an element with name '" + blockName + "'");
          throw new AcadAssertFailedException(builder.ToString());
        }
      }
    }

    public void Contains(ObjectId objectId)
    {
      using (var tr = db.TransactionManager.StartTransaction())
      {
        if (!ContainsInteral(tr, containerId, objectId))
        {
          builder.Append(" does not contain an element with ObjectId '" + objectId + "'");
          throw new AcadAssertFailedException(builder.ToString());
        }
      }
    }
  }

  public class TableAssertions<T> : ContainerAssertions where T : SymbolTable
  {
    public TableAssertions(Database db, ObjectId tableId, StringBuilder builder)
      : base(db, tableId, typeof(T).Name, builder)
    {
    }

    protected override bool ContainsInteral(Transaction tr, ObjectId containerId, string elementName)
    {
      var table = tr.GetObject(containerId, OpenMode.ForRead) as T;
      return table.Has(elementName);
    }

    protected override bool ContainsInteral(Transaction tr, ObjectId containerId, ObjectId objectId)
    {
      var table = tr.GetObject(containerId, OpenMode.ForRead) as T;
      return table.Has(objectId);
    }
  }

  public class DictionaryAssertions : ContainerAssertions
  {
    public DictionaryAssertions(Database db, ObjectId dictionaryId, string dictionaryName, StringBuilder builder)
      : base(db, dictionaryId, dictionaryName, builder)
    {
    }

    protected override bool ContainsInteral(Transaction tr, ObjectId containerId, string elementName)
    {
      var dict = tr.GetObject(containerId, OpenMode.ForRead) as DBDictionary;
      return dict.Contains(elementName);
    }

    protected override bool ContainsInteral(Transaction tr, ObjectId containerId, ObjectId objectId)
    {
      var dict = tr.GetObject(containerId, OpenMode.ForRead) as DBDictionary;
      return dict.Contains(objectId);
    }
  }
}

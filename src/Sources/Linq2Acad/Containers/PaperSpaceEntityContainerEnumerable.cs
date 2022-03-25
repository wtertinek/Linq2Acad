using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  internal class PaperSpaceLayoutContainer : IEnumerable<PaperSpaceEntityContainer>
  {
    private readonly Database database;
    private readonly Transaction transaction;

    public PaperSpaceLayoutContainer(Database database, Transaction transaction)
    {
      this.database = database;
      this.transaction = transaction;
    }

    public IEnumerator<PaperSpaceEntityContainer> GetEnumerator()
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      var layouts = new LayoutContainer(database, transaction, database.LayoutDictionaryId);

      foreach (var layout in layouts.Where(l => !l.ModelType)
                                    .OrderBy(l => l.TabOrder))
      {
        yield return new PaperSpaceEntityContainer(database, transaction, layout.BlockTableRecordId, layout.LayoutName);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
      => GetEnumerator();
  }

  /// <summary>
  /// Extension methods for IEnumerable&lt;PaperSpaceLayout&gt;.
  /// </summary>
  public static class PaperSpaceLayoutEnumerableExtensions
  {
    /// <summary>
    /// Provides access to the entities of the layout with the given name.
    /// </summary>
    /// <param name="name">The name of the layout.</param>
    /// <param name="source">The list of paper space layouts.</param>
    /// <returns>An EntityContainer to access the layout's entities.</returns>
    public static PaperSpaceEntityContainer Element(this IEnumerable<PaperSpaceEntityContainer> source, string name)
    {
      Require.ParameterNotNull(name, nameof(name));
      return source.First(l => l.Name == name);
    }
  }
}

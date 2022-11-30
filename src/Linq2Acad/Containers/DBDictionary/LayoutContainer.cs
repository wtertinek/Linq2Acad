using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the elements of the Layout dictionary. In addition to the standard LINQ operations this class provides methods to create, add and import Layouts.
  /// </summary>
  public sealed class LayoutContainer : DBDictionaryEnumerableBase<Layout>
  {
    internal LayoutContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, l => l.LayoutName, () => nameof(Layout.LayoutName))
    {
    }

    /// <summary>
    /// Creates a new Layout with the specified name.
    /// </summary>
    /// <param name="name">The unique name of the Layout element.</param>
    public Layout Create(string name)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Layout>(Contains(name), name);

      return AddInternal((Layout)transaction.GetObject(LayoutManager.Current.CreateLayout(name), OpenMode.ForWrite), name);
    }
  }
}

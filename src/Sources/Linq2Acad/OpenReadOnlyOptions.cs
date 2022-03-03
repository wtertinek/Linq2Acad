using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Provides options for opening a database readonly.
  /// </summary>
  public class OpenReadOnlyOptions
  {
    /// <summary>
    /// The password to use when opening the database.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Set this option to true, if the database should be kept open after it has been used.
    /// This is an advanced feature, use with caution.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }
}

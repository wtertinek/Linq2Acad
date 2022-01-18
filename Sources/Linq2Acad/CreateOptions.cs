using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Provides options for creationg a database.
  /// </summary>
  public class CreateOptions
  {
    /// <summary>
    /// If specified, path to save the database to.
    /// </summary>
    public string SaveFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Defaults to that used by the running AutoCAD version. 
    /// </summary>
    public SaveAsDwgVersion SaveDwgVersion { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// This is an advanced feature, use with caution.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }
}

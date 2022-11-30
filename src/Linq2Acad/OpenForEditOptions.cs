using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Provides options for opening a database for edit.
  /// </summary>
  public class OpenForEditOptions
  {
    /// <summary>
    /// If specified, the database will be saved to the given file path instead of the file path the DWG has been opened from.
    /// </summary>
    public string SaveAsFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Keeps the current version by default.
    /// </summary>
    public SaveAsDwgVersion DwgVersion { get; set; }

    /// <summary>
    /// The password to use when opening the database.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Set thi option to true, if the database should be kept open after it has been used.
    /// This is an advanced feature, use with caution.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }
}

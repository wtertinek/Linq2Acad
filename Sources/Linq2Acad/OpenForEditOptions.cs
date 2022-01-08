using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class OpenForEditOptions
  {
    public string SaveAsFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Keeps the current version by default.
    /// </summary>
    public SaveAsDwgVersion DwgVersion { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }

    public string Password { get; set; }
  }
}

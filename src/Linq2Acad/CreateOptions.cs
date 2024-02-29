using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Provides options for creating a database.
  /// </summary>
  public class CreateOptions
  {
    /// <summary>
    /// If specified, path to save the database to.
    /// </summary>
    public string SaveFileName { get; set; }

    /// <summary>
    /// DWG version to use when saving the database to file. 
    /// Defaults to the DWG version used by the running AutoCAD version. 
    /// </summary>
    public SaveAsDwgVersion SaveDwgVersion { get; set; }
  }
}

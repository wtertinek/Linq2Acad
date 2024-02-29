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
  }
}

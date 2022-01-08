using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class OpenReadOnlyOptions
  {
    public string Password { get; set; }

    /// <summary>
    /// True, if the database should be kept open after it has been used.
    /// False, if the database should be closed.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    public bool KeepDatabaseOpen { get; set; }
  }
}

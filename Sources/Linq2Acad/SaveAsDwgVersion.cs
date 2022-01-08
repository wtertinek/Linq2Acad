using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// DWG version enum.
  /// </summary>
  public enum SaveAsDwgVersion
  {
    /// <summary>
    /// This is the default value
    /// </summary>
    DontChange,
    DWG2004,
    DWG2007,
    DWG2010,
    DWG2013,
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022
    DWG2018,
#endif
    NewestAvailable
  }
}

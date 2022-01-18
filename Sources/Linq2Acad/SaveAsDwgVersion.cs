using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// The version to use whan a new copy of a DWG is saved.
  /// </summary>
  public enum SaveAsDwgVersion
  {
    /// <summary>
    /// Does not change the DWG version. This is the default value.
    /// </summary>
    DontChange,
    /// <summary>
    /// DWG 2004 format
    /// </summary>
    DWG2004,
    /// <summary>
    /// DWG 2007 format
    /// </summary>
    DWG2007,
    /// <summary>
    /// DWG 2010 format
    /// </summary>
    DWG2010,
    /// <summary>
    /// DWG 213 format
    /// </summary>
    DWG2013,
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022
    /// <summary>
    /// DWG 2018 format
    /// </summary>
    DWG2018,
#endif
    NewestAvailable
  }
}

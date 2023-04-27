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
    /// DWG 2004 format (AC1015)
    /// </summary>
    DWG2004,
    /// <summary>
    /// DWG 2007 format (AC1021)
    /// </summary>
    DWG2007,
    /// <summary>
    /// DWG 2010 format (AC1024)
    /// </summary>
    DWG2010,
    /// <summary>
    /// DWG 2013 format (AC1027)
    /// </summary>
    DWG2013,
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022 || AutoCAD_2023 || AutoCAD_2024
    /// <summary>
    /// DWG 2018 format (AC1032)
    /// </summary>
    DWG2018,
#endif
    /// <summary>
    /// The newest available DWG version.
    /// </summary>
    NewestAvailable
  }
}

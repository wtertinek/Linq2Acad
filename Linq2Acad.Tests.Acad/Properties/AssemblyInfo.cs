using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Runtime;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Linq2Acad.Tests.Acad")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Linq2Acad")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c12ba054-301b-4578-96c5-657408bd25c9")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: CommandClass(typeof(Linq2Acad.Tests.BlockTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.LayerTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.DimStyleTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.LinetypeTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.RegAppTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.TextStyleTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.UcsTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.ViewportTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.ViewTableRecordTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.LayoutTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.GroupTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.MLeaderStyleTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.MlineStyleTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.MaterialTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.DBVisualStyleTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.PlotSettingsTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.TableStyleTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.SectionViewStyleTests))]
[assembly: CommandClass(typeof(Linq2Acad.Tests.DetailViewStyleTests))]
# Linq2Acad
A library that aims to simplify AutoCAD .NET plugin code. Available for `AutoCAD 2015` and later.

### Overview
- [Getting started](#get-started)
- [NuGet package](#nuget-package)
- [API documentation](#api-documentation)
- [How it works](#how-it-works)
- [Contributing](#contributing)
- [License](#license)

## Getting started
`Linq2Acad` is a library that aims to simplify AutoCAD .NET plugin code. It should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

#### As a simple example, let's print all layer names using Linq2Acad:

```cs
using (var db = AcadDatabase.Active())
{
  var layerNames = db.Layers.Select(l => l.Name);
  MessageBox.Show(string.Join(", ", layerNames));
}
```

#### Linq2Acad makes it easy to delete all BlockReferences from the model space:

```cs
using (var db = AcadDatabase.Active())
{
  foreach (var br in db.ModelSpace
                       .OfType<BlockReference>()
                       .UpgradeOpen())
  {
    br.Erase();
  }
}
```

#### This code shows how to import a block from a DWG file into the active document:

```cs
using (var sourceDb = AcadDatabase.OpenReadOnly(@"C:\Blocks\Shapes.dwg"))
using (var targetDb = AcadDatabase.Active())
{
  var block = sourceDb.Blocks.Element("TRIANGLE");
  targetDb.Blocks.Import(block);
}
```

#### You can easily store data on entities:

```c#
var entityId = GetEntity("Pick an Entity");
var key = GetString("Enter key");
var str = GetString("Enter string to save");

// We first write the data (it is stored in the Entity's extension data)
using (var db = AcadDatabase.Active())
{
  db.CurrentSpace
    .Element(entityId)
    .SaveData(key, str);

  WriteMessage($"Key-value-pair {key}:{str} saved on Entity");
}

// Then we read it back
using (var db = AcadDatabase.Active())
{
  var str = db.CurrentSpace
              .Element(entityId)
              .GetData<string>(key);

  WriteMessage($"String {str} read from Entity");
}
```

#### You can use Linq2Acad to changes the summary info of the active document:

```cs
using (var db = AcadDatabase.Active())
{
  db.SummaryInfo.Author = "John Doe";
  db.SummaryInfo.CustomProperties["CustomData1"] = "42";
}
```

#### There's also a simple way to, for example, reload all loaded XRefs:

```cs
using (var db = AcadDatabase.Active())
{
  foreach (var xRef in db.XRefs
                         .Where(xr => xr.IsLoaded))
  {
    xRef.Reload();
  }
}
```
      
More code samples (in C# and VB.NET) can be found [here](docs/CodeSamples.md).


## Installation

### How to upgrade from source code to Nuget

1. Remove your existing `Linq2Acad` **PROJECT** completely from your existing Visual Studio solution. (You should see a lot of red squiggly lines.)
2. Install the Nuget Packages. 

(If you add Nuget while already having a `Linq2Acad` project there, and THEN you subsequently remove the latter project - you might have a lot of problems.)

### Breaking changes not reflected in version updates
Beginning with the Nuget release of version 1.0.0, Linq2Acad is released in accordance with the rules of [semantic versioning](https://semver.org). Previous to that, there have been breaking changes to the API that were not reflected in the API's version number. The changes in question can be found [here](docs/BreakingChanges.md).

## API documentation
The best entry point into the API documentation is the class [AcadDatabase](docs/api/T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class). An overview of all classes can be found [here](docs/api/Index.md#Linq2Acad-Namespace).

## How it works?
[This blog series](https://wtertinek.com/2016/07/06/linq-and-the-autocad-net-api-final-part) discusses:

- the original problem this library seeks to solve,
- the design / implementation decisions involved in deriving the API. 

## Contributing
See [contributing documentation](/.github/CONTRIBUTING.md).

## License
Linq2Acad is licended unter the [MIT License (MIT)](LICENSE).

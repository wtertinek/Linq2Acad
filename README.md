# Linq2Acad
A library that aims to simplify AutoCAD .NET addin code. Available for **AutoCAD 2015** and later.

### Overview
- [Getting started](#get-started)
- [NuGet package](#nuget-package)
- [API documentation](#api-documentation)
- [Code samples](#code-samples)
- [How it works](#how-it-works)
- [Contributing](#contributing)
- [License](#license)

## Getting started
**Linq2Acad** is a library that aims to simplify AutoCAD .NET addin code. It should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

#### As a simple example, let's print all layer names using Linq2Acad:

```cs
using (var db = AcadDatabase.Active())
{
  var layerNames = new List<string>();
  
  foreach (var layer in db.Layers)
  {
    layerNames.Add(layer.Name);
  }
  
  MessageBox.Show($"Layers: {string.Join(", ", layerNames)}.");
}
```

#### Or, let's delete all BlockReferences from the model space:

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
using (var sourceDb = AcadDatabase.OpenReadOnly(@"C:\Blocks\Block.dwg"))
using (var targetDb = AcadDatabase.Active())
{
  var block = sourceDb.Blocks
                      .Element("My_Block_V8");
  targetDb.Blocks
          .Import(block);
}
```
  
More code samples (in C# and VB.NET) can be found [here](docs/CodeSamples.md).

## NuGet package
**UPDATE**: Version 1.0.0 of Linq2Acad is now available on NuGet! There is a dedicated NuGet package for each AutoCAD version, the packages are named **Linq2Acad-20xx** ([see nuget.org](https://www.nuget.org/packages?q=linq2acad)).

### How to upgrade from source code to Nuget

1. Remove your existing `Linq2Acad` **PROJECT** completely from your existing Visual Studio solution. (You should see a lot of red squiggly lines.)
2. Install the Nuget Packages. 

(If you add Nuget while already having a `Linq2Acad` project there, and THEN you subsequently remove the latter project - you might have a lot of problems.)

### Breaking changes not reflected in version updates
Beginning with the Nuget release of version 1.0.0, Linq2Acad is released in accordance with the rules of [semantic versioning](https://semver.org). Previous to that, there have been breaking changes to the API that were not reflected in the API's version number. The changes in question can be found [here](docs/BreakingChanges.md).

## API documentation
The best entry point into the API documentation is the class [AcadDatabase](docs/api/T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class). An overview of all classes can be found [here](docs/api/Index.md#Linq2Acad-Namespace).

## Code samples
Code samples in C# and VB.NET can be found [here](docs/CodeSamples.md).

## How it works?
[This blog series](https://wtertinek.com/2016/07/06/linq-and-the-autocad-net-api-final-part) discusses:

- the original problem this library seeks to solve,
- the design / implementation decisions involved in deriving the API. 

## Contributing

Please:

- open an [issue](https://github.com/wtertinek/Linq2Acad/issues) if you've found a bug,
- a [discussion](https://github.com/wtertinek/Linq2Acad/discussions/new) for a feature request, and then, 
- a pull request if you want to help extending the library.

For those with graphic design abilities: it would be nice to have a Linq2Acad logo.

## License
Linq2Acad is licended unter the [MIT License (MIT)](LICENSE).

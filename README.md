# Linq2Acad
A library that aims to simplify AutoCAD .NET plugin code. Available for `AutoCAD 2015` and later.

### Overview
- [News](#news)
- [Getting started](#get-started)
- [Installation](#installation)
- [API documentation](#api-documentation)
- [How it works](#how-it-works)
- [Contributing](#contributing)
- [License](#license)

# News
`Linq2Acad-2024` is now available as a release candidate on [NuGet](https://www.nuget.org/packages/Linq2Acad-2024/). If you test it, please provide some feedback to the [pull request](https://github.com/wtertinek/Linq2Acad/pull/34).

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

### NuGet packages

Linq2Acad is available on NuGet. There is a dedicated Linq2Acad package for each AutoCAD version. Simply add the package for your AutoCAD version to your C#/VB project in Visual Studio. Available packages:

<a href="https://www.nuget.org/packages/Linq2Acad-2024">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2024?label=Linq2Acad-2024-rc2&style=plastic" alt="Linq2Acad-2024-rc2" />
</a>
<br/>
<a href="https://www.nuget.org/packages/Linq2Acad-2023">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2023?label=Linq2Acad-2023&style=plastic" alt="Linq2Acad-2023" />
</a>
<a href="https://www.nuget.org/packages/Linq2Acad-2022">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2022?label=Linq2Acad-2022&style=plastic" alt="Linq2Acad-2022" />
</a>
<br/>
<a href="https://www.nuget.org/packages/Linq2Acad-2021">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2021?label=Linq2Acad-2021&style=plastic" alt="Linq2Acad-2021" />
</a>
<a href="https://www.nuget.org/packages/Linq2Acad-2020">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2020?label=Linq2Acad-2020&style=plastic" alt="Linq2Acad-2020" />
</a>
<br/>
<a href="https://www.nuget.org/packages/Linq2Acad-2019">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2019?label=Linq2Acad-2019&style=plastic" alt="Linq2Acad-2019" />
</a>
<a href="https://www.nuget.org/packages/Linq2Acad-2018">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2018?label=Linq2Acad-2018&style=plastic" alt="Linq2Acad-2018" />
</a>
<br/>
<a href="https://www.nuget.org/packages/Linq2Acad-2017">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2017?label=Linq2Acad-2017&style=plastic" alt="Linq2Acad-2017" />
</a>
<a href="https://www.nuget.org/packages/Linq2Acad-2016">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2016?label=Linq2Acad-2016&style=plastic" alt="Linq2Acad-2016" />
</a>
<br/>
<a href="https://www.nuget.org/packages/Linq2Acad-2015">
  <img src="https://img.shields.io/nuget/v/Linq2Acad-2015?label=Linq2Acad-2015&style=plastic" alt="Linq2Acad-2015" />
</a>

### How to upgrade from source code to NuGet

1. Remove your existing Linq2Acad project completely from your existing Visual Studio solution (You should see a lot of red squiggly lines)
2. Install the NuGet Package

(If you add NuGet while already having a Linq2Acad project there, and THEN you subsequently remove the latter project - you might have a lot of problems)

## API documentation
The best entry point into the API documentation is the class [AcadDatabase](docs/api/T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class). An overview of all classes can be found [here](docs/api/Index.md#Linq2Acad-Namespace).

## How it works?
[This blog series](https://wtertinek.com/2016/07/06/linq-and-the-autocad-net-api-final-part) discusses:

- the original problem this library seeks to solve,
- the design / implementation decisions involved in deriving the API. 

## Contributing
We would love for you to contribute to Linq2Acad and help to make the life of AutoCAD plugin developers easier. We welcome ideas, suggestions and discussions to push the development forward. Implementation of bugfixes or new features are also always welcome. For details see the [contributing guidelines](.github/CONTRIBUTING.md).

## License
Linq2Acad is licended unter the [MIT License (MIT)](LICENSE).

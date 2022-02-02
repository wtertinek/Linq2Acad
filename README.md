### Linq2Acad
**Linq2Acad** is a library that aims to simplify AutoCAD .NET addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information (no IntelliSense) and having some performance implications. Using Linq2Acad the type information is preserved and there are no performance implications.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

**Supported AutoCAD versions**: 2015 and later

## !!! Linq2Acad is on NuGet !!!
A public Beta of Linq2Acad is now available on **NuGet**! There is a dedicated package per AutoCAD version, the packages are named [**Linq2Acad-20xx**](https://www.nuget.org/packages?q=linq2acad). Since we are in beta phase, the packages are declared as prerelease, so check *Include prereleases* in the NuGet browser in Visual Studio in order to find them.

### Sample Code
Code from [SampleCode.cs](https://github.com/wtertinek/Linq2Acad/blob/master/Sources/Linq2Acad.SampleCode.CS/SampleCode.cs). See [SampleCode.vb](https://github.com/wtertinek/Linq2Acad/blob/master/Sources/Linq2Acad.SampleCode.VB/SampleCode.vb) for VB samples.

Removing all entities from the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Clear();
}
```

Removing all BlockReferences from the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .OfType<BlockReference>()
    .UpgradeOpen()
    .ForEach(br => br.Erase());
}
```

Adding a line to the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Add(new Line(new Point3d(0, 0, 0),
                  new Point3d(100, 100, 0)));
}
```

Creating a new layer:

```c#
var name = GetString("Enter layer name");
var colorName = GetString("Enter color name");

using (var db = AcadDatabase.Active())
{
  var layer = db.Layers.Create(name);
  layer.Color = Color.FromColor(System.Drawing.Color.FromName(colorName));
}
```

Printing all layer names:

```c#
using (var db = AcadDatabase.Active())
{
  db.Layers
    .ForEach(l => WriteMessage(l.Name));
}
```

Turning off all layers, except the one the user enters:

```c#
var layerName = GetString("Enter layer name");

using (var db = AcadDatabase.Active())
{
  var layer = db.Layers
                .Element(layerName);

  db.Layers
    .Except(layer)
    .UpgradeOpen()
    .ForEach(l => l.IsOff = true);
}
```

Creating a layer and adding all red lines in the model space to it:

```c#
var layerName = GetString("Enter layer name");

using (var db = AcadDatabase.Active())
{
  var lines = db.ModelSpace
                .OfType<Line>()
                .Where(l => l.Color.ColorValue.Name == "ffff0000");
  db.Layers
    .Create(layerName, lines);

  WriteMessage("All red lines moved to new layer " + layerName);
}
```

Moving entities from one layer to another:

```c#
var sourceLayerName = GetString("Enter source layer name");
var targetLayerName = GetString("Enter target layer name");

using (var db = AcadDatabase.Active())
{
  var entities = db.CurrentSpace
                   .Where(e => e.Layer == sourceLayerName);
  db.Layers
    .Element(targetLayerName)
    .AddRange(entities);
}
```

Importing a block from a drawing file:

```c#
var filePath = GetString("Enter file path");
var blockName = GetString("Enter block name");

using (var sourceDb = AcadDatabase.Open(filePath, DwgOpenMode.ReadOnly))
{
  var block = sourceDb.Blocks
                      .Element(blockName);

  using (var activeDb = AcadDatabase.Active())
  {
    activeDb.Blocks
            .Import(block);
  }
}
```

Opening a drawing from file and counting the BlockReferences in the model space:

```c#
var filePath = GetString("Enter file path");

using (var db = AcadDatabase.Open(filePath, DwgOpenMode.ReadOnly))
{
  var count = db.ModelSpace
                .OfType<BlockReference>()
                .Count();

  WriteMessage("Model space block references in file " + filePath + ": " + count);
}
```

Picking an entity and saving a string on it:

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter key");
var str = GetString("Enter string to save");

using (var db = AcadDatabase.Active())
{
  db.CurrentSpace
    .Element(entityId)
    .SaveData(key, str);
}
```

Picking an entity and reading a string from it:

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter key");

using (var db = AcadDatabase.Active())
{
  var str = db.CurrentSpace
              .Element(entityId)
              .GetData<string>(key);

  WriteMessage("String " + str + " read from entity");
}
```

Picking an entity and reading a string from it (with XData as the data source):

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter RegApp name");

using (var db = AcadDatabase.Active())
{
  var str = db.CurrentSpace
              .Element(entityId)
              .GetData<string>(key, true);

  WriteMessage("String " + str + " read from entity's XData");
}
```

Counting the number of entities in all paper space layouts:

```c#
using (var db = AcadDatabase.Active())
{
  var count = db.PaperSpace()
                .SelectMany(ps => ps)
                .Count();

  WriteMessage(count + " entities in all paper space layouts");
}
```

Changing the summary info:

```c#
using (var db = AcadDatabase.Active())
{
  db.SummaryInfo.Author = "John Doe";
  db.SummaryInfo.CustomProperties["CustomData1"] = "42";
}
```

Reloading all loaded XRefs:

```c#
using (var db = AcadDatabase.Active())
{
  db.XRefs
    .Where(xr => xr.Status.IsLoaded)
    .Reload();
}
```

Binding all XRefs:

```c#
using (var db = AcadDatabase.Active())
{
  db.XRefs
    .Bind();
}
```

### Understanding how this library works

Please refer to the following which details in a step-by-step fashion: (i) how this library works and (ii) the decision making process and logic behind the library's derivation: https://wtertinek.com/2016/07/06/linq-and-the-autocad-net-api-final-part/

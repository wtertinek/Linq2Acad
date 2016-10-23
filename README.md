###Linq2Acad
**Linq2Acad** is a library that aims to simplify AutoCAD .NET addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information (no IntelliSense) and having some performance implications. Using Linq2Acad the type information is preserved and there are no performance implications.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

### TODO
The library is still under development. Working with Blocks and Entities works best at the moment. Access to other tables and dictionaries is implemented, but basically untested. Tests and features will be added little by little.

### Configuration
In order to be able to compile the solution, you have to execute the batch file *Configure.bat* which is located in the subfolder *Configuration*.

###Sample Code
Code from [SampleCode.cs](https://github.com/wtertinek/Linq2Acad/blob/master/Linq2Acad.SampleCode.CS/SampleCode.cs). See [SampleCode.vb](https://github.com/wtertinek/Linq2Acad/blob/master/Linq2Acad.SampleCode.VB/SampleCode.vb) for VB samples.

Removing all entities from the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Clear();
}

WriteMessage("Model space cleared");
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

WriteMessage("All block references removed from model space");
```

Adding a line to the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Add(new Line(new Point3d(0, 0, 0),
                  new Point3d(100, 100, 0)));
}

WriteMessage("Line added to model space");
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

WriteMessage("Layer " + name + " created");
```

Printing all layer names:

```c#
using (var db = AcadDatabase.Active())
{
  db.Layers
    .UpgradeOpen()
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
    .Except(new[] { layer })
    .UpgradeOpen()
    .ForEach(l => l.IsOff = true);
}

WriteMessage("All layers (except " + layerName + ") turned off");
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

WriteMessage("All entities on layer " + sourceLayerName + " moved to layer " + targetLayerName);
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

WriteMessage("Block " + blockName + " imported");
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

WriteMessage("Key-value-pair " + key + ":" + str + " saved on entity");
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

Counting the number of entities in all paper space layouts:

```c#
using (var db = AcadDatabase.Active())
{
  var allEntities = db.PaperSpace()
                      .SelectMany(ps => ps);

  WriteMessage(allEntities.Count() + " entities in all paper space layouts");
}
```

Changing the summary info:

```c#
using (var db = AcadDatabase.Active())
{
  db.SummaryInfo.Author = "John Doe";
  db.SummaryInfo.CustomProperties["CustomData1"] = "42";
}

WriteMessage("Summary info updated");
```

Reloading all loaded XRefs:

```c#
using (var db = AcadDatabase.Active())
{
  db.XRefs
    .Where(xr => xr.Status.IsLoaded)
    .Reload();
}

WriteMessage("XRefs reloaded");
```

Binding all XRefs:

```c#
using (var db = AcadDatabase.Active())
{
  db.XRefs
    .Bind();
}

WriteMessage("XRefs bound");
```


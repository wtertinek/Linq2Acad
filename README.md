###Linq2Acad
**Linq2Acad** is a library that aims to simplify AutoCAD .NET addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information (no IntelliSense) and having some performance implications. Using Linq2Acad the type information is preserved and there are no performance implications.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

### Referencing AutoCAD assemblies
In order to be able to compile the solution, you have to add your local AutoCAD installation folder to the *Reference Paths* of each project.

Let's assume your AutoCAD installation folder is *C:\Program Files\Autodesk\AutoCAD 2016*. To add this folder as a reference path for the project Linq2Acad, go to the project properties, select the tab *Reference Paths* and add *C:\Program Files\Autodesk\AutoCAD 2016*. Repeat this procedure for Linq2Acad.Examples as well.

### TODO
The code is not yet ready for production. The implementation of the main functionality is done, but not yet thoroughly tested. Further functionality will be added little by little.

###Examples
From [Examples.cs](https://github.com/wtertinek/Linq2Acad/blob/master/Linq2Acad.Examples/Examples.cs)

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

  WriteMessage("All red Lines moved to new layer " + layerName);
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


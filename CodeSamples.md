Code from [SampleCode.cs](https://github.com/wtertinek/Linq2Acad/blob/master/Sources/Linq2Acad.SampleCode.CS/SampleCode.cs). See [SampleCode.vb](https://github.com/wtertinek/Linq2Acad/blob/master/Sources/Linq2Acad.SampleCode.VB/SampleCode.vb) for VB samples.

#### This sample removes all entities from the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Clear();
}
```

#### This sample removes all BlockReferences from the model space:

```c#
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

#### This sample adds a line to the model space:

```c#
using (var db = AcadDatabase.Active())
{
  db.ModelSpace
    .Add(new Line(Point3d.Origin,
                  new Point3d(100, 100, 0)));
}
```

#### This sample creates a new layer:

```c#
var layerName = GetString("Enter layer name");
var colorName = GetString("Enter color name");

if (layerName != null &&
    colorName != null)
{
  using (var db = AcadDatabase.Active())
  {
    var layer = db.Layers.Create(layerName);
    layer.Color = Color.FromColor(System.Drawing.Color.FromName(colorName));
  }

  WriteMessage($"Layer {layerName} created");
}
```

#### This sample prints all layer names:

```c#
using (var db = AcadDatabase.Active())
{
  foreach (var layer in db.Layers)
  {
    WriteMessage(layer.Name);
  }
}
```

#### This sample turns off all layers, except the one the user enters:

```c#
var layerName = GetString("Enter layer name");

if (layerName != null)
{
  using (var db = AcadDatabase.Active())
  {
    var layerToIgnore = db.Layers
                          .Element(layerName);

    foreach (var layer in db.Layers
                            .Except(layerToIgnore)
                            .UpgradeOpen())
    {
      layer.IsOff = true;
    }
  }

  WriteMessage($"All layers turned off, except {layerName}");
}
```

#### This sample creates a layer and adds all red lines in the model space to it:

```c#
var layerName = GetString("Enter layer name");

if (layerName != null)
{
  using (var db = AcadDatabase.Active())
  {
    var lines = db.ModelSpace
                  .OfType<Line>()
                  .Where(l => l.Color.ColorValue.Name == "ffff0000");
    db.Layers
      .Create(layerName, lines);
  }

  WriteMessage($"All red lines moved to new layer {layerName}");
}
```

#### This sample moves entities from one layer to another:

```c#
var sourceLayerName = GetString("Enter source layer name");
var targetLayerName = GetString("Enter target layer name");

if (sourceLayerName != null && targetLayerName != null)
{
  using (var db = AcadDatabase.Active())
  {
    var entities = db.CurrentSpace
                     .Where(e => e.Layer == sourceLayerName);
    db.Layers
      .Element(targetLayerName)
      .AddRange(entities);
  }

  WriteMessage($"All entities on layer {sourceLayerName} moved to layer {targetLayerName}");
}
```

#### This sample imports a block from a drawing file:

```c#
var filePath = GetString("Enter file path");
var blockName = GetString("Enter block name");

if (filePath != null && blockName != null)
{
  using (var sourceDb = AcadDatabase.OpenReadOnly(filePath))
  {
    var block = sourceDb.Blocks
                        .Element(blockName);

    using (var activeDb = AcadDatabase.Active())
    {
      activeDb.Blocks
              .Import(block);
    }
  }

  WriteMessage($"Block {blockName} imported");
}
```

#### This sample opens a drawing from file and counts the BlockReferences in the model space:

```c#
var filePath = GetString("Enter file path");

if (filePath != null)
{
  using (var db = AcadDatabase.OpenReadOnly(filePath))
  {
    var count = db.ModelSpace
                  .OfType<BlockReference>()
                  .Count();

    WriteMessage($"Model space block references in file {filePath}: {count}");
  }
}
```

#### This sample picks an entity and saves a string on it:

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter key");
var str = GetString("Enter string to save");

if (entityId.IsValid && key != null && str != null)
{
  using (var db = AcadDatabase.Active())
  {
    db.CurrentSpace
      .Element(entityId)
      .SaveData(key, str);
  }

  WriteMessage($"Key-value-pair {key}:{str} saved on entity");
}
```

#### This sample picks an entity and reads a string from it:

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter key");

if (entityId.IsValid && key != null)
{
  using (var db = AcadDatabase.Active())
  {
    var str = db.CurrentSpace
                .Element(entityId)
                .GetData<string>(key);

    WriteMessage($"String {str} read from entity");
  }
}
```

#### This sample picks an entity and reads a string from it (with XData as the data source):

```c#
var entityId = GetEntity("Pick an entity");
var key = GetString("Enter RegApp name");

if (entityId.IsValid && key != null)
{
  using (var db = AcadDatabase.Active())
  {
    var str = db.CurrentSpace
                .Element(entityId)
                .GetData<string>(key, true);

    WriteMessage($"String {str} read from entity's XData");
  }
}
```

#### This sample counts the number of entities in all paper space layouts:

```c#
using (var db = AcadDatabase.Active())
{
  var count = db.PaperSpace
                .SelectMany(ps => ps)
                .Count();

  WriteMessage($"{count} entities in all paper space layouts");
}
```

#### This sample changes the summary info:

```c#
using (var db = AcadDatabase.Active())
{
  db.SummaryInfo.Author = "John Doe";
  db.SummaryInfo.CustomProperties["CustomData1"] = "42";
}
```

#### This sample reloads all loaded XRefs:

```c#
using (var db = AcadDatabase.Active())
{
  foreach (var xRef in db.XRefs
                         .Where(xr => xr.IsLoaded))
  {
    xRef.Reload();
  }
}
```
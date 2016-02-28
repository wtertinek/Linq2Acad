###Linq2Acad
**Linq2Acad** is a library that aims to simplify .NET AutoCAD addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information. Using Linq2Acad the type information is preserved.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

###Note
In order to be able to compile the solution, you have to add the AutoCAD assembly path to the project's reference path.

As an example, let's assume your AutoCAD installation folder is *C:\Program Files\Autodesk\AutoCAD 2016*. To add the reference paths, open the solution, go to the project properties, select the tab *Reference Paths* and add the folder *C:\Program Files\Autodesk\AutoCAD 2016*. Do this for both projects, Linq2Acad and Linq2Acad.Tests.

###Examples
As an example, erasing all BlockReferences from the model space can be done like this:

```c#
using (var db = L2ADatabase.Active())
{
  db.ModelSpace
    .OfType<BlockReference>()
    .ForEach(br => br.Erase());
}
```

Adding a line to the model space:

```c#
using (var db = L2ADatabase.Active())
{
  db.ModelSpace
    .Add(new Line(new Point3d(5, 5, 0),
                  new Point3d(12, 3, 0)));
}
```

Printing all layer names:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.Active())
{
  db.Layers.ForEach(l => editor.WriteLine(l.Name));
}
```

Creating a group and adding all lines in the model space to it:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.Active())
{
  if (db.Groups.Contains("LineGroup"))
  {
    editor.WriteLine("LineGroup already exists");
  }
  else
  {
    var lines = db.ModelSpace
                  .OfType<Line>();
    db.Groups
      .Create("LineGroup", lines);
  }
}
```

Picking an entity and saving a string on it:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

var result1 = editor.GetEntity("Pick an entity:");

if (result1.Status == PromptStatus.OK)
{
  var result2 = editor.GetString("Enter key:");

  if (result2.Status == PromptStatus.OK)
  {
    var result3 = editor.GetString("Enter text to save:");

    if (result3.Status == PromptStatus.OK)
    {
      var entityID = result1.ObjectId;
      var key = result2.StringResult;
      var value = result3.StringResult;
      
      using (var db = L2ADatabase.Active())
      {
        db.ModelSpace
          .ByID(entityID)
          .SaveData(key, value);
      }
    }
  }
}
```

Picking an entity and reading a string from it:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

var result1 = editor.GetEntity("Pick an entity:");

if (result1.Status == PromptStatus.OK)
{
  var result2 = editor.GetString("Enter key:");

  if (result2.Status == PromptStatus.OK)
  {
    var entityID = result1.ObjectId;
    var key = result2.StringResult;
      
    using (var db = L2ADatabase.Active())
    {
      var value = db.ModelSpace
                    .ByID(entityID)
                    .GetData<string>(key);
      
      editor.WriteLine("Value: " + value);
    }
  }
}
```

Picking an entity and turning off all layers, except the entity's layer:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.Active())
{
  var result = editor.GetEntity("Select an entity");

  if (result.Status == PromptStatus.OK)
  {
    var layerID = db.CurrentSpace
                    .ByID(result.ObjectId)
                    .LayerId;
    db.Layers
      .Where(l => l.Id != layerID)
      .ForEach(l => l.IsOff = true);
  }
}
```

Moving entities from one layer to another:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.Active())
{
  var result = editor.GetString("Enter source layer name:",
                                s => db.Layers.Contains(s));

  if (result.Status == PromptStatus.OK)
  {
    var sourceLayer = db.Layers
                        .ByName(result.StringResult);

    result = Editor.GetString("Enter target layer name:",
                              s => db.Layers.Contains(s));

    if (result.Status == PromptStatus.OK)
    {
      var entities = db.ModelSpace
                        .Where(e => e.Layer == result.StringResult);
      db.Layers
        .ByName(result.StringResult)
        .AddRange(entities);
    }
  }
}
```

Opening a drawing from file and count the BlockReferences in the model space:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;
var result = editor.GetString("Enter file path:", s => File.Exists(s));

if (result.Status == PromptStatus.OK)
{
  using (var db = L2ADatabase.Open(result.StringResult))
  {
    var count = db.ModelSpace
                  .OfType<BlockReference>()
                  .Count();

    editor.WriteLine("Model space BlockReferences in file " + result.StringResult + ": " + count);
  }
}
```

Importing a block from a drawing file:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;
var result = editor.GetString("Enter file path:", s => File.Exists(s));
      
if (result.Status == PromptStatus.OK)
{
  using (var sourceDB = L2ADatabase.Open(result.StringResult))
  {
    result = editor.GetString("Enter block name:",
                              s => sourceDB.Blocks.Contains(s));

    if (result.Status == PromptStatus.OK)
    {
      var block = sourceDB.Blocks.ByName(result.StringResult);

      using (var activeDB = L2ADatabase.Active())
      {
        activeDB.Blocks
                .Import(block, true);
      }

      editor.WriteLine("Block " + result.StringResult + " successfully imported");
    }
  }
}
```

### TODO
I just started the project, at the moment it's just a working prototype for AutoCAD 2016. If you have any comments or suggestions, **I'm very happy to hear from you**.

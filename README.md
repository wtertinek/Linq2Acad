###Linq2Acad
**Linq2Acad** is a library that aims to simplify .NET AutoCAD addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information. Using Linq2Acad the type information is preserved.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

###Examples
As an example, erasing all BlockReferences from the model space can be done like this:

```c#
using (var db = AcadDatabase.FromActiveDocument())
{
  db.ModelSpace
    .OfType<BlockReference>()
    .ForEach(br => br.Erase());
}
```

Adding a line to the model space:

```c#
using (var db = AcadDatabase.FromActiveDocument())
{
  db.ModelSpace
    .Add(new Line(new Point3d(5, 5, 0),
                  new Point3d(12, 3, 0)));
}
```

Printing all layer names:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

using (var db = AcadDatabase.FromActiveDocument())
{
  db.Layers
    .ForEach(l => editor.WriteLine(l.Name));
}
```

Creating a group and adding all lines in the model space to it:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

using (var db = AcadDatabase.FromActiveDocument())
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
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

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
      
      using (var db = AcadDatabase.FromActiveDocument())
      {
        db.ModelSpace
          .Element(entityID)
          .SaveData(key, value);
      }
    }
  }
}
```

Picking an entity and reading a string from it:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

var result1 = editor.GetEntity("Pick an entity:");

if (result1.Status == PromptStatus.OK)
{
  var result2 = editor.GetString("Enter key:");

  if (result2.Status == PromptStatus.OK)
  {
    var entityID = result1.ObjectId;
    var key = result2.StringResult;
      
    using (var db = AcadDatabase.FromActiveDocument())
    {
      var value = db.ModelSpace
                    .Element(entityID)
                    .GetData<string>(key);
      
      editor.WriteLine("Value: " + value);
    }
  }
}
```

Turning off all layers, except the one the user enters:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

using (var db = AcadDatabase.FromActiveDocument())
{
  var result = editor.GetString("Enter layer name",
                                s => db.Layers.Contains(s));

  if (result.Status == PromptStatus.OK)
  {
    var layer = db.Layers.Element(result.StringResult);

    db.Layers
      .Except(new[] { layer })
      .ForEach(l => l.IsOff = true);
  }
}
```

Moving entities from one layer to another:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;

using (var db = AcadDatabase.FromActiveDocument())
{
  var result = editor.GetString("Enter source layer name:",
                                s => db.Layers.Contains(s));

  if (result.Status == PromptStatus.OK)
  {
    var sourceLayer = db.Layers
                        .Element(result.StringResult);

    result = Editor.GetString("Enter target layer name:",
                              s => db.Layers.Contains(s));

    if (result.Status == PromptStatus.OK)
    {
      var entities = db.ModelSpace
                        .Where(e => e.Layer == result.StringResult);
      db.Layers
        .Element(result.StringResult)
        .AddRange(entities);
    }
  }
}
```

Opening a drawing from file and count the BlockReferences in the model space:

```c#
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;
var result = editor.GetString("Enter file path:", s => File.Exists(s));

if (result.Status == PromptStatus.OK)
{
  using (var db = AcadDatabase.FromFile(result.StringResult))
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
var editor = Application.DocumentManager.MdiFromActiveDocumentDocument.Editor;
var result = editor.GetString("Enter file path:", s => File.Exists(s));
      
if (result.Status == PromptStatus.OK)
{
  using (var sourceDB = AcadDatabase.FromFile(result.StringResult))
  {
    result = editor.GetString("Enter block name:",
                              s => sourceDB.Blocks.Contains(s));

    if (result.Status == PromptStatus.OK)
    {
      var block = sourceDB.Blocks
                          .Element(result.StringResult);

      using (var activeDB = AcadDatabase.FromActiveDocument())
      {
        activeDB.Blocks
                .Import(block, true);
      }

      editor.WriteLine("Block " + result.StringResult + " successfully imported");
    }
  }
}
```

### Referencing AutoCAD assemblies
In order to be able to compile the solution, you have to add your local AutoCAD installation folder to the *Reference Paths* of each project.

Let's assume your AutoCAD installation folder is *C:\Program Files\Autodesk\AutoCAD 2016*. To add this folder as a reference path for the project Linq2Acad, go to the project properties, select the tab *Reference Paths* and add *C:\Program Files\Autodesk\AutoCAD 2016*. Repeat this procedure for Linq2Acad.Tests as well.

### TODO
I just started the project, at the moment it's just a working prototype for AutoCAD 2016. If you have any comments or suggestions, **I'm very happy to hear from you**.
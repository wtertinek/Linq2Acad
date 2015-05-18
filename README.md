###Linq2Acad
**Linq2Acad** is a library that aims to simplify .NET AutoCAD addin code. The use of transactions is abstracted away through extension methods and ```IEnumerable<T>``` implementations which enumerate the datatabase objects. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ queries through the ```dynamic``` keyword, which has the drawback of losing all type information. Using Linq2Acad the type information is preserved.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

###Examples
As an example, erasing all BlockReferences from the model space can be done like this:

```c#
using (var db = L2ADatabase.ActiveDatabase())
{
  db.ModelSpace
    .OfType<BlockReference>()
    .ForEach(br => br.Erase());
}
```

Adding a line to the model space:

```c#
using (var db = L2ADatabase.ActiveDatabase())
{
  db.ModelSpace
    .Add(new Line(new Point3d(5, 5, 0),
                  new Point3d(12, 3, 0)));
}
```

Printing all layer names:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.ActiveDatabase())
{
  db.Layers.ForEach(l => editor.WriteLine(l.Name));
}
```

Creating a group and adding all lines in the model space to it:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.ActiveDatabase())
{
  if (db.Groups.Contains("LineGroup"))
  {
    editor.WriteMessage("LineGroup already exists");
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

Picking an entity and turning off all layers, except the entity's layer:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = L2ADatabase.ActiveDatabase())
{
  var result = editor.GetEntity("Select an entity");

  if (result.Status == PromptStatus.OK)
  {
    var layerID = db.AcadDatabase
                    .GetObject<Entity>(result.ObjectId)
                    .LayerId;
    db.Layers
      .Where(l => l.Id != layerID)
      .ForEach(l => l.IsOff = true);
  }
}
```

Opening a drawing from file and count the BlockReferences in the model space:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;
var result = editor.GetString("Enter file path:");

if (result.Status == PromptStatus.OK && File.Exists(result.StringResult))
{
  using (var db = L2ADatabase.Open(result.StringResult))
  {
    var count = db.ModelSpace
                  .OfType<BlockReference>()
                  .Count();

    editor.WriteMessage("\n" + result.StringResult + ": " + count + " BlockReferences");
  }
}
```

Importing a block from a drawing file:

```c#
var editor = Application.DocumentManager.MdiActiveDocument.Editor;
var result = editor.GetString("Enter file path:");

if (result.Status == PromptStatus.OK &&
    File.Exists(result.StringResult))
{
  using (var sourceDB = L2ADatabase.Open(result.StringResult))
  {
    result = editor.GetString("Enter block name:");

    if (result.Status == PromptStatus.OK &&
        sourceDB.Blocks.Contains(result.StringResult))
    {
      var block = sourceDB.Blocks
                          .Item(result.StringResult);

      using (var activeDB = L2ADatabase.ActiveDatabase())
      {
        activeDB.Blocks
                .Import(block, true);
      }

      editor.WriteMessage("\nBlock " + result.StringResult + " successfully imported");
    }
  }
}
```

### TODO
I just started the project, at the moment it's just a working prototype for AutoCAD 2016. If you have any comments or suggestions, **I'm very happy to hear from you**.

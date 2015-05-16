###Linq2AcDb
**Linq2AcDb** is a library that aims to simplify .NET AutoCAD addin code. The use of transactions is abstracted away by using extension methods and ```IEnumerable<T>```. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ query through the ```dynamic``` keyword, which has the drawback of losing the type information. Using Linq2AcDb the full type information is preserved.

In general, the library should be a more intuitive API for working with the drawing database, making the learning curve for beginners less steep.

###Examples
As an example, erasing all BlockReferences from the model space can be done like this:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;

using (var db = new ActiveDatabase(database))
{
  db.ModelSpace
    .Items()
    .OfType<BlockReference>()
    .UpgradeOpen()
    .ForEach(br => br.Erase());
}
```

Adding a line to the model space:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;

using (var db = new ActiveDatabase(Database))
{
  db.ModelSpace
    .Add(new Line(new Point3d(5, 5, 0),
                  new Point3d(12, 3, 0)));
}
```

Printing all layer names:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = new ActiveDatabase(database))
{
  db.Layers
    .ForEach(l => editor.WriteLine(l.Name));
}
```

Creating a group:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = new ActiveDatabase(database))
{
  if (db.Groups.Contains("Group1"))
  {
    editor.WriteMessage("Group1 already exists");
  }
  else
  {
    db.Groups.Add("Group1", new Group("This is Group 1", true));
  }
}
```

Picking an entity and turning off all layers, except the entity's layer:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = new ActiveDatabase(database))
{
  var result = editor.GetEntity("Select an entity");

  if (result.Status == PromptStatus.OK)
  {
    var layerID = db.Database
                    .GetObject<Entity>(result.ObjectId)
                    .LayerId;
    db.Layers
      .Where(l => l.Id != layerID)
      .ForEach(l => l.IsOff = true);
  }
}
```

### TODO
I just started the project, at the moment it's just a working prototype for AutoCAD 2016. If you have any comments or suggestions, **I'm very happy to hear from you**.
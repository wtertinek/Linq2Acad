###Linq2AcDb
**Linq2AcDb** is a library that aims to simplify .NET AutoCAD addin code. The use of transactions is abstracted away by using extension methods and ```IEnumerable<T>```. This provides the possibility to execute LINQ queries on database-resident objects. The AutoCAD .NET API already offers a way to use LINQ query through ```dynamic``` keyword, which has the drawback of losing the type information. Using Linq2AcDb the full type information is preserved.

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

Printing all layer names:

```c#
var database = Application.DocumentManager.MdiActiveDocument.Database;
var editor = Application.DocumentManager.MdiActiveDocument.Editor;

using (var db = new ActiveDatabase(Database))
{
  db.Layers
    .ForEach(l => editor.WriteLine(l.Name));
}
```

### TODO
I just started the project, at the moment it's just a working prototype for AutoCAD 2016. If you have any comments or suggestions, I'm very happy to hear from you.

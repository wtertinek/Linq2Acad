# LayerTableRecordExtensions.AddRange Method 
 

Adds the given entities to this layer.

## Syntax

**C#**<br />
``` C#
public static void AddRange(
	this LayerTableRecord layer,
	IEnumerable<Entity> entities
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub AddRange ( 
	layer As LayerTableRecord,
	entities As IEnumerable(Of Entity)
)
```


#### Parameters
<dl><dt>layer</dt><dd>Type: LayerTableRecord<br />The layer instance.</dd><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The entities to add.</dd></dl>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type LayerTableRecord. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>entities</i> is null.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when adding an entity throws an exception.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_LayerTableRecordExtensions.md">LayerTableRecordExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

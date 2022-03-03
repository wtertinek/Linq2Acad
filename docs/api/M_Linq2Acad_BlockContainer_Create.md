# BlockContainer.Create Method (string, IEnumerable(Entity))
 

Creates a new BlockTableRecord with the specified name and adds the Entities to it.

## Syntax

**C#**<br />
``` C#
public BlockTableRecord Create(
	string name,
	IEnumerable<Entity> entities
)
```

**VB**<br />
``` VB
Public Function Create ( 
	name As String,
	entities As IEnumerable(Of Entity)
) As BlockTableRecord
```


#### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the new BlockTableRecord.</dd><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The Entities that should be added to the BlockTableRecord.</dd></dl>

#### Return Value
Type: BlockTableRecord<br />A new instance of BlockTableRecord.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameters <i>name</i> or <i>entities</i> is null.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md">BlockContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

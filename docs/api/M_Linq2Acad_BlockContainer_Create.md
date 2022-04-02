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


### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the new BlockTableRecord.</dd><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The Entities that should be added to the BlockTableRecord.</dd></dl>

### Return Value
Type: BlockTableRecord<br />A new instance of BlockTableRecord.
<a href="#BlockContainerCreate-Method-string-IEnumerableEntity">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md#BlockContainer-Class">BlockContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

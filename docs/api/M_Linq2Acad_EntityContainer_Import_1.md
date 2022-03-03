# EntityContainer.Import Method (Entity, bool)
 

Imports the specified Entity into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<Entity> Import(
	Entity element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As Entity,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of Entity)
```


#### Parameters
<dl><dt>element</dt><dd>Type: Entity<br />The Entity to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Entity should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(Entity)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_EntityContainer.md">EntityContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

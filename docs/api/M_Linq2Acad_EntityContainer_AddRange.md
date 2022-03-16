# EntityContainer.AddRange Method 
 

Adds Entities to the container.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ObjectId> AddRange(
	IEnumerable<Entity> entities,
	bool setDatabaseDefaults = false
)
```

**VB**<br />
``` VB
Public Function AddRange ( 
	entities As IEnumerable(Of Entity),
	Optional setDatabaseDefaults As Boolean = false
) As IReadOnlyCollection(Of ObjectId)
```


#### Parameters
<dl><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The Entities to be added.</dd><dt>setDatabaseDefaults (Optional)</dt><dd>Type: bool<br />True, if the database defaults should be set.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(ObjectId)<br />The ObjectIds of the Entities that were added.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>entities</i> is null.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when the an Entity belongs to another block or an AutoCAD error occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_EntityContainer.md">EntityContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

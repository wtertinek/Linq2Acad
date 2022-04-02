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


### Parameters
<dl><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The Entities to be added.</dd><dt>setDatabaseDefaults (Optional)</dt><dd>Type: bool<br />True, if the database defaults should be set.</dd></dl>

### Return Value
Type: IReadOnlyCollection(ObjectId)<br />The ObjectIds of the Entities that were added.
<a href="#EntityContainerAddRange-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_EntityContainer.md#EntityContainer-Class">EntityContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

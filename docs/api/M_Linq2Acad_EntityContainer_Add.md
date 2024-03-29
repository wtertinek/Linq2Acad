# EntityContainer.Add Method 
 

Adds an Entity to the container.

## Syntax

**C#**<br />
``` C#
public ObjectId Add(
	Entity entity,
	bool setDatabaseDefaults = false
)
```

**VB**<br />
``` VB
Public Function Add ( 
	entity As Entity,
	Optional setDatabaseDefaults As Boolean = false
) As ObjectId
```


### Parameters
<dl><dt>entity</dt><dd>Type: Entity<br />The Entity to be added.</dd><dt>setDatabaseDefaults (Optional)</dt><dd>Type: bool<br />True, if the database defaults should be set.</dd></dl>

### Return Value
Type: ObjectId<br />The ObjectId of the Entity that was added.
<a href="#EntityContainerAdd-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_EntityContainer.md#EntityContainer-Class">EntityContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

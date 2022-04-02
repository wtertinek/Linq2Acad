# PaperSpaceEntityContainer.Element Method (ObjectId, bool)
 

Returns the Entity with the specified ID.

## Syntax

**C#**<br />
``` C#
public Entity Element(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As Entity
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the Entity.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: Entity<br />The Entity with the specified ID.
<a href="#PaperSpaceEntityContainerElement-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_PaperSpaceEntityContainer.md#PaperSpaceEntityContainer-Class">PaperSpaceEntityContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

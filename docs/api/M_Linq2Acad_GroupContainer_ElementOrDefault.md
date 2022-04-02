# GroupContainer.ElementOrDefault Method (ObjectId, bool)
 

Returns the Group with the specified ID or <i>null</i> if the Group cannot be found.

## Syntax

**C#**<br />
``` C#
public Group ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As Group
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the Group.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: Group<br />The Group with the specified ID.
<a href="#GroupContainerElementOrDefault-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_GroupContainer.md#GroupContainer-Class">GroupContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

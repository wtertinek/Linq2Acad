# RegAppContainer.ElementOrDefault Method (ObjectId, bool)
 

Returns the RegAppTableRecord with the specified ID or <i>null</i> if the RegAppTableRecord cannot be found.

## Syntax

**C#**<br />
``` C#
public RegAppTableRecord ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As RegAppTableRecord
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the RegAppTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: RegAppTableRecord<br />The RegAppTableRecord with the specified ID.
<a href="#RegAppContainerElementOrDefault-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_RegAppContainer.md#RegAppContainer-Class">RegAppContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

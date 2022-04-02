# ViewContainer.ElementOrDefault Method (ObjectId, bool)
 

Returns the ViewTableRecord with the specified ID or <i>null</i> if the ViewTableRecord cannot be found.

## Syntax

**C#**<br />
``` C#
public ViewTableRecord ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As ViewTableRecord
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the ViewTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: ViewTableRecord<br />The ViewTableRecord with the specified ID.
<a href="#ViewContainerElementOrDefault-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_ViewContainer.md#ViewContainer-Class">ViewContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

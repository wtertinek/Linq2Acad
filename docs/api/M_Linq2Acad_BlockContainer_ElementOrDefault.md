# BlockContainer.ElementOrDefault Method (ObjectId, bool)
 

Returns the BlockTableRecord with the specified ID or <i>null</i> if the BlockTableRecord cannot be found.

## Syntax

**C#**<br />
``` C#
public BlockTableRecord ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As BlockTableRecord
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the BlockTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: BlockTableRecord<br />The BlockTableRecord with the specified ID.
<a href="#BlockContainerElementOrDefault-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md#BlockContainer-Class">BlockContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

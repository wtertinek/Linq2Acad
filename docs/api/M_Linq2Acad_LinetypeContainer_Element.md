# LinetypeContainer.Element Method (ObjectId, bool)
 

Returns the LinetypeTableRecord with the specified ID.

## Syntax

**C#**<br />
``` C#
public LinetypeTableRecord Element(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As LinetypeTableRecord
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the LinetypeTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: LinetypeTableRecord<br />The LinetypeTableRecord with the specified ID.
<a href="#LinetypeContainerElement-Method-ObjectId-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LinetypeContainer.md#LinetypeContainer-Class">LinetypeContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

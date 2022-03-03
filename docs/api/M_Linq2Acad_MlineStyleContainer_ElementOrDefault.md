# MlineStyleContainer.ElementOrDefault Method 
 

Returns the MlineStyle with the specified ID or <i>null</i> if the MlineStyle cannot be found.

## Syntax

**C#**<br />
``` C#
public MlineStyle ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As MlineStyle
```


#### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the MlineStyle.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: MlineStyle<br />The MlineStyle with the specified ID.

## See Also


#### Reference
<a href="T_Linq2Acad_MlineStyleContainer.md">MlineStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

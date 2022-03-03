# MaterialContainer.ElementOrDefault Method 
 

Returns the Material with the specified ID or <i>null</i> if the Material cannot be found.

## Syntax

**C#**<br />
``` C#
public Material ElementOrDefault(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As Material
```


#### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the Material.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: Material<br />The Material with the specified ID.

## See Also


#### Reference
<a href="T_Linq2Acad_MaterialContainer.md">MaterialContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

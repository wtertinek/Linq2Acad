# GroupContainer.Element Method 
 

Returns the Group with the specified ID.

## Syntax

**C#**<br />
``` C#
public Group Element(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As Group
```


#### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the Group.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: Group<br />The Group with the specified ID.

## See Also


#### Reference
<a href="T_Linq2Acad_GroupContainer.md">GroupContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

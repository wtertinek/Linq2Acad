# SectionViewStyleContainer.Element Method 
 

Returns the SectionViewStyle with the specified ID.

## Syntax

**C#**<br />
``` C#
public SectionViewStyle Element(
	ObjectId id,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As SectionViewStyle
```


#### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the SectionViewStyle.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: SectionViewStyle<br />The SectionViewStyle with the specified ID.

## See Also


#### Reference
<a href="T_Linq2Acad_SectionViewStyleContainer.md">SectionViewStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

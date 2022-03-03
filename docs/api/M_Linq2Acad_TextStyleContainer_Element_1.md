# TextStyleContainer.Element Method (string, bool)
 

Returns the TextStyleTableRecord with the specified name.

## Syntax

**C#**<br />
``` C#
public TextStyleTableRecord Element(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	name As String,
	Optional openForWrite As Boolean = false
) As TextStyleTableRecord
```


#### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the TextStyleTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: TextStyleTableRecord<br />The TextStyleTableRecord with the specified name.

## See Also


#### Reference
<a href="T_Linq2Acad_TextStyleContainer.md">TextStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

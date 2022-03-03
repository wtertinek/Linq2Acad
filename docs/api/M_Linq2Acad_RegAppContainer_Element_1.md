# RegAppContainer.Element Method (string, bool)
 

Returns the RegAppTableRecord with the specified name.

## Syntax

**C#**<br />
``` C#
public RegAppTableRecord Element(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function Element ( 
	name As String,
	Optional openForWrite As Boolean = false
) As RegAppTableRecord
```


#### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the RegAppTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: RegAppTableRecord<br />The RegAppTableRecord with the specified name.

## See Also


#### Reference
<a href="T_Linq2Acad_RegAppContainer.md">RegAppContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

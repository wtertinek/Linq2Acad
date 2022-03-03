# LinetypeContainer.ElementOrDefault Method (string, bool)
 

Returns the LinetypeTableRecord with the specified name or <i>null</i> if the LinetypeTableRecord cannot be found.

## Syntax

**C#**<br />
``` C#
public LinetypeTableRecord ElementOrDefault(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	name As String,
	Optional openForWrite As Boolean = false
) As LinetypeTableRecord
```


#### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the LinetypeTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

#### Return Value
Type: LinetypeTableRecord<br />The LinetypeTableRecord with the specified name.

## See Also


#### Reference
<a href="T_Linq2Acad_LinetypeContainer.md">LinetypeContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

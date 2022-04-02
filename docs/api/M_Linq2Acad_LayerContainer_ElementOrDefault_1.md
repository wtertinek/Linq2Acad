# LayerContainer.ElementOrDefault Method (string, bool)
 

Returns the LayerTableRecord with the specified name or <i>null</i> if the LayerTableRecord cannot be found.

## Syntax

**C#**<br />
``` C#
public LayerTableRecord ElementOrDefault(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	name As String,
	Optional openForWrite As Boolean = false
) As LayerTableRecord
```


### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the LayerTableRecord.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: LayerTableRecord<br />The LayerTableRecord with the specified name.
<a href="#LayerContainerElementOrDefault-Method-string-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LayerContainer.md#LayerContainer-Class">LayerContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

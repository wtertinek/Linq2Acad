# UcsContainer.Import Method (UcsTableRecord, bool)
 

Imports the specified UcsTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<UcsTableRecord> Import(
	UcsTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As UcsTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of UcsTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: UcsTableRecord<br />The UcsTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported UcsTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(UcsTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_UcsContainer.md">UcsContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

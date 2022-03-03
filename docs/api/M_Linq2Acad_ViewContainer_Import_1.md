# ViewContainer.Import Method (ViewTableRecord, bool)
 

Imports the specified ViewTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<ViewTableRecord> Import(
	ViewTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As ViewTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of ViewTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: ViewTableRecord<br />The ViewTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported ViewTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(ViewTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_ViewContainer.md">ViewContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

# RegAppContainer.Import Method (RegAppTableRecord, bool)
 

Imports the specified RegAppTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<RegAppTableRecord> Import(
	RegAppTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As RegAppTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of RegAppTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: RegAppTableRecord<br />The RegAppTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported RegAppTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(RegAppTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_RegAppContainer.md">RegAppContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

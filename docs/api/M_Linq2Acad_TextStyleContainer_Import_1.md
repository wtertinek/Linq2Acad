# TextStyleContainer.Import Method (TextStyleTableRecord, bool)
 

Imports the specified TextStyleTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<TextStyleTableRecord> Import(
	TextStyleTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As TextStyleTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of TextStyleTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: TextStyleTableRecord<br />The TextStyleTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported TextStyleTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(TextStyleTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_TextStyleContainer.md">TextStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

# DBVisualStyleContainer.Import Method (DBVisualStyle, bool)
 

Imports the specified DBVisualStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<DBVisualStyle> Import(
	DBVisualStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As DBVisualStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of DBVisualStyle)
```


#### Parameters
<dl><dt>element</dt><dd>Type: DBVisualStyle<br />The DBVisualStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DBVisualStyle should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(DBVisualStyle)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_DBVisualStyleContainer.md">DBVisualStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

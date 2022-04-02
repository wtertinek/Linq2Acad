# TableStyleContainer.Import Method (TableStyle, bool)
 

Imports the specified TableStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<TableStyle> Import(
	TableStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As TableStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of TableStyle)
```


### Parameters
<dl><dt>element</dt><dd>Type: TableStyle<br />The TableStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported TableStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(TableStyle)<br />An object that represents the result of an import operation.
<a href="#TableStyleContainerImport-Method-TableStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_TableStyleContainer.md#TableStyleContainer-Class">TableStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

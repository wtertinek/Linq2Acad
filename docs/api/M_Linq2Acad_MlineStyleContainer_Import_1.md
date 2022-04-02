# MlineStyleContainer.Import Method (MlineStyle, bool)
 

Imports the specified MlineStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<MlineStyle> Import(
	MlineStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As MlineStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of MlineStyle)
```


### Parameters
<dl><dt>element</dt><dd>Type: MlineStyle<br />The MlineStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported MlineStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(MlineStyle)<br />An object that represents the result of an import operation.
<a href="#MlineStyleContainerImport-Method-MlineStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_MlineStyleContainer.md#MlineStyleContainer-Class">MlineStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

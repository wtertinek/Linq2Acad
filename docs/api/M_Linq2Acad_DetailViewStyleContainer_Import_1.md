# DetailViewStyleContainer.Import Method (DetailViewStyle, bool)
 

Imports the specified DetailViewStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<DetailViewStyle> Import(
	DetailViewStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As DetailViewStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of DetailViewStyle)
```


### Parameters
<dl><dt>element</dt><dd>Type: DetailViewStyle<br />The DetailViewStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DetailViewStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(DetailViewStyle)<br />An object that represents the result of an import operation.
<a href="#DetailViewStyleContainerImport-Method-DetailViewStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DetailViewStyleContainer.md#DetailViewStyleContainer-Class">DetailViewStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

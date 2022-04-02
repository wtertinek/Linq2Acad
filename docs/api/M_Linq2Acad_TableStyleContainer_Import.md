# TableStyleContainer.Import Method (IEnumerable(TableStyle), bool)
 

Imports the specified TableStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<TableStyle>> Import(
	IEnumerable<TableStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of TableStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of TableStyle))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(TableStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported TableStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(TableStyle))<br />An object that represents the result of an import operation.
<a href="#TableStyleContainerImport-Method-IEnumerableTableStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_TableStyleContainer.md#TableStyleContainer-Class">TableStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

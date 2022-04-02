# TextStyleContainer.Import Method (IEnumerable(TextStyleTableRecord), bool)
 

Imports the specified TextStyleTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<TextStyleTableRecord>> Import(
	IEnumerable<TextStyleTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of TextStyleTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of TextStyleTableRecord))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(TextStyleTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported TextStyleTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(TextStyleTableRecord))<br />An object that represents the result of an import operation.
<a href="#TextStyleContainerImport-Method-IEnumerableTextStyleTableRecord-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_TextStyleContainer.md#TextStyleContainer-Class">TextStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

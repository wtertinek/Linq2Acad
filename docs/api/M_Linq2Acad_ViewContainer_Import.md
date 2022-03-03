# ViewContainer.Import Method (IEnumerable(ViewTableRecord), bool)
 

Imports the specified ViewTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<ViewTableRecord>> Import(
	IEnumerable<ViewTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of ViewTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of ViewTableRecord))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(ViewTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported ViewTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(ViewTableRecord))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_ViewContainer.md">ViewContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

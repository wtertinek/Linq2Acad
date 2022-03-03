# UcsContainer.Import Method (IEnumerable(UcsTableRecord), bool)
 

Imports the specified UcsTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<UcsTableRecord>> Import(
	IEnumerable<UcsTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of UcsTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of UcsTableRecord))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(UcsTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported UcsTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(UcsTableRecord))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_UcsContainer.md">UcsContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

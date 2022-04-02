# BlockContainer.Import Method (IEnumerable(BlockTableRecord), bool)
 

Imports the specified BlockTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<BlockTableRecord>> Import(
	IEnumerable<BlockTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of BlockTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of BlockTableRecord))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(BlockTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported BlockTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(BlockTableRecord))<br />An object that represents the result of an import operation.
<a href="#BlockContainerImport-Method-IEnumerableBlockTableRecord-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md#BlockContainer-Class">BlockContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

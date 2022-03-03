# DimStyleContainer.Import Method (IEnumerable(DimStyleTableRecord), bool)
 

Imports the specified DimStyleTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<DimStyleTableRecord>> Import(
	IEnumerable<DimStyleTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of DimStyleTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of DimStyleTableRecord))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(DimStyleTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DimStyleTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(DimStyleTableRecord))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_DimStyleContainer.md">DimStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

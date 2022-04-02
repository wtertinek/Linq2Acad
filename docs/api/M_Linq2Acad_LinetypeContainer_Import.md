# LinetypeContainer.Import Method (IEnumerable(LinetypeTableRecord), bool)
 

Imports the specified LinetypeTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<LinetypeTableRecord>> Import(
	IEnumerable<LinetypeTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of LinetypeTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of LinetypeTableRecord))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(LinetypeTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported LinetypeTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(LinetypeTableRecord))<br />An object that represents the result of an import operation.
<a href="#LinetypeContainerImport-Method-IEnumerableLinetypeTableRecord-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LinetypeContainer.md#LinetypeContainer-Class">LinetypeContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

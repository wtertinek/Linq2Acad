# RegAppContainer.Import Method (IEnumerable(RegAppTableRecord), bool)
 

Imports the specified RegAppTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<RegAppTableRecord>> Import(
	IEnumerable<RegAppTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of RegAppTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of RegAppTableRecord))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(RegAppTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported RegAppTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(RegAppTableRecord))<br />An object that represents the result of an import operation.
<a href="#RegAppContainerImport-Method-IEnumerableRegAppTableRecord-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_RegAppContainer.md#RegAppContainer-Class">RegAppContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

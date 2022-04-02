# LayerContainer.Import Method (IEnumerable(LayerTableRecord), bool)
 

Imports the specified LayerTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<LayerTableRecord>> Import(
	IEnumerable<LayerTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of LayerTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of LayerTableRecord))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(LayerTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported LayerTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(LayerTableRecord))<br />An object that represents the result of an import operation.
<a href="#LayerContainerImport-Method-IEnumerableLayerTableRecord-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LayerContainer.md#LayerContainer-Class">LayerContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

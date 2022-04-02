# ViewportContainer.Import Method (IEnumerable(Viewport), bool)
 

Imports the specified Viewports into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<Viewport>> Import(
	IEnumerable<Viewport> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of Viewport),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of Viewport))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(Viewport)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Viewport should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(Viewport))<br />An object that represents the result of an import operation.
<a href="#ViewportContainerImport-Method-IEnumerableViewport-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_ViewportContainer.md#ViewportContainer-Class">ViewportContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

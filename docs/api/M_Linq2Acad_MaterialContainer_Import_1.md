# MaterialContainer.Import Method (Material, bool)
 

Imports the specified Material into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<Material> Import(
	Material element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As Material,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of Material)
```


### Parameters
<dl><dt>element</dt><dd>Type: Material<br />The Material to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Material should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(Material)<br />An object that represents the result of an import operation.
<a href="#MaterialContainerImport-Method-Material-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_MaterialContainer.md#MaterialContainer-Class">MaterialContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

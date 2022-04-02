# LayoutContainer.Import Method (Layout, bool)
 

Imports the specified Layout into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<Layout> Import(
	Layout element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As Layout,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of Layout)
```


### Parameters
<dl><dt>element</dt><dd>Type: Layout<br />The Layout to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Layout should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(Layout)<br />An object that represents the result of an import operation.
<a href="#LayoutContainerImport-Method-Layout-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LayoutContainer.md#LayoutContainer-Class">LayoutContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

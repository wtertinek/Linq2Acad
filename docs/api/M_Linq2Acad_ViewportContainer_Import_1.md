# ViewportContainer.Import Method (Viewport, bool)
 

Imports the specified Viewport into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<Viewport> Import(
	Viewport element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As Viewport,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of Viewport)
```


#### Parameters
<dl><dt>element</dt><dd>Type: Viewport<br />The Viewport to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Viewport should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(Viewport)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_ViewportContainer.md">ViewportContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

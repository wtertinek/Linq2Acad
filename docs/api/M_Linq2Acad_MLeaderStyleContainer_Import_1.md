# MLeaderStyleContainer.Import Method (MLeaderStyle, bool)
 

Imports the specified MLeaderStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<MLeaderStyle> Import(
	MLeaderStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As MLeaderStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of MLeaderStyle)
```


#### Parameters
<dl><dt>element</dt><dd>Type: MLeaderStyle<br />The MLeaderStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported MLeaderStyle should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(MLeaderStyle)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_MLeaderStyleContainer.md">MLeaderStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

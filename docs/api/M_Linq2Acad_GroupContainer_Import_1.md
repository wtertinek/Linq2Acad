# GroupContainer.Import Method (Group, bool)
 

Imports the specified Group into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<Group> Import(
	Group element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As Group,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of Group)
```


#### Parameters
<dl><dt>element</dt><dd>Type: Group<br />The Group to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Group should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(Group)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_GroupContainer.md">GroupContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

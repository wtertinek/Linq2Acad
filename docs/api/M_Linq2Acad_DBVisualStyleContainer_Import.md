# DBVisualStyleContainer.Import Method (IEnumerable(DBVisualStyle), bool)
 

Imports the specified DBVisualStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<DBVisualStyle>> Import(
	IEnumerable<DBVisualStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of DBVisualStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of DBVisualStyle))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(DBVisualStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DBVisualStyle should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(DBVisualStyle))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_DBVisualStyleContainer.md">DBVisualStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

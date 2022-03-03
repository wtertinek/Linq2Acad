# DetailViewStyleContainer.Import Method (IEnumerable(DetailViewStyle), bool)
 

Imports the specified DetailViewStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<DetailViewStyle>> Import(
	IEnumerable<DetailViewStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of DetailViewStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of DetailViewStyle))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(DetailViewStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DetailViewStyle should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(DetailViewStyle))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_DetailViewStyleContainer.md">DetailViewStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

# SectionViewStyleContainer.Import Method (IEnumerable(SectionViewStyle), bool)
 

Imports the specified SectionViewStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<SectionViewStyle>> Import(
	IEnumerable<SectionViewStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of SectionViewStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of SectionViewStyle))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(SectionViewStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported SectionViewStyle should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(SectionViewStyle))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_SectionViewStyleContainer.md">SectionViewStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

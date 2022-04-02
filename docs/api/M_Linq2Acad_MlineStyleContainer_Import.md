# MlineStyleContainer.Import Method (IEnumerable(MlineStyle), bool)
 

Imports the specified MlineStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<MlineStyle>> Import(
	IEnumerable<MlineStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of MlineStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of MlineStyle))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(MlineStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported MlineStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(MlineStyle))<br />An object that represents the result of an import operation.
<a href="#MlineStyleContainerImport-Method-IEnumerableMlineStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_MlineStyleContainer.md#MlineStyleContainer-Class">MlineStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

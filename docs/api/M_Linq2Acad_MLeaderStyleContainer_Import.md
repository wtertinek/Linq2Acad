# MLeaderStyleContainer.Import Method (IEnumerable(MLeaderStyle), bool)
 

Imports the specified MLeaderStyles into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<MLeaderStyle>> Import(
	IEnumerable<MLeaderStyle> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of MLeaderStyle),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of MLeaderStyle))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(MLeaderStyle)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported MLeaderStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(MLeaderStyle))<br />An object that represents the result of an import operation.
<a href="#MLeaderStyleContainerImport-Method-IEnumerableMLeaderStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_MLeaderStyleContainer.md#MLeaderStyleContainer-Class">MLeaderStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

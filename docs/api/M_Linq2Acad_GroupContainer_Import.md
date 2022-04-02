# GroupContainer.Import Method (IEnumerable(Group), bool)
 

Imports the specified Groups into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<Group>> Import(
	IEnumerable<Group> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of Group),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of Group))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(Group)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Group should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(Group))<br />An object that represents the result of an import operation.
<a href="#GroupContainerImport-Method-IEnumerableGroup-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_GroupContainer.md#GroupContainer-Class">GroupContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

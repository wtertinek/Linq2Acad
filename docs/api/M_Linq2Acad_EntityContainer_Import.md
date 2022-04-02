# EntityContainer.Import Method (IEnumerable(Entity), bool)
 

Imports the specified Entities into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<Entity>> Import(
	IEnumerable<Entity> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of Entity),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of Entity))
```


### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(Entity)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported Entity should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(Entity))<br />An object that represents the result of an import operation.
<a href="#EntityContainerImport-Method-IEnumerableEntity-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_EntityContainer.md#EntityContainer-Class">EntityContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

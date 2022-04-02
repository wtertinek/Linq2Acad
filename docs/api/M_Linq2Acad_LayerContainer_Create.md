# LayerContainer.Create Method (string, IEnumerable(Entity))
 

Creates a new LayerTableRecord with the specified name and adds the Entities to it.

## Syntax

**C#**<br />
``` C#
public LayerTableRecord Create(
	string name,
	IEnumerable<Entity> entities
)
```

**VB**<br />
``` VB
Public Function Create ( 
	name As String,
	entities As IEnumerable(Of Entity)
) As LayerTableRecord
```


### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the new LayerTableRecord.</dd><dt>entities</dt><dd>Type: IEnumerable(Entity)<br />The Entities that should be added to the new LayerTableRecord.</dd></dl>

### Return Value
Type: LayerTableRecord<br />A new instance of LayerTableRecord.
<a href="#LayerContainerCreate-Method-string-IEnumerableEntity">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LayerContainer.md#LayerContainer-Class">LayerContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

# DbObjectContainer.Elements(*T*) Method (ObjectIdCollection, bool)
 

Returns the database objects with the given ObjectIds.

## Syntax

**C#**<br />
``` C#
public IEnumerable<T> Elements<T>(
	ObjectIdCollection ids,
	bool openForWrite = false
)
where T : DBObject

```

**VB**<br />
``` VB
Public Function Elements(Of T As DBObject) ( 
	ids As ObjectIdCollection,
	Optional openForWrite As Boolean = false
) As IEnumerable(Of T)
```


### Parameters
<dl><dt>ids</dt><dd>Type: ObjectIdCollection<br />The ids of the objects.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the objects should be opened for-write. By default the objects are opened readonly.</dd></dl>


### Return Value
Type: IEnumerable(*T*)<br />The objects with the given ObjectIds.
<a href="#DbObjectContainerElementsT-Method-ObjectIdCollection-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectContainer.md#DbObjectContainer-Class">DbObjectContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

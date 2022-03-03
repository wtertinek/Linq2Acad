# DbObjectContainer.Elements(*T*) Method (IEnumerable(ObjectId), bool)
 

Returns the database objects with the given ObjectIds.

## Syntax

**C#**<br />
``` C#
public IEnumerable<T> Elements<T>(
	IEnumerable<ObjectId> ids,
	bool openForWrite = false
)
where T : DBObject

```

**VB**<br />
``` VB
Public Function Elements(Of T As DBObject) ( 
	ids As IEnumerable(Of ObjectId),
	Optional openForWrite As Boolean = false
) As IEnumerable(Of T)
```


#### Parameters
<dl><dt>ids</dt><dd>Type: IEnumerable(ObjectId)<br />The ids of the objects.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the objects should be opened for-write. By default the objects are opened readonly.</dd></dl>


#### Return Value
Type: IEnumerable(*T*)<br />The objects with the given ObjectIds.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>ids</i> is null.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.invalidcastexception" target="_blank" rel="noopener noreferrer">InvalidCastException</a></td><td>Thrown when an object cannot be casted to the target type.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when an ObjectId is invalid or getting an element throws an exception.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectContainer.md">DbObjectContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

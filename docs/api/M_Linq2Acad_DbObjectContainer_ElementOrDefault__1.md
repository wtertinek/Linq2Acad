# DbObjectContainer.ElementOrDefault(*T*) Method 
 

Returns the database object with the given ObjectId, or <i>null</i> if the element does not exist.

## Syntax

**C#**<br />
``` C#
public T ElementOrDefault<T>(
	ObjectId id,
	bool openForWrite = false
)
where T : DBObject

```

**VB**<br />
``` VB
Public Function ElementOrDefault(Of T As DBObject) ( 
	id As ObjectId,
	Optional openForWrite As Boolean = false
) As T
```


### Parameters
<dl><dt>id</dt><dd>Type: ObjectId<br />The ID of the object.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>


### Return Value
Type: *T*<br />The object with the given ObjectId.
<a href="#DbObjectContainerElementOrDefaultT-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectContainer.md#DbObjectContainer-Class">DbObjectContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

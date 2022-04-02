# DbObjectExtensions.SaveData(*T*) Method 
 

Save an object in the source object's extension dictionary.

## Syntax

**C#**<br />
``` C#
public static void SaveData<T>(
	this DBObject source,
	string key,
	T data
)

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub SaveData(Of T) ( 
	source As DBObject,
	key As String,
	data As T
)
```


### Parameters
<dl><dt>source</dt><dd>Type: DBObject<br />The source object to write the object to.</dd><dt>key</dt><dd>Type: string<br />A string that acts as the key in the extension dictionary.</dd><dt>data</dt><dd>Type: <i>T</i><br />The object to store.</dd></dl>


#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type DBObject. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#DbObjectExtensionsSaveDataT-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectExtensions.md#DbObjectExtensions-Class">DbObjectExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

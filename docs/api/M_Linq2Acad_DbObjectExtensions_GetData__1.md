# DbObjectExtensions.GetData(*T*) Method (DBObject, string)
 

Reads an object from the source object's extension dictionary.

## Syntax

**C#**<br />
``` C#
public static T GetData<T>(
	this DBObject source,
	string key
)

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetData(Of T) ( 
	source As DBObject,
	key As String
) As T
```


### Parameters
<dl><dt>source</dt><dd>Type: DBObject<br />The source object to read the object from.</dd><dt>key</dt><dd>Type: string<br />A string that acts as the key in the extension dictionary.</dd></dl>


### Return Value
Type: *T*<br />The object in the extension dictionary.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type DBObject. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#DbObjectExtensionsGetDataT-Method-DBObject-string">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectExtensions.md#DbObjectExtensions-Class">DbObjectExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

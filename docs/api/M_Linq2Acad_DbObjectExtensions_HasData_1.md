# DbObjectExtensions.HasData Method (DBObject, string, bool)
 

Returns true, if the source object has an entry with the given key in the extension dictionary.

## Syntax

**C#**<br />
``` C#
public static bool HasData(
	this DBObject source,
	string key,
	bool useXData
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function HasData ( 
	source As DBObject,
	key As String,
	useXData As Boolean
) As Boolean
```


### Parameters
<dl><dt>source</dt><dd>Type: DBObject<br />The source object to check.</dd><dt>key</dt><dd>Type: string<br />If parameter *useXData* is true, this string is the name of the RegApp to read the data from. If parameter *useXData* is false, this string acts as the key in the extension dictionary.</dd><dt>useXData</dt><dd>Type: bool<br />True, if data should be read from the source object's XData. False, if data should be read from the source object's extension dictionary.</dd></dl>

### Return Value
Type: bool<br />True, if the extension dictionary contains an entry with the given key.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type DBObject. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#DbObjectExtensionsHasData-Method-DBObject-string-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectExtensions.md#DbObjectExtensions-Class">DbObjectExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

# DbObjectExtensions.HasData Method (DBObject, string)
 

Returns true, if the source object has an entry with the given key in the extension dictionary.

## Syntax

**C#**<br />
``` C#
public static bool HasData(
	this DBObject source,
	string key
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function HasData ( 
	source As DBObject,
	key As String
) As Boolean
```


#### Parameters
<dl><dt>source</dt><dd>Type: DBObject<br />The source object to check.</dd><dt>key</dt><dd>Type: string<br />A string that acts as the key in the extension dictionary.</dd></dl>

#### Return Value
Type: bool<br />True, if the extension dictionary contains an entry with the given key.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type DBObject. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when an AutoCAD error occurs.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_DbObjectExtensions.md">DbObjectExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

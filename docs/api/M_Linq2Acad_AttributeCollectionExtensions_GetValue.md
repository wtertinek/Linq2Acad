# AttributeCollectionExtensions.GetValue Method 
 

Gets the value of the AttributeReference with the given tag.

## Syntax

**C#**<br />
``` C#
public static string GetValue(
	this AttributeCollection attributes,
	string tag
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetValue ( 
	attributes As AttributeCollection,
	tag As String
) As String
```


#### Parameters
<dl><dt>attributes</dt><dd>Type: AttributeCollection<br />The AttributeCollection.</dd><dt>tag</dt><dd>Type: string<br />The tag to look for.</dd></dl>

#### Return Value
Type: <a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type AttributeCollection. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_Linq2Acad_AttributeCollectionExtensions.md">AttributeCollectionExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

# AttributeCollectionExtensions.SetValue Method 
 

Sets the value of the AttributeReference with the given tag.

## Syntax

**C#**<br />
``` C#
public static void SetValue(
	this AttributeCollection attributes,
	string tag,
	string value
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub SetValue ( 
	attributes As AttributeCollection,
	tag As String,
	value As String
)
```


#### Parameters
<dl><dt>attributes</dt><dd>Type: AttributeCollection<br />The AttributeCollection.</dd><dt>tag</dt><dd>Type: string<br />The tag to look for.</dd><dt>value</dt><dd>Type: string<br />The value to set.</dd></dl>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type AttributeCollection. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_Linq2Acad_AttributeCollectionExtensions.md">AttributeCollectionExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

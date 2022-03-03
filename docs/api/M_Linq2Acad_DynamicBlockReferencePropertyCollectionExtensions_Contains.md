# DynamicBlockReferencePropertyCollectionExtensions.Contains Method 
 

Checks the given DynamicBlockReferencePropertyCollection for a DynamicBlockReferenceProperty with the given name.

## Syntax

**C#**<br />
``` C#
public static bool Contains(
	this DynamicBlockReferencePropertyCollection properties,
	string name
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function Contains ( 
	properties As DynamicBlockReferencePropertyCollection,
	name As String
) As Boolean
```


#### Parameters
<dl><dt>properties</dt><dd>Type: DynamicBlockReferencePropertyCollection<br />The DynamicBlockReferencePropertyCollection.</dd><dt>name</dt><dd>Type: string<br />The name to look for.</dd></dl>

#### Return Value
Type: bool<br />True if the DynamicBlockReferencePropertyCollection contains a DynamicBlockReferenceProperty with the given name, otherwise false.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type DynamicBlockReferencePropertyCollection. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="T_Linq2Acad_DynamicBlockReferencePropertyCollectionExtensions.md">DynamicBlockReferencePropertyCollectionExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

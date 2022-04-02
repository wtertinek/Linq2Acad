# PaperSpaceLayoutEnumerableExtensions.Element Method 
 

Provides access to the entities of the layout with the given name.

## Syntax

**C#**<br />
``` C#
public static PaperSpaceEntityContainer Element(
	this IEnumerable<PaperSpaceEntityContainer> source,
	string name
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function Element ( 
	source As IEnumerable(Of PaperSpaceEntityContainer),
	name As String
) As PaperSpaceEntityContainer
```


### Parameters
<dl><dt>source</dt><dd>Type: IEnumerable(<a href="T_Linq2Acad_PaperSpaceEntityContainer.md#PaperSpaceEntityContainer-Class">PaperSpaceEntityContainer</a>)<br />The list of paper space layouts.</dd><dt>name</dt><dd>Type: string<br />The name of the layout.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_PaperSpaceEntityContainer.md#PaperSpaceEntityContainer-Class">PaperSpaceEntityContainer</a><br />An EntityContainer to access the layout's entities.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type IEnumerable(<a href="T_Linq2Acad_PaperSpaceEntityContainer.md#PaperSpaceEntityContainer-Class">PaperSpaceEntityContainer</a>). When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#PaperSpaceLayoutEnumerableExtensionsElement-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_PaperSpaceLayoutEnumerableExtensions.md#PaperSpaceLayoutEnumerableExtensions-Class">PaperSpaceLayoutEnumerableExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

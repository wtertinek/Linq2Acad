# EditorExtensions.WriteLine Method 
 

Displays a message on the AutoCAD text screen.

## Syntax

**C#**<br />
``` C#
public static void WriteLine(
	this Editor editor,
	string formatString,
	params Object[] args
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Sub WriteLine ( 
	editor As Editor,
	formatString As String,
	ParamArray args As Object()
)
```


### Parameters
<dl><dt>editor</dt><dd>Type: Editor<br />The editor instance.</dd><dt>formatString</dt><dd>Type: string<br />A format string to display.</dd><dt>args</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">System.Object</a>[]<br />Arguments to the format string.</dd></dl>

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type Editor. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#EditorExtensionsWriteLine-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_EditorExtensions.md#EditorExtensions-Class">EditorExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

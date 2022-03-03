# EditorExtensions.Getstring Method (Editor, string, Func(string, bool))
 

Gets user input for a string.

## Syntax

**C#**<br />
``` C#
public static PromptResult GetString(
	this Editor editor,
	string message,
	Func<string, bool> validate
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetString ( 
	editor As Editor,
	message As String,
	validate As Func(Of String, Boolean)
) As PromptResult
```


#### Parameters
<dl><dt>editor</dt><dd>Type: Editor<br />The editor instance.</dd><dt>message</dt><dd>Type: string<br />Input message to be displayed to the user during the prompt.</dd><dt>validate</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.func-2" target="_blank" rel="noopener noreferrer">System.Func</a>(<a href="https://docs.microsoft.com/dotnet/api/system.string" target="_blank" rel="noopener noreferrer">String</a>, bool)<br />A function that validates the user input. If it evaluates to true, the PromptResult is returned. Else, the input message is repeatedly displayed.</dd></dl>

#### Return Value
Type: PromptResult<br />Returns the PromptResult.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type Editor. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>message</i> or <i>validate</i> is null.</td></tr></table>

## Remarks
After the user enters an input, the entered value is evaluated by the validation function. If the function evaluates to true, the Prompt result is returned. If the function evaluated to false, the user is again asked for the input.

## See Also


#### Reference
<a href="T_Linq2Acad_EditorExtensions.md">EditorExtensions Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

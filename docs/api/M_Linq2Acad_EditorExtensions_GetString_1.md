# EditorExtensions.Getstring Method (Editor, string, Func(string, bool), string)
 

Gets user input for a string.

## Overview
- [Syntax](#syntax)
- [Remarks](#remarks)


## Syntax

**C#**<br />
``` C#
public static PromptResult GetString(
	this Editor editor,
	string message,
	Func<string, bool> validate,
	string errorMessage
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetString ( 
	editor As Editor,
	message As String,
	validate As Func(Of String, Boolean),
	errorMessage As String
) As PromptResult
```


### Parameters
<dl><dt>editor</dt><dd>Type: Editor<br />The editor instance.</dd><dt>message</dt><dd>Type: string<br />Input message to be displayed to the user during the prompt.</dd><dt>validate</dt><dd>Type: <a href="https://docs.microsoft.com/dotnet/api/system.func-2" target="_blank" rel="noopener noreferrer">System.Func</a>(string, bool)<br />A function that validates the user input. If it evaluates to true, the PromptResult is returned. Else, the input message is repeatedly displayed.</dd><dt>errorMessage</dt><dd>Type: string<br />An error message that is displayed, if the validation function evaluates to false.</dd></dl>

### Return Value
Type: PromptResult<br />Returns the PromptResult.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type Editor. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="https://docs.microsoft.com/dotnet/visual-basic/programming-guide/language-features/procedures/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (Visual Basic)</a> or <a href="https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank" rel="noopener noreferrer">Extension Methods (C# Programming Guide)</a>.
<a href="#EditorExtensionsGetstring-Method-Editor-string-Funcstring-bool-string">Back to Top</a>

## Remarks
After the user enters an input, the entered value is evaluated by the validation function. If the function evaluates to true, the Prompt result is returned. If the function evaluated to false, the user is again asked for the input.
<a href="#EditorExtensionsGetstring-Method-Editor-string-Funcstring-bool-string">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_EditorExtensions.md#EditorExtensions-Class">EditorExtensions Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

# XRefContainer.Overlay Method 
 

Overlays the XRef at the given file location.

## Syntax

**C#**<br />
``` C#
public XRef Overlay(
	string fileName,
	string blockName = null
)
```

**VB**<br />
``` VB
Public Function Overlay ( 
	fileName As String,
	Optional blockName As String = Nothing
) As XRef
```


#### Parameters
<dl><dt>fileName</dt><dd>Type: string<br />The file name of the XRef.</dd><dt>blockName (Optional)</dt><dd>Type: string<br />The XRef's block name. If not specified, the file name is used as the XRef's block name.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_XRef.md">XRef</a><br />A new instance of XRef.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>file name</i> is null.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_XRefContainer.md">XRefContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

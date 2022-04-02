# XRefContainer.Attach Method 
 

Attaches the XRef at the given file location.

## Syntax

**C#**<br />
``` C#
public XRef Attach(
	string fileName,
	string blockName = null
)
```

**VB**<br />
``` VB
Public Function Attach ( 
	fileName As String,
	Optional blockName As String = Nothing
) As XRef
```


### Parameters
<dl><dt>fileName</dt><dd>Type: string<br />The file name of the XRef.</dd><dt>blockName (Optional)</dt><dd>Type: string<br />The XRef's block name. If not specified, the file name is used as the XRef's block name.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_XRef.md#XRef-Class">XRef</a><br />A new instance of XRef.
<a href="#XRefContainerAttach-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_XRefContainer.md#XRefContainer-Class">XRefContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

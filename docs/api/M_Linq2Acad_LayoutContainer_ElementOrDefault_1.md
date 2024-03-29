# LayoutContainer.ElementOrDefault Method (string, bool)
 

Returns the Layout with the specified name or <i>null</i> if the Layout cannot be found.

## Syntax

**C#**<br />
``` C#
public Layout ElementOrDefault(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	name As String,
	Optional openForWrite As Boolean = false
) As Layout
```


### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the Layout.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: Layout<br />The Layout with the specified name.
<a href="#LayoutContainerElementOrDefault-Method-string-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_LayoutContainer.md#LayoutContainer-Class">LayoutContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

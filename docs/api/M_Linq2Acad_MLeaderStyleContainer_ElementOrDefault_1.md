# MLeaderStyleContainer.ElementOrDefault Method (string, bool)
 

Returns the MLeaderStyle with the specified name or <i>null</i> if the MLeaderStyle cannot be found.

## Syntax

**C#**<br />
``` C#
public MLeaderStyle ElementOrDefault(
	string name,
	bool openForWrite = false
)
```

**VB**<br />
``` VB
Public Function ElementOrDefault ( 
	name As String,
	Optional openForWrite As Boolean = false
) As MLeaderStyle
```


### Parameters
<dl><dt>name</dt><dd>Type: string<br />The name of the MLeaderStyle.</dd><dt>openForWrite (Optional)</dt><dd>Type: bool<br />True, if the object should be opened for-write. By default the object is opened readonly.</dd></dl>

### Return Value
Type: MLeaderStyle<br />The MLeaderStyle with the specified name.
<a href="#MLeaderStyleContainerElementOrDefault-Method-string-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_MLeaderStyleContainer.md#MLeaderStyleContainer-Class">MLeaderStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

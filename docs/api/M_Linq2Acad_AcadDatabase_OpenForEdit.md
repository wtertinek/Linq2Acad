# AcadDatabase.OpenForEdit Method 
 

Provides read/write access to the drawing database in the given file.

## Syntax

**C#**<br />
``` C#
public static AcadDatabase OpenForEdit(
	string fileName,
	OpenForEditOptions options = null
)
```

**VB**<br />
``` VB
Public Shared Function OpenForEdit ( 
	fileName As String,
	Optional options As OpenForEditOptions = Nothing
) As AcadDatabase
```


#### Parameters
<dl><dt>fileName</dt><dd>Type: string<br />The name of the drawing database to open.</dd><dt>options (Optional)</dt><dd>Type: <a href="T_Linq2Acad_OpenForEditOptions.md">Linq2Acad.OpenForEditOptions</a><br />Options for opening and closing the database.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_AcadDatabase.md">AcadDatabase</a><br />The AcadDatabase instance.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>fileName</i> is null.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.io.filenotfoundexception" target="_blank" rel="noopener noreferrer">FileNotFoundException</a></td><td>Thrown when the file cannot be found.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when opening the drawing database throws an exception.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_AcadDatabase.md">AcadDatabase Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

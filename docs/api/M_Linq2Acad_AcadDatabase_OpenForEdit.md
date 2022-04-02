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


### Parameters
<dl><dt>fileName</dt><dd>Type: string<br />The name of the drawing database to open.</dd><dt>options (Optional)</dt><dd>Type: <a href="T_Linq2Acad_OpenForEditOptions.md#OpenForEditOptions-Class">OpenForEditOptions</a><br />Options for opening and closing the database.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class">AcadDatabase</a><br />The AcadDatabase instance.
<a href="#AcadDatabaseOpenForEdit-Method">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class">AcadDatabase Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

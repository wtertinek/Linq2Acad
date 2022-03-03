# AcadDatabase.Use Method (Database)
 

Provides access to the given drawing database.

## Syntax

**C#**<br />
``` C#
public static AcadDatabase Use(Database database)
```

**VB**<br />
``` VB
Public Shared Function Use (database As Database) As AcadDatabase
```


#### Parameters
<dl><dt>database</dt><dd>Type: Database<br />The drawing database to use.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_AcadDatabase.md">AcadDatabase</a><br />The AcadDatabase instance.

## Exceptions
&nbsp;<table><tr><th>Exception</th><th>Condition</th></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.argumentnullexception" target="_blank" rel="noopener noreferrer">ArgumentNullException</a></td><td>Thrown when parameter <i>database</i> is null.</td></tr><tr><td><a href="https://docs.microsoft.com/dotnet/api/system.exception" target="_blank" rel="noopener noreferrer">Exception</a></td><td>Thrown when the database is invalid.</td></tr></table>

## See Also


#### Reference
<a href="T_Linq2Acad_AcadDatabase.md">AcadDatabase Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

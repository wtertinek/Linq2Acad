# XRefContainer.Import Method (XRefTableRecord, bool)
 

Imports the specified XRefTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<XRefTableRecord> Import(
	XRefTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As XRefTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of XRefTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: XRefTableRecord<br />The XRefTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported XRefTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(XRefTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_XRefContainer.md">XRefContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

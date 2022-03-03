# XRefContainer.Import Method (IEnumerable(XRefTableRecord), bool)
 

Imports the specified XRefTableRecords into the current database.

## Syntax

**C#**<br />
``` C#
public IReadOnlyCollection<ImportResult<XRefTableRecord>> Import(
	IEnumerable<XRefTableRecord> elements,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	elements As IEnumerable(Of XRefTableRecord),
	Optional replaceIfDuplicate As Boolean = false
) As IReadOnlyCollection(Of ImportResult(Of XRefTableRecord))
```


#### Parameters
<dl><dt>elements</dt><dd>Type: IEnumerable(XRefTableRecord)<br />The elements to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported XRefTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: IReadOnlyCollection(<a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(XRefTableRecord))<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_XRefContainer.md">XRefContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

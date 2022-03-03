# BlockContainer.Import Method (BlockTableRecord, bool)
 

Imports the specified BlockTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<BlockTableRecord> Import(
	BlockTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As BlockTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of BlockTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: BlockTableRecord<br />The BlockTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported BlockTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(BlockTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md">BlockContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

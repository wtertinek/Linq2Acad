# DimStyleContainer.Import Method (DimStyleTableRecord, bool)
 

Imports the specified DimStyleTableRecord into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<DimStyleTableRecord> Import(
	DimStyleTableRecord element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As DimStyleTableRecord,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of DimStyleTableRecord)
```


#### Parameters
<dl><dt>element</dt><dd>Type: DimStyleTableRecord<br />The DimStyleTableRecord to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported DimStyleTableRecord should be replaced if it is already present; otherwise, false.</dd></dl>

#### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md">ImportResult</a>(DimStyleTableRecord)<br />An object that represents the result of an import operation.

## See Also


#### Reference
<a href="T_Linq2Acad_DimStyleContainer.md">DimStyleContainer Class</a><br /><a href="N_Linq2Acad.md">Linq2Acad Namespace</a><br />

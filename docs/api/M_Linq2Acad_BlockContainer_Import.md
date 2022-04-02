# BlockContainer.Import Method (string, string)
 

Creates a new block and imports all model space entities from the given drawing file to it.

## Syntax

**C#**<br />
``` C#
public BlockTableRecord Import(
	string newBlockName,
	string fileName
)
```

**VB**<br />
``` VB
Public Function Import ( 
	newBlockName As String,
	fileName As String
) As BlockTableRecord
```


### Parameters
<dl><dt>newBlockName</dt><dd>Type: string<br />The name of the new BlockTableRecord.</dd><dt>fileName</dt><dd>Type: string<br />The name of the drawing file that should be imported.</dd></dl>

### Return Value
Type: BlockTableRecord<br />A new instance of BlockTableRecord.
<a href="#BlockContainerImport-Method-string-string">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_BlockContainer.md#BlockContainer-Class">BlockContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

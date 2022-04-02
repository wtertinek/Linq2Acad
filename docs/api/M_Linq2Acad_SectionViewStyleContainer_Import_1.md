# SectionViewStyleContainer.Import Method (SectionViewStyle, bool)
 

Imports the specified SectionViewStyle into the current database.

## Syntax

**C#**<br />
``` C#
public ImportResult<SectionViewStyle> Import(
	SectionViewStyle element,
	bool replaceIfDuplicate = false
)
```

**VB**<br />
``` VB
Public Function Import ( 
	element As SectionViewStyle,
	Optional replaceIfDuplicate As Boolean = false
) As ImportResult(Of SectionViewStyle)
```


### Parameters
<dl><dt>element</dt><dd>Type: SectionViewStyle<br />The SectionViewStyle to import.</dd><dt>replaceIfDuplicate (Optional)</dt><dd>Type: bool<br />true, if the the imported SectionViewStyle should be replaced if it is already present; otherwise, false.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_ImportResult_1.md#ImportResultT-Class">ImportResult</a>(SectionViewStyle)<br />An object that represents the result of an import operation.
<a href="#SectionViewStyleContainerImport-Method-SectionViewStyle-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_SectionViewStyleContainer.md#SectionViewStyleContainer-Class">SectionViewStyleContainer Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

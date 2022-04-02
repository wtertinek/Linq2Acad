# AcadDatabase.Active Method (Transaction, bool, bool)
 

Provides access to the drawing database of the active document. This is an advanced feature, use with caution.

## Syntax

**C#**<br />
``` C#
public static AcadDatabase Active(
	Transaction transaction,
	bool commitTransaction,
	bool disposeTransaction
)
```

**VB**<br />
``` VB
Public Shared Function Active ( 
	transaction As Transaction,
	commitTransaction As Boolean,
	disposeTransaction As Boolean
) As AcadDatabase
```


### Parameters
<dl><dt>transaction</dt><dd>Type: Transaction<br />The transaction to use.</dd><dt>commitTransaction</dt><dd>Type: bool<br />True, if the transaction in use should be committed when this instance is disposed of.</dd><dt>disposeTransaction</dt><dd>Type: bool<br />True, if the transaction in use should be disposed of when this instance is disposed of.</dd></dl>

### Return Value
Type: <a href="T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class">AcadDatabase</a><br />The AcadDatabase instance.
<a href="#AcadDatabaseActive-Method-Transaction-bool-bool">Back to Top</a>

## See Also


#### Reference
<a href="T_Linq2Acad_AcadDatabase.md#AcadDatabase-Class">AcadDatabase Class</a><br /><a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

# AcadDatabase Class
 

This class manages the life cycles of the database and the transaction.


## Overview
- [Inheritance](#inheritance)
- [Methods](#methods)
- [Static Methods](#static-methods)


## Inheritance
<table><tr><td><strong>Derived</strong></td><td><a href="T_Linq2Acad_AcadDataModel.md#AcadDataModel-Class">AcadDataModel</a></td></tr>
</table>

## Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_Abort.md#AcadDatabaseAbort-Method">Abort()</a></td><td>
Immediately discards all changes and aborts the underlying transaction. The session is no longer usable after calling this method.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_Dispose.md#AcadDatabaseDispose-Method">Dispose()</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr></table>
<a href="#AcadDatabase-Class">Back to Top</a>

## Static Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Active.md#AcadDatabaseActive-Method">Active()</a></td><td>
Provides access to the drawing database of the active document.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Create.md#AcadDatabaseCreate-Method">Create([CreateOptions])</a></td><td>
Provides access to a newly created drawing database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenForEdit.md#AcadDatabaseOpenForEdit-Method">OpenForEdit(string, [OpenForEditOptions])</a></td><td>
Provides read/write access to the drawing database in the given file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenReadOnly.md#AcadDatabaseOpenReadOnly-Method">OpenReadOnly(string, [OpenReadOnlyOptions])</a></td><td>
Provides read-only access to the drawing database in the given file.</td></tr></table>
<a href="#AcadDatabase-Class">Back to Top</a>

## See Also


#### Reference
<a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

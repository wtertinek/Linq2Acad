# AcadDatabase Class
 

The main class that provides access to the drawing database.


## Overview
- [Properties](#properties)
- [Methods](#methods)
- [Static Methods](#static-methods)


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Blocks.md#AcadDatabaseBlocks-Property">Blocks</a></td><td>
Provides access to the elements of the Block table and methods to create, add and import BlockTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_CurrentSpace.md#AcadDatabaseCurrentSpace-Property">CurrentSpace</a></td><td>
Provides access to the entities of the currently active space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Database.md#AcadDatabaseDatabase-Property">Database</a></td><td>
The drawing database in use.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_DbObjects.md#AcadDatabaseDbObjects-Property">DbObjects</a></td><td>
Provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Groups.md#AcadDatabaseGroups-Property">Groups</a></td><td>
Provides access to the elements of the Group dictionary and methods to create, add and import Groups.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Layers.md#AcadDatabaseLayers-Property">Layers</a></td><td>
Provides access to the elements of the Layer table and methods to create, add and import LayerTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Layouts.md#AcadDatabaseLayouts-Property">Layouts</a></td><td>
Provides access to the elements of the Layout dictionary and methods to create, add and import Layouts.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Linetypes.md#AcadDatabaseLinetypes-Property">Linetypes</a></td><td>
Provides access to the elements of the Linetype table and methods to create, add and import LinetypeTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Materials.md#AcadDatabaseMaterials-Property">Materials</a></td><td>
Provides access to the elements of the Material dictionary and methods to create, add and import Materials.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_ModelSpace.md#AcadDatabaseModelSpace-Property">ModelSpace</a></td><td>
Provides access to the entities of the model space. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_PaperSpace.md#AcadDatabasePaperSpace-Property">PaperSpace</a></td><td>
Provides access to the entities of the paper space layouts.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_PlotSettings.md#AcadDatabasePlotSettings-Property">PlotSettings</a></td><td>
Provides access to the elements of the PlotSettings dictionary and methods to create, add and import PlotSettings.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_RegApps.md#AcadDatabaseRegApps-Property">RegApps</a></td><td>
Provides access to the elements of the RegApp table and methods to create, add and import RegAppTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Styles.md#AcadDatabaseStyles-Property">Styles</a></td><td>
Provides access to all style related tables and dictionaries.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_SummaryInfo.md#AcadDatabaseSummaryInfo-Property">SummaryInfo</a></td><td>
Provies access to the summary info.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Ucss.md#AcadDatabaseUcss-Property">Ucss</a></td><td>
Provides access to the elements of the Ucs table and methods to create, add and import UcsTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Viewports.md#AcadDatabaseViewports-Property">Viewports</a></td><td>
Provides access to the elements of the Viewport table and methods to create, add and import ViewportTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_Views.md#AcadDatabaseViews-Property">Views</a></td><td>
Provides access to the elements of the View table and methods to create, add and import ViewTableRecords.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_Linq2Acad_AcadDatabase_XRefs.md#AcadDatabaseXRefs-Property">XRefs</a></td><td>
Provides access to all XRef elements and methods to attach, overlay, resolve, reload and unload XRefs.</td></tr></table>&nbsp;
<a href="#AcadDatabase-Class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_Abort.md#AcadDatabaseAbort-Method">Abort()</a></td><td>
Immediately discards all changes and aborts the underlying transaction. The session is no longer usable after calling this method.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_Linq2Acad_AcadDatabase_Dispose.md#AcadDatabaseDispose-Method">Dispose()</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr></table>
<a href="#AcadDatabase-Class">Back to Top</a>

## Static Methods
&nbsp;<table><tr><th></th><th>Class</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Active.md#AcadDatabaseActive-Method">Active()</a></td><td>
Provides access to the drawing database of the active document.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Active_1.md#AcadDatabaseActive-Method-Transaction-bool-bool">Active(Transaction, bool, bool)</a></td><td>
Provides access to the drawing database of the active document. This is an advanced feature, use with caution.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Create.md#AcadDatabaseCreate-Method">Create([CreateOptions])</a></td><td>
Provides access to a newly created drawing database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenForEdit.md#AcadDatabaseOpenForEdit-Method">OpenForEdit(string, [OpenForEditOptions])</a></td><td>
Provides read/write access to the drawing database in the given file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_OpenReadOnly.md#AcadDatabaseOpenReadOnly-Method">OpenReadOnly(string, [OpenReadOnlyOptions])</a></td><td>
Provides read-only access to the drawing database in the given file.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Use.md#AcadDatabaseUse-Method-Database">Use(Database)</a></td><td>
Provides access to the given drawing database.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="M_Linq2Acad_AcadDatabase_Use_1.md#AcadDatabaseUse-Method-Database-Transaction-bool-bool">Use(Database, Transaction, bool, bool)</a></td><td>
Provides access to the given drawing database. This is an advanced feature, use with caution.</td></tr></table>
<a href="#AcadDatabase-Class">Back to Top</a>

## See Also


#### Reference
<a href="N_Linq2Acad.md#Linq2Acad-Namespace">Linq2Acad Namespace</a><br />

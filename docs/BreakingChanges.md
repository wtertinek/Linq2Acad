Beginning with the Nuget release of version 1.0.0, Linq2Acad is released in accordance with the rules of [semantic versioning](https://semver.org). Previous to that, there have been breaking changes to the API that were not reflected in the API's version number. The changes in question (see the commit messages for more information):

* AcadDatabase.DiscardChanges replaced by AcadDatabase.Abort (263dc61)
* XRef's property setters changed to methodes, XRefInfo class integrated into XRef class (f8c6a54)
* XRefExtensions removed (5bb5340)
* Style related access properties grouped together into one Style property (c3c72f7)
* EntityContainer.Add(IEnumerable<Entity>) renamed to EntityContainer.AddRange(IEnumerable<Entity>) (1a80da8)
* PaperSpace(int) and PaperSpace(string) methods removed (4948842)
* Extension method renamed from CleanValues to ClearValues (5f4b666)
* Element-methods and AddNewlyCreatedDbObject moved to a dedicated class called DbObjects (f48f74e)
* AcadDatabase.Open refactored to AcadDatabase.OpenReadOnly and AcadDatabase.OpenForEdit, signature of AcadDatabase.Create changed (9b7d547)
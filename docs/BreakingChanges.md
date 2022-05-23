Beginning with the Nuget release of version 1.0.0, Linq2Acad is released in accordance with the rules of [semantic versioning](https://semver.org). Previous to that, there have been breaking changes to the API that were not reflected in the API's version number. The changes in question are (see the commit messages for more information):

* AcadDatabase.DiscardChanges replaced by AcadDatabase.Abort ([263dc61](https://github.com/wtertinek/Linq2Acad/commit/263dc6170167dbd97999c977e4b13f3cba1c0bf9))
* XRef's property setters changed to methodes, XRefInfo class integrated into XRef class ([f8c6a54](https://github.com/wtertinek/Linq2Acad/commit/f8c6a54b2d772f116ea6caf98d4688cb4ae9fdce))
* XRefExtensions removed ([5bb5340](https://github.com/wtertinek/Linq2Acad/commit/5bb53400cfb7787062ff72523bff57b37e0b1d79))
* Style related access properties grouped together into one Style property ([c3c72f7](https://github.com/wtertinek/Linq2Acad/commit/c3c72f7588ffd1b7b3ee22bc44c6a1ad1eb89dcc))
* EntityContainer.Add(IEnumerable<Entity>) renamed to EntityContainer.AddRange(IEnumerable<Entity>) ([1a80da8](https://github.com/wtertinek/Linq2Acad/commit/1a80da801c83de898a01b01f719a487bd47cbc9f))
* PaperSpace(int) and PaperSpace(string) methods removed ([4948842](https://github.com/wtertinek/Linq2Acad/commit/494884238adb4a4521581d7f36dd267ceded7cdf))
* Extension method renamed from CleanValues to ClearValues ([5f4b666](https://github.com/wtertinek/Linq2Acad/commit/5f4b6668a3cbec67c644410d0178255d244c8028))
* Element-methods and AddNewlyCreatedDbObject moved to a dedicated class called DbObjects ([f48f74e](https://github.com/wtertinek/Linq2Acad/commit/f48f74ede2554ef2c968fe52b03e1ec1f0d956ff))
* AcadDatabase.Open refactored to AcadDatabase.OpenReadOnly and AcadDatabase.OpenForEdit, signature of AcadDatabase.Create changed ([9b7d547](https://github.com/wtertinek/Linq2Acad/commit/9b7d547ba8ebab4d1b9d3eedb469b5fcb66d77af))
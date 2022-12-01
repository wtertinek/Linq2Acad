### Contributing

Please:

- open an [issue](https://github.com/wtertinek/Linq2Acad/issues) if you've found a bug,
- a [discussion](https://github.com/wtertinek/Linq2Acad/discussions/new) for a feature request, and then, 
- a pull request if you want to help extending the library.


There are 9 separate VS Projects associated with this repository - one for every version of AutoCAD from 2015 to 2023. But the associated source files are all the same. Consequently, all source files must be contained in the `.\Linq2Acad\src\Sources` directory, and must subsequently be linked to each project.

The best way to do this is to Open the (.csproj) in a text editor and to add the following lines (amend the file names and paths):

```xml
<Compile Include="..\..\Sources\Linq2Acad\Extensions\GroupExtensions.cs">
  <Link>Extensions\GroupExtensions.cs</Link>
</Compile>
```

For those with graphic design abilities: it would be nice to have a Linq2Acad logo.
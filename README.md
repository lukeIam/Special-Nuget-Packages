# SpecialNugetPackages 
## lukeIam.nuget.copy

Copies files and folders to $(TargetDir) before every build process.
Files are choosen depending on the target platform.

- 'x86' folder content is copied when $(Platform)==x86
- 'x64'  folder content is copied when $(Platform)==x64
- 'Any CPU' folder content is copied when $(Platform)=='Any CPU'
- 'All' folder content is copied always

**Usage:**

- Copy the package 
- Edit the metadata 
- Rename `build\lukeIam.nuget.copy.targets` to `build\[NewId].targets`
- Copy your files to the folders (`x86`, `x64`, `Any CPU`, `All`)

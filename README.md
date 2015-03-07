# SpecialNugetPackages
Tip for editing: [NuGet Package Explorer](https://npe.codeplex.com/releases)
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

## lukeIam.nuget.downloadAndExtract

Checks if some files exits in a directory - if at least one file is missing a zip file will be downloaded and extracted to that folder.

**Usage:**

- Copy the package 
- Edit the metadata 
- Edit `build\lukeIam.nuget.copy.targets`:
  * ZipAdress: The url of the zip file
  * TargetFolder: TargetFolder for the new files
  * FilesToCheck: Files that should be checked before downloading (separated by `;`)
- Rename `build\lukeIam.nuget.copy.targets` to `build\[NewId].targets`

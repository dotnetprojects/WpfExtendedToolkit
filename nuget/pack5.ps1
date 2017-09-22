$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Write-Host "root: $root"

$version = [System.Reflection.Assembly]::LoadFile("$root\Src\Xceed.Wpf.AvalonDock.Themes.VS2010\bin\Release\DotNetProjects.Wpf.AvalonDock.Themes.VS2010.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\NuGet\Avalondock.Themes.VS2010.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\nuget\Avalondock.Themes.VS2010.compiled.nuspec

& $root\NuGet\NuGet.exe pack $root\nuget\Avalondock.Themes.VS2010.compiled.nuspec
$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Write-Host "root: $root"

$version = [System.Reflection.Assembly]::LoadFile("$root\Src\Xceed.Wpf.AvalonDock\bin\Release\DotNetProjects.Wpf.AvalonDock.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\NuGet\Avalondock.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\nuget\Avalondock.compiled.nuspec

& $root\NuGet\NuGet.exe pack $root\nuget\Avalondock.compiled.nuspec
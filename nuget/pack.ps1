$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Write-Host "root: $root"

$version = [System.Reflection.Assembly]::LoadFile("$root\Src\Xceed.Wpf.Toolkit\bin\Release\DotNetProjects.Wpf.Extended.Toolkit.dll").GetName().Version
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\NuGet\Toolkit.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\nuget\Toolkit.compiled.nuspec

& $root\NuGet\NuGet.exe pack $root\nuget\Toolkit.compiled.nuspec
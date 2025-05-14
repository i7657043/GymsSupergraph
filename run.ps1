if (-not (npm list -g wgc --depth=0 2>$null)) {
    npm install -g wgc@latest
    Write-Host "Installed wgc globally."
}
else {
    Write-Host "wgc is already installed globally."
}

if (!(Test-Path -Path "apis_temp")) { 
    New-Item -ItemType Directory -Path "apis_temp" 
}

$dir = "Apis/ManagersApi/ManagersApi.csproj"
$target = "exec_temp/managers"
dotnet restore $dir
dotnet build $dir -c Release -o $target/build
dotnet publish $dir -c Release -o $target/pub /p:UseAppHost=false
Start-Process -FilePath "dotnet" -ArgumentList "$target/pub/ManagersApi.dll --urls http://localhost:4002"

$dir = "Apis/GymApi/GymApi.csproj"
$target = "exec_temp/gyms"
dotnet restore $dir
dotnet build $dir -c Release -o $target/build
dotnet publish $dir -c Release -o $target/pub /p:UseAppHost=false
Start-Process -FilePath "dotnet" -ArgumentList "$target/pub/GymApi.dll --urls http://localhost:4001"

wgc router compose --input router/router.yaml --out router/router.json
Start-Process -FilePath "bin\router.exe" -WorkingDirectory "router"
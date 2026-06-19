# Run Waterworks MVP

## Backend API

```powershell
cd "D:\Vibe Coding\BTL KTLTUD\BTL_CongtrinhThuyLoi"
.\.dotnet-local\dotnet.exe build src\Waterworks.Api\Waterworks.Api.csproj
.\.dotnet-local\dotnet.exe run --project src\Waterworks.Api\Waterworks.Api.csproj --urls http://127.0.0.1:5178
```

Smoke test:

```powershell
Invoke-RestMethod http://127.0.0.1:5178/api/health
Invoke-RestMethod http://127.0.0.1:5178/api/congtrinh/
Invoke-RestMethod http://127.0.0.1:5178/api/auth/login -Method Post -ContentType "application/json" -Body '{"userName":"admin","password":"1234"}'
```

The API creates `waterworks.local.db` automatically on first run.

## Web client

Run the web client after the API is running:

```powershell
cd "D:\Vibe Coding\BTL KTLTUD\BTL_CongtrinhThuyLoi"
.\.dotnet-local\dotnet.exe build src\Waterworks.Web\Waterworks.Web.csproj
.\.dotnet-local\dotnet.exe run --project src\Waterworks.Web\Waterworks.Web.csproj --urls http://127.0.0.1:5179
```

Open:

```text
http://127.0.0.1:5179
```

Default login:

```text
admin / 1234
```

## Public preview tunnel

For a quick public preview without buying a domain, publish the web into the API and expose the single API port:

```powershell
cd "D:\Vibe Coding\BTL KTLTUD\BTL_CongtrinhThuyLoi"

.\.dotnet-local\dotnet.exe publish src\Waterworks.Web\Waterworks.Web.csproj -c Release -o artifacts\web-publish
Remove-Item src\Waterworks.Api\wwwroot -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force -Path src\Waterworks.Api\wwwroot
Copy-Item artifacts\web-publish\wwwroot\* src\Waterworks.Api\wwwroot -Recurse -Force

.\.dotnet-local\dotnet.exe run --project src\Waterworks.Api\Waterworks.Api.csproj --urls http://127.0.0.1:5188
```

In another terminal:

```powershell
.\.tools\cloudflared.exe tunnel --no-autoupdate --url http://127.0.0.1:5188
```

Cloudflare will print a temporary `https://*.trycloudflare.com` URL. This URL works while the PC and tunnel process are running.

## MAUI client

This repo is pinned to .NET SDK 9.0.314 in `global.json` because the projects target `net9.0`.

This machine currently has Android/iOS workloads but not the MAUI workload. Install it first from **Visual Studio Installer**:

1. Open Visual Studio Installer.
2. Modify Visual Studio Community 2022.
3. Check `.NET Multi-platform App UI development`.
4. Include Android SDK/emulator tools if you want to run Android.
5. Apply changes.

After that, verify:

```powershell
dotnet workload list
dotnet new list maui
```

Expected workloads should include at least `maui`, `maui-android`, or `maui-windows`.

This repo also has a non-admin portable SDK fallback in `.dotnet-local`. Use it when the machine-level SDK still cannot see MAUI:

```powershell
.\.dotnet-local\dotnet.exe workload list
.\.dotnet-local\dotnet.exe build src\Waterworks.Maui\Waterworks.Maui.csproj -f net9.0-windows10.0.19041.0
.\.dotnet-local\dotnet.exe build src\Waterworks.Maui\Waterworks.Maui.csproj -f net9.0-android /p:AndroidSdkDirectory="$PWD\.android-sdk" /p:JavaSdkDirectory="$PWD\.jdk17\jdk-17.0.19+10"
```

If running from an elevated/admin terminal works on your machine, this is the CLI equivalent:

```powershell
dotnet workload install maui
```

Build Windows target:

```powershell
dotnet build src\Waterworks.Maui\Waterworks.Maui.csproj -f net9.0-windows10.0.19041.0
```

Build Android target:

```powershell
dotnet build src\Waterworks.Maui\Waterworks.Maui.csproj -f net9.0-android
```

The MAUI app calls `http://127.0.0.1:5178` on Windows and `http://10.0.2.2:5178` on Android emulator.

For Android physical devices, run the API on LAN instead:

```powershell
dotnet run --project src\Waterworks.Api\Waterworks.Api.csproj --urls http://0.0.0.0:5178
```

Then update `ApiClient.DefaultBaseAddress` to your computer LAN IP, for example `http://192.168.1.10:5178`.

iOS and Mac Catalyst require building on a Mac with Xcode, or Visual Studio Pair to Mac.

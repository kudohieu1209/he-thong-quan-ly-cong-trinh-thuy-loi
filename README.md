# He thong quan ly cong trinh thuy loi

Ban remake theo kien truc:

- `Waterworks.Web`: Blazor WebAssembly client cho trinh duyet.
- `Waterworks.Api`: ASP.NET Core Web API.
- `Waterworks.Maui`: .NET MAUI client cho desktop/mobile.
- `Waterworks.Shared`: DTO/request/response dung chung.
- `Waterworks.Data`: data access MVP hien dung SQLite local.

## MVP hien co

- Dang nhap demo: `admin` / `1234`.
- Home/menu.
- Danh sach cong trinh.
- Them, sua, xoa cong trinh.
- API CRUD cong trinh.
- Docker deploy Web + API chung mot service.

## Chay local

```powershell
.\.dotnet-local\dotnet.exe run --project src\Waterworks.Api\Waterworks.Api.csproj --urls http://127.0.0.1:5188
```

Mo:

```text
http://127.0.0.1:5188
```

## Deploy

Repo co san `Dockerfile` va `render.yaml`. Xem chi tiet trong `docs/DEPLOY.md`.

## Ghi chu

`database/Database.sql` duoc giu lai lam schema tham khao khi migrate sang PostgreSQL/Neon/Supabase hoac SQL Server.

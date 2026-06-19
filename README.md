# Hệ thống quản lý công trình thủy lợi

Đây là bản remake theo kiến trúc client-server, tách ứng dụng khỏi việc kết nối trực tiếp đến cơ sở dữ liệu.

## Kiến trúc

- `Waterworks.Web`: ứng dụng web Blazor WebAssembly chạy trên trình duyệt.
- `Waterworks.Api`: ASP.NET Core Web API, xử lý đăng nhập và dữ liệu nghiệp vụ.
- `Waterworks.Maui`: ứng dụng .NET MAUI cho desktop/mobile.
- `Waterworks.Shared`: DTO, request, response dùng chung giữa client và API.
- `Waterworks.Data`: lớp truy cập dữ liệu. MVP hiện dùng SQLite local.

## Chức năng MVP

- Đăng nhập demo: `admin` / `1234`.
- Trang Home/Menu.
- Danh sách công trình.
- Thêm, sửa, xóa công trình.
- API CRUD công trình.
- Docker deploy Web + API chung một service.

## Chạy local

```powershell
.\.dotnet-local\dotnet.exe run --project src\Waterworks.Api\Waterworks.Api.csproj --urls http://127.0.0.1:5188
```

Mở trình duyệt tại:

```text
http://127.0.0.1:5188
```

## Deploy

Repo có sẵn `Dockerfile` và `render.yaml`. Xem chi tiết trong `docs/DEPLOY.md`.

## Ghi chú

`database/Database.sql` được giữ lại làm schema tham khảo khi migrate sang PostgreSQL/Neon/Supabase hoặc SQL Server.

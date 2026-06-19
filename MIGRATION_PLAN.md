# Waterworks MAUI + API Migration Plan

## Hien trang da inspect

- App cu la WPF .NET Framework 4.7.2 trong `WinApp`, solution cu la `WinApp.sln`.
- Database cu la SQL Server Express `KTPM`, connection truc tiep qua `WinApp/Models/Migration/Provider.cs`.
- Schema goc nam trong `Database.sql`.
- Menu/quyen nam trong `WinApp/App_Data/Actions.json`.
- Controller WPF nam trong `WinApp/Controller`, UI nam trong `WinApp/Views`, framework tu viet nam trong `WinApp/VST/MVC`.

## Huong migrate

1. Giu nguyen `WinApp` va `WinApp.sln` de tham chieu va rollback.
2. Tao solution moi `Waterworks.slnx` canh solution cu.
3. Tach client MAUI khoi database: client chi goi HTTPS API.
4. Dua schema/entity/DTO dung chung vao `Waterworks.Shared`.
5. Dua truy cap database vao `Waterworks.Data`.
6. Tao `Waterworks.Api` lam backend ASP.NET Core.
7. Tao `Waterworks.Maui` lam client moi, khong auto-convert WPF XAML.

## Cau truc solution moi

- `src/Waterworks.Shared`: DTO/request/response dung chung giua API va MAUI.
- `src/Waterworks.Data`: repository va database provider.
- `src/Waterworks.Api`: ASP.NET Core Web API.
- `src/Waterworks.Maui`: MAUI app scaffold cho login/menu/cong trinh.

## MVP dang lam

- Login don gian bang bang `TaiKhoan`.
- Home/menu tu y tuong `Actions.json`.
- Danh sach cong trinh.
- Them/sua/xoa cong trinh.
- API CRUD `/api/congtrinh`.
- API login `/api/auth/login`.
- Local database SQLite de chay free ngay. Sau MVP co the doi sang PostgreSQL/Neon, Supabase, hoac SQL Server.

## Mo rong sau MVP

- Tai khoan va phan quyen day du.
- Hanh chinh/don vi.
- Loai/cap cong trinh.
- Ket qua tuoi tieu.
- Bao tri/nhat ky/van ban/tai lieu.
- Thong ke va ban do.

## Ghi chu database

SQLite duoc dung cho buoc dau vi khong can cai SQL Server/PostgreSQL. `Database.sql` van la schema tham chieu. Khi deploy cloud, nen uu tien:

- PostgreSQL tren Neon/Supabase free tier neu can cloud nhanh, chi phi thap.
- SQL Server neu muon giu gan schema T-SQL cu va da co moi truong SQL Server.

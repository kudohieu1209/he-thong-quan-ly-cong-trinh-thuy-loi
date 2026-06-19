-- ============================================================
-- HỆ THỐNG QUẢN LÝ CÔNG TRÌNH THỦY LỢI
-- ============================================================

USE master;
GO

-- Ngắt kết nối và xóa database cũ (nếu có)
IF DB_ID('KTPM') IS NOT NULL
BEGIN
    ALTER DATABASE KTPM SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE KTPM;
END
GO

-- Tạo cơ sở dữ liệu mới
CREATE DATABASE KTPM;
GO

USE KTPM;
GO

-- ============================================================
-- PHẦN 1: QUẢN TRỊ HỆ THỐNG & PHÂN QUYỀN
-- ============================================================

-- Bảng HoSo
IF OBJECT_ID('HoSo', 'U') IS NOT NULL DROP TABLE HoSo;
GO

CREATE TABLE HoSo (
    Id INT PRIMARY KEY IDENTITY,
    Ten NVARCHAR(100),
    SDT VARCHAR(20),
    Email VARCHAR(100),
    Ext NVARCHAR(MAX)
);
GO

INSERT INTO HoSo (Ten, SDT, Email) VALUES
    (N'Vũ Song Tùng', '0989154248', 'tung.vusong@hust.edu.vn'),
    (N'Đào Lê Thu Thảo', '0989708960', 'thao.daolethu@hust.edu.vn');
GO

-- Bảng Quyen
IF OBJECT_ID('Quyen', 'U') IS NOT NULL DROP TABLE Quyen;
GO

CREATE TABLE Quyen (
    Id INT PRIMARY KEY IDENTITY,
    Ten NVARCHAR(50),
    Ext NVARCHAR(MAX)
);
GO

INSERT INTO Quyen (Ten, Ext) VALUES
    (N'Lập trình viên', 'Developer'),
    (N'Quản trị hệ thống', 'Admin'),
    (N'Cán bộ nghiệp vụ', 'Staff');
GO

-- Bảng TaiKhoan
IF OBJECT_ID('TaiKhoan', 'U') IS NOT NULL DROP TABLE TaiKhoan;
GO

CREATE TABLE TaiKhoan (
    Ten VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(255),
    QuyenId INT FOREIGN KEY REFERENCES Quyen(Id),
    HoSoId INT FOREIGN KEY REFERENCES HoSo(Id)
);
GO

INSERT INTO TaiKhoan (Ten, MatKhau, QuyenId, HoSoId) VALUES
    ('dev', '1234', 1, NULL),
    ('admin', '1234', 2, NULL),
    ('0989154248', '1234', 3, 1),
    ('0989708960', '1234', 3, 2);
GO
-- Bảng LichSuTruyCap
IF OBJECT_ID('LichSuTruyCap', 'U') IS NOT NULL DROP TABLE LichSuTruyCap;
GO

CREATE TABLE LichSuTruyCap (
    Id INT PRIMARY KEY IDENTITY,
    HoSoId INT FOREIGN KEY REFERENCES HoSo(Id),
    ThoiGian DATETIME,
    HanhDong NVARCHAR(200),
    IP VARCHAR(50),
    DoiTuongThaoTac NVARCHAR(200)
);
GO

-- ============================================================
-- PHẦN 2: ĐỊA GIỚI HÀNH CHÍNH
-- ============================================================

-- Bảng HanhChinh
IF OBJECT_ID('HanhChinh', 'U') IS NOT NULL DROP TABLE HanhChinh;
GO

CREATE TABLE HanhChinh (
    Id INT PRIMARY KEY IDENTITY,
    Ten NVARCHAR(50),
    TrucThuocId INT FOREIGN KEY REFERENCES HanhChinh(Id)
);
GO

INSERT INTO HanhChinh (Ten, TrucThuocId) VALUES
    (N'Tỉnh/Thành', NULL),
    (N'Quận/Huyện', 1),
    (N'Phường/Xã', 2),
    (N'Tổ/Thôn', 3);
GO

-- Bảng TenHanhChinh
IF OBJECT_ID('TenHanhChinh', 'U') IS NOT NULL DROP TABLE TenHanhChinh;
GO

CREATE TABLE TenHanhChinh (
    Ten NVARCHAR(50)
);
GO

INSERT INTO TenHanhChinh (Ten) VALUES
    (N'Ấp'), (N'Bản'), (N'Buôn'), (N'Huyện'), (N'Làng'),
    (N'Phường'), (N'Quận'), (N'Sóc'), (N'Thành phố'),
    (N'Thị xã'), (N'Thị trấn'), (N'Thôn'), (N'Tỉnh'),
    (N'Tổ'), (N'Xã');
GO

-- Bảng DonVi
IF OBJECT_ID('DonVi', 'U') IS NOT NULL DROP TABLE DonVi;
GO

CREATE TABLE DonVi (
    Id INT PRIMARY KEY IDENTITY,
    Ten NVARCHAR(50),
    HanhChinhId INT FOREIGN KEY REFERENCES HanhChinh(Id),
    TenHanhChinh NVARCHAR(50),
    TrucThuocId INT FOREIGN KEY REFERENCES DonVi(Id)
);
GO

INSERT INTO DonVi (Ten, HanhChinhId, TenHanhChinh, TrucThuocId) VALUES
    (N'Hà Nội', 1, N'Thành phố', NULL),
    (N'Hai Bà Trưng', 2, N'Quận', 1),
    (N'Bách Khoa', 3, N'Phường', 2),
    (N'Đồng Tâm', 3, N'Phường', 2),
    (N'Thái Bình', 1, N'Tỉnh', NULL),
    (N'Thái Thụy', 2, N'Huyện', 5),
    (N'Thụy Hải', 3, N'Xã', 6),
    (N'Thụy Xuân', 3, N'Xã', 6);
GO

-- ============================================================
-- PHẦN 3: QUẢN LÝ CÔNG TRÌNH THỦY LỢI
-- ============================================================

-- Bảng LoaiCongTrinh
IF OBJECT_ID('LoaiCongTrinh', 'U') IS NOT NULL DROP TABLE LoaiCongTrinh;
GO

CREATE TABLE LoaiCongTrinh (
    Id INT PRIMARY KEY IDENTITY,
    TenLoai NVARCHAR(100),
    MoTa NVARCHAR(500)
);
GO

INSERT INTO LoaiCongTrinh (TenLoai, MoTa) VALUES
    (N'Kè', N'Công trình kè chắn sóng, bảo vệ bờ'),
    (N'Đường ống', N'Hệ thống đường ống dẫn nước'),
    (N'Kênh mương', N'Hệ thống kênh mương tưới tiêu'),
    (N'Trạm bơm', N'Trạm bơm nước phục vụ tưới tiêu'),
    (N'Hồ chứa', N'Hồ chứa nước'),
    (N'Đập tràn', N'Công trình đập tràn xả lũ');
GO

-- Bảng CapCongTrinh
IF OBJECT_ID('CapCongTrinh', 'U') IS NOT NULL DROP TABLE CapCongTrinh;
GO

CREATE TABLE CapCongTrinh (
    Id INT PRIMARY KEY IDENTITY,
    TenCap NVARCHAR(50),
    MoTa NVARCHAR(500)
);
GO

INSERT INTO CapCongTrinh (TenCap, MoTa) VALUES
    (N'Cấp I', N'Công trình đặc biệt quan trọng'),
    (N'Cấp II', N'Công trình quan trọng'),
    (N'Cấp III', N'Công trình trung bình'),
    (N'Cấp IV', N'Công trình nhỏ');
GO

-- Bảng CongTrinh
IF OBJECT_ID('CongTrinh', 'U') IS NOT NULL DROP TABLE CongTrinh;
GO

CREATE TABLE CongTrinh (
    Id INT PRIMARY KEY IDENTITY,
    TenCongTrinh NVARCHAR(200),
    MaHieu VARCHAR(50),
    CapCongTrinhId INT FOREIGN KEY REFERENCES CapCongTrinh(Id),
    LoaiCongTrinhId INT FOREIGN KEY REFERENCES LoaiCongTrinh(Id),
    DonViQuanLyId INT FOREIGN KEY REFERENCES DonVi(Id),
    DonViHanhChinhId INT FOREIGN KEY REFERENCES DonVi(Id),
    DiaDiem NVARCHAR(500),
    DuLieuGIS NVARCHAR(MAX),
    NamXayDung INT,
    MoTa NVARCHAR(MAX),
    TrangThai NVARCHAR(50),
    HinhAnh NVARCHAR(500)
);
GO

INSERT INTO CongTrinh (TenCongTrinh, MaHieu, CapCongTrinhId, LoaiCongTrinhId, DonViQuanLyId, DonViHanhChinhId, DiaDiem, DuLieuGIS, NamXayDung, MoTa, TrangThai, HinhAnh) VALUES
    (N'Kè biển Thụy Hải', 'KE-TH-001', 2, 1, 7, 7, N'Xã Thụy Hải, Thái Thụy, Thái Bình', NULL, 2018, N'Kè bảo vệ bờ biển dài 1.5km', N'Đang hoạt động', NULL),
    (N'Trạm bơm Bách Khoa', 'TB-BK-001', 3, 4, 3, 3, N'Phường Bách Khoa, Hai Bà Trưng, Hà Nội', NULL, 2015, N'Trạm bơm phục vụ tưới', N'Đang hoạt động', NULL),
    (N'Hồ chứa Đồng Tâm', 'HC-DT-001', 2, 5, 4, 4, N'Phường Đồng Tâm, Hai Bà Trưng, Hà Nội', NULL, 2010, N'Hồ chứa nước ngọt', N'Đang hoạt động', NULL),
    (N'Kênh N1 Thụy Xuân', 'KM-TX-001', 3, 3, 8, 8, N'Xã Thụy Xuân, Thái Thụy, Thái Bình', NULL, 2012, N'Kênh mương tưới tiêu chính', N'Đang hoạt động', NULL),
    (N'Đập tràn Thái Thụy', 'DT-TT-001', 2, 6, 6, 6, N'Huyện Thái Thụy, Thái Bình', NULL, 2008, N'Đập tràn xả lũ cho hồ chứa', N'Đang hoạt động', NULL);
GO

-- Bảng ChiTietKe
IF OBJECT_ID('ChiTietKe', 'U') IS NOT NULL DROP TABLE ChiTietKe;
GO

CREATE TABLE ChiTietKe (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    ChieuDai DECIMAL(10,2),
    CaoTrinhDinhKe DECIMAL(10,2),
    KetCau NVARCHAR(200)
);
GO

INSERT INTO ChiTietKe (CongTrinhId, ChieuDai, CaoTrinhDinhKe, KetCau) VALUES
    (1, 1500.00, 4.50, N'Đá hộc đổ'),
    (1, 800.00, 4.00, N'Bê tông cốt thép');
GO

-- Bảng ChiTietDuongOng
IF OBJECT_ID('ChiTietDuongOng', 'U') IS NOT NULL DROP TABLE ChiTietDuongOng;
GO

CREATE TABLE ChiTietDuongOng (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    ChieuDai DECIMAL(10,2),
    DuongKinh INT,
    VatLieu NVARCHAR(100)
);
GO

-- Bảng ChiTietKenhMuong
IF OBJECT_ID('ChiTietKenhMuong', 'U') IS NOT NULL DROP TABLE ChiTietKenhMuong;
GO

CREATE TABLE ChiTietKenhMuong (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    ChieuDai DECIMAL(10,2),
    ChieuRong DECIMAL(10,2),
    ChieuCao DECIMAL(10,2),
    KetCau NVARCHAR(200),
    LuuLuong DECIMAL(10,2)
);
GO

INSERT INTO ChiTietKenhMuong (CongTrinhId, ChieuDai, ChieuRong, ChieuCao, KetCau, LuuLuong) VALUES
    (4, 2000.00, 3.00, 2.50, N'Bê tông lót', 5.00),
    (4, 1500.00, 2.50, 2.00, N'Đất đắp', 3.50);
GO

-- Bảng ChiTietTramBom
IF OBJECT_ID('ChiTietTramBom', 'U') IS NOT NULL DROP TABLE ChiTietTramBom;
GO

CREATE TABLE ChiTietTramBom (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    SoMayBom INT,
    CongSuatMay DECIMAL(10,2),
    LuuLuongThietKe DECIMAL(10,2),
    CotNuocThietKe DECIMAL(10,2)
);
GO
-- Bảng ChiTietHoChua
IF OBJECT_ID('ChiTietHoChua', 'U') IS NOT NULL DROP TABLE ChiTietHoChua;
GO

CREATE TABLE ChiTietHoChua (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    TongDungTich DECIMAL(15,2),
    DungTichHuuIch DECIMAL(15,2),
    DungTichChet DECIMAL(15,2),
    MucNuocDangBinhThuong DECIMAL(10,2),
    MucNuocLuThietKe DECIMAL(10,2),
    DienTichMatNuoc DECIMAL(15,2)
);
GO

-- Bảng ChiTietDapTran
IF OBJECT_ID('ChiTietDapTran', 'U') IS NOT NULL DROP TABLE ChiTietDapTran;
GO

CREATE TABLE ChiTietDapTran (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    ChieuDaiDap DECIMAL(10,2),
    ChieuCaoDap DECIMAL(10,2),
    CaoTrinhNguongTran DECIMAL(10,2),
    HinhThucTieuNang NVARCHAR(200),
    KetCauDap NVARCHAR(200)
);
GO


-- ============================================================
-- PHẦN 4: Quản lí tưới tiêu
-- ============================================================

-- Bảng VuMua
IF OBJECT_ID('VuMua', 'U') IS NOT NULL DROP TABLE VuMua;
GO

CREATE TABLE VuMua (
    Id INT PRIMARY KEY IDENTITY,
    TenVu NVARCHAR(50),
    Nam INT,
    ThoiGianBatDau DATE,
    ThoiGianKetThuc DATE
);
GO

-- Bảng KetQuaTuoi
IF OBJECT_ID('KetQuaTuoi', 'U') IS NOT NULL DROP TABLE KetQuaTuoi;
GO

CREATE TABLE KetQuaTuoi (
    Id INT PRIMARY KEY IDENTITY,
    VuMuaId INT FOREIGN KEY REFERENCES VuMua(Id),
    DonViHanhChinhId INT FOREIGN KEY REFERENCES DonVi(Id),
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    DienTichKeHoach DECIMAL(15,2),
    DienTichThucTe DECIMAL(15,2),
    NangSuat DECIMAL(10,2),
    SanLuong DECIMAL(15,2)
);
GO

-- ============================================================
-- PHẦN 5: QUẢN LÝ VẬN HÀNH & BẢO TRÌ
-- ============================================================
-- Bảng KyQuyHoach
IF OBJECT_ID('KyQuyHoach', 'U') IS NOT NULL DROP TABLE KyQuyHoach;
GO

CREATE TABLE KyQuyHoach (
    Id INT PRIMARY KEY IDENTITY,
    TenKyQuyHoach NVARCHAR(200),
    NamBatDau INT,
    NamKetThuc INT,
    MoTa NVARCHAR(MAX),
    TrangThai NVARCHAR(50)
);
GO

-- Bảng LichSuBaoTri
IF OBJECT_ID('LichSuBaoTri', 'U') IS NOT NULL DROP TABLE LichSuBaoTri;
GO

CREATE TABLE LichSuBaoTri (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    NoiDung NVARCHAR(MAX),
    DonViThucHien NVARCHAR(200),
    KinhPhi DECIMAL(15,2),
    KetQua NVARCHAR(MAX)
);
GO

-- Bảng NhatKyVanHanh
IF OBJECT_ID('NhatKyVanHanh', 'U') IS NOT NULL DROP TABLE NhatKyVanHanh;
GO

CREATE TABLE NhatKyVanHanh (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    NguoiThucHien NVARCHAR(100),
    NoiDung NVARCHAR(MAX),
    KetQua NVARCHAR(MAX),
    ChiPhi DECIMAL(15,2)
);
GO

-- Bảng VanBanPhapLy
IF OBJECT_ID('VanBanPhapLy', 'U') IS NOT NULL DROP TABLE VanBanPhapLy;
GO

CREATE TABLE VanBanPhapLy (
    Id INT PRIMARY KEY IDENTITY,
    CongTrinhId INT FOREIGN KEY REFERENCES CongTrinh(Id),
    SoKyHieu VARCHAR(50),
    NgayBanHanh DATE,
    TrichYeu NVARCHAR(500),
    LoaiVanBan NVARCHAR(100),
    TepDinhKem NVARCHAR(500)
);
GO

-- Bảng TaiLieuDinhKem
IF OBJECT_ID('TaiLieuDinhKem', 'U') IS NOT NULL DROP TABLE TaiLieuDinhKem;
GO

CREATE TABLE TaiLieuDinhKem (
    Id INT PRIMARY KEY IDENTITY,
    DoiTuongId INT,
    LoaiDoiTuong NVARCHAR(50),
    TenFile NVARCHAR(200),
    DuongDan NVARCHAR(500),
    MoTa NVARCHAR(500)
);
GO




-- ============================================================
-- PHẦN 6: VIEW (CHO GIAO DIỆN & BÁO CÁO)
-- ============================================================

-- View ViewDonVi
IF OBJECT_ID('ViewDonVi', 'V') IS NOT NULL DROP VIEW ViewDonVi;
GO

CREATE VIEW ViewDonVi AS
SELECT
    T.*,
    DonVi.Ten AS TrucThuoc
FROM (
    SELECT
        DonVi.*,
        HanhChinh.Ten AS Cap
    FROM DonVi
    INNER JOIN HanhChinh ON DonVi.HanhChinhId = HanhChinh.Id
) AS T
LEFT JOIN DonVi ON T.TrucThuocId = DonVi.Id;
GO

-- View ViewHoSo
IF OBJECT_ID('ViewHoSo', 'V') IS NOT NULL DROP VIEW ViewHoSo;
GO

CREATE VIEW ViewHoSo AS
SELECT
    HoSo.*,
    TaiKhoan.Ten AS TenDangNhap,
    TaiKhoan.MatKhau,
    TaiKhoan.QuyenId,
    Quyen.Ten AS Quyen
FROM TaiKhoan
INNER JOIN Quyen ON TaiKhoan.QuyenId = Quyen.Id
INNER JOIN HoSo ON TaiKhoan.HoSoId = HoSo.Id;
GO


-- ViewLichSuTruyCap: Dùng cho WinApp.Views.LichSuTruyCap
IF OBJECT_ID('ViewLichSuTruyCap', 'V') IS NOT NULL DROP VIEW ViewLichSuTruyCap;
GO
CREATE VIEW ViewLichSuTruyCap AS
SELECT 
    ls.Id,
    ls.ThoiGian,
    COALESCE(hs.Ten, 'Unknown') AS TenNguoiDung,
    ls.HanhDong,
    ls.DoiTuongThaoTac,
    ls.IP,
    ls.HoSoId -- Để binding form edit
FROM LichSuTruyCap ls
LEFT JOIN HoSo hs ON ls.HoSoId = hs.Id;
GO


-- ============================================================
--  NHÓM VIEW CÔNG TRÌNH CHUNG & BẢN ĐỒ 
-- ============================================================

-- ViewCongTrinh: Dùng cho WinApp.Views.CongTrinh (Danh sách tổng)
IF OBJECT_ID('ViewCongTrinh', 'V') IS NOT NULL DROP VIEW ViewCongTrinh;
GO
CREATE VIEW ViewCongTrinh AS
SELECT 
    ct.Id, 
    ct.TenCongTrinh, 
    ct.MaHieu,
    ct.NamXayDung,
    ct.TrangThai,
    ct.DiaDiem,
    ct.MoTa,
    ct.HinhAnh,
    ct.DuLieuGIS,
    ct.CapCongTrinhId, ct.LoaiCongTrinhId, ct.DonViQuanLyId, ct.DonViHanhChinhId, -- Các cột ID để binding Select
    lc.TenLoai AS LoaiCongTrinh,
    cc.TenCap AS CapCongTrinh,
    dv1.Ten AS DonViQuanLy,
    dv2.Ten AS DonViHanhChinh
FROM CongTrinh ct
LEFT JOIN LoaiCongTrinh lc ON ct.LoaiCongTrinhId = lc.Id
LEFT JOIN CapCongTrinh cc ON ct.CapCongTrinhId = cc.Id
LEFT JOIN DonVi dv1 ON ct.DonViQuanLyId = dv1.Id
LEFT JOIN DonVi dv2 ON ct.DonViHanhChinhId = dv2.Id;
GO

-- ViewBanDo
IF OBJECT_ID('ViewBanDo', 'V') IS NOT NULL DROP VIEW ViewBanDo;
GO
CREATE VIEW ViewBanDoCongTrinh AS
SELECT 
    ct.Id, 
    ct.TenCongTrinh, 
    ct.MaHieu, 
    ct.DiaDiem, 
    ct.DuLieuGIS,
    lc.TenLoai AS LoaiCongTrinh
FROM CongTrinh ct
LEFT JOIN LoaiCongTrinh lc ON ct.LoaiCongTrinhId = lc.Id
WHERE ct.DuLieuGIS IS NOT NULL AND ct.DuLieuGIS <> '';
GO

-- ============================================================
--  NHÓM VIEW CHI TIẾT KỸ THUẬT 
-- ============================================================

-- ViewChiTietDapTran
IF OBJECT_ID('ViewChiTietDapTran', 'V') IS NOT NULL DROP VIEW ViewChiTietDapTran;
GO
CREATE VIEW ViewChiTietDapTran AS
SELECT 
    dt.Id, dt.CongTrinhId, ct.TenCongTrinh,
    dt.ChieuDaiDap, dt.ChieuCaoDap, dt.CaoTrinhNguongTran, 
    dt.HinhThucTieuNang, dt.KetCauDap
FROM ChiTietDapTran dt
INNER JOIN CongTrinh ct ON dt.CongTrinhId = ct.Id;
GO

-- ViewChiTietDuongOng
IF OBJECT_ID('ViewChiTietDuongOng', 'V') IS NOT NULL DROP VIEW ViewChiTietDuongOng;
GO
CREATE VIEW ViewChiTietDuongOng AS
SELECT 
    do.Id, do.CongTrinhId, ct.TenCongTrinh,
    do.ChieuDai, do.DuongKinh, do.VatLieu
FROM ChiTietDuongOng do
INNER JOIN CongTrinh ct ON do.CongTrinhId = ct.Id;
GO

-- ViewChiTietHoChua
IF OBJECT_ID('ViewChiTietHoChua', 'V') IS NOT NULL DROP VIEW ViewChiTietHoChua;
GO
CREATE VIEW ViewChiTietHoChua AS
SELECT 
    hc.Id, hc.CongTrinhId, ct.TenCongTrinh,
    hc.TongDungTich, hc.DungTichHuuIch, hc.DungTichChet,
    hc.MucNuocDangBinhThuong, hc.MucNuocLuThietKe, hc.DienTichMatNuoc
FROM ChiTietHoChua hc
INNER JOIN CongTrinh ct ON hc.CongTrinhId = ct.Id;
GO

-- ViewChiTietKe
IF OBJECT_ID('ViewChiTietKe', 'V') IS NOT NULL DROP VIEW ViewChiTietKe;
GO
CREATE VIEW ViewChiTietKe AS
SELECT 
    k.Id, k.CongTrinhId, ct.TenCongTrinh,
    k.ChieuDai, k.CaoTrinhDinhKe, k.KetCau
FROM ChiTietKe k
INNER JOIN CongTrinh ct ON k.CongTrinhId = ct.Id;
GO

-- ViewChiTietKenhMuong
IF OBJECT_ID('ViewChiTietKenhMuong', 'V') IS NOT NULL DROP VIEW ViewChiTietKenhMuong;
GO
CREATE VIEW ViewChiTietKenhMuong AS
SELECT 
    km.Id, km.CongTrinhId, ct.TenCongTrinh,
    km.ChieuDai, km.ChieuRong, km.ChieuCao, 
    km.LuuLuong, km.KetCau
FROM ChiTietKenhMuong km
INNER JOIN CongTrinh ct ON km.CongTrinhId = ct.Id;
GO

-- ViewChiTietTramBom
IF OBJECT_ID('ViewChiTietTramBom', 'V') IS NOT NULL DROP VIEW ViewChiTietTramBom;
GO
CREATE VIEW ViewChiTietTramBom AS
SELECT 
    tb.Id, tb.CongTrinhId, ct.TenCongTrinh,
    tb.SoMayBom, tb.CongSuatMay, 
    tb.LuuLuongThietKe, tb.CotNuocThietKe
FROM ChiTietTramBom tb
INNER JOIN CongTrinh ct ON tb.CongTrinhId = ct.Id;
GO

-- ============================================================
-- NHÓM VIEW NGHIỆP VỤ & VẬN HÀNH 
-- ============================================================

-- ViewNhatKyVanHanh: Cần format ngày tháng ra string cho C# grid
IF OBJECT_ID('ViewNhatKyVanHanh', 'V') IS NOT NULL DROP VIEW ViewNhatKyVanHanh;
GO
CREATE VIEW ViewNhatKyVanHanh AS
SELECT 
    nk.Id,
    nk.CongTrinhId,
    ct.TenCongTrinh,
    nk.NgayBatDau,
    nk.NgayKetThuc,
    -- Format date sang string dd/MM/yyyy để hiện lên Grid
    FORMAT(nk.NgayBatDau, 'dd/MM/yyyy') AS NgayBatDauStr,
    FORMAT(nk.NgayKetThuc, 'dd/MM/yyyy') AS NgayKetThucStr,
    nk.NguoiThucHien,
    nk.NoiDung,
    nk.KetQua,
    nk.ChiPhi
FROM NhatKyVanHanh nk
INNER JOIN CongTrinh ct ON nk.CongTrinhId = ct.Id;
GO

-- ViewLichSuBaoTri: Tương tự nhật ký, cần format ngày
IF OBJECT_ID('ViewLichSuBaoTri', 'V') IS NOT NULL DROP VIEW ViewLichSuBaoTri;
GO
CREATE VIEW ViewLichSuBaoTri AS
SELECT 
    bt.Id,
    bt.CongTrinhId,
    ct.TenCongTrinh,
    bt.NgayBatDau,
    bt.NgayKetThuc,
    FORMAT(bt.NgayBatDau, 'dd/MM/yyyy') AS NgayBatDauStr,
    FORMAT(bt.NgayKetThuc, 'dd/MM/yyyy') AS NgayKetThucStr,
    bt.NoiDung,
    bt.DonViThucHien,
    bt.KinhPhi,
    bt.KetQua
FROM LichSuBaoTri bt
INNER JOIN CongTrinh ct ON bt.CongTrinhId = ct.Id;
GO

-- ViewVuMua: Format ngày tháng
IF OBJECT_ID('ViewVuMua', 'V') IS NOT NULL DROP VIEW ViewVuMua;
GO
CREATE VIEW ViewVuMua AS
SELECT 
    vm.Id,
    vm.TenVu,
    vm.Nam,
    vm.ThoiGianBatDau,
    vm.ThoiGianKetThuc,
    FORMAT(vm.ThoiGianBatDau, 'dd/MM/yyyy') AS NgayBatDauStr,
    FORMAT(vm.ThoiGianKetThuc, 'dd/MM/yyyy') AS NgayKetThucStr
FROM VuMua vm;
GO

-- ViewKetQuaTuoi: Tính toán tỷ lệ, lấy tên hành chính
IF OBJECT_ID('ViewKetQuaTuoi', 'V') IS NOT NULL DROP VIEW ViewKetQuaTuoi;
GO
CREATE VIEW ViewKetQuaTuoi AS
SELECT 
    kq.Id,
    kq.VuMuaId, kq.DonViHanhChinhId, kq.CongTrinhId, -- ID cho Form Edit
    vm.TenVu AS TenVuMua,
    dv.Ten AS TenHanhChinh, 
    ct.TenCongTrinh,
    kq.DienTichKeHoach,
    kq.DienTichThucTe,
    kq.NangSuat,
    kq.SanLuong
FROM KetQuaTuoi kq
LEFT JOIN VuMua vm ON kq.VuMuaId = vm.Id
LEFT JOIN DonVi dv ON kq.DonViHanhChinhId = dv.Id
LEFT JOIN CongTrinh ct ON kq.CongTrinhId = ct.Id;
GO

-- ViewVanBanPhapLy
IF OBJECT_ID('ViewVanBanPhapLy', 'V') IS NOT NULL DROP VIEW ViewVanBanPhapLy;
GO
CREATE VIEW ViewVanBanPhapLy AS
SELECT 
    vb.Id,
    vb.CongTrinhId,
    ct.TenCongTrinh,
    vb.SoKyHieu,
    vb.NgayBanHanh,
    vb.TrichYeu,
    vb.LoaiVanBan,
    vb.TepDinhKem
FROM VanBanPhapLy vb
LEFT JOIN CongTrinh ct ON vb.CongTrinhId = ct.Id;
GO

-- ViewTaiLieu (Dùng chung)
IF OBJECT_ID('ViewTaiLieu', 'V') IS NOT NULL DROP VIEW ViewTaiLieu;
GO
CREATE VIEW ViewTaiLieu AS
SELECT * FROM TaiLieuDinhKem;
GO

-- ============================================================
-- NHÓM VIEW THỐNG KÊ & BÁO CÁO (Folder: ThongKe...)
-- ============================================================

-- ViewThongKeCongTrinh
IF OBJECT_ID('ViewThongKeCongTrinh', 'V') IS NOT NULL DROP VIEW ViewThongKeCongTrinh;
GO
CREATE VIEW ViewThongKeCongTrinh AS
SELECT 
    lc.TenLoai AS TenNhom,
    COUNT(ct.Id) AS SoLuong,
    CAST(COUNT(ct.Id) * 100.0 / (SELECT COUNT(*) FROM CongTrinh) AS DECIMAL(10, 2)) AS TyLe,
    MAX(lc.MoTa) AS GhiChu
FROM CongTrinh ct
JOIN LoaiCongTrinh lc ON ct.LoaiCongTrinhId = lc.Id
GROUP BY lc.TenLoai;
GO

-- ViewThongKeKetQuaTuoi
IF OBJECT_ID('ViewThongKeKetQuaTuoi', 'V') IS NOT NULL DROP VIEW ViewThongKeKetQuaTuoi;
GO
CREATE VIEW ViewThongKeKetQuaTuoi AS
SELECT 
    dv.Ten AS TenDoiTuong,
    SUM(kq.DienTichKeHoach) AS DienTichKeHoach,
    SUM(kq.DienTichThucTe) AS DienTichThucTe,
    CAST(CASE WHEN SUM(kq.DienTichKeHoach) > 0 
         THEN SUM(kq.DienTichThucTe) * 100.0 / SUM(kq.DienTichKeHoach) 
         ELSE 0 END AS DECIMAL(10, 2)) AS TyLeDat
FROM KetQuaTuoi kq
JOIN DonVi dv ON kq.DonViHanhChinhId = dv.Id
GROUP BY dv.Ten;
GO

PRINT N'Hoàn thành khởi tạo cơ sở dữ liệu Thủy lợi!';
GO

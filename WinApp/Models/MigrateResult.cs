using System;

namespace Models
{
    // ======================= DANH MỤC (CATALOGS) =======================

    public partial class CapCongTrinh
    {
        public int? Id { get; set; }
        public string TenCap { get; set; }
        public string MoTa { get; set; }
    }

    public partial class LoaiCongTrinh
    {
        public int? Id { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
    }

    public partial class HanhChinh
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public int? TrucThuocId { get; set; }
    }

    public partial class TenHanhChinh
    {
        public string Ten { get; set; }
    }

    public partial class DonVi
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public int? HanhChinhId { get; set; }
        public string TenHanhChinh { get; set; }
        public int? TrucThuocId { get; set; }
    }

    // ======================= CÔNG TRÌNH (CONSTRUCTIONS) =======================

    public partial class CongTrinh
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public string TenCongTrinh { get; set; }
        public string MaHieu { get; set; }
        public int? CapCongTrinhId { get; set; }
        public int? LoaiCongTrinhId { get; set; }
        public int? DonViQuanLyId { get; set; }
        public int? DonViHanhChinhId { get; set; }
        public string DiaDiem { get; set; }
        public string DuLieuGIS { get; set; }
        public int? NamXayDung { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }

        // ---- View/Extension Properties ----
        public string LoaiCongTrinh { get; set; }
        public string CapCongTrinh { get; set; }
        public string DonViQuanLy { get; set; }
    }

    // ======================= CHI TIẾT CÔNG TRÌNH =======================

    public partial class ChiTietDapTran
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? ChieuDaiDap { get; set; }
        public decimal? ChieuCaoDap { get; set; }
        public decimal? CaoTrinhNguongTran { get; set; }
        public string HinhThucTieuNang { get; set; }
        public string KetCauDap { get; set; }
    }

    public partial class ChiTietDuongOng
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? ChieuDai { get; set; }
        public int? DuongKinh { get; set; }
        public string VatLieu { get; set; }
    }

    public partial class ChiTietHoChua
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? TongDungTich { get; set; }
        public decimal? DungTichHuuIch { get; set; }
        public decimal? DungTichChet { get; set; }
        public decimal? MucNuocDangBinhThuong { get; set; }
        public decimal? MucNuocLuThietKe { get; set; }
        public decimal? DienTichMatNuoc { get; set; }
    }

    public partial class ChiTietKe
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? ChieuDai { get; set; }
        public decimal? CaoTrinhDinhKe { get; set; }
        public string KetCau { get; set; }
    }

    public partial class ChiTietKenhMuong
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? ChieuDai { get; set; }
        public decimal? ChieuRong { get; set; }
        public decimal? ChieuCao { get; set; }
        public string KetCau { get; set; }
        public decimal? LuuLuong { get; set; }
    }

    public partial class ChiTietTramBom
    {
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public int? SoMayBom { get; set; }
        public decimal? CongSuatMay { get; set; }
        public decimal? LuuLuongThietKe { get; set; }
        public decimal? CotNuocThietKe { get; set; }
    }

    // ======================= QUẢN LÝ VỤ MÙA & QUY HOẠCH =======================

    public partial class VuMua
    {
        public int? Id { get; set; }
        public string TenVu { get; set; }
        public int? Nam { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public string NgayBatDauStr => ThoiGianBatDau?.ToString("dd/MM/yyyy") ?? "";
        public string NgayKetThucStr => ThoiGianKetThuc?.ToString("dd/MM/yyyy") ?? "";
        public string TenHienThi => $"{TenVu} - {Nam}";
    }

    public partial class KyQuyHoach
    {
        public int? Id { get; set; }
        public string TenKyQuyHoach { get; set; }
        public int? NamBatDau { get; set; }
        public int? NamKetThuc { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
    }

    public partial class KetQuaTuoi
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public int? VuMuaId { get; set; }
        public int? DonViHanhChinhId { get; set; }
        public int? CongTrinhId { get; set; }
        public decimal? DienTichKeHoach { get; set; }
        public decimal? DienTichThucTe { get; set; }
        public decimal? NangSuat { get; set; }
        public decimal? SanLuong { get; set; }

        // ---- View Properties ----
        public string TenCongTrinh { get; set; }
        public string TenVuMua { get; set; }
        public string TenHanhChinh { get; set; }
    }

    // ======================= HỒ SƠ – TÀI KHOẢN =======================

    public partial class HoSo
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string Ext { get; set; }
    }

    public partial class TaiKhoan
    {
        public string Ten { get; set; }
        public string MatKhau { get; set; }
        public int? QuyenId { get; set; }
        public int? HoSoId { get; set; }
    }

    public partial class Quyen
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public string Ext { get; set; }
    }

    public partial class LichSuTruyCap
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public int? HoSoId { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string HanhDong { get; set; }
        public string IP { get; set; }
        public string DoiTuongThaoTac { get; set; }

        // ---- View Properties ----
        public string TenNguoiDung { get; set; }
        public string ThoiGianHienThi { get; set; }
    }

    // ======================= NHẬT KÝ – BẢO TRÌ – VĂN BẢN =======================

    public partial class LichSuBaoTri
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NoiDung { get; set; }
        public string DonViThucHien { get; set; }
        public decimal? KinhPhi { get; set; }
        public string KetQua { get; set; }

        public string NgayBatDauStr => NgayBatDau?.ToString("dd/MM/yyyy") ?? "";
        public string NgayKetThucStr => NgayKetThuc?.ToString("dd/MM/yyyy") ?? "";
        public string TenCongTrinh { get; set; }
    }

    public partial class NhatKyVanHanh
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiThucHien { get; set; }
        public string NoiDung { get; set; }
        public string KetQua { get; set; }
        public decimal? ChiPhi { get; set; }
        public string NgayBatDauStr => NgayBatDau?.ToString("dd/MM/yyyy") ?? "";
        public string NgayKetThucStr => NgayKetThuc?.ToString("dd/MM/yyyy") ?? "";

        // ---- View Properties ----
        public string TenCongTrinh { get; set; }
    }

    public partial class VanBanPhapLy
    {
        // ---- DB Properties ----
        public int? Id { get; set; }
        public int? CongTrinhId { get; set; }
        public string SoKyHieu { get; set; }
        public DateTime? NgayBanHanh { get; set; }
        public string TrichYeu { get; set; }
        public string LoaiVanBan { get; set; }
        public string TepDinhKem { get; set; }

        // ---- View Properties ----
        public string TenCongTrinh { get; set; }
    }

    public partial class TaiLieuDinhKem
    {
        public int? Id { get; set; }
        public int? DoiTuongId { get; set; }
        public string LoaiDoiTuong { get; set; }
        public string TenFile { get; set; }
        public string DuongDan { get; set; }
        public string MoTa { get; set; }
    }

    public partial class HuongDanSuDung
    {
        public int? Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }

    // ======================= SQL VIEWS (TỔNG HỢP) =======================

    public partial class ViewCongTrinh
    {
        public int? Id { get; set; }
        public string TenCongTrinh { get; set; }
        public string MaHieu { get; set; }
        public int? NamXayDung { get; set; }
        public string TrangThai { get; set; }
        public string DiaDiem { get; set; }
        public string MoTa { get; set; }
        public string LoaiCongTrinh { get; set; }
        public string CapCongTrinh { get; set; }
        public string DonViQuanLy { get; set; }
        public string DonViHanhChinh { get; set; }
    }

    public partial class ViewDonVi
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public int? HanhChinhId { get; set; }
        public string TenHanhChinh { get; set; }
        public int? TrucThuocId { get; set; }
        public string Cap { get; set; }
        public string TrucThuoc { get; set; }
    }

    public partial class ViewHoSo
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string Ext { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? QuyenId { get; set; }
        public string Quyen { get; set; }
    }

    public partial class ViewKetQuaTuoi
    {
        public int? Id { get; set; }
        public string TenVu { get; set; }
        public int? Nam { get; set; }
        public string DonVi { get; set; }
        public string TenCongTrinh { get; set; }
        public decimal? DienTichKeHoach { get; set; }
        public decimal? DienTichThucTe { get; set; }
        public decimal? NangSuat { get; set; }
        public decimal? SanLuong { get; set; }
        public decimal? TyLeHoanThanh { get; set; }
    }

    public partial class ViewLichSuBaoTri
    {
        public int? Id { get; set; }
        public string TenCongTrinh { get; set; }
        public string MaHieu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public int? SoNgayThucHien { get; set; }
        public string NoiDung { get; set; }
        public string DonViThucHien { get; set; }
        public decimal? KinhPhi { get; set; }
        public string KetQua { get; set; }
        public string NgayBatDauStr => NgayBatDau?.ToString("dd/MM/yyyy") ?? "";
        public string NgayKetThucStr => NgayKetThuc?.ToString("dd/MM/yyyy") ?? "";
    }

    public partial class ViewNhatKyVanHanh
    {
        public int? Id { get; set; }
        public string TenCongTrinh { get; set; }
        public string MaHieu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string NguoiThucHien { get; set; }
        public string NoiDung { get; set; }
        public string KetQua { get; set; }
        public decimal? ChiPhi { get; set; }
    }

    public partial class ViewVanBanPhapLy
    {
        public int? Id { get; set; }
        public string SoKyHieu { get; set; }
        public DateTime? NgayBanHanh { get; set; }
        public string TrichYeu { get; set; }
        public string LoaiVanBan { get; set; }
        public string TenCongTrinh { get; set; }
        public string MaHieu { get; set; }
    }

    // ======================= THỐNG KÊ (DTO) =======================

    public partial class ThongKe
    {
        public string TenDoiTuong { get; set; }
        public decimal DienTichKeHoach { get; set; }
        public decimal DienTichThucTe { get; set; }
        public decimal TyLeDat { get; set; }
        public string GhiChu { get; set; }
    }
    public partial class ThongKeCongTrinh
    {
        public string TenNhom { get; set; } // Tên Loại, Tên Cấp, hoặc Tên Đơn vị
        public int SoLuong { get; set; }
        public double TyLe { get; set; }  
        public string GhiChu { get; set; }
    }
}

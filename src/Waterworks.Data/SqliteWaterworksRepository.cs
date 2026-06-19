using Microsoft.Data.Sqlite;
using Waterworks.Shared;

namespace Waterworks.Data;

public sealed class SqliteWaterworksRepository(string connectionString) : IWaterworksRepository
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        var commands = new[]
        {
            """
            CREATE TABLE IF NOT EXISTS Quyen (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Ten TEXT,
                Ext TEXT
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS HoSo (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Ten TEXT,
                SDT TEXT,
                Email TEXT,
                Ext TEXT
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS TaiKhoan (
                Ten TEXT PRIMARY KEY,
                MatKhau TEXT,
                QuyenId INTEGER REFERENCES Quyen(Id),
                HoSoId INTEGER REFERENCES HoSo(Id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS CapCongTrinh (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                TenCap TEXT,
                MoTa TEXT
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS LoaiCongTrinh (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                TenLoai TEXT,
                MoTa TEXT
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS DonVi (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Ten TEXT,
                HanhChinhId INTEGER,
                TenHanhChinh TEXT,
                TrucThuocId INTEGER REFERENCES DonVi(Id)
            );
            """,
            """
            CREATE TABLE IF NOT EXISTS CongTrinh (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                TenCongTrinh TEXT,
                MaHieu TEXT,
                CapCongTrinhId INTEGER REFERENCES CapCongTrinh(Id),
                LoaiCongTrinhId INTEGER REFERENCES LoaiCongTrinh(Id),
                DonViQuanLyId INTEGER REFERENCES DonVi(Id),
                DonViHanhChinhId INTEGER REFERENCES DonVi(Id),
                DiaDiem TEXT,
                DuLieuGIS TEXT,
                NamXayDung INTEGER,
                MoTa TEXT,
                TrangThai TEXT,
                HinhAnh TEXT
            );
            """
        };

        foreach (var sql in commands)
        {
            await ExecuteAsync(connection, sql, cancellationToken);
        }

        await SeedAsync(connection, cancellationToken);
    }

    public async Task<UserInfoDto?> ValidateLoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        const string sql = """
            SELECT tk.Ten, q.Ext, hs.Ten
            FROM TaiKhoan tk
            LEFT JOIN Quyen q ON q.Id = tk.QuyenId
            LEFT JOIN HoSo hs ON hs.Id = tk.HoSoId
            WHERE tk.Ten = $userName AND tk.MatKhau = $password
            LIMIT 1;
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$userName", request.UserName);
        command.Parameters.AddWithValue("$password", request.Password);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new UserInfoDto(
            reader.GetString(0),
            ReadNullableString(reader, 1) ?? "Staff",
            ReadNullableString(reader, 2));
    }

    public async Task<IReadOnlyList<CongTrinhDto>> GetCongTrinhAsync(CancellationToken cancellationToken = default)
    {
        const string sql = BaseCongTrinhSelect + " ORDER BY ct.Id DESC;";
        return await QueryCongTrinhAsync(sql, null, cancellationToken);
    }

    public async Task<CongTrinhDto?> GetCongTrinhByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = BaseCongTrinhSelect + " WHERE ct.Id = $id;";
        var rows = await QueryCongTrinhAsync(sql, command => command.Parameters.AddWithValue("$id", id), cancellationToken);
        return rows.FirstOrDefault();
    }

    public async Task<CongTrinhDto> CreateCongTrinhAsync(CongTrinhUpsertRequest request, CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO CongTrinh (
                TenCongTrinh, MaHieu, CapCongTrinhId, LoaiCongTrinhId, DonViQuanLyId,
                DonViHanhChinhId, DiaDiem, DuLieuGIS, NamXayDung, MoTa, TrangThai, HinhAnh
            )
            VALUES (
                $tenCongTrinh, $maHieu, $capCongTrinhId, $loaiCongTrinhId, $donViQuanLyId,
                $donViHanhChinhId, $diaDiem, $duLieuGIS, $namXayDung, $moTa, $trangThai, $hinhAnh
            );
            SELECT last_insert_rowid();
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddCongTrinhParameters(command, request);

        var newId = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        return await GetCongTrinhByIdAsync(newId, cancellationToken)
            ?? throw new InvalidOperationException("Created record could not be loaded.");
    }

    public async Task<bool> UpdateCongTrinhAsync(int id, CongTrinhUpsertRequest request, CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE CongTrinh
            SET TenCongTrinh = $tenCongTrinh,
                MaHieu = $maHieu,
                CapCongTrinhId = $capCongTrinhId,
                LoaiCongTrinhId = $loaiCongTrinhId,
                DonViQuanLyId = $donViQuanLyId,
                DonViHanhChinhId = $donViHanhChinhId,
                DiaDiem = $diaDiem,
                DuLieuGIS = $duLieuGIS,
                NamXayDung = $namXayDung,
                MoTa = $moTa,
                TrangThai = $trangThai,
                HinhAnh = $hinhAnh
            WHERE Id = $id;
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$id", id);
        AddCongTrinhParameters(command, request);

        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteCongTrinhAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM CongTrinh WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);
        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public Task<IReadOnlyList<LookupItemDto>> GetLoaiCongTrinhAsync(CancellationToken cancellationToken = default) =>
        QueryLookupAsync("SELECT Id, TenLoai FROM LoaiCongTrinh ORDER BY TenLoai;", cancellationToken);

    public Task<IReadOnlyList<LookupItemDto>> GetCapCongTrinhAsync(CancellationToken cancellationToken = default) =>
        QueryLookupAsync("SELECT Id, TenCap FROM CapCongTrinh ORDER BY Id;", cancellationToken);

    public Task<IReadOnlyList<LookupItemDto>> GetDonViAsync(CancellationToken cancellationToken = default) =>
        QueryLookupAsync("SELECT Id, Ten FROM DonVi ORDER BY Ten;", cancellationToken);

    private SqliteConnection CreateConnection() => new(connectionString);

    private static async Task ExecuteAsync(SqliteConnection connection, string sql, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task SeedAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        var hasData = Convert.ToInt32(await ScalarAsync(connection, "SELECT COUNT(*) FROM CongTrinh;", cancellationToken)) > 0;
        if (hasData)
        {
            return;
        }

        var seedCommands = new[]
        {
            "INSERT INTO Quyen (Id, Ten, Ext) VALUES (1, 'Developer', 'Developer'), (2, 'Admin', 'Admin'), (3, 'Staff', 'Staff');",
            "INSERT INTO HoSo (Id, Ten, SDT, Email, Ext) VALUES (1, 'Nguoi dung demo', '0989154248', 'demo@example.com', NULL);",
            "INSERT INTO TaiKhoan (Ten, MatKhau, QuyenId, HoSoId) VALUES ('dev', '1234', 1, NULL), ('admin', '1234', 2, NULL), ('0989154248', '1234', 3, 1);",
            "INSERT INTO LoaiCongTrinh (Id, TenLoai, MoTa) VALUES (1, 'Ke', 'Cong trinh ke chan song'), (2, 'Duong ong', 'He thong duong ong dan nuoc'), (3, 'Kenh muong', 'He thong kenh muong tuoi tieu'), (4, 'Tram bom', 'Tram bom phuc vu tuoi tieu'), (5, 'Ho chua', 'Ho chua nuoc');",
            "INSERT INTO CapCongTrinh (Id, TenCap, MoTa) VALUES (1, 'Cap I', 'Cong trinh dac biet quan trong'), (2, 'Cap II', 'Cong trinh quan trong'), (3, 'Cap III', 'Cong trinh trung binh'), (4, 'Cap IV', 'Cong trinh nho');",
            "INSERT INTO DonVi (Id, Ten, HanhChinhId, TenHanhChinh, TrucThuocId) VALUES (1, 'Ha Noi', 1, 'Thanh pho', NULL), (2, 'Hai Ba Trung', 2, 'Quan', 1), (3, 'Bach Khoa', 3, 'Phuong', 2), (4, 'Dong Tam', 3, 'Phuong', 2), (5, 'Thai Binh', 1, 'Tinh', NULL), (6, 'Thai Thuy', 2, 'Huyen', 5), (7, 'Thuy Hai', 3, 'Xa', 6);",
            """
            INSERT INTO CongTrinh (TenCongTrinh, MaHieu, CapCongTrinhId, LoaiCongTrinhId, DonViQuanLyId, DonViHanhChinhId, DiaDiem, DuLieuGIS, NamXayDung, MoTa, TrangThai, HinhAnh)
            VALUES
                ('Ke bien Thuy Hai', 'KE-TH-001', 2, 1, 7, 7, 'Xa Thuy Hai, Thai Thuy, Thai Binh', NULL, 2018, 'Ke bao ve bo bien dai 1.5km', 'Dang hoat dong', NULL),
                ('Tram bom Bach Khoa', 'TB-BK-001', 3, 4, 3, 3, 'Phuong Bach Khoa, Hai Ba Trung, Ha Noi', NULL, 2015, 'Tram bom phuc vu tuoi', 'Dang hoat dong', NULL),
                ('Ho chua Dong Tam', 'HC-DT-001', 2, 5, 4, 4, 'Phuong Dong Tam, Hai Ba Trung, Ha Noi', NULL, 2010, 'Ho chua nuoc ngot', 'Dang hoat dong', NULL);
            """
        };

        foreach (var sql in seedCommands)
        {
            await ExecuteAsync(connection, sql, cancellationToken);
        }
    }

    private static async Task<object?> ScalarAsync(SqliteConnection connection, string sql, CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        return await command.ExecuteScalarAsync(cancellationToken);
    }

    private async Task<IReadOnlyList<CongTrinhDto>> QueryCongTrinhAsync(
        string sql,
        Action<SqliteCommand>? configure,
        CancellationToken cancellationToken)
    {
        var items = new List<CongTrinhDto>();
        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        configure?.Invoke(command);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new CongTrinhDto(
                reader.GetInt32(0),
                ReadNullableString(reader, 1),
                ReadNullableString(reader, 2),
                ReadNullableInt(reader, 3),
                ReadNullableInt(reader, 4),
                ReadNullableInt(reader, 5),
                ReadNullableInt(reader, 6),
                ReadNullableString(reader, 7),
                ReadNullableString(reader, 8),
                ReadNullableInt(reader, 9),
                ReadNullableString(reader, 10),
                ReadNullableString(reader, 11),
                ReadNullableString(reader, 12),
                ReadNullableString(reader, 13),
                ReadNullableString(reader, 14),
                ReadNullableString(reader, 15),
                ReadNullableString(reader, 16)));
        }

        return items;
    }

    private async Task<IReadOnlyList<LookupItemDto>> QueryLookupAsync(string sql, CancellationToken cancellationToken)
    {
        var items = new List<LookupItemDto>();
        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new LookupItemDto(reader.GetInt32(0), reader.GetString(1)));
        }

        return items;
    }

    private static void AddCongTrinhParameters(SqliteCommand command, CongTrinhUpsertRequest request)
    {
        command.Parameters.AddWithValue("$tenCongTrinh", DbValue(request.TenCongTrinh));
        command.Parameters.AddWithValue("$maHieu", DbValue(request.MaHieu));
        command.Parameters.AddWithValue("$capCongTrinhId", DbValue(request.CapCongTrinhId));
        command.Parameters.AddWithValue("$loaiCongTrinhId", DbValue(request.LoaiCongTrinhId));
        command.Parameters.AddWithValue("$donViQuanLyId", DbValue(request.DonViQuanLyId));
        command.Parameters.AddWithValue("$donViHanhChinhId", DbValue(request.DonViHanhChinhId));
        command.Parameters.AddWithValue("$diaDiem", DbValue(request.DiaDiem));
        command.Parameters.AddWithValue("$duLieuGIS", DbValue(request.DuLieuGIS));
        command.Parameters.AddWithValue("$namXayDung", DbValue(request.NamXayDung));
        command.Parameters.AddWithValue("$moTa", DbValue(request.MoTa));
        command.Parameters.AddWithValue("$trangThai", DbValue(request.TrangThai));
        command.Parameters.AddWithValue("$hinhAnh", DbValue(request.HinhAnh));
    }

    private static object DbValue<T>(T? value) => value is null ? DBNull.Value : value;

    private static string? ReadNullableString(SqliteDataReader reader, int ordinal) =>
        reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);

    private static int? ReadNullableInt(SqliteDataReader reader, int ordinal) =>
        reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);

    private const string BaseCongTrinhSelect = """
        SELECT
            ct.Id,
            ct.TenCongTrinh,
            ct.MaHieu,
            ct.CapCongTrinhId,
            ct.LoaiCongTrinhId,
            ct.DonViQuanLyId,
            ct.DonViHanhChinhId,
            ct.DiaDiem,
            ct.DuLieuGIS,
            ct.NamXayDung,
            ct.MoTa,
            ct.TrangThai,
            ct.HinhAnh,
            lc.TenLoai AS LoaiCongTrinh,
            cc.TenCap AS CapCongTrinh,
            dvql.Ten AS DonViQuanLy,
            dvhc.Ten AS DonViHanhChinh
        FROM CongTrinh ct
        LEFT JOIN LoaiCongTrinh lc ON lc.Id = ct.LoaiCongTrinhId
        LEFT JOIN CapCongTrinh cc ON cc.Id = ct.CapCongTrinhId
        LEFT JOIN DonVi dvql ON dvql.Id = ct.DonViQuanLyId
        LEFT JOIN DonVi dvhc ON dvhc.Id = ct.DonViHanhChinhId
        """;
}

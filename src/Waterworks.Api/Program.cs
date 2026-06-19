using Waterworks.Data;
using Waterworks.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebClient", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5179",
                "http://127.0.0.1:5179",
                "http://localhost:5180",
                "http://127.0.0.1:5180")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddSingleton<IWaterworksRepository>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("Waterworks")
        ?? "Data Source=waterworks.local.db";
    return new SqliteWaterworksRepository(connectionString);
});

var app = builder.Build();

var repository = app.Services.GetRequiredService<IWaterworksRepository>();
await repository.InitializeAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("WebClient");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/health", () => Results.Ok(new
{
    service = "Waterworks.Api",
    status = "ok",
    storage = "sqlite-local"
}));

app.MapPost("/api/auth/login", async (LoginRequest request, IWaterworksRepository repo, CancellationToken cancellationToken) =>
{
    var user = await repo.ValidateLoginAsync(request, cancellationToken);
    if (user is null)
    {
        return Results.Unauthorized();
    }

    var response = new LoginResponse(
        user.UserName,
        user.Role,
        user.DisplayName,
        AccessToken: $"mvp-local-token:{user.UserName}:{user.Role}");

    return Results.Ok(response);
});

app.MapGet("/api/menu", () => Results.Ok(MenuCatalog.GetDefault()));

var congTrinh = app.MapGroup("/api/congtrinh");

congTrinh.MapGet("/", async (IWaterworksRepository repo, CancellationToken cancellationToken) =>
    Results.Ok(await repo.GetCongTrinhAsync(cancellationToken)));

congTrinh.MapGet("/{id:int}", async (int id, IWaterworksRepository repo, CancellationToken cancellationToken) =>
{
    var item = await repo.GetCongTrinhByIdAsync(id, cancellationToken);
    return item is null ? Results.NotFound() : Results.Ok(item);
});

congTrinh.MapPost("/", async (CongTrinhUpsertRequest request, IWaterworksRepository repo, CancellationToken cancellationToken) =>
{
    var validation = ValidateCongTrinh(request);
    if (validation is not null)
    {
        return validation;
    }

    var created = await repo.CreateCongTrinhAsync(request, cancellationToken);
    return Results.Created($"/api/congtrinh/{created.Id}", created);
});

congTrinh.MapPut("/{id:int}", async (int id, CongTrinhUpsertRequest request, IWaterworksRepository repo, CancellationToken cancellationToken) =>
{
    var validation = ValidateCongTrinh(request);
    if (validation is not null)
    {
        return validation;
    }

    var updated = await repo.UpdateCongTrinhAsync(id, request, cancellationToken);
    return updated ? Results.NoContent() : Results.NotFound();
});

congTrinh.MapDelete("/{id:int}", async (int id, IWaterworksRepository repo, CancellationToken cancellationToken) =>
{
    var deleted = await repo.DeleteCongTrinhAsync(id, cancellationToken);
    return deleted ? Results.NoContent() : Results.NotFound();
});

congTrinh.MapGet("/lookups/loai", async (IWaterworksRepository repo, CancellationToken cancellationToken) =>
    Results.Ok(await repo.GetLoaiCongTrinhAsync(cancellationToken)));

congTrinh.MapGet("/lookups/cap", async (IWaterworksRepository repo, CancellationToken cancellationToken) =>
    Results.Ok(await repo.GetCapCongTrinhAsync(cancellationToken)));

congTrinh.MapGet("/lookups/donvi", async (IWaterworksRepository repo, CancellationToken cancellationToken) =>
    Results.Ok(await repo.GetDonViAsync(cancellationToken)));

app.MapFallbackToFile("index.html");

app.Run();

static IResult? ValidateCongTrinh(CongTrinhUpsertRequest request)
{
    if (string.IsNullOrWhiteSpace(request.TenCongTrinh))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["tenCongTrinh"] = ["Ten cong trinh la bat buoc."]
        });
    }

    return null;
}

static class MenuCatalog
{
    public static IReadOnlyList<MenuGroupDto> GetDefault() =>
    [
        new("home", "Tong quan",
        [
            new("home", "Trang chu", "/home")
        ]),
        new("congtrinh", "Cong trinh thuy loi",
        [
            new("congtrinh/index", "Danh sach cong trinh", "/congtrinh"),
            new("loaicongtrinh/index", "Loai cong trinh", "/loaicongtrinh"),
            new("capcongtrinh/index", "Cap cong trinh", "/capcongtrinh")
        ]),
        new("ketquatuoi", "Ket qua tuoi tieu",
        [
            new("ketquatuoi/index", "Danh sach ket qua tuoi", "/ketquatuoi"),
            new("thongke/tonghoptoantinh", "Thong ke tong hop", "/thongke")
        ])
    ];
}

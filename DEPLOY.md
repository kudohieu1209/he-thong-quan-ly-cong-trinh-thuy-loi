# Deploy Waterworks Web + API

Ban MVP hien tai gom Blazor WebAssembly va ASP.NET Core API. Khi deploy, API se phuc vu luon static web, nen chi can mot hosting service.

## Cach nhanh nhat de public lau dai

1. Tao mot GitHub repo trong tren tai khoan cua ban, vi du `waterworks-mvp`.
2. Push thu muc nay len repo do.
3. Tao Web Service tren Render/Railway/Fly/Azure App Service tu repo.
4. Chon Docker deploy. Dockerfile o root se build:
   - `Waterworks.Web` thanh static files.
   - `Waterworks.Api` thanh backend.
   - Copy web vao `Waterworks.Api/wwwroot`.
5. Sau deploy, mo URL hosting va kiem tra:
   - `/api/health`
   - `/login`
   - Dang nhap demo: `admin` / `1234`

## Render

Repo nay da co `render.yaml`, nen Render co the doc cau hinh tu dong. Service dang khai bao `runtime: docker`, `plan: free`, va health check `/api/health`.

Luu y ban MVP dang dung SQLite local trong container:

```text
ConnectionStrings__Waterworks=Data Source=/tmp/waterworks.local.db
```

Cach nay phu hop public demo nhanh. Du lieu co the bi reset khi service restart/redeploy vi filesystem free service la tam thoi. Khi can du lieu that cho nhieu nguoi dung, chuyen connection string sang PostgreSQL/Neon/Supabase va them repository PostgreSQL.

## Can minh lam tiep

Neu ban gui GitHub remote URL, vi du:

```text
https://github.com/<user>/waterworks-mvp.git
```

minh co the init git, commit source, gan remote va push len cho ban. Sau do ban chi can vao hosting connect repo.

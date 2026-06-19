using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace WinApp.Views.BanDo
{
    public class MapControl : UserControl
    {
        private GMapControl gMapControl;
        private GMapOverlay markersOverlay;
        private Panel panelTop;
        private Label lblTitle;
        private Label lblCount;
        private TextBox txtSearch;
        private Button btnSearch;
        private List<Models.CongTrinh> allData;

        public MapControl()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.panelTop = new Panel();
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 100;
            this.panelTop.BackColor = Color.FromArgb(41, 128, 185);
            this.panelTop.Padding = new Padding(20, 0, 20, 0);

            this.lblTitle = new Label();
            this.lblTitle.Text = "Bản đồ công trình thủy lợi";
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitle.Location = new Point(20, 0);
            this.lblTitle.Size = new Size(500, 60);

            this.lblCount = new Label();
            this.lblCount.Text = "";
            this.lblCount.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblCount.ForeColor = Color.FromArgb(236, 240, 241);
            this.lblCount.AutoSize = false;
            this.lblCount.TextAlign = ContentAlignment.MiddleRight;
            this.lblCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblCount.Location = new Point(this.Width - 220, 0);
            this.lblCount.Size = new Size(200, 60);

            Panel pnlSearch = new Panel();
            pnlSearch.Location = new Point(20, 62);
            pnlSearch.Size = new Size(420, 32);
            pnlSearch.BackColor = Color.White;
            pnlSearch.BorderStyle = BorderStyle.FixedSingle;

            Label lblIcon = new Label();
            lblIcon.Text = "🔍";
            lblIcon.Font = new Font("Segoe UI", 11F);
            lblIcon.Location = new Point(8, 5);
            lblIcon.Size = new Size(25, 22);
            lblIcon.BackColor = Color.Transparent;

            this.txtSearch = new TextBox();
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.Location = new Point(35, 5);
            this.txtSearch.Size = new Size(300, 22);
            this.txtSearch.BorderStyle = BorderStyle.None;
            this.txtSearch.KeyPress += TxtSearch_KeyPress;

            this.btnSearch = new Button();
            this.btnSearch.Text = "Tìm";
            this.btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSearch.Location = new Point(340, 3);
            this.btnSearch.Size = new Size(70, 24);
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Cursor = Cursors.Hand;
            this.btnSearch.Click += BtnSearch_Click;

            pnlSearch.Controls.Add(lblIcon);
            pnlSearch.Controls.Add(this.txtSearch);
            pnlSearch.Controls.Add(this.btnSearch);

            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblCount);
            this.panelTop.Controls.Add(pnlSearch);

            // GMapControl
            this.gMapControl = new GMapControl();
            this.gMapControl.Dock = DockStyle.Fill;
            this.gMapControl.Location = new Point(0, 100);
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.EmptyTileColor = Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 18;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomEnabled = true;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Zoom = 6D;

            // Add controls
            this.Controls.Add(this.gMapControl);
            this.Controls.Add(this.panelTop);

            this.Name = "MapControl";
            this.Size = new Size(1200, 800);

            this.ResumeLayout(false);
        }

        private void InitializeMap()
        {
            try
            {
                GMaps.Instance.Mode = AccessMode.ServerAndCache;
                gMapControl.MapProvider = GMapProviders.GoogleMap;

                gMapControl.Position = new PointLatLng(16.0544, 108.2022); // Đà Nẵng
                gMapControl.MinZoom = 5;
                gMapControl.MaxZoom = 18;
                gMapControl.Zoom = 6;

                gMapControl.ShowCenter = false;
                gMapControl.DragButton = MouseButtons.Left;

                markersOverlay = new GMapOverlay("markers");
                gMapControl.Overlays.Add(markersOverlay);

                System.Diagnostics.Debug.WriteLine("Map initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeMap Error: {ex.Message}");
                MessageBox.Show($"Lỗi khởi tạo bản đồ: {ex.Message}\n\nVui lòng kiểm tra kết nối internet!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDuLieu(List<Models.CongTrinh> data)
        {
            try
            {
                if (data == null || data.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu công trình!", "Thông báo");
                    return;
                }

                allData = data;
                markersOverlay.Markers.Clear();

                int count = 0;
                foreach (var ct in data)
                {
                    var coords = ParseCoordinates(ct.DuLieuGIS);
                    if (coords.HasValue)
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(
                            coords.Value,
                            GMarkerGoogleType.red_big_stop
                        );
                        marker.ToolTipText = $"{ct.TenCongTrinh}\n" +
                                           $"Mã: {ct.MaHieu}\n" +
                                           $"{ct.DiaDiem}";
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.Tag = ct;

                        markersOverlay.Markers.Add(marker);
                        count++;
                    }
                }

                if (markersOverlay.Markers.Count > 0)
                {
                    gMapControl.ZoomAndCenterMarkers("markers");
                }
                lblCount.Text = $"{count}/{data.Count} công trình";

                System.Diagnostics.Debug.WriteLine($"Loaded {count} markers on map");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi");
            }
        }

        private PointLatLng? ParseCoordinates(string gisData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gisData))
                    return null;

                var parts = gisData.Split(',');
                if (parts.Length >= 2)
                {
                    string latStr = parts[0].Trim().Replace("lat:", "").Replace("latitude:", "");
                    string lngStr = parts[1].Trim().Replace("lng:", "").Replace("lon:", "");

                    if (double.TryParse(latStr, out double lat) &&
                        double.TryParse(lngStr, out double lng))
                    {
                        return new PointLatLng(lat, lng);
                    }
                }
            }
            catch { }
            return null;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchCongTrinh();
        }
        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SearchCongTrinh();
            }
        }
        private void SearchCongTrinh()
        {
            try
            {
                string keyword = txtSearch.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo");
                    return;
                }

                if (allData == null || allData.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu!", "Thông báo");
                    return;
                }

                // Tìm công trình theo tên
                var results = allData.FindAll(ct =>
                    ct.TenCongTrinh.ToLower().Contains(keyword) ||
                    ct.MaHieu.ToLower().Contains(keyword)
                );

                if (results.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy công trình nào với từ khóa: {txtSearch.Text}", "Kết quả tìm kiếm");
                    return;
                }

                markersOverlay.Markers.Clear();

                int count = 0;
                foreach (var ct in results)
                {
                    var coords = ParseCoordinates(ct.DuLieuGIS);
                    if (coords.HasValue)
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(
                            coords.Value,
                            GMarkerGoogleType.green_big_go
                        );

                        marker.ToolTipText = $"✓ {ct.TenCongTrinh}\n" +
                                           $"Mã: {ct.MaHieu}\n" +
                                           $"{ct.DiaDiem}";
                        marker.ToolTipMode = MarkerTooltipMode.Always;
                        marker.Tag = ct;

                        markersOverlay.Markers.Add(marker);
                        count++;
                    }
                }

                // Zoom vào kết quả
                if (markersOverlay.Markers.Count > 0)
                {
                    if (markersOverlay.Markers.Count == 1)
                    {
                        gMapControl.Position = markersOverlay.Markers[0].Position;
                        gMapControl.Zoom = 15;
                    }
                    else
                    {
                        gMapControl.ZoomAndCenterMarkers("markers");
                    }
                }

                lblCount.Text = $"Tìm thấy: {count} công trình";
                MessageBox.Show($"Tìm thấy {count} công trình!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gMapControl?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
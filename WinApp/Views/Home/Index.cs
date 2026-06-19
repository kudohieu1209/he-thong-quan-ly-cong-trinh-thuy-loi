using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Vst.Controls;

namespace WinApp.Views.Home
{
    class Missing : Index
    {
        protected override void BuildContent(string roleName)
        {
            PrepareSurface();
            AddHero(
                "Chức năng chưa sẵn sàng",
                "Màn hình này chưa được phát triển. Hãy quay lại trang chủ hoặc chọn một chức năng khác trong thanh bên.");
            AddFootnote("Gợi ý: các nhóm chức năng nằm ở thanh bên trái và được cập nhật theo vai trò đăng nhập.");
        }
    }

    class Index : BaseView<MyScrollViewer>
    {
        private static readonly Brush Surface = BrushFrom("#FFFFFF");
        private static readonly Brush SurfaceSoft = BrushFrom("#FAFAFC");
        private static readonly Brush TextPrimary = BrushFrom("#1D1D1F");
        private static readonly Brush TextSecondary = BrushFrom("#6E6E73");
        private static readonly Brush Accent = BrushFrom("#007AFF");
        private static readonly Brush AccentSoft = BrushFrom("#EAF4FF");
        private static readonly Brush BorderSoft = BrushFrom("#E5E5EA");

        private static Brush BrushFrom(string color)
        {
            return (Brush)new BrushConverter().ConvertFromString(color);
        }

        protected void PrepareSurface()
        {
            MainView.Padding = new Thickness(42, 34, 42, 40);
            MainView.Background = Brushes.Transparent;
            MainView.MaxWidth = 1120;
            MainView.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private TextBlock Text(string value, double size, Brush color, FontWeight? weight = null)
        {
            return new TextBlock
            {
                Text = value,
                FontSize = size,
                Foreground = color,
                FontWeight = weight ?? FontWeights.Normal,
                TextWrapping = TextWrapping.Wrap,
                LineHeight = size * 1.35,
            };
        }

        private TextBlock SectionTitle(string value)
        {
            var title = Text(value, 18, TextPrimary, FontWeights.SemiBold);
            title.Margin = new Thickness(0, 24, 0, 10);
            MainView.Children.Add(title);
            return title;
        }

        protected void AddHero(string title, string subtitle)
        {
            var eyebrow = Text("TỔNG QUAN", 12, TextSecondary, FontWeights.SemiBold);
            eyebrow.Margin = new Thickness(0, 0, 0, 10);
            MainView.Children.Add(eyebrow);

            var heading = Text(title, 32, TextPrimary, FontWeights.SemiBold);
            heading.Margin = new Thickness(0, 0, 0, 8);
            MainView.Children.Add(heading);

            var sub = Text(subtitle, 15, TextSecondary);
            sub.MaxWidth = 760;
            sub.Margin = new Thickness(0, 0, 0, 26);
            MainView.Children.Add(sub);
        }

        private Border GroupBox()
        {
            return new Border
            {
                Background = Surface,
                BorderBrush = BorderSoft,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(14),
                Margin = new Thickness(0, 0, 0, 10),
            };
        }

        private void AddMetricRow()
        {
            SectionTitle("Tổng quan hôm nay");

            var row = new WrapPanel();
            row.Children.Add(MetricTile("Quản lý", "Công trình, bản đồ, kỹ thuật", "4 nhóm"));
            row.Children.Add(MetricTile("Vận hành", "Bảo trì, nhật ký, mùa vụ", "Liên tục"));
            row.Children.Add(MetricTile("Báo cáo", "Tưới tiêu và số lượng công trình", "Tổng hợp"));
            MainView.Children.Add(row);
        }

        private Border MetricTile(string title, string body, string badge)
        {
            var tile = new Border
            {
                Background = SurfaceSoft,
                BorderBrush = BorderSoft,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(14),
                Padding = new Thickness(18),
                Width = 300,
                MinHeight = 112,
                Margin = new Thickness(0, 0, 14, 14),
            };

            var panel = new StackPanel();
            panel.Children.Add(Text(title, 16, TextPrimary, FontWeights.SemiBold));

            var description = Text(body, 13, TextSecondary);
            description.Margin = new Thickness(0, 7, 0, 12);
            panel.Children.Add(description);

            panel.Children.Add(new Border
            {
                Background = AccentSoft,
                CornerRadius = new CornerRadius(999),
                Padding = new Thickness(9, 4, 9, 4),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = Text(badge, 12, Accent, FontWeights.SemiBold),
            });

            tile.Child = panel;
            return tile;
        }

        private Vst.Controls.Button LinkButton(string text, string url)
        {
            return new Vst.Controls.Button
            {
                Text = text,
                Url = url,
                Width = 72,
                Height = 30,
                Background = AccentSoft,
                BorderBrush = AccentSoft,
                Foreground = Accent,
                BorderRadius = 999,
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
            };
        }

        private void AddQuickActions(string roleName)
        {
            SectionTitle("Thao tác nhanh");

            Tuple<string, string, string, string>[] actions = roleName == "Developer"
                ? new[]
                {
                    Tuple.Create("Nạp cơ sở dữ liệu", "Đồng bộ cấu trúc và dữ liệu mẫu phục vụ kiểm thử.", "Mở", "migrate"),
                    Tuple.Create("Sinh mã C#", "Tạo model, controller và view từ cấu hình dữ liệu.", "C#", "migrate/csharp"),
                    Tuple.Create("Sinh SQL", "Chuẩn bị procedure và script cơ sở dữ liệu.", "SQL", "migrate/sql"),
                }
                : new[]
                {
                    Tuple.Create("Công trình thủy lợi", "Quản lý danh sách, bản đồ và chi tiết kỹ thuật.", "Mở", "congtrinh/index"),
                    Tuple.Create("Hành chính", "Quản lý đơn vị cấp huyện, xã và dữ liệu nền.", "Mở", "hanhchinh"),
                    Tuple.Create("Tài liệu", "Tra cứu hồ sơ, văn bản pháp lý và tài liệu chung.", "Mở", "tailieu/index"),
                    Tuple.Create("Thống kê", "Xem báo cáo tổng hợp theo loại, cấp và địa bàn.", "Mở", "thongkecongtrinh/toantinh"),
                };

            var group = GroupBox();
            var stack = new StackPanel();

            for (int i = 0; i < actions.Length; i++)
            {
                var item = actions[i];
                stack.Children.Add(ActionRow(item.Item1, item.Item2, item.Item3, item.Item4, i < actions.Length - 1));
            }

            group.Child = stack;
            MainView.Children.Add(group);
        }

        private Border ActionRow(string title, string body, string buttonText, string url, bool hasDivider)
        {
            var row = new Border
            {
                BorderBrush = BorderSoft,
                BorderThickness = new Thickness(0, 0, 0, hasDivider ? 1 : 0),
                Padding = new Thickness(18, 14, 16, 14),
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var copy = new StackPanel();
            copy.Children.Add(Text(title, 15, TextPrimary, FontWeights.SemiBold));

            var description = Text(body, 13, TextSecondary);
            description.Margin = new Thickness(0, 4, 22, 0);
            copy.Children.Add(description);

            grid.Children.Add(copy);

            var button = LinkButton(buttonText, url);
            button.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(button);

            row.Child = grid;
            return row;
        }

        protected void AddFootnote(string text)
        {
            var note = Text(text, 13, TextSecondary);
            note.Margin = new Thickness(2, 12, 0, 0);
            MainView.Children.Add(note);
        }

        protected virtual void BuildContent(string roleName)
        {
            PrepareSurface();
            var displayRole = roleName ?? "Guest";

            AddHero(
                "Bảng điều khiển công trình thủy lợi",
                "Quản lý dữ liệu, vận hành và báo cáo trong một không gian gọn, rõ và ít gây nhiễu hơn.");
            AddMetricRow();
            AddQuickActions(displayRole);
            AddFootnote($"Đang đăng nhập với vai trò {displayRole}. Chọn nhóm ở thanh bên trái để mở thêm chức năng.");
        }

        public Index()
        {
            string roleName = null;
            if (App.User != null)
            {
                roleName = App.User.GetType().Name;
            }

            BuildContent(roleName);
        }
    }
}

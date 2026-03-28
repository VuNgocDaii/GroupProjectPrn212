using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class DangKyView : Page
    {
        private readonly DangKyBLL _bll = new DangKyBLL();
        private readonly HocVienBLL _hocVienBLL = new HocVienBLL();
        private readonly LopHocBLL _lopHocBLL = new LopHocBLL();
        private DangKy? _selected;

        public DangKyView()
        {
            InitializeComponent();
            LoadComboboxes();
            LoadData();
        }

        private void LoadComboboxes()
        {
            cbHocVien.ItemsSource = _hocVienBLL.GetAll();
            cbLopHoc.ItemsSource = _lopHocBLL.GetAll();
        }

        private void LoadData() => dgDangKy.ItemsSource = _bll.GetAll();

        private DangKy BuildEntityFromForm()
        {
            return new DangKy
            {
                DangKyId = _selected?.DangKyId ?? 0,
                HocVienId = cbHocVien.SelectedValue is int hv ? hv : 0,
                LopHocId = cbLopHoc.SelectedValue is int lh ? lh : 0,
                NgayDangKy = dpNgayDangKy.SelectedDate.HasValue ? DateOnly.FromDateTime(dpNgayDangKy.SelectedDate.Value) : DateOnly.FromDateTime(System.DateTime.Now),
                TrangThai = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text,
                IsDeleted = false
            };
        }

        private void FillForm(DangKy x)
        {
            cbHocVien.SelectedValue = x.HocVienId;
            cbLopHoc.SelectedValue = x.LopHocId;
            dpNgayDangKy.SelectedDate = x.NgayDangKy.ToDateTime(TimeOnly.MinValue);
            cbTrangThai.Text = x.TrangThai;
        }

        private void ClearForm()
        {
            _selected = null;
            cbHocVien.SelectedIndex = -1;
            cbLopHoc.SelectedIndex = -1;
            dpNgayDangKy.SelectedDate = null;
            cbTrangThai.SelectedIndex = -1;
            dgDangKy.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn đăng ký cần sửa."); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn đăng ký cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtKeyword.Text.Trim().ToLower();
            dgDangKy.ItemsSource = _bll.GetAll()
                .Where(x => x.HocVien != null && x.HocVien.HoTen != null && x.HocVien.HoTen.ToLower().Contains(keyword))
                .ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgDangKy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgDangKy.SelectedItem as DangKy;
            if (_selected != null) FillForm(_selected);
        }
    }
}

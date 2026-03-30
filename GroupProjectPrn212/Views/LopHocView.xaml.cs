using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class LopHocView : Page
    {
        private readonly LopHocBLL _bll = new LopHocBLL();
        private readonly KhoaHocBLL _khoaHocBLL = new KhoaHocBLL();
        private readonly GiangVienBLL _giangVienBLL = new GiangVienBLL();
        private readonly PhongHocBLL _phongHocBLL = new PhongHocBLL();
        private readonly CaHocBLL _caHocBLL = new CaHocBLL();
        private LopHoc? _selected;

        public LopHocView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                LoadComboboxes();
                LoadData();
            }
        }

        private void LoadComboboxes()
        {
            cbKhoaHoc.ItemsSource = _khoaHocBLL.GetAll();
            cbGiangVien.ItemsSource = _giangVienBLL.GetAll();
            cbPhongHoc.ItemsSource = _phongHocBLL.GetAll();
            cbCaHoc.ItemsSource = _caHocBLL.GetAll();
        }

        private void LoadData()
        {
            dgLopHoc.ItemsSource = null;
            dgLopHoc.ItemsSource = _bll.GetAll();
        }

        private bool ValidateForm(out string message)
        {
            if (!FrontendValidation.IsRequired(txtMaLop.Text))
            {
                message = "Mã lớp không được để trống.";
                txtMaLop.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(txtTenLop.Text))
            {
                message = "Tên lớp không được để trống.";
                txtTenLop.Focus();
                return false;
            }

            if (cbKhoaHoc.SelectedValue == null)
            {
                message = "Vui lòng chọn khóa học.";
                cbKhoaHoc.Focus();
                return false;
            }

            if (cbGiangVien.SelectedValue == null)
            {
                message = "Vui lòng chọn giảng viên.";
                cbGiangVien.Focus();
                return false;
            }

            if (cbPhongHoc.SelectedValue == null)
            {
                message = "Vui lòng chọn phòng học.";
                cbPhongHoc.Focus();
                return false;
            }

            if (cbCaHoc.SelectedValue == null)
            {
                message = "Vui lòng chọn ca học.";
                cbCaHoc.Focus();
                return false;
            }

            if (!dpNgayBatDau.SelectedDate.HasValue)
            {
                message = "Vui lòng chọn ngày bắt đầu.";
                dpNgayBatDau.Focus();
                return false;
            }

            if (!dpNgayKetThuc.SelectedDate.HasValue)
            {
                message = "Vui lòng chọn ngày kết thúc.";
                dpNgayKetThuc.Focus();
                return false;
            }

            if (dpNgayKetThuc.SelectedDate.Value < dpNgayBatDau.SelectedDate.Value)
            {
                message = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.";
                dpNgayKetThuc.Focus();
                return false;
            }

            if (!FrontendValidation.IsPositiveInt(txtSiSoToiDa.Text, out _))
            {
                message = "Sĩ số tối đa phải lớn hơn 0.";
                txtSiSoToiDa.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(cbTrangThai.Text))
            {
                message = "Vui lòng chọn trạng thái.";
                cbTrangThai.Focus();
                return false;
            }

            message = string.Empty;
            return true;
        }

        private LopHoc BuildEntityFromForm()
        {
            int.TryParse(txtSiSoToiDa.Text.Trim(), out int siSo);
            return new LopHoc
            {
                LopHocId = _selected?.LopHocId ?? 0,
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                KhoaHocId = cbKhoaHoc.SelectedValue is int kh ? kh : 0,
                GiangVienId = cbGiangVien.SelectedValue is int gv ? gv : 0,
                PhongHocId = cbPhongHoc.SelectedValue is int ph ? ph : 0,
                CaHocId = cbCaHoc.SelectedValue is int ch ? ch : 0,
                NgayBatDau = dpNgayBatDau.SelectedDate.HasValue ? DateOnly.FromDateTime(dpNgayBatDau.SelectedDate.Value) : null,
                NgayKetThuc = dpNgayKetThuc.SelectedDate.HasValue ? DateOnly.FromDateTime(dpNgayKetThuc.SelectedDate.Value) : null,
                ThuHoc = txtThuHoc.Text.Trim(),
                SiSoToiDa = siSo,
                TrangThai = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text,
                IsDeleted = false
            };
        }

        private void FillForm(LopHoc x)
        {
            txtMaLop.Text = x.MaLop;
            txtTenLop.Text = x.TenLop;
            cbKhoaHoc.SelectedValue = x.KhoaHocId;
            cbGiangVien.SelectedValue = x.GiangVienId;
            cbPhongHoc.SelectedValue = x.PhongHocId;
            cbCaHoc.SelectedValue = x.CaHocId;
            dpNgayBatDau.SelectedDate = x.NgayBatDau?.ToDateTime(TimeOnly.MinValue);
            dpNgayKetThuc.SelectedDate = x.NgayKetThuc?.ToDateTime(TimeOnly.MinValue);
            txtThuHoc.Text = x.ThuHoc;
            txtSiSoToiDa.Text = x.SiSoToiDa.ToString();
            cbTrangThai.Text = x.TrangThai;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaLop.Clear();
            txtTenLop.Clear();
            cbKhoaHoc.SelectedIndex = -1;
            cbGiangVien.SelectedIndex = -1;
            cbPhongHoc.SelectedIndex = -1;
            cbCaHoc.SelectedIndex = -1;
            dpNgayBatDau.SelectedDate = null;
            dpNgayKetThuc.SelectedDate = null;
            txtThuHoc.Clear();
            txtSiSoToiDa.Clear();
            cbTrangThai.SelectedIndex = -1;
            dgLopHoc.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm(out string error)) { MessageBox.Show(error); return; }
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn lớp học cần sửa."); return; }
            if (!ValidateForm(out string error)) { MessageBox.Show(error); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn lớp học cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgLopHoc.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgLopHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgLopHoc.SelectedItem as LopHoc;
            if (_selected != null) FillForm(_selected);
        }
    }
}

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class PhongHocView : Page
    {
        private readonly PhongHocBLL _bll = new PhongHocBLL();
        private PhongHoc? _selected;

        public PhongHocView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            dgPhongHoc.ItemsSource = null;
            dgPhongHoc.ItemsSource = _bll.GetAll();
        }

        private bool ValidateForm(out string message)
        {
            if (!FrontendValidation.IsRequired(txtMaPhong.Text))
            {
                message = "Mã phòng không được để trống.";
                txtMaPhong.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(txtTenPhong.Text))
            {
                message = "Tên phòng không được để trống.";
                txtTenPhong.Focus();
                return false;
            }

            if (!FrontendValidation.IsPositiveInt(txtSucChua.Text, out _))
            {
                message = "Sức chứa phải lớn hơn 0.";
                txtSucChua.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(cbLoaiPhong.Text))
            {
                message = "Vui lòng chọn loại phòng.";
                cbLoaiPhong.Focus();
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

        private PhongHoc BuildEntityFromForm()
        {
            int.TryParse(txtSucChua.Text.Trim(), out int sucChua);
            return new PhongHoc
            {
                PhongHocId = _selected?.PhongHocId ?? 0,
                MaPhong = txtMaPhong.Text.Trim(),
                TenPhong = txtTenPhong.Text.Trim(),
                SucChua = sucChua,
                LoaiPhong = (cbLoaiPhong.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbLoaiPhong.Text,
                TrangThai = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text,
                MoTa = txtMoTa.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(PhongHoc x)
        {
            txtMaPhong.Text = x.MaPhong;
            txtTenPhong.Text = x.TenPhong;
            txtSucChua.Text = x.SucChua.ToString();
            cbLoaiPhong.Text = x.LoaiPhong;
            cbTrangThai.Text = x.TrangThai;
            txtMoTa.Text = x.MoTa;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtSucChua.Clear();
            cbLoaiPhong.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
            txtMoTa.Clear();
            dgPhongHoc.SelectedItem = null;
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
            if (_selected == null) { MessageBox.Show("Chọn phòng học cần sửa."); return; }
            if (!ValidateForm(out string error)) { MessageBox.Show(error); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn phòng học cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgPhongHoc.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgPhongHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgPhongHoc.SelectedItem as PhongHoc;
            if (_selected != null) FillForm(_selected);
        }
    }
}

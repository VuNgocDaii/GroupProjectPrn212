using System;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class TaiKhoanView : Page
    {
        private readonly TaiKhoanBLL _bll = new TaiKhoanBLL();
        private TaiKhoan? _selected;

        public TaiKhoanView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() => dgTaiKhoan.ItemsSource = _bll.GetAll();

        private TaiKhoan BuildEntityFromForm()
        {
            bool.TryParse((cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text, out bool trangThai);

            return new TaiKhoan
            {
                TaiKhoanId = _selected?.TaiKhoanId ?? 0,
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                MatKhau = string.IsNullOrWhiteSpace(txtMatKhau.Password) && _selected != null ? _selected.MatKhau : txtMatKhau.Password.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                VaiTro = (cbVaiTro.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbVaiTro.Text,
                TrangThai = trangThai,
                LanDangNhapCuoi = _selected?.LanDangNhapCuoi,
                IsDeleted = false
            };
        }

        private void FillForm(TaiKhoan x)
        {
            txtTenDangNhap.Text = x.TenDangNhap;
            txtMatKhau.Password = x.MatKhau;
            txtHoTen.Text = x.HoTen;
            cbVaiTro.Text = x.VaiTro;
            cbTrangThai.Text = x.TrangThai.ToString();
        }

        private void ClearForm()
        {
            _selected = null;
            txtTenDangNhap.Clear();
            txtMatKhau.Password = string.Empty;
            txtHoTen.Clear();
            cbVaiTro.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
            dgTaiKhoan.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn tài khoản cần sửa."); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn tài khoản cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgTaiKhoan.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgTaiKhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgTaiKhoan.SelectedItem as TaiKhoan;
            if (_selected != null) FillForm(_selected);
        }
    }
}

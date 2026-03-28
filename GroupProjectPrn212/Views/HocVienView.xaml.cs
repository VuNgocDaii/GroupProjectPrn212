using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class HocVienView : Page
    {
        private readonly HocVienBLL _bll = new HocVienBLL();
        private HocVien? _selected;

        public HocVienView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgHocVien.ItemsSource = _bll.GetAll();
        }

        private HocVien BuildEntityFromForm()
        {
            return new HocVien
            {
                HocVienId = _selected?.HocVienId ?? 0,
                MaHocVien = txtMaHocVien.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dpNgaySinh.SelectedDate.HasValue ? DateOnly.FromDateTime(dpNgaySinh.SelectedDate.Value) : null,
                GioiTinh = (cbGioiTinh.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(HocVien x)
        {
            txtMaHocVien.Text = x.MaHocVien;
            txtHoTen.Text = x.HoTen;
            dpNgaySinh.SelectedDate = x.NgaySinh?.ToDateTime(TimeOnly.MinValue);
            cbGioiTinh.Text = x.GioiTinh;
            txtSoDienThoai.Text = x.SoDienThoai;
            txtDiaChi.Text = x.DiaChi;
            txtEmail.Text = x.Email;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaHocVien.Clear();
            txtHoTen.Clear();
            dpNgaySinh.SelectedDate = null;
            cbGioiTinh.SelectedIndex = -1;
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            dgHocVien.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn học viên cần sửa."); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn học viên cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgHocVien.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtKeyword.Clear();
            LoadData();
        }

        private void dgHocVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgHocVien.SelectedItem as HocVien;
            if (_selected != null) FillForm(_selected);
        }
    }
}

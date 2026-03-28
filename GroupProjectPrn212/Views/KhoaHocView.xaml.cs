using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class KhoaHocView : Page
    {
        private readonly KhoaHocBLL _bll = new KhoaHocBLL();
        private KhoaHoc? _selected;

        public KhoaHocView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() => dgKhoaHoc.ItemsSource = _bll.GetAll();

        private KhoaHoc BuildEntityFromForm()
        {
            decimal.TryParse(txtHocPhi.Text.Trim(), out decimal hocPhi);
            int.TryParse(txtThoiLuong.Text.Trim(), out int thoiLuong);

            return new KhoaHoc
            {
                KhoaHocId = _selected?.KhoaHocId ?? 0,
                MaKhoaHoc = txtMaKhoaHoc.Text.Trim(),
                TenKhoaHoc = txtTenKhoaHoc.Text.Trim(),
                LoaiKhoaHoc = (cbLoaiKhoaHoc.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbLoaiKhoaHoc.Text,
                HocPhi = hocPhi,
                ThoiLuong = thoiLuong,
                MoTa = txtMoTa.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(KhoaHoc x)
        {
            txtMaKhoaHoc.Text = x.MaKhoaHoc;
            txtTenKhoaHoc.Text = x.TenKhoaHoc;
            cbLoaiKhoaHoc.Text = x.LoaiKhoaHoc;
            txtHocPhi.Text = x.HocPhi.ToString();
            txtThoiLuong.Text = x.ThoiLuong?.ToString();
            txtMoTa.Text = x.MoTa;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaKhoaHoc.Clear();
            txtTenKhoaHoc.Clear();
            cbLoaiKhoaHoc.SelectedIndex = -1;
            txtHocPhi.Clear();
            txtThoiLuong.Clear();
            txtMoTa.Clear();
            dgKhoaHoc.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn khóa học cần sửa."); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn khóa học cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgKhoaHoc.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgKhoaHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgKhoaHoc.SelectedItem as KhoaHoc;
            if (_selected != null) FillForm(_selected);
        }
    }
}

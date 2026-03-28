using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class GiangVienView : Page
    {
        private readonly GiangVienBLL _bll = new GiangVienBLL();
        private GiangVien? _selected;

        public GiangVienView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() => dgGiangVien.ItemsSource = _bll.GetAll();

        private GiangVien BuildEntityFromForm()
        {
            return new GiangVien
            {
                GiangVienId = _selected?.GiangVienId ?? 0,
                MaGiangVien = txtMaGiangVien.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                ChuyenMon = (cbChuyenMon.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbChuyenMon.Text,
                SoDienThoai = txtSoDienThoai.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(GiangVien x)
        {
            txtMaGiangVien.Text = x.MaGiangVien;
            txtHoTen.Text = x.HoTen;
            cbChuyenMon.Text = x.ChuyenMon;
            txtSoDienThoai.Text = x.SoDienThoai;
            txtEmail.Text = x.Email;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaGiangVien.Clear();
            txtHoTen.Clear();
            cbChuyenMon.SelectedIndex = -1;
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            dgGiangVien.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn giảng viên cần sửa."); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn giảng viên cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgGiangVien.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgGiangVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgGiangVien.SelectedItem as GiangVien;
            if (_selected != null) FillForm(_selected);
        }
    }
}

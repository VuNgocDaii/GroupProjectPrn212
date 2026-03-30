using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class CaHocView : Page
    {
        private readonly CaHocBLL _bll = new CaHocBLL();
        private CaHoc? _selected;

        public CaHocView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            dgCaHoc.ItemsSource = null;
            dgCaHoc.ItemsSource = _bll.GetAll();
        }

        private bool ValidateForm(out string message)
        {
            if (!FrontendValidation.IsRequired(txtMaCa.Text))
            {
                message = "Mã ca không được để trống.";
                txtMaCa.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(txtTenCa.Text))
            {
                message = "Tên ca không được để trống.";
                txtTenCa.Focus();
                return false;
            }

            if (!TimeOnly.TryParse(txtGioBatDau.Text.Trim(), out TimeOnly start))
            {
                message = "Giờ bắt đầu không đúng định dạng HH:mm.";
                txtGioBatDau.Focus();
                return false;
            }

            if (!TimeOnly.TryParse(txtGioKetThuc.Text.Trim(), out TimeOnly end))
            {
                message = "Giờ kết thúc không đúng định dạng HH:mm.";
                txtGioKetThuc.Focus();
                return false;
            }

            if (!FrontendValidation.IsTimeRangeValid(start, end))
            {
                message = "Giờ kết thúc phải lớn hơn giờ bắt đầu.";
                txtGioKetThuc.Focus();
                return false;
            }

            message = string.Empty;
            return true;
        }

        private CaHoc BuildEntityFromForm()
        {
            TimeOnly.TryParse(txtGioBatDau.Text.Trim(), out TimeOnly start);
            TimeOnly.TryParse(txtGioKetThuc.Text.Trim(), out TimeOnly end);

            return new CaHoc
            {
                CaHocId = _selected?.CaHocId ?? 0,
                MaCa = txtMaCa.Text.Trim(),
                TenCa = txtTenCa.Text.Trim(),
                GioBatDau = start,
                GioKetThuc = end,
                GhiChu = txtGhiChu.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(CaHoc x)
        {
            txtMaCa.Text = x.MaCa;
            txtTenCa.Text = x.TenCa;
            txtGioBatDau.Text = x.GioBatDau.ToString("HH:mm");
            txtGioKetThuc.Text = x.GioKetThuc.ToString("HH:mm");
            txtGhiChu.Text = x.GhiChu;
        }

        private void ClearForm()
        {
            _selected = null;
            txtMaCa.Clear();
            txtTenCa.Clear();
            txtGioBatDau.Clear();
            txtGioKetThuc.Clear();
            txtGhiChu.Clear();
            dgCaHoc.SelectedItem = null;
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
            if (_selected == null) { MessageBox.Show("Chọn ca học cần sửa."); return; }
            if (!ValidateForm(out string error)) { MessageBox.Show(error); return; }
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);
            if (msg == "OK") { LoadData(); ClearForm(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) { MessageBox.Show("Chọn ca học cần xóa."); return; }
            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();
        private void btnSearch_Click(object sender, RoutedEventArgs e) => dgCaHoc.ItemsSource = _bll.Search(txtKeyword.Text.Trim());
        private void btnRefresh_Click(object sender, RoutedEventArgs e) { txtKeyword.Clear(); LoadData(); }

        private void dgCaHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgCaHoc.SelectedItem as CaHoc;
            if (_selected != null) FillForm(_selected);
        }
    }
}

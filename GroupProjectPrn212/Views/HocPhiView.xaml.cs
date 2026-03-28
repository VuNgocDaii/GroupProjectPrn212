using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class HocPhiView : Page
    {
        private readonly HocPhiBLL _bll = new HocPhiBLL();
        private readonly DangKyBLL _dangKyBLL = new DangKyBLL();
        private HocPhi? _selected;

        public HocPhiView()
        {
            InitializeComponent();
            LoadComboboxes();
            LoadData();
        }

        private void LoadComboboxes()
        {
            cbDangKy.ItemsSource = null;
            cbDangKy.ItemsSource = _dangKyBLL.GetAll();
        }

        private void LoadData()
        {
            dgHocPhi.ItemsSource = null;
            dgHocPhi.ItemsSource = _bll.GetAll();
            dgHocPhi.Items.Refresh();
        }

        private HocPhi BuildEntityFromForm()
        {
            decimal.TryParse(txtSoTien.Text.Trim(), out decimal soTien);

            return new HocPhi
            {
                HocPhiId = _selected?.HocPhiId ?? 0,
                DangKyId = cbDangKy.SelectedValue is int dk ? dk : 0,
                SoTien = soTien,
                NgayThu = dpNgayThu.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(dpNgayThu.SelectedDate.Value)
                    : null,
                TrangThaiThanhToan = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text,
                IsDeleted = false
            };
        }

        private void FillForm(HocPhi x)
        {
            cbDangKy.SelectedValue = x.DangKyId;
            txtSoTien.Text = x.SoTien.ToString();
            dpNgayThu.SelectedDate = x.NgayThu?.ToDateTime(TimeOnly.MinValue);
            cbTrangThai.Text = x.TrangThaiThanhToan;
        }

        private void ClearForm()
        {
            _selected = null;
            cbDangKy.SelectedIndex = -1;
            txtSoTien.Clear();
            dpNgayThu.SelectedDate = null;
            cbTrangThai.SelectedIndex = -1;
            dgHocPhi.SelectedItem = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var msg = _bll.Add(BuildEntityFromForm());
            MessageBox.Show(msg);

            if (msg == "OK")
            {
                LoadData();
                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Chọn học phí cần sửa.");
                return;
            }

            int id = _selected.HocPhiId;
            var msg = _bll.Update(BuildEntityFromForm());
            MessageBox.Show(msg);

            if (msg == "OK")
            {
                LoadData();
                var updated = _bll.GetById(id);
                if (updated != null)
                {
                    _selected = updated;
                    FillForm(updated);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Chọn học phí cần xóa.");
                return;
            }

            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgHocPhi.ItemsSource = null;
            dgHocPhi.ItemsSource = _bll.GetUnpaidList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtKeyword.Clear();
            LoadData();
        }

        private void btnMarkPaid_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Chọn học phí.");
                return;
            }

            MessageBox.Show(_bll.MarkAsPaid(_selected));
            LoadData();
            ClearForm();
        }

        private void dgHocPhi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgHocPhi.SelectedItem as HocPhi;
            if (_selected != null)
            {
                FillForm(_selected);
            }
        }
    }
}
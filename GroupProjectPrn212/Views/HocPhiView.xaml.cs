using System.ComponentModel;
using System.Linq;
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
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                LoadComboboxes();
                LoadData();
            }
        }

        private void LoadComboboxes()
        {
            var dangKyList = _dangKyBLL.GetAll()
                .Select(x => new
                {
                    x.DangKyId,
                    DisplayText = $"{x.HocVien?.HoTen ?? ""} - {x.LopHoc?.TenLop ?? ""}"
                })
                .ToList();

            cbDangKy.ItemsSource = null;
            cbDangKy.ItemsSource = dangKyList;
        }

        private void LoadData()
        {
            var data = _bll.GetAll()
                .Select(x => new
                {
                    x.HocPhiId,
                    x.DangKyId,
                    HocVienTen = x.DangKy?.HocVien?.HoTen ?? "",
                    LopHocTen = x.DangKy?.LopHoc?.TenLop ?? "",
                    x.SoTien,
                    x.NgayThu,
                    x.TrangThaiThanhToan,
                    RawData = x
                })
                .ToList();

            dgHocPhi.ItemsSource = null;
            dgHocPhi.ItemsSource = data;
            dgHocPhi.Items.Refresh();
        }

        private bool ValidateForm(out string message)
        {
            if (cbDangKy.SelectedValue == null)
            {
                message = "Vui lòng chọn đăng ký.";
                cbDangKy.Focus();
                return false;
            }

            if (!FrontendValidation.IsPositiveDecimal(txtSoTien.Text, out _))
            {
                message = "Số tiền phải lớn hơn 0.";
                txtSoTien.Focus();
                return false;
            }

            string trangThai = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? cbTrangThai.Text;
            if (!FrontendValidation.IsRequired(trangThai))
            {
                message = "Vui lòng chọn trạng thái thanh toán.";
                cbTrangThai.Focus();
                return false;
            }

            if (trangThai == "Đã đóng" && !dpNgayThu.SelectedDate.HasValue)
            {
                message = "Trạng thái Đã đóng bắt buộc phải có ngày thu.";
                dpNgayThu.Focus();
                return false;
            }

            message = string.Empty;
            return true;
        }

        private HocPhi BuildEntityFromForm()
        {
            decimal.TryParse(txtSoTien.Text.Trim(), out decimal soTien);

            return new HocPhi
            {
                HocPhiId = _selected?.HocPhiId ?? 0,
                DangKyId = cbDangKy.SelectedValue is int dk ? dk : 0,
                SoTien = soTien,
                NgayThu = dpNgayThu.SelectedDate.HasValue ? DateOnly.FromDateTime(dpNgayThu.SelectedDate.Value) : null,
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
            if (!ValidateForm(out string error))
            {
                MessageBox.Show(error, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

            if (!ValidateForm(out string error))
            {
                MessageBox.Show(error, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            dgHocPhi.ItemsSource = _bll.GetUnpaidList()
                .Select(x => new
                {
                    x.HocPhiId,
                    x.DangKyId,
                    HocVienTen = x.DangKy?.HocVien?.HoTen ?? "",
                    LopHocTen = x.DangKy?.LopHoc?.TenLop ?? "",
                    x.SoTien,
                    x.NgayThu,
                    x.TrangThaiThanhToan,
                    RawData = x
                })
                .ToList();
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
            if (dgHocPhi.SelectedItem == null) return;

            var rawProp = dgHocPhi.SelectedItem.GetType().GetProperty("RawData");
            if (rawProp != null)
            {
                _selected = rawProp.GetValue(dgHocPhi.SelectedItem) as HocPhi;
                if (_selected != null)
                {
                    FillForm(_selected);
                }
            }
        }
    }
}

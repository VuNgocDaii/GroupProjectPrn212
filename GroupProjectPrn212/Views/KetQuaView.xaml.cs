using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.Views
{
    public partial class KetQuaView : Page
    {
        private readonly KetQuaBLL _bll = new KetQuaBLL();
        private readonly DangKyBLL _dangKyBLL = new DangKyBLL();
        private KetQua? _selected;

        public KetQuaView()
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
                    x.KetQuaId,
                    x.DangKyId,
                    HocVienTen = x.DangKy?.HocVien?.HoTen ?? "",
                    LopHocTen = x.DangKy?.LopHoc?.TenLop ?? "",
                    x.Diem,
                    x.XepLoai,
                    x.GhiChu,
                    RawData = x
                })
                .ToList();

            dgKetQua.ItemsSource = null;
            dgKetQua.ItemsSource = data;
            dgKetQua.Items.Refresh();
        }

        private bool ValidateForm(out string message)
        {
            if (cbDangKy.SelectedValue == null)
            {
                message = "Vui lòng chọn đăng ký.";
                cbDangKy.Focus();
                return false;
            }

            if (!FrontendValidation.IsScore(txtDiem.Text, out decimal diem))
            {
                message = "Điểm phải nằm trong khoảng từ 0 đến 10.";
                txtDiem.Focus();
                return false;
            }

            txtXepLoai.Text = FrontendValidation.BuildXepLoai(diem);
            message = string.Empty;
            return true;
        }

        private KetQua BuildEntityFromForm()
        {
            decimal.TryParse(txtDiem.Text.Trim(), out decimal diem);

            return new KetQua
            {
                KetQuaId = _selected?.KetQuaId ?? 0,
                DangKyId = cbDangKy.SelectedValue is int dk ? dk : 0,
                Diem = diem,
                XepLoai = FrontendValidation.BuildXepLoai(diem),
                GhiChu = txtGhiChu.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(KetQua x)
        {
            cbDangKy.SelectedValue = x.DangKyId;
            txtDiem.Text = x.Diem?.ToString();
            txtXepLoai.Text = x.XepLoai;
            txtGhiChu.Text = x.GhiChu;
        }

        private void ClearForm()
        {
            _selected = null;
            cbDangKy.SelectedIndex = -1;
            txtDiem.Clear();
            txtXepLoai.Clear();
            txtGhiChu.Clear();
            dgKetQua.SelectedItem = null;
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
                MessageBox.Show("Chọn kết quả cần sửa.");
                return;
            }

            if (!ValidateForm(out string error))
            {
                MessageBox.Show(error, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = _selected.KetQuaId;
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
                MessageBox.Show("Chọn kết quả cần xóa.");
                return;
            }

            MessageBox.Show(_bll.Delete(_selected));
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) => ClearForm();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtKeyword.Text.Trim().ToLower();

            var data = _bll.GetAll()
                .Where(x =>
                    (x.DangKy?.HocVien?.HoTen != null && x.DangKy.HocVien.HoTen.ToLower().Contains(keyword)) ||
                    (x.DangKy?.LopHoc?.TenLop != null && x.DangKy.LopHoc.TenLop.ToLower().Contains(keyword)) ||
                    (x.XepLoai != null && x.XepLoai.ToLower().Contains(keyword)) ||
                    (x.GhiChu != null && x.GhiChu.ToLower().Contains(keyword)))
                .Select(x => new
                {
                    x.KetQuaId,
                    x.DangKyId,
                    HocVienTen = x.DangKy?.HocVien?.HoTen ?? "",
                    LopHocTen = x.DangKy?.LopHoc?.TenLop ?? "",
                    x.Diem,
                    x.XepLoai,
                    x.GhiChu,
                    RawData = x
                })
                .ToList();

            dgKetQua.ItemsSource = null;
            dgKetQua.ItemsSource = data;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtKeyword.Clear();
            LoadData();
        }

        private void txtDiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!decimal.TryParse(txtDiem.Text.Trim(), out decimal diem))
            {
                txtXepLoai.Text = string.Empty;
                return;
            }

            txtXepLoai.Text = FrontendValidation.BuildXepLoai(diem);
        }

        private void dgKetQua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgKetQua.SelectedItem == null) return;

            var rawProp = dgKetQua.SelectedItem.GetType().GetProperty("RawData");
            if (rawProp != null)
            {
                _selected = rawProp.GetValue(dgKetQua.SelectedItem) as KetQua;
                if (_selected != null)
                {
                    FillForm(_selected);
                }
            }
        }
    }
}

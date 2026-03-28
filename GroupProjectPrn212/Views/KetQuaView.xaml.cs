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
            dgKetQua.ItemsSource = null;
            dgKetQua.ItemsSource = _bll.GetAll();
            dgKetQua.Items.Refresh();
        }

        private KetQua BuildEntityFromForm()
        {
            decimal.TryParse(txtDiem.Text.Trim(), out decimal diem);

            return new KetQua
            {
                KetQuaId = _selected?.KetQuaId ?? 0,
                DangKyId = cbDangKy.SelectedValue is int dk ? dk : 0,
                Diem = diem,
                XepLoai = _selected.XepLoai,
                GhiChu = txtGhiChu.Text.Trim(),
                IsDeleted = false
            };
        }

        private void FillForm(KetQua x)
        {
            cbDangKy.SelectedValue = x.DangKyId;
            txtDiem.Text = x.Diem?.ToString();
         
            txtGhiChu.Text = x.GhiChu;
        }

        private void ClearForm()
        {
            _selected = null;
            cbDangKy.SelectedIndex = -1;
            txtDiem.Clear();
           
            txtGhiChu.Clear();
            dgKetQua.SelectedItem = null;
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
                MessageBox.Show("Chọn kết quả cần sửa.");
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

            dgKetQua.ItemsSource = null;
            dgKetQua.ItemsSource = _bll.GetAll().Where(x =>
                (x.DangKy?.HocVien?.HoTen != null && x.DangKy.HocVien.HoTen.ToLower().Contains(keyword)) ||
                (x.DangKy?.LopHoc?.TenLop != null && x.DangKy.LopHoc.TenLop.ToLower().Contains(keyword)) ||
                (x.XepLoai != null && x.XepLoai.ToLower().Contains(keyword)) ||
                (x.GhiChu != null && x.GhiChu.ToLower().Contains(keyword))
            ).ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtKeyword.Clear();
            LoadData();
        }

        private void dgKetQua_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgKetQua.SelectedItem as KetQua;
            if (_selected != null)
            {
                FillForm(_selected);
            }
        }
    }
}
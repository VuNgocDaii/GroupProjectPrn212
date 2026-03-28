using System.Linq;
using System.Windows.Controls;
using GroupProjectPrn212.BLL;

namespace GroupProjectPrn212.Views
{
    public partial class DashboardView : Page
    {
        private readonly HocVienBLL _hocVienBLL = new HocVienBLL();
        private readonly GiangVienBLL _giangVienBLL = new GiangVienBLL();
        private readonly LopHocBLL _lopHocBLL = new LopHocBLL();
        private readonly HocPhiBLL _hocPhiBLL = new HocPhiBLL();

        public DashboardView()
        {
            InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var hocViens = _hocVienBLL.GetAll();
            var giangViens = _giangVienBLL.GetAll();
            var lopHocs = _lopHocBLL.GetAll();
            var hocPhis = _hocPhiBLL.GetAll();

            txtTongHocVien.Text = hocViens.Count.ToString();
            txtTongGiangVien.Text = giangViens.Count.ToString();
            txtTongLopDangMo.Text = lopHocs.Count(x => x.TrangThai == "Đang mở").ToString();
            txtTongDoanhThu.Text = hocPhis
                .Where(x => x.TrangThaiThanhToan == "Đã đóng")
                .Sum(x => x.SoTien)
                .ToString("N0") + " đ";

            dgRecentClasses.ItemsSource = lopHocs;
        }
    }
}

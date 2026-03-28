using System.Windows;
using GroupProjectPrn212.Views;

namespace GroupProjectPrn212
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new DashboardView());
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e) => MainFrame.Navigate(new DashboardView());
        private void NavigateToHocVien(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HocVienView());
        private void NavigateToGiangVien(object sender, RoutedEventArgs e) => MainFrame.Navigate(new GiangVienView());
        private void NavigateToKhoaHoc(object sender, RoutedEventArgs e) => MainFrame.Navigate(new KhoaHocView());
        private void NavigateToPhongHoc(object sender, RoutedEventArgs e) => MainFrame.Navigate(new PhongHocView());
        private void NavigateToCaHoc(object sender, RoutedEventArgs e) => MainFrame.Navigate(new CaHocView());
        private void NavigateToLopHoc(object sender, RoutedEventArgs e) => MainFrame.Navigate(new LopHocView());
        private void NavigateToDangKy(object sender, RoutedEventArgs e) => MainFrame.Navigate(new DangKyView());
        private void NavigateToHocPhi(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HocPhiView());
        private void NavigateToKetQua(object sender, RoutedEventArgs e) => MainFrame.Navigate(new KetQuaView());
        private void NavigateToTaiKhoan(object sender, RoutedEventArgs e) => MainFrame.Navigate(new TaiKhoanView());

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}

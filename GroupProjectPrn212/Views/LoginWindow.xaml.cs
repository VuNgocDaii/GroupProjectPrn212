using System.Windows;
using GroupProjectPrn212.BLL;

namespace GroupProjectPrn212.Views
{
    public partial class LoginWindow : Window
    {
        private readonly TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            var user = _taiKhoanBLL.Login(username, password);
            if (user == null)
            {
                txtMessage.Text = "Sai tên đăng nhập hoặc mật khẩu.";
                return;
            }

            var main = new MainWindow();
            main.Show();
            Close();
        }
    }
}

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

        private bool ValidateForm(out string message)
        {
            if (!FrontendValidation.IsRequired(txtUsername.Text))
            {
                message = "Vui lòng nhập tên đăng nhập.";
                txtUsername.Focus();
                return false;
            }

            if (!FrontendValidation.IsRequired(txtPassword.Password))
            {
                message = "Vui lòng nhập mật khẩu.";
                txtPassword.Focus();
                return false;
            }

            message = string.Empty;
            return true;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm(out string error))
            {
                txtMessage.Text = error;
                return;
            }

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

using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _dal = new TaiKhoanDAL();

        public List<TaiKhoan> GetAll()
        {
            return _dal.GetAll();
        }

        public TaiKhoan? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return _dal.Login(username.Trim(), password.Trim());
        }

        public TaiKhoan? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public TaiKhoan? GetByUsername(string username)
        {
            return _dal.GetByUsername(username.Trim());
        }

        public List<TaiKhoan> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(TaiKhoan entity)
        {
            if (string.IsNullOrWhiteSpace(entity.TenDangNhap))
                return "Tên đăng nhập không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.MatKhau))
                return "Mật khẩu không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên không được để trống.";

            if (_dal.GetByUsername(entity.TenDangNhap) != null)
                return "Tên đăng nhập đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(TaiKhoan entity)
        {
            if (string.IsNullOrWhiteSpace(entity.TenDangNhap))
                return "Tên đăng nhập không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên không được để trống.";

            var existing = _dal.GetByUsername(entity.TenDangNhap);
            if (existing != null && existing.TaiKhoanId != entity.TaiKhoanId)
                return "Tên đăng nhập đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(TaiKhoan entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

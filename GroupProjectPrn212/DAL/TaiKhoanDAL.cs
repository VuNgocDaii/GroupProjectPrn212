using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class TaiKhoanDAL : SoftDeleteRepository<TaiKhoan>
    {
        public TaiKhoan? Login(string username, string password)
        {
            return _context.TaiKhoans.FirstOrDefault(x =>
                x.TenDangNhap == username &&
                x.MatKhau == password &&
                x.TrangThai == true &&
                x.IsDeleted == false);
        }

        public override List<TaiKhoan> GetAll()
        {
            return _context.TaiKhoans
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.TenDangNhap)
                .ToList();
        }

        public TaiKhoan? GetByUsername(string username)
        {
            return _context.TaiKhoans.FirstOrDefault(x =>
                x.TenDangNhap == username &&
                x.IsDeleted == false);
        }

        public List<TaiKhoan> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.TaiKhoans
                .Where(x => x.IsDeleted == false &&
                    (x.TenDangNhap.Contains(keyword) ||
                     x.HoTen.Contains(keyword) ||
                     x.VaiTro.Contains(keyword)))
                .OrderBy(x => x.TenDangNhap)
                .ToList();
        }
    }
}

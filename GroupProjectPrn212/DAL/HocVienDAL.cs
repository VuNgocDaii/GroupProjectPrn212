using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class HocVienDAL : SoftDeleteRepository<HocVien>
    {
        public override List<HocVien> GetAll()
        {
            return _context.HocViens
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaHocVien)
                .ToList();
        }

        public List<HocVien> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.HocViens
                .Where(x => x.IsDeleted == false &&
                    (x.MaHocVien.Contains(keyword) ||
                     x.HoTen.Contains(keyword) ||
                     (x.SoDienThoai != null && x.SoDienThoai.Contains(keyword)) ||
                     (x.Email != null && x.Email.Contains(keyword))))
                .OrderBy(x => x.MaHocVien)
                .ToList();
        }

        public HocVien? GetByCode(string maHocVien)
        {
            return _context.HocViens.FirstOrDefault(x =>
                x.MaHocVien == maHocVien &&
                x.IsDeleted == false);
        }
    }
}

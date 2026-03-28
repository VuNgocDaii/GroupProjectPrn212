using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class PhongHocDAL : SoftDeleteRepository<PhongHoc>
    {
        public override List<PhongHoc> GetAll()
        {
            return _context.PhongHocs
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaPhong)
                .ToList();
        }

        public List<PhongHoc> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.PhongHocs
                .Where(x => x.IsDeleted == false &&
                    (x.MaPhong.Contains(keyword) ||
                     x.TenPhong.Contains(keyword) ||
                     (x.LoaiPhong != null && x.LoaiPhong.Contains(keyword)) ||
                     (x.TrangThai != null && x.TrangThai.Contains(keyword))))
                .OrderBy(x => x.MaPhong)
                .ToList();
        }

        public PhongHoc? GetByCode(string maPhong)
        {
            return _context.PhongHocs.FirstOrDefault(x =>
                x.MaPhong == maPhong &&
                x.IsDeleted == false);
        }
    }
}

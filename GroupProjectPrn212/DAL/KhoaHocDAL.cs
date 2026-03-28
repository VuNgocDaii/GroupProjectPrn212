using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class KhoaHocDAL : SoftDeleteRepository<KhoaHoc>
    {
        public override List<KhoaHoc> GetAll()
        {
            return _context.KhoaHocs
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaKhoaHoc)
                .ToList();
        }

        public List<KhoaHoc> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.KhoaHocs
                .Where(x => x.IsDeleted == false &&
                    (x.MaKhoaHoc.Contains(keyword) ||
                     x.TenKhoaHoc.Contains(keyword) ||
                     (x.LoaiKhoaHoc != null && x.LoaiKhoaHoc.Contains(keyword))))
                .OrderBy(x => x.MaKhoaHoc)
                .ToList();
        }

        public KhoaHoc? GetByCode(string maKhoaHoc)
        {
            return _context.KhoaHocs.FirstOrDefault(x =>
                x.MaKhoaHoc == maKhoaHoc &&
                x.IsDeleted == false);
        }
    }
}

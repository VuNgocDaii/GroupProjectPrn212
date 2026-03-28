using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class GiangVienDAL : SoftDeleteRepository<GiangVien>
    {
        public override List<GiangVien> GetAll()
        {
            return _context.GiangViens
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaGiangVien)
                .ToList();
        }

        public List<GiangVien> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.GiangViens
                .Where(x => x.IsDeleted == false &&
                    (x.MaGiangVien.Contains(keyword) ||
                     x.HoTen.Contains(keyword) ||
                     (x.ChuyenMon != null && x.ChuyenMon.Contains(keyword)) ||
                     (x.Email != null && x.Email.Contains(keyword))))
                .OrderBy(x => x.MaGiangVien)
                .ToList();
        }

        public GiangVien? GetByCode(string maGiangVien)
        {
            return _context.GiangViens.FirstOrDefault(x =>
                x.MaGiangVien == maGiangVien &&
                x.IsDeleted == false);
        }
    }
}

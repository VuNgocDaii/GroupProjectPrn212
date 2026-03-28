using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectPrn212.DAL
{
    public class LopHocDAL : SoftDeleteRepository<LopHoc>
    {
        public override List<LopHoc> GetAll()
        {
            return _context.LopHocs
                .Include(x => x.KhoaHoc)
                .Include(x => x.GiangVien)
                .Include(x => x.PhongHoc)
                .Include(x => x.CaHoc)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaLop)
                .ToList();
        }

        public List<LopHoc> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.LopHocs
                .Include(x => x.KhoaHoc)
                .Include(x => x.GiangVien)
                .Include(x => x.PhongHoc)
                .Include(x => x.CaHoc)
                .Where(x => x.IsDeleted == false &&
                    (x.MaLop.Contains(keyword) ||
                     x.TenLop.Contains(keyword) ||
                     (x.ThuHoc != null && x.ThuHoc.Contains(keyword)) ||
                     (x.TrangThai != null && x.TrangThai.Contains(keyword))))
                .OrderBy(x => x.MaLop)
                .ToList();
        }

        public LopHoc? GetDetailById(int id)
        {
            return _context.LopHocs
                .Include(x => x.KhoaHoc)
                .Include(x => x.GiangVien)
                .Include(x => x.PhongHoc)
                .Include(x => x.CaHoc)
                .FirstOrDefault(x => x.LopHocId == id && x.IsDeleted == false);
        }

        public LopHoc? GetByCode(string maLop)
        {
            return _context.LopHocs.FirstOrDefault(x =>
                x.MaLop == maLop &&
                x.IsDeleted == false);
        }
    }
}

using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectPrn212.DAL
{
    public class DangKyDAL : SoftDeleteRepository<DangKy>
    {
        public override List<DangKy> GetAll()
        {
            return _context.DangKies
                .AsNoTracking()
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.NgayDangKy)
                .ToList();
        }

        public List<DangKy> GetByHocVien(int hocVienId)
        {
            return _context.DangKies
                .AsNoTracking()
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .Where(x => x.HocVienId == hocVienId && x.IsDeleted == false)
                .OrderByDescending(x => x.DangKyId)
                .ToList();
        }

        public List<DangKy> GetByLopHoc(int lopHocId)
        {
            return _context.DangKies
                .AsNoTracking()
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .Where(x => x.LopHocId == lopHocId && x.IsDeleted == false)
                .OrderByDescending(x => x.DangKyId)
                .ToList();
        }

        public bool ExistsActiveRegistration(int hocVienId, int lopHocId)
        {
            return _context.DangKies.Any(x =>
                x.HocVienId == hocVienId &&
                x.LopHocId == lopHocId &&
                x.IsDeleted == false);
        }

        public DangKy? GetDetailById(int id)
        {
            return _context.DangKies
                .AsNoTracking()
                .Include(x => x.HocVien)
                .Include(x => x.LopHoc)
                .FirstOrDefault(x => x.DangKyId == id && x.IsDeleted == false);
        }
    }
}
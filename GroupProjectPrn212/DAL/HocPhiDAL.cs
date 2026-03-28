using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectPrn212.DAL
{
    public class HocPhiDAL : SoftDeleteRepository<HocPhi>
    {
        public override List<HocPhi> GetAll()
        {
            return _context.HocPhis
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.HocPhiId)
                .ToList();
        }

        public HocPhi? GetByDangKyId(int dangKyId)
        {
            return _context.HocPhis
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .FirstOrDefault(x => x.DangKyId == dangKyId && x.IsDeleted == false);
        }

        public override HocPhi? GetById(object id)
        {
            if (id is not int hocPhiId) return null;

            return _context.HocPhis
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .FirstOrDefault(x => x.HocPhiId == hocPhiId && x.IsDeleted == false);
        }

        public List<HocPhi> GetUnpaidList()
        {
            return _context.HocPhis
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .Where(x => x.IsDeleted == false && x.TrangThaiThanhToan == "Chưa đóng")
                .OrderByDescending(x => x.HocPhiId)
                .ToList();
        }
    }
}
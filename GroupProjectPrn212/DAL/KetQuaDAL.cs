using GroupProjectPrn212.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProjectPrn212.DAL
{
    public class KetQuaDAL : SoftDeleteRepository<KetQua>
    {
        public override List<KetQua> GetAll()
        {
            return _context.KetQuas
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.KetQuaId)
                .ToList();
        }

        public KetQua? GetByDangKyId(int dangKyId)
        {
            return _context.KetQuas
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .FirstOrDefault(x => x.DangKyId == dangKyId && x.IsDeleted == false);
        }

        public override KetQua? GetById(object id)
        {
            if (id is not int ketQuaId) return null;

            return _context.KetQuas
                .AsNoTracking()
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.HocVien)
                .Include(x => x.DangKy)
                    .ThenInclude(x => x.LopHoc)
                .FirstOrDefault(x => x.KetQuaId == ketQuaId && x.IsDeleted == false);
        }
    }
}
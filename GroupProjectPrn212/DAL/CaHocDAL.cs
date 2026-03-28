using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.DAL
{
    public class CaHocDAL : SoftDeleteRepository<CaHoc>
    {
        public override List<CaHoc> GetAll()
        {
            return _context.CaHocs
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.MaCa)
                .ToList();
        }

        public List<CaHoc> Search(string keyword)
        {
            keyword = keyword?.Trim() ?? string.Empty;

            return _context.CaHocs
                .Where(x => x.IsDeleted == false &&
                    (x.MaCa.Contains(keyword) ||
                     x.TenCa.Contains(keyword)))
                .OrderBy(x => x.MaCa)
                .ToList();
        }

        public CaHoc? GetByCode(string maCa)
        {
            return _context.CaHocs.FirstOrDefault(x =>
                x.MaCa == maCa &&
                x.IsDeleted == false);
        }
    }
}

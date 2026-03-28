using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class KhoaHocBLL
    {
        private readonly KhoaHocDAL _dal = new KhoaHocDAL();

        public List<KhoaHoc> GetAll()
        {
            return _dal.GetAll();
        }

        public KhoaHoc? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public KhoaHoc? GetByCode(string maKhoaHoc)
        {
            return _dal.GetByCode(maKhoaHoc);
        }

        public List<KhoaHoc> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(KhoaHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaKhoaHoc))
                return "Mã khóa học không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenKhoaHoc))
                return "Tên khóa học không được để trống.";

            if (entity.HocPhi < 0)
                return "Học phí không hợp lệ.";

            if (_dal.GetByCode(entity.MaKhoaHoc) != null)
                return "Mã khóa học đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(KhoaHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaKhoaHoc))
                return "Mã khóa học không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenKhoaHoc))
                return "Tên khóa học không được để trống.";

            if (entity.HocPhi < 0)
                return "Học phí không hợp lệ.";

            var existing = _dal.GetByCode(entity.MaKhoaHoc);
            if (existing != null && existing.KhoaHocId != entity.KhoaHocId)
                return "Mã khóa học đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(KhoaHoc entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class PhongHocBLL
    {
        private readonly PhongHocDAL _dal = new PhongHocDAL();

        public List<PhongHoc> GetAll()
        {
            return _dal.GetAll();
        }

        public PhongHoc? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public PhongHoc? GetByCode(string maPhong)
        {
            return _dal.GetByCode(maPhong);
        }

        public List<PhongHoc> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(PhongHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaPhong))
                return "Mã phòng không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenPhong))
                return "Tên phòng không được để trống.";

            if (entity.SucChua <= 0)
                return "Sức chứa phải lớn hơn 0.";

            if (_dal.GetByCode(entity.MaPhong) != null)
                return "Mã phòng đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(PhongHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaPhong))
                return "Mã phòng không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenPhong))
                return "Tên phòng không được để trống.";

            if (entity.SucChua <= 0)
                return "Sức chứa phải lớn hơn 0.";

            var existing = _dal.GetByCode(entity.MaPhong);
            if (existing != null && existing.PhongHocId != entity.PhongHocId)
                return "Mã phòng đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(PhongHoc entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

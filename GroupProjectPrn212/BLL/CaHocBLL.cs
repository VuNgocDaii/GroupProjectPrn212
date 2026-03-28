using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class CaHocBLL
    {
        private readonly CaHocDAL _dal = new CaHocDAL();

        public List<CaHoc> GetAll()
        {
            return _dal.GetAll();
        }

        public CaHoc? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public CaHoc? GetByCode(string maCa)
        {
            return _dal.GetByCode(maCa);
        }

        public List<CaHoc> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(CaHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaCa))
                return "Mã ca không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenCa))
                return "Tên ca không được để trống.";

            if (entity.GioKetThuc <= entity.GioBatDau)
                return "Giờ kết thúc phải lớn hơn giờ bắt đầu.";

            if (_dal.GetByCode(entity.MaCa) != null)
                return "Mã ca đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(CaHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaCa))
                return "Mã ca không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenCa))
                return "Tên ca không được để trống.";

            if (entity.GioKetThuc <= entity.GioBatDau)
                return "Giờ kết thúc phải lớn hơn giờ bắt đầu.";

            var existing = _dal.GetByCode(entity.MaCa);
            if (existing != null && existing.CaHocId != entity.CaHocId)
                return "Mã ca đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(CaHoc entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class HocVienBLL
    {
        private readonly HocVienDAL _dal = new HocVienDAL();

        public List<HocVien> GetAll()
        {
            return _dal.GetAll();
        }

        public HocVien? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public HocVien? GetByCode(string maHocVien)
        {
            return _dal.GetByCode(maHocVien);
        }

        public List<HocVien> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(HocVien entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaHocVien))
                return "Mã học viên không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên học viên không được để trống.";

            if (_dal.GetByCode(entity.MaHocVien) != null)
                return "Mã học viên đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(HocVien entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaHocVien))
                return "Mã học viên không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên học viên không được để trống.";

            var existing = _dal.GetByCode(entity.MaHocVien);
            if (existing != null && existing.HocVienId != entity.HocVienId)
                return "Mã học viên đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(HocVien entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

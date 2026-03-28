using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class GiangVienBLL
    {
        private readonly GiangVienDAL _dal = new GiangVienDAL();

        public List<GiangVien> GetAll()
        {
            return _dal.GetAll();
        }

        public GiangVien? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public GiangVien? GetByCode(string maGiangVien)
        {
            return _dal.GetByCode(maGiangVien);
        }

        public List<GiangVien> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(GiangVien entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaGiangVien))
                return "Mã giảng viên không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên giảng viên không được để trống.";

            if (_dal.GetByCode(entity.MaGiangVien) != null)
                return "Mã giảng viên đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(GiangVien entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaGiangVien))
                return "Mã giảng viên không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.HoTen))
                return "Họ tên giảng viên không được để trống.";

            var existing = _dal.GetByCode(entity.MaGiangVien);
            if (existing != null && existing.GiangVienId != entity.GiangVienId)
                return "Mã giảng viên đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(GiangVien entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

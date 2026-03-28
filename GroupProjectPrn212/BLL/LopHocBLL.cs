using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class LopHocBLL
    {
        private readonly LopHocDAL _dal = new LopHocDAL();

        public List<LopHoc> GetAll()
        {
            return _dal.GetAll();
        }

        public LopHoc? GetById(int id)
        {
            return _dal.GetDetailById(id);
        }

        public LopHoc? GetByCode(string maLop)
        {
            return _dal.GetByCode(maLop);
        }

        public List<LopHoc> Search(string keyword)
        {
            return _dal.Search(keyword);
        }

        public string Add(LopHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaLop))
                return "Mã lớp không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenLop))
                return "Tên lớp không được để trống.";

            if (entity.KhoaHocId <= 0 || entity.GiangVienId <= 0 || entity.PhongHocId <= 0 || entity.CaHocId <= 0)
                return "Thông tin khóa học, giảng viên, phòng học hoặc ca học chưa hợp lệ.";

            if (entity.SiSoToiDa <= 0)
                return "Sĩ số tối đa phải lớn hơn 0.";

            if (_dal.GetByCode(entity.MaLop) != null)
                return "Mã lớp đã tồn tại.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(LopHoc entity)
        {
            if (string.IsNullOrWhiteSpace(entity.MaLop))
                return "Mã lớp không được để trống.";

            if (string.IsNullOrWhiteSpace(entity.TenLop))
                return "Tên lớp không được để trống.";

            if (entity.KhoaHocId <= 0 || entity.GiangVienId <= 0 || entity.PhongHocId <= 0 || entity.CaHocId <= 0)
                return "Thông tin khóa học, giảng viên, phòng học hoặc ca học chưa hợp lệ.";

            if (entity.SiSoToiDa <= 0)
                return "Sĩ số tối đa phải lớn hơn 0.";

            var existing = _dal.GetByCode(entity.MaLop);
            if (existing != null && existing.LopHocId != entity.LopHocId)
                return "Mã lớp đã tồn tại.";

            _dal.Update(entity);
            return "OK";
        }

        public string Delete(LopHoc entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

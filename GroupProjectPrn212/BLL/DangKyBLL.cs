using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class DangKyBLL
    {
        private readonly DangKyDAL _dangKyDAL = new DangKyDAL();
        private readonly LopHocDAL _lopHocDAL = new LopHocDAL();

        public List<DangKy> GetAll()
        {
            return _dangKyDAL.GetAll();
        }

        public DangKy? GetById(int id)
        {
            return _dangKyDAL.GetDetailById(id);
        }

        public List<DangKy> GetByHocVien(int hocVienId)
        {
            return _dangKyDAL.GetByHocVien(hocVienId);
        }

        public List<DangKy> GetByLopHoc(int lopHocId)
        {
            return _dangKyDAL.GetByLopHoc(lopHocId);
        }

        public string Add(DangKy entity)
        {
            if (entity.HocVienId <= 0)
                return "Học viên không hợp lệ.";

            if (entity.LopHocId <= 0)
                return "Lớp học không hợp lệ.";

            if (_dangKyDAL.ExistsActiveRegistration(entity.HocVienId, entity.LopHocId))
                return "Học viên đã đăng ký lớp này rồi.";

            var lop = _lopHocDAL.GetDetailById(entity.LopHocId);
            if (lop == null)
                return "Không tìm thấy lớp học.";

            var currentCount = _dangKyDAL.GetByLopHoc(entity.LopHocId).Count;
            if (currentCount >= lop.SiSoToiDa)
                return "Lớp học đã đủ sĩ số.";

            if (entity.NgayDangKy == default)
                entity.NgayDangKy = DateOnly.FromDateTime(DateTime.Now);

            _dangKyDAL.Add(entity);
            return "OK";
        }

        public string Update(DangKy entity)
        {
            if (entity.HocVienId <= 0 || entity.LopHocId <= 0)
                return "Thông tin đăng ký không hợp lệ.";

            _dangKyDAL.Update(entity);
            return "OK";
        }

        public string Delete(DangKy entity, string deletedBy = "admin")
        {
            _dangKyDAL.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

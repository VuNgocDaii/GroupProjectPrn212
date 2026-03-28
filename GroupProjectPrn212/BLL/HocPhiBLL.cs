using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class HocPhiBLL
    {
        private readonly HocPhiDAL _dal = new HocPhiDAL();

        public List<HocPhi> GetAll()
        {
            return _dal.GetAll();
        }

        public HocPhi? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public HocPhi? GetByDangKyId(int dangKyId)
        {
            return _dal.GetByDangKyId(dangKyId);
        }

        public List<HocPhi> GetUnpaidList()
        {
            return _dal.GetUnpaidList();
        }

        public string Add(HocPhi entity)
        {
            if (entity.DangKyId <= 0)
                return "Đăng ký không hợp lệ.";

            if (entity.SoTien < 0)
                return "Số tiền không hợp lệ.";

            if (_dal.GetByDangKyId(entity.DangKyId) != null)
                return "Đăng ký này đã có học phí.";

            _dal.Add(entity);
            return "OK";
        }

        public string Update(HocPhi entity)
        {
            if (entity.DangKyId <= 0)
                return "Đăng ký không hợp lệ.";

            if (entity.SoTien < 0)
                return "Số tiền không hợp lệ.";

            _dal.Update(entity);
            return "OK";
        }

        public string MarkAsPaid(HocPhi entity)
        {
            entity.TrangThaiThanhToan = "Đã đóng";
            entity.NgayThu = DateOnly.FromDateTime(DateTime.Now);
            _dal.Update(entity);
            return "OK";
        }

        public string Delete(HocPhi entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }
    }
}

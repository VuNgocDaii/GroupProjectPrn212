using GroupProjectPrn212.DAL;
using GroupProjectPrn212.Models;

namespace GroupProjectPrn212.BLL
{
    public class KetQuaBLL
    {
        private readonly KetQuaDAL _dal = new KetQuaDAL();

        public List<KetQua> GetAll()
        {
            return _dal.GetAll();
        }

        public KetQua? GetById(int id)
        {
            return _dal.GetById(id);
        }

        public KetQua? GetByDangKyId(int dangKyId)
        {
            return _dal.GetByDangKyId(dangKyId);
        }

        public string Add(KetQua entity)
        {
            if (entity.DangKyId <= 0)
                return "Đăng ký không hợp lệ.";

            if (entity.Diem < 0 || entity.Diem > 10)
                return "Điểm phải trong khoảng từ 0 đến 10.";

            if (_dal.GetByDangKyId(entity.DangKyId) != null)
                return "Đăng ký này đã có kết quả.";

            entity.XepLoai = BuildXepLoai(entity.Diem);
            _dal.Add(entity);
            return "OK";
        }

        public string Update(KetQua entity)
        {
            if (entity.DangKyId <= 0)
                return "Đăng ký không hợp lệ.";

            if (entity.Diem < 0 || entity.Diem > 10)
                return "Điểm phải trong khoảng từ 0 đến 10.";

            entity.XepLoai = BuildXepLoai(entity.Diem);
            _dal.Update(entity);
            return "OK";
        }

        public string Delete(KetQua entity, string deletedBy = "admin")
        {
            _dal.SoftDelete(entity, deletedBy);
            return "OK";
        }

        private string BuildXepLoai(decimal? diem)
        {
            if (diem == null) return "Chưa xếp loại";
            if (diem >= 8) return "Giỏi";
            if (diem >= 6.5m) return "Khá";
            if (diem >= 5) return "Trung bình";
            return "Yếu";
        }
    }
}

BLL và DAL full cho project WPF + EF Core.
Namespace đang dùng: GroupProjectPrn212

Luu y:
1. Code nay dung QuanLyTrungTamTinHocNgoaiNguContext nam trong folder Models.
2. Code gia dinh cac entity scaffold dung ten property theo SQL da tao.
3. Neu scaffold cua ban sinh DateTime thay vi DateOnly, can doi lai:
   - DateOnly.FromDateTime(DateTime.Now)
   thanh
   - DateTime.Now
4. Neu scaffold cua ban sinh navigation property khac ten, can sua lai o LopHocDAL, DangKyDAL, HocPhiDAL, KetQuaDAL.

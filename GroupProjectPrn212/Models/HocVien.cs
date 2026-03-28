using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class HocVien
{
    public int HocVienId { get; set; }

    public string MaHocVien { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public virtual ICollection<DangKy> DangKies { get; set; } = new List<DangKy>();
}

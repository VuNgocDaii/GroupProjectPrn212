using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VGiangVien
{
    public int GiangVienId { get; set; }

    public string MaGiangVien { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? ChuyenMon { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

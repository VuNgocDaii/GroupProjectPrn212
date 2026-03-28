using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VDangKy
{
    public int DangKyId { get; set; }

    public int HocVienId { get; set; }

    public int LopHocId { get; set; }

    public DateOnly NgayDangKy { get; set; }

    public string TrangThai { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

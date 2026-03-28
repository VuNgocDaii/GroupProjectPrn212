using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VKhoaHoc
{
    public int KhoaHocId { get; set; }

    public string MaKhoaHoc { get; set; } = null!;

    public string TenKhoaHoc { get; set; } = null!;

    public string? LoaiKhoaHoc { get; set; }

    public decimal HocPhi { get; set; }

    public int? ThoiLuong { get; set; }

    public string? MoTa { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

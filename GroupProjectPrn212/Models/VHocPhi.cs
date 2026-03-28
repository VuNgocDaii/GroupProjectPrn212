using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VHocPhi
{
    public int HocPhiId { get; set; }

    public int DangKyId { get; set; }

    public decimal SoTien { get; set; }

    public DateOnly? NgayThu { get; set; }

    public string TrangThaiThanhToan { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

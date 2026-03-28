using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class KetQua
{
    public int KetQuaId { get; set; }

    public int DangKyId { get; set; }

    public decimal? Diem { get; set; }

    public string? XepLoai { get; set; }

    public string? GhiChu { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public virtual DangKy DangKy { get; set; } = null!;
}

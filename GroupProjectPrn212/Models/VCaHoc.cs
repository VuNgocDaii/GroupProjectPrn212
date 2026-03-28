using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VCaHoc
{
    public int CaHocId { get; set; }

    public string MaCa { get; set; } = null!;

    public string TenCa { get; set; } = null!;

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public string? GhiChu { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

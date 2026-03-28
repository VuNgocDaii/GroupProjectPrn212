using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class PhongHoc
{
    public int PhongHocId { get; set; }

    public string MaPhong { get; set; } = null!;

    public string TenPhong { get; set; } = null!;

    public int SucChua { get; set; }

    public string? LoaiPhong { get; set; }

    public string? MoTa { get; set; }

    public string TrangThai { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();
}

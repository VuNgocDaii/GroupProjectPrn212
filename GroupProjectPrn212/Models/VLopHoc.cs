using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class VLopHoc
{
    public int LopHocId { get; set; }

    public string MaLop { get; set; } = null!;

    public string TenLop { get; set; } = null!;

    public int KhoaHocId { get; set; }

    public int GiangVienId { get; set; }

    public int PhongHocId { get; set; }

    public int CaHocId { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public string? ThuHoc { get; set; }

    public int SiSoToiDa { get; set; }

    public string TrangThai { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}

using System;
using System.Collections.Generic;

namespace GroupProjectPrn212.Models;

public partial class DangKy
{
    public int DangKyId { get; set; }

    public int HocVienId { get; set; }

    public int LopHocId { get; set; }

    public DateOnly NgayDangKy { get; set; }

    public string TrangThai { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public virtual HocPhi? HocPhi { get; set; }

    public virtual HocVien HocVien { get; set; } = null!;

    public virtual KetQua? KetQua { get; set; }

    public virtual LopHoc LopHoc { get; set; } = null!;
}

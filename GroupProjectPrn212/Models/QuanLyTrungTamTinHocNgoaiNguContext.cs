using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace GroupProjectPrn212.Models;

public partial class QuanLyTrungTamTinHocNgoaiNguContext : DbContext
{
    public QuanLyTrungTamTinHocNgoaiNguContext()
    {
    }

    public QuanLyTrungTamTinHocNgoaiNguContext(DbContextOptions<QuanLyTrungTamTinHocNgoaiNguContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaHoc> CaHocs { get; set; }

    public virtual DbSet<DangKy> DangKies { get; set; }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<HocPhi> HocPhis { get; set; }

    public virtual DbSet<HocVien> HocViens { get; set; }

    public virtual DbSet<KetQua> KetQuas { get; set; }

    public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }

    public virtual DbSet<LopHoc> LopHocs { get; set; }

    public virtual DbSet<PhongHoc> PhongHocs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<VCaHoc> VCaHocs { get; set; }

    public virtual DbSet<VDangKy> VDangKies { get; set; }

    public virtual DbSet<VGiangVien> VGiangViens { get; set; }

    public virtual DbSet<VHocPhi> VHocPhis { get; set; }

    public virtual DbSet<VHocVien> VHocViens { get; set; }

    public virtual DbSet<VKetQua> VKetQuas { get; set; }

    public virtual DbSet<VKhoaHoc> VKhoaHocs { get; set; }

    public virtual DbSet<VLopHoc> VLopHocs { get; set; }

    public virtual DbSet<VPhongHoc> VPhongHocs { get; set; }

    public virtual DbSet<VTaiKhoan> VTaiKhoans { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=DESKTOP-76CI3B4\\SQLEXPRESS;Database=QuanLyTrungTamTinHocNgoaiNgu;User Id=sa;Password=123456;TrustServerCertificate=True;");


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    private String GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionString"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaHoc>(entity =>
        {
            entity.HasKey(e => e.CaHocId).HasName("PK__CaHoc__18F9CE11240971A7");

            entity.ToTable("CaHoc");

            entity.HasIndex(e => e.MaCa, "UQ__CaHoc__27258E7AAC6C31B5").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.MaCa).HasMaxLength(20);
            entity.Property(e => e.TenCa).HasMaxLength(50);
        });

        modelBuilder.Entity<DangKy>(entity =>
        {
            entity.HasKey(e => e.DangKyId).HasName("PK__DangKy__03CF2CDB15D06358");

            entity.ToTable("DangKy");

            entity.HasIndex(e => new { e.HocVienId, e.LopHocId }, "UQ_DangKy").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Đã đăng ký");

            entity.HasOne(d => d.HocVien).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.HocVienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DangKy_HocVien");

            entity.HasOne(d => d.LopHoc).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DangKy_LopHoc");
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasKey(e => e.GiangVienId).HasName("PK__GiangVie__626127E28BC1991D");

            entity.ToTable("GiangVien");

            entity.HasIndex(e => e.MaGiangVien, "UQ__GiangVie__C03BEEBB64471A9E").IsUnique();

            entity.Property(e => e.ChuyenMon).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaGiangVien).HasMaxLength(20);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<HocPhi>(entity =>
        {
            entity.HasKey(e => e.HocPhiId).HasName("PK__HocPhi__4685B3AFD62F0658");

            entity.ToTable("HocPhi");

            entity.HasIndex(e => e.DangKyId, "UQ__HocPhi__03CF2CDA6AF8564C").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThaiThanhToan).HasMaxLength(30);

            entity.HasOne(d => d.DangKy).WithOne(p => p.HocPhi)
                .HasForeignKey<HocPhi>(d => d.DangKyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HocPhi_DangKy");
        });

        modelBuilder.Entity<HocVien>(entity =>
        {
            entity.HasKey(e => e.HocVienId).HasName("PK__HocVien__28242CDE2B5BC585");

            entity.ToTable("HocVien");

            entity.HasIndex(e => e.MaHocVien, "UQ__HocVien__685B0E6BB1FF96FD").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaHocVien).HasMaxLength(20);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<KetQua>(entity =>
        {
            entity.HasKey(e => e.KetQuaId).HasName("PK__KetQua__0CFF0203283F0B0A");

            entity.ToTable("KetQua");

            entity.HasIndex(e => e.DangKyId, "UQ__KetQua__03CF2CDAF30C8E22").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Diem).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.XepLoai).HasMaxLength(50);

            entity.HasOne(d => d.DangKy).WithOne(p => p.KetQua)
                .HasForeignKey<KetQua>(d => d.DangKyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KetQua_DangKy");
        });

        modelBuilder.Entity<KhoaHoc>(entity =>
        {
            entity.HasKey(e => e.KhoaHocId).HasName("PK__KhoaHoc__AADD6C92A915686E");

            entity.ToTable("KhoaHoc");

            entity.HasIndex(e => e.MaKhoaHoc, "UQ__KhoaHoc__48F0FF99A980587D").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.HocPhi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LoaiKhoaHoc).HasMaxLength(50);
            entity.Property(e => e.MaKhoaHoc).HasMaxLength(20);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenKhoaHoc).HasMaxLength(100);
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.HasKey(e => e.LopHocId).HasName("PK__LopHoc__DBC496202A45DBD0");

            entity.ToTable("LopHoc");

            entity.HasIndex(e => e.MaLop, "UQ__LopHoc__3B98D272746FFC52").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.MaLop).HasMaxLength(20);
            entity.Property(e => e.TenLop).HasMaxLength(100);
            entity.Property(e => e.ThuHoc).HasMaxLength(50);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Đang mở");

            entity.HasOne(d => d.CaHoc).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.CaHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHoc_CaHoc");

            entity.HasOne(d => d.GiangVien).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.GiangVienId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHoc_GiangVien");

            entity.HasOne(d => d.KhoaHoc).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.KhoaHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHoc_KhoaHoc");

            entity.HasOne(d => d.PhongHoc).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.PhongHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHoc_PhongHoc");
        });

        modelBuilder.Entity<PhongHoc>(entity =>
        {
            entity.HasKey(e => e.PhongHocId).HasName("PK__PhongHoc__0CA93ADBD2CFAF75");

            entity.ToTable("PhongHoc");

            entity.HasIndex(e => e.MaPhong, "UQ__PhongHoc__20BD5E5A0C22D536").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
            entity.Property(e => e.MaPhong).HasMaxLength(20);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenPhong).HasMaxLength(100);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Hoạt động");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.TaiKhoanId).HasName("PK__TaiKhoan__9A124B45BFDE7B60");

            entity.ToTable("TaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TaiKhoan__55F68FC0EF5C4650").IsUnique();

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.LanDangNhapCuoi).HasColumnType("datetime");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
            entity.Property(e => e.VaiTro).HasMaxLength(30);
        });

        modelBuilder.Entity<VCaHoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_CaHoc");

            entity.Property(e => e.CaHocId).ValueGeneratedOnAdd();
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.MaCa).HasMaxLength(20);
            entity.Property(e => e.TenCa).HasMaxLength(50);
        });

        modelBuilder.Entity<VDangKy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_DangKy");

            entity.Property(e => e.DangKyId).ValueGeneratedOnAdd();
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(30);
        });

        modelBuilder.Entity<VGiangVien>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_GiangVien");

            entity.Property(e => e.ChuyenMon).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GiangVienId).ValueGeneratedOnAdd();
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaGiangVien).HasMaxLength(20);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<VHocPhi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_HocPhi");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.HocPhiId).ValueGeneratedOnAdd();
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThaiThanhToan).HasMaxLength(30);
        });

        modelBuilder.Entity<VHocVien>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_HocVien");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.HocVienId).ValueGeneratedOnAdd();
            entity.Property(e => e.MaHocVien).HasMaxLength(20);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<VKetQua>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_KetQua");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Diem).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.KetQuaId).ValueGeneratedOnAdd();
            entity.Property(e => e.XepLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<VKhoaHoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_KhoaHoc");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.HocPhi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KhoaHocId).ValueGeneratedOnAdd();
            entity.Property(e => e.LoaiKhoaHoc).HasMaxLength(50);
            entity.Property(e => e.MaKhoaHoc).HasMaxLength(20);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenKhoaHoc).HasMaxLength(100);
        });

        modelBuilder.Entity<VLopHoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_LopHoc");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.LopHocId).ValueGeneratedOnAdd();
            entity.Property(e => e.MaLop).HasMaxLength(20);
            entity.Property(e => e.TenLop).HasMaxLength(100);
            entity.Property(e => e.ThuHoc).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasMaxLength(30);
        });

        modelBuilder.Entity<VPhongHoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_PhongHoc");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.LoaiPhong).HasMaxLength(50);
            entity.Property(e => e.MaPhong).HasMaxLength(20);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.PhongHocId).ValueGeneratedOnAdd();
            entity.Property(e => e.TenPhong).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasMaxLength(30);
        });

        modelBuilder.Entity<VTaiKhoan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_TaiKhoan");

            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.LanDangNhapCuoi).HasColumnType("datetime");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TaiKhoanId).ValueGeneratedOnAdd();
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

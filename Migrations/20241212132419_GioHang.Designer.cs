﻿// <auto-generated />
using System;
using ClothesShoping.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClothesShoping.Migrations
{
    [DbContext(typeof(ClothesShopingDbContext))]
    [Migration("20241212132419_GioHang")]
    partial class GioHang
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClothesShoping.Models.DatHang", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DiaChiGiaoHang")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DienThoaiGiaoHang")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("NgayDatHang")
                        .HasColumnType("datetime2");

                    b.Property<int>("NguoiDungID")
                        .HasColumnType("int");

                    b.Property<int>("TinhTrangID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("NguoiDungID");

                    b.HasIndex("TinhTrangID");

                    b.ToTable("DatHang", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.DatHang_ChiTIet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DatHangID")
                        .HasColumnType("int");

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<short>("SoLuong")
                        .HasColumnType("smallint");

                    b.HasKey("ID");

                    b.HasIndex("DatHangID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("DatHang_ChiTiet", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.GioHang", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SanPhamID")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongTrongGio")
                        .HasColumnType("int");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ThoiGian")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("GioHang", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.HangSanXuat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenHangSanXuat")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenHangSanXuatKhongDau")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("HangSanXuat", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenLoaiKhongDau")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("LoaiSanPham", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.NguoiDung", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DienThoai")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HoVaTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Quyen")
                        .HasColumnType("bit");

                    b.Property<string>("TenDangNhap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("NguoiDung", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.SanPham", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<int>("HangSanXuatID")
                        .HasColumnType("int");

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("LoaiSanPhamID")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("ntext");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenSanPhamKhongDau")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.HasIndex("HangSanXuatID");

                    b.HasIndex("LoaiSanPhamID");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.TinhTrang", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MoTaKhongDau")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.ToTable("TinhTrang", (string)null);
                });

            modelBuilder.Entity("ClothesShoping.Models.DatHang", b =>
                {
                    b.HasOne("ClothesShoping.Models.NguoiDung", "NguoiDung")
                        .WithMany("DatHang")
                        .HasForeignKey("NguoiDungID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClothesShoping.Models.TinhTrang", "TinhTrang")
                        .WithMany("DatHang")
                        .HasForeignKey("TinhTrangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("TinhTrang");
                });

            modelBuilder.Entity("ClothesShoping.Models.DatHang_ChiTIet", b =>
                {
                    b.HasOne("ClothesShoping.Models.DatHang", "DatHang")
                        .WithMany("DatHang_ChiTiet")
                        .HasForeignKey("DatHangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClothesShoping.Models.SanPham", "SanPham")
                        .WithMany("DatHang_ChiTiet")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DatHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ClothesShoping.Models.GioHang", b =>
                {
                    b.HasOne("ClothesShoping.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ClothesShoping.Models.SanPham", b =>
                {
                    b.HasOne("ClothesShoping.Models.HangSanXuat", "HangSanXuat")
                        .WithMany("SanPham")
                        .HasForeignKey("HangSanXuatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClothesShoping.Models.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPham")
                        .HasForeignKey("LoaiSanPhamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HangSanXuat");

                    b.Navigation("LoaiSanPham");
                });

            modelBuilder.Entity("ClothesShoping.Models.DatHang", b =>
                {
                    b.Navigation("DatHang_ChiTiet");
                });

            modelBuilder.Entity("ClothesShoping.Models.HangSanXuat", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ClothesShoping.Models.LoaiSanPham", b =>
                {
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ClothesShoping.Models.NguoiDung", b =>
                {
                    b.Navigation("DatHang");
                });

            modelBuilder.Entity("ClothesShoping.Models.SanPham", b =>
                {
                    b.Navigation("DatHang_ChiTiet");
                });

            modelBuilder.Entity("ClothesShoping.Models.TinhTrang", b =>
                {
                    b.Navigation("DatHang");
                });
#pragma warning restore 612, 618
        }
    }
}

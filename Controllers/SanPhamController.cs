﻿using ClothesShoping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClothesShoping.Models.SanPham;

namespace ClothesShoping.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ClothesShopingDbContext _context;

        public SanPhamController(ClothesShopingDbContext context)
        {
            _context = context;
        }
        
        // GET: Index 
        public IActionResult Index(int? trang)
        {
            var danhSach = LayDanhSachSanPham(trang ?? 1);
            if (danhSach.SanPham.Count == 0)
                return NotFound();
            else
                return View(danhSach);
        }

        private PhanTrangSanPham LayDanhSachSanPham(int trangHienTai)
        {
            int maxRows = 20;

            PhanTrangSanPham phanTrang = new PhanTrangSanPham();
            phanTrang.SanPham = _context.SanPham
                .Include(s => s.HangSanXuat)
                .Include(s => s.LoaiSanPham)
                .OrderBy(r => r.LoaiSanPhamID)
                .Skip((trangHienTai - 1) * maxRows)
                .Take(maxRows).ToList();

            decimal tongSoTrang = Convert.ToDecimal(_context.SanPham.Count()) / Convert.ToDecimal(maxRows);
            phanTrang.TongSoTrang = (int)Math.Ceiling(tongSoTrang);
            phanTrang.TrangHienTai = trangHienTai;

            return phanTrang;
        }
        // GET: PhanLoai 
        public IActionResult PhanLoai(string tenLoai, int? trang)


        {
            var danhSachPhanLoai = LayDanhSachSanPhamTheoPhanLoai(tenLoai, trang ?? 1);
            if (danhSachPhanLoai.SanPham.Count == 0)
                return NotFound();
            else
                return View(danhSachPhanLoai);
        }

        private PhanTrangSanPham LayDanhSachSanPhamTheoPhanLoai(string tenLoai, int trangHienTai)
        {
            int maxRows = 20;

            var sanPhamPhanLoai = _context.SanPham
                .Include(s => s.HangSanXuat)
                .Include(s => s.LoaiSanPham)
                .Where(r => r.LoaiSanPham.TenLoaiKhongDau == tenLoai);

            PhanTrangSanPham phanTrang = new PhanTrangSanPham();
            phanTrang.SanPham = sanPhamPhanLoai.OrderBy(r => r.LoaiSanPhamID)
                .Skip((trangHienTai - 1) * maxRows)
                .Take(maxRows).ToList();

            decimal tongSoTrang = Convert.ToDecimal(sanPhamPhanLoai.Count()) / Convert.ToDecimal(maxRows);
            phanTrang.TongSoTrang = (int)Math.Ceiling(tongSoTrang);
            phanTrang.TrangHienTai = trangHienTai;

            return phanTrang;
        }
        public IActionResult ChiTiet(string tenLoai, string tenSanPham)
        {
            var sanPham = _context.SanPham
                .Include(s => s.HangSanXuat)
                .Include(s => s.LoaiSanPham)
                .Where(r => r.TenSanPhamKhongDau == tenSanPham).SingleOrDefault();
            if (sanPham == null)
                return NotFound();
            else
                return View(sanPham);
        }
        public IActionResult TimKiemSanPhamTheoTen(string tenSanPham,int trang =1)
        {
            int maxRows = 20;
             var sanPhams = _context.SanPham
                .Include(s => s.HangSanXuat)
                .Include(s => s.LoaiSanPham)
                .Where(s => s.TenSanPham.Contains(tenSanPham) || s.LoaiSanPham.TenLoai.Contains(tenSanPham)); 
            var phanTrang = new PhanTrangSanPham
            { 
                SanPham = sanPhams
                .OrderBy(r => r.TenSanPham)
                .Skip((trang - 1) * maxRows)
                .Take(maxRows).ToList(),
                TongSoTrang = (int)Math.Ceiling(sanPhams.Count() / (double)maxRows), 
                TrangHienTai = trang
            }; 
                ViewBag.SearchString = tenSanPham; 
                return View(phanTrang);
        }
    }
}

using ClothesShoping.Logic;
using ClothesShoping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using BC = BCrypt.Net.BCrypt;

namespace ClothesShoping.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class KhachHangController : Controller
    {
        private readonly ClothesShopingDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailLogic _mailLogic;

        public KhachHangController(ClothesShopingDbContext context, IHttpContextAccessor httpContextAccessor, IMailLogic mailLogic)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mailLogic = mailLogic;
        }

        public IActionResult Index(string? successMessage)
        {
            int maNguoiDung = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value);
            var nguoiDung = _context.NguoiDung
                .Where(r => r.ID == maNguoiDung)
                .Include(s => s.DatHang)
                .SingleOrDefault();

            if (nguoiDung == null)
                return NotFound();

            int soLuongDonHang = nguoiDung.DatHang.Count();
            TempData["SoLuongDonHang"] = soLuongDonHang;
            if (!string.IsNullOrEmpty(successMessage))
                TempData["ThongBao"] = successMessage;

            return View(new NguoiDung_ChinhSua(nguoiDung));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatHoSo(int id, [Bind("ID,HoVaTen,Email,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] NguoiDung_ChinhSua nguoiDung)
        {
            if (id != nguoiDung.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var n = await _context.NguoiDung.FindAsync(id);

                    if (nguoiDung.MatKhau == null)
                    {
                        n.HoVaTen = nguoiDung.HoVaTen;
                        n.DienThoai = nguoiDung.DienThoai;
                        n.DiaChi = nguoiDung.DiaChi;
                    }
                    else
                    {
                        n.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                        n.XacNhanMatKhau = BC.HashPassword(nguoiDung.MatKhau);
                    }

                    _context.Update(n);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }

                return RedirectToAction("Index", new { successMessage = "Đã cập nhật thông tin thành công." });
            }

            return View("Index", nguoiDung);
        }

        public IActionResult DatHang()
        {
            var gioHangLogic = new GioHangLogic(_context);
            var gioHang = gioHangLogic.LayGioHang();
            TempData["TongTien"] = gioHangLogic.LayTongTienSanPham();

            return View(gioHang);
        }

        [HttpPost]
        public async Task<IActionResult> DatHang(DatHang datHang)
        {
            var gioHangLogic = new GioHangLogic(_context);
            var gioHang = gioHangLogic.LayGioHang();

            if (string.IsNullOrWhiteSpace(datHang.DienThoaiGiaoHang) || string.IsNullOrWhiteSpace(datHang.DiaChiGiaoHang))
            {
                TempData["TongTien"] = gioHangLogic.LayTongTienSanPham();
                TempData["ThongBaoLoi"] = "Thông tin giao hàng không được bỏ trống.";
                return View(gioHang);
            }

            try
            {
                var dh = new DatHang
                {
                    NguoiDungID = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value),
                    TinhTrangID = 1,
                    DienThoaiGiaoHang = datHang.DienThoaiGiaoHang,
                    DiaChiGiaoHang = datHang.DiaChiGiaoHang,
                    NgayDatHang = DateTime.Now
                };

                _context.DatHang.Add(dh);
                await _context.SaveChangesAsync();

                foreach (var item in gioHang)
                {
                    var ct = new DatHang_ChiTIet
                    {
                        DatHangID = dh.ID,
                        SanPhamID = item.SanPhamID,
                        SoLuong = Convert.ToInt16(item.SoLuongTrongGio),
                        DonGia = item.SanPham.DonGia
                    };
                    _context.DatHang_ChiTiet.Add(ct);
                    await _context.SaveChangesAsync();
                }

                var mailInfo = new MailInfo { Subject = "Đặt hàng thành công tại ClothesShop.Com.Vn" };
                var datHangInfo = _context.DatHang
                    .Where(r => r.ID == dh.ID)
                    .Include(s => s.NguoiDung)
                    .Include(s => s.DatHang_ChiTiet)
                    .SingleOrDefault();

                await _mailLogic.GoiEmailDatHangThanhCong(datHangInfo, mailInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

            return RedirectToAction("DatHangThanhCong");
        }

        public async Task<IActionResult> DonHangCuaToi()
        {
            int maNguoiDung = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value);
            var datHang = _context.DatHang
                .Where(r => r.NguoiDungID == maNguoiDung)
                .Include(d => d.NguoiDung)
                .Include(d => d.TinhTrang)
                .Include(d => d.DatHang_ChiTiet)
                .ThenInclude(s => s.SanPham);

            return View(await datHang.ToListAsync());
        }

        public IActionResult DatHangThanhCong()
        {
            var gioHangLogic = new GioHangLogic(_context);
            gioHangLogic.XoaTatCa();

            return View();
        }
    }
}

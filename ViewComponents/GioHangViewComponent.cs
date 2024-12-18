using ClothesShoping.Logic;
using ClothesShoping.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShoping.ViewComponents
{
    public class GioHangViewComponent : ViewComponent
    {
        private readonly ClothesShopingDbContext _context;

        public GioHangViewComponent(ClothesShopingDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            GioHangLogic gioHangLogic = new GioHangLogic(_context);
            decimal tongTien = gioHangLogic.LayTongTienSanPham();
            decimal tongSoLuong = gioHangLogic.LayTongSoLuong();
            TempData["TopMenu_TongTien"] = tongTien;
            TempData["TopMenu_TongSoLuong"] = tongSoLuong;
            return View("Default");
        }
    }
}

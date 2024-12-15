using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesShoping.Models;

namespace ClothesShoping.Areas.Controllers
{
    public class DatHang_ChiTIetController : Controller
    {
        private readonly ClothesShopingDbContext _context;

        public DatHang_ChiTIetController(ClothesShopingDbContext context)
        {
            _context = context;
        }

        // GET: DatHang_ChiTIet
        public async Task<IActionResult> Index()
        {
            var clothesShopingDbContext = _context.DatHang_ChiTiet.Include(d => d.DatHang).Include(d => d.SanPham);
            return View(await clothesShopingDbContext.ToListAsync());
        }

        // GET: DatHang_ChiTIet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang_ChiTIet = await _context.DatHang_ChiTiet
                .Include(d => d.DatHang)
                .Include(d => d.SanPham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datHang_ChiTIet == null)
            {
                return NotFound();
            }

            return View(datHang_ChiTIet);
        }

        // GET: DatHang_ChiTIet/Create
        public IActionResult Create()
        {
            ViewData["DatHangID"] = new SelectList(_context.DatHang, "ID", "ID");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "ID", "ID");
            return View();
        }

        // POST: DatHang_ChiTIet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DatHangID,SanPhamID,SoLuong,DonGia")] DatHang_ChiTIet datHang_ChiTIet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datHang_ChiTIet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DatHangID"] = new SelectList(_context.DatHang, "ID", "ID", datHang_ChiTIet.DatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "ID", "ID", datHang_ChiTIet.SanPhamID);
            return View(datHang_ChiTIet);
        }

        // GET: DatHang_ChiTIet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang_ChiTIet = await _context.DatHang_ChiTiet.FindAsync(id);
            if (datHang_ChiTIet == null)
            {
                return NotFound();
            }
            ViewData["DatHangID"] = new SelectList(_context.DatHang, "ID", "ID", datHang_ChiTIet.DatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "ID", "ID", datHang_ChiTIet.SanPhamID);
            return View(datHang_ChiTIet);
        }

        // POST: DatHang_ChiTIet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DatHangID,SanPhamID,SoLuong,DonGia")] DatHang_ChiTIet datHang_ChiTIet)
        {
            if (id != datHang_ChiTIet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHang_ChiTIet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHang_ChiTIetExists(datHang_ChiTIet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DatHangID"] = new SelectList(_context.DatHang, "ID", "ID", datHang_ChiTIet.DatHangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "ID", "ID", datHang_ChiTIet.SanPhamID);
            return View(datHang_ChiTIet);
        }

        // GET: DatHang_ChiTIet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang_ChiTIet = await _context.DatHang_ChiTiet
                .Include(d => d.DatHang)
                .Include(d => d.SanPham)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (datHang_ChiTIet == null)
            {
                return NotFound();
            }

            return View(datHang_ChiTIet);
        }

        // POST: DatHang_ChiTIet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datHang_ChiTIet = await _context.DatHang_ChiTiet.FindAsync(id);
            if (datHang_ChiTIet != null)
            {
                _context.DatHang_ChiTiet.Remove(datHang_ChiTIet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatHang_ChiTIetExists(int id)
        {
            return _context.DatHang_ChiTiet.Any(e => e.ID == id);
        }
    }
}

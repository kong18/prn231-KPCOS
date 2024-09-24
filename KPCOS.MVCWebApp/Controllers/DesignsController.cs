using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Common;
using Newtonsoft.Json;
using KPCOS.Service.Base;

namespace KPCOS.MVCWebApp.Controllers
{
    public class DesignsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;

        public DesignsController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: Designs
        public async Task<IActionResult> Index()
        {
            //var fA24_SE1717_PRN231_G4_KPCOSContext = _context.Designs.Include(d => d.Customer).Include(d => d.Template);
            //return View(await fA24_SE1717_PRN231_G4_KPCOSContext.ToListAsync());

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Design"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ProjectResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Design>>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Design>());
        }

        // GET: Designs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs
                .Include(d => d.Customer)
                .Include(d => d.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // GET: Designs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Id");
            return View();
        }

        // POST: Designs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Budget,ConsultantBy,CreateDate,CustomerId,Depth,DesignType,FiltrationSystem,KoiCountRange,KoiType,Location,MinLength,MinWidth,Note,Shape,Status,TemplateId,UpdateDate,WaterLevel,WaterQuality")] Design design)
        {
            if (ModelState.IsValid)
            {
                _context.Add(design);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Id", design.TemplateId);
            return View(design);
        }

        // GET: Designs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs.FindAsync(id);
            if (design == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Id", design.TemplateId);
            return View(design);
        }

        // POST: Designs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Budget,ConsultantBy,CreateDate,CustomerId,Depth,DesignType,FiltrationSystem,KoiCountRange,KoiType,Location,MinLength,MinWidth,Note,Shape,Status,TemplateId,UpdateDate,WaterLevel,WaterQuality")] Design design)
        {
            if (id != design.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(design);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignExists(design.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Id", design.TemplateId);
            return View(design);
        }

        // GET: Designs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var design = await _context.Designs
                .Include(d => d.Customer)
                .Include(d => d.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (design == null)
            {
                return NotFound();
            }

            return View(design);
        }

        // POST: Designs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var design = await _context.Designs.FindAsync(id);
            if (design != null)
            {
                _context.Designs.Remove(design);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignExists(string id)
        {
            return _context.Designs.Any(e => e.Id == id);
        }
    }
}

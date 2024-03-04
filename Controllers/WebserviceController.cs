using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebserviceApp.Data;
using WebserviceApp.Models;

namespace WebserviceApp.Controllers
{

    public class WebserviceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebserviceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Webservice
        public async Task<IActionResult> Index()
        {
            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return NotFound();
            }
            return View(await _context.Webservices.ToListAsync());
        }

        // GET: Webservice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return NotFound();
            }

            var webserviceModel = await _context.Webservices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webserviceModel == null)
            {
                return NotFound();
            }

            return View(webserviceModel);
        }

        // GET: Webservice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Webservice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Url,ApiKeyRequired")] WebserviceModel webserviceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webserviceModel);

                // add logged in user to CreatedBy
                webserviceModel.CreatedBy = User.Identity?.Name ?? "Unknown";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webserviceModel);
        }

        // GET: Webservice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return NotFound();
            }
            var webserviceModel = await _context.Webservices.FindAsync(id);
            if (webserviceModel == null)
            {
                return NotFound();
            }
            return View(webserviceModel);
        }

        // POST: Webservice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Url,ApiKeyRequired")] WebserviceModel webserviceModel)
        {
            if (id != webserviceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webserviceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebserviceModelExists(webserviceModel.Id))
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
            return View(webserviceModel);
        }

        // GET: Webservice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return NotFound();
            }

            var webserviceModel = await _context.Webservices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webserviceModel == null)
            {
                return NotFound();
            }

            return View(webserviceModel);
        }

        // POST: Webservice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return NotFound();
            }
            var webserviceModel = await _context.Webservices.FindAsync(id);
            if (webserviceModel != null)
            {
                _context.Webservices.Remove(webserviceModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebserviceModelExists(int id)
        {
            // check if _context.Webservices is null 
            if (_context.Webservices == null)
            {
                // if it is, return NotFound
                return false;
            }
            return _context.Webservices.Any(e => e.Id == id);
        }
    }
}

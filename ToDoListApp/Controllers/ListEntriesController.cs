using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ToDoListApp.Controllers
{
    public class ListEntriesController : Controller
    {
        private readonly ToDoListAppContext _context;

        public ListEntriesController(ToDoListAppContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: ListEntries
        public async Task<IActionResult> Index() 
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Username == User.Identity.Name);
            var toDoListAppContext = _context.ListEntry.Where(l => l.UserID == currentUser.ID);//<---------------
            return View(await toDoListAppContext.ToListAsync());
            
        }

        // GET: ListEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listEntry = await _context.ListEntry
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (listEntry == null)
            {
                return NotFound();
            }

            return View(listEntry);
        }

        // GET: ListEntries/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID");
            return View();
        }

        // POST: ListEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Entry,UserID")] ListEntry listEntry)
        {
            if (ModelState.IsValid)
            {
                listEntry.UserID = _context.Users.FirstOrDefault(x => x.Username == User.Identity.Name).ID;
                _context.Add(listEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", listEntry.UserID);
            return View(listEntry);
        }

        // GET: ListEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listEntry = await _context.ListEntry.FindAsync(id);
            if (listEntry == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", listEntry.UserID);
            return View(listEntry);
        }

        // POST: ListEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Entry,UserID")] ListEntry listEntry)
        {
            if (id != listEntry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    listEntry.UserID = _context.Users.FirstOrDefault(x => x.Username == User.Identity.Name).ID;
                    _context.Update(listEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListEntryExists(listEntry.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "ID", listEntry.UserID);
            return View(listEntry);
        }

        // GET: ListEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listEntry = await _context.ListEntry
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (listEntry == null)
            {
                return NotFound();
            }

            return View(listEntry);
        }

        // POST: ListEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listEntry = await _context.ListEntry.FindAsync(id);
            _context.ListEntry.Remove(listEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListEntryExists(int id)
        {
            return _context.ListEntry.Any(e => e.ID == id);
        }
    }
}

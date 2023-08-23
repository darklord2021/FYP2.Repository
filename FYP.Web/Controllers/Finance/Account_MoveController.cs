using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP.DB.Context;
using FYP.DB.DBTables;
using Microsoft.AspNetCore.Authorization;

namespace FYP.Web.Controllers.Finance
{
    [Authorize(Roles =("Admin,Finance"))]
    public class Account_MoveController : Controller
    {
        private readonly FYPContext _context;

        public Account_MoveController(FYPContext context)
        {
            _context = context;
        }

        // GET: Account_Move
        public async Task<IActionResult> Index()
        {
            var fYPContext = _context.Account_Moves.Include(a => a.Source_DocNavigation).Include(a => a.purchase_source_docNavigation);
            return View(await fYPContext.ToListAsync());
        }

        // GET: Account_Move/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Account_Moves == null)
            {
                return NotFound();
            }

            var account_Move = await _context.Account_Moves
                .Include(a => a.Source_DocNavigation)
                .Include(a => a.purchase_source_docNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account_Move == null)
            {
                return NotFound();
            }

            return View(account_Move);
        }

        // GET: Account_Move/Create
        public IActionResult Create()
        {
            ViewData["Source_Doc"] = new SelectList(_context.Sale_Orders, "name", "name");
            ViewData["purchase_source_doc"] = new SelectList(_context.Purchase_Orders, "doc_name", "doc_name");
            return View();
        }

        // POST: Account_Move/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Doc_Name,Total_Amount,Date_Created,Taxed_Amount,Source_Doc,Status,operation_type,tax,purchase_source_doc")] Account_Move account_Move)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(account_Move);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["Source_Doc"] = new SelectList(_context.Sale_Orders, "name", "name", account_Move.Source_Doc);
            ViewData["purchase_source_doc"] = new SelectList(_context.Purchase_Orders, "doc_name", "doc_name", account_Move.purchase_source_doc);
            return View(account_Move);
        }

        // GET: Account_Move/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Account_Moves == null)
            {
                return NotFound();
            }

            var account_Move = await _context.Account_Moves.FindAsync(id);
            if (account_Move == null)
            {
                return NotFound();
            }
            ViewData["Source_Doc"] = new SelectList(_context.Sale_Orders, "name", "name", account_Move.Source_Doc);
            ViewData["purchase_source_doc"] = new SelectList(_context.Purchase_Orders, "doc_name", "doc_name", account_Move.purchase_source_doc);
            return View(account_Move);
        }

        // POST: Account_Move/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Doc_Name,Total_Amount,Date_Created,Taxed_Amount,Source_Doc,Status,operation_type,tax,purchase_source_doc")] Account_Move account_Move)
        {
            if (id != account_Move.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account_Move);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Account_MoveExists(account_Move.ID))
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
            ViewData["Source_Doc"] = new SelectList(_context.Sale_Orders, "name", "name", account_Move.Source_Doc);
            ViewData["purchase_source_doc"] = new SelectList(_context.Purchase_Orders, "doc_name", "doc_name", account_Move.purchase_source_doc);
            return View(account_Move);
        }

        // GET: Account_Move/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account_Moves == null)
            {
                return NotFound();
            }

            var account_Move = await _context.Account_Moves
                .Include(a => a.Source_DocNavigation)
                .Include(a => a.purchase_source_docNavigation)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (account_Move == null)
            {
                return NotFound();
            }

            return View(account_Move);
        }

        // POST: Account_Move/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account_Moves == null)
            {
                return Problem("Entity set 'FYPContext.Account_Moves'  is null.");
            }
            var account_Move = await _context.Account_Moves.FindAsync(id);
            if (account_Move != null)
            {
                _context.Account_Moves.Remove(account_Move);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Account_MoveExists(int id)
        {
            return (_context.Account_Moves?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> MarkasPaid(int id)
        {
            var inv = await _context.Account_Moves.FirstOrDefaultAsync(tr => tr.ID == id);
            if (inv is null)
            {
                return NotFound();
            }
            else
            {
                inv.paid = true;
                _context.Update(inv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }
    }
}

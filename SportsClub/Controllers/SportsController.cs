﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsClub.DbContext;
using SportsClub.Models;

namespace SportsClub.Controllers
{
    public class SportsController : Controller
    {

		private readonly ApplicationDbContext _context;

		public SportsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Departments
		public async Task<IActionResult> Index()
		{
			return View(await _context.Sports.ToListAsync());
		}

		// GET: Departments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sport = await _context.Sports.Include(sp => sp.Players).FirstOrDefaultAsync(m => m.Id == id);
			if (sport == null)
			{
				return NotFound();
			}

			return View(sport);
		}

		// GET: Departments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Departments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Description")] Sport sport)
		{
			if (ModelState.IsValid)
			{
				_context.Add(sport);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(sport);
		}

		// GET: Departments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sport = await _context.Sports.FindAsync(id);
			if (sport == null)
			{
				return NotFound();
			}
			return View(sport);
		}

		// POST: Departments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Sport sport)
		{
			if (id != sport.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(sport);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SportExists(sport.Id))
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
			return View(sport);
		}

		// GET: Departments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var sport = await _context.Sports.Include(sp => sp.Players)
			 .FirstOrDefaultAsync(m => m.Id == id);
			if (sport == null)
			{
				return NotFound();
			}

			return View(sport);
		}

		// POST: Departments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var sport = await _context.Sports.FindAsync(id);
			if (sport != null)
			{
				_context.Sports.Remove(sport);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SportExists(int id)
		{
			return _context.Sports.Any(e => e.Id == id);
		}
	}
}
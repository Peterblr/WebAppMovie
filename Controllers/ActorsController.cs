using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    public class ActorsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IActorsService _service;

        //public ActorsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        // GET: Actors
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Actors.ToListAsync());
        //}
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Actors/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var actor = await _context.Actors
        //        .FirstOrDefaultAsync(m => m.ActorId == id);
        //    if (actor == null)
        //    {
        //        return View("NotFound");
        //    }

        //    return View(actor);
        //} 
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null)
            //{
            //    return View("NotFound");
            //}

            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(actor);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        //public async Task<IActionResult> Create([Bind("ActorId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Actor actor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(actor);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(actor);
        //}

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null)
            //{
            //    return View("NotFound");
            //}

            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }
            return View(actor);
        }
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var actor = await _context.Actors.FindAsync(id);
        //    if (actor == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(actor);
        //}

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Actor actor)
        {
            if (id != actor.ActorId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(actor);

                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(actor);
        }

        //public async Task<IActionResult> Edit(int id, [Bind("ActorId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Actor actor)
        //{
        //    if (id != actor.ActorId)
        //    {
        //        return View("NotFound");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(actor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ActorExists(actor.ActorId))
        //            {
        //                return View("NotFound");
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(actor);
        //}

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null)
            //{
            //    return View("NotFound");
            //}

            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }

            return View(actor);
        }
        // public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return View("NotFound");
        //    }

        //    var actor = await _context.Actors
        //        .FirstOrDefaultAsync(m => m.ActorId == id);
        //    if (actor == null)
        //    {
        //        return View("NotFound");
        //    }

        //    return View(actor);
        //}

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);

            await _service.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var actor = await _context.Actors.FindAsync(id);
        //    _context.Actors.Remove(actor);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ActorExists(int id)
        //{
        //    return _context.Actors.Any(e => e.ActorId == id);
        //}
    }
}

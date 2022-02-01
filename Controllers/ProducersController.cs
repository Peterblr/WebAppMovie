using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WebAppMovie.Data;
using WebAppMovie.Models;
using WebAppMovie.Repository.Interfaces;

namespace WebAppMovie.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        private readonly IToastNotification _toastNotification;

        public ProducersController(IProducerService service, IToastNotification toastNotification)
        {
            _service = service;

            _toastNotification = toastNotification;
        }

        // GET: Producers
        public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null)
            {
                return View("NotFound");
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(producer);

                await _service.SaveAsync();

                _toastNotification.AddSuccessToastMessage("Actor created");

                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null)
            {
                return View("NotFound");
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerId,FirstName,LastName,DayOfBirth,ImageUrl,Biografy")] Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(producer);

                await _service.SaveAsync();

                _toastNotification.AddSuccessToastMessage("Actor created");

                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null)
            {
                return View("NotFound");
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);

            await _service.SaveAsync();

            _toastNotification.AddAlertToastMessage("Actor deleted");

            return RedirectToAction(nameof(Index));
        }
    }
}

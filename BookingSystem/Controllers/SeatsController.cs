using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.DTO;
using BookingSystem.Models;
using BookingSystem.Operations;
using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class SeatsController : Controller
    {
        private readonly BookingContext _context;
        private int BookingID = DatabaseManager.BookingId;

        public SeatsController(BookingContext context)
        {
            _context = context;
        }

        // GET: Seats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seat.ToListAsync());
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat
                .SingleOrDefaultAsync(m => m.Id == id);

            //Pass information over so it can be sent to database and used in other categories
            BookingSystemDTO bookingsystemdto = new BookingSystemDTO();
            mySeat mySeat = new mySeat();

            mySeat.Id = seat.Id;
            DatabaseManager.BookingId = seat.BookingId;
            mySeat.SeatNumber = seat.SeatNumber;

            var allseats = _context.Seat.ToList();
            bookingsystemdto.seats = allseats;


            bookingsystemdto.mySeat = mySeat;

            


                        if (seat == null)
            {
                return NotFound();
            }

            return View(bookingsystemdto);
        }


        // GET: Seats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingId,SeatNumber")] Seat seat)
        {


            if (ModelState.IsValid)
                DatabaseManager.NumberOfSeats = DatabaseManager.NumberOfSeats - 1;
            



               
                {
                    _context.Add(seat);
                    await _context.SaveChangesAsync();
                    if (DatabaseManager.NumberOfSeats <= 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                    return RedirectToAction(nameof(Create));
                }
                }

                return View(seat);
            


          
        }

        // GET: Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat.SingleOrDefaultAsync(m => m.Id == id);
            if (seat == null)
            {
                return NotFound();
            }
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingId,SeatNumber")] Seat seat)
        {
            if (id != seat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.Id))
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
            return View(seat);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat
                .SingleOrDefaultAsync(m => m.Id == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seat = await _context.Seat.SingleOrDefaultAsync(m => m.Id == id);
            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Complete()
        {
            return View();
        }

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.Id == id);
        }
    }
}

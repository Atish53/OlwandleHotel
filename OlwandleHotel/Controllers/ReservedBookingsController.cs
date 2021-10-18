using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OlwandleHotel.Models;

namespace OlwandleHotel.Controllers
{
    public class ReservedBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedBookings
        public async Task<ActionResult> Index()
        {
            var reservedBookings = db.ReservedBookings.Include(r => r.Room);
            return View(await reservedBookings.ToListAsync());
        }

        // GET: ReservedBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedBooking reservedBooking = await db.ReservedBookings.FindAsync(id);
            if (reservedBooking == null)
            {
                return HttpNotFound();
            }
            return View(reservedBooking);
        }

        // GET: ReservedBookings/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomLocation");
            return View();
        }

        // POST: ReservedBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReservedBookingId,Name,LastName,IDNumber,RoomId,ReservedCost")] ReservedBooking reservedBooking)
        {
            if (ModelState.IsValid)
            {
                reservedBooking.Name = User.Identity.GetFirstName();
                reservedBooking.LastName = User.Identity.GetLastName();
                reservedBooking.IDNumber = User.Identity.GetIDNumber();
                reservedBooking.ReservedCost = reservedBooking.Room.FixedCost;                              

                db.ReservedBookings.Add(reservedBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomLocation", reservedBooking.RoomId);
            return View(reservedBooking);
        }

        // GET: ReservedBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedBooking reservedBooking = await db.ReservedBookings.FindAsync(id);
            if (reservedBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomLocation", reservedBooking.RoomId);
            return View(reservedBooking);
        }

        // POST: ReservedBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReservedBookingId,Name,LastName,IDNumber,RoomId,ReservedCost")] ReservedBooking reservedBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomLocation", reservedBooking.RoomId);
            return View(reservedBooking);
        }

        // GET: ReservedBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedBooking reservedBooking = await db.ReservedBookings.FindAsync(id);
            if (reservedBooking == null)
            {
                return HttpNotFound();
            }
            return View(reservedBooking);
        }

        // POST: ReservedBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReservedBooking reservedBooking = await db.ReservedBookings.FindAsync(id);
            db.ReservedBookings.Remove(reservedBooking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

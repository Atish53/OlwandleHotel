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
    public class TourBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TourBookings
        public async Task<ActionResult> Index()
        {
            var tourBookings = db.TourBookings.Include(t => t.Tour);
            return View(await tourBookings.ToListAsync());
        }

        // GET: TourBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourBooking tourBooking = await db.TourBookings.FindAsync(id);
            if (tourBooking == null)
            {
                return HttpNotFound();
            }
            return View(tourBooking);
        }

        // GET: TourBookings/Create
        public ActionResult Create()
        {
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "TourTitle");
            return View();
        }

        // POST: TourBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TourBookingId,TourId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,AttendDateAndTime,TicketNumber")] TourBooking tourBooking)
        {
            if (ModelState.IsValid)
            {
                db.TourBookings.Add(tourBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TourId = new SelectList(db.Tours, "TourId", "TourTitle", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: TourBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourBooking tourBooking = await db.TourBookings.FindAsync(id);
            if (tourBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "TourTitle", tourBooking.TourId);
            return View(tourBooking);
        }

        // POST: TourBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TourBookingId,TourId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,AttendDateAndTime,TicketNumber")] TourBooking tourBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TourId = new SelectList(db.Tours, "TourId", "TourTitle", tourBooking.TourId);
            return View(tourBooking);
        }

        // GET: TourBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourBooking tourBooking = await db.TourBookings.FindAsync(id);
            if (tourBooking == null)
            {
                return HttpNotFound();
            }
            return View(tourBooking);
        }

        // POST: TourBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TourBooking tourBooking = await db.TourBookings.FindAsync(id);
            db.TourBookings.Remove(tourBooking);
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

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
    public class RoomBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoomBookings
        [Authorize(Roles = "Hotel")]
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.RoomBookings
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.CustomerSurname.Contains(searchString)
                                       || s.CustomerName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.CustomerSurname);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.DateBooked);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.DateBooked);
                    break;
                default:
                    students = students.OrderBy(s => s.CustomerSurname);
                    break;
            }

            return View(students.ToList());
        }

        // GET: RoomBookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }
             
        public async Task<ActionResult> CheckIn(int id)
        {
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            roomBooking.CheckinDate = DateTime.Now.ToString();
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }

        public async Task<ActionResult> CheckOut(int id)
        {
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            roomBooking.CheckoutDate = DateTime.Now.ToString();
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }



        // GET: RoomBookings/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "HotelId");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RoomBookingId,RoomId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,InvoiceNumber")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(roomBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "HotelId", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "HotelId", roomBooking.RoomId);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RoomBookingId,RoomId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,InvoiceNumber")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "HotelId", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoomBooking roomBooking = await db.RoomBookings.FindAsync(id);
            db.RoomBookings.Remove(roomBooking);
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

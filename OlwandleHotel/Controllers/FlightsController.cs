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
    public class FlightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flights
        public async Task<ActionResult> Index()
        {
            return View(await db.Flights.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FlightId,FlightL,DestinationL,DateFlight,DateReturn,returnTicket,TotalCost,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,BoardDateAndTime,TicketNumber")] Flight flight)
        {
            Flight flights = new Flight();
            double finalCost = 0;

            var vFirstName = User.Identity.GetFirstName();
            var vLastName = User.Identity.GetLastName();
            var vAddress = User.Identity.GetAddress();
            var vIdNumber = User.Identity.GetIDNumber();
            string FirstName = vFirstName.ToString();
            string LastName = vLastName.ToString();
            string IdNumber = vIdNumber.ToString();
            string Address = vAddress.ToString();

            flights.CustomerName = FirstName;
            flights.CustomerSurname = LastName;
            flights.IdNumber = IdNumber;
            flights.Address = Address;
            flights.DateBooked = DateTime.Now.ToString();

            DateTime returns = DateTime.Parse(flight.DateReturn);
            DateTime departs = DateTime.Parse(flight.DateFlight);

            TimeSpan DaysBooked = returns.Subtract(departs);
            int numDays = DaysBooked.Days;

            flights.DateFlight = departs.ToString();
            flights.DateReturn = returns.ToString();

            string Ticket = "#300" + flights.FlightId + FirstName.Substring(0, 1) + LastName.Substring(0, 1) + DateTime.Now.ToString("FFFFF"); //100000ths of a second makes the ticket unique. (FFFFF)

            flights.TicketNumber = Ticket;

            //Destinations
            if (flight.DestinationL.ToString() == "Durban")
            {
                finalCost += 1000;
            }
            else if (flight.DestinationL.ToString() == "Johannesburg")
            {
                finalCost += 1500;
            }
            else if (flight.DestinationL.ToString() == "CapeTown")
            {
                finalCost += 1750;
            }

            //Aircrafts
            if (flight.FlightL.ToString() == "Kulula")
            {
                finalCost += 600;
            }
            else if (flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += 800;
            }
            else if (flight.FlightL.ToString() == "SAA")
            {
                finalCost += 450;
            }

            finalCost = (finalCost * numDays * 0.3);

            if (flight.returnTicket == true)
            {
                finalCost = (finalCost * 0.2) + finalCost;
            }

            flights.TotalCost = finalCost;
            flights.FlightL = flight.FlightL;
            flights.DestinationL = flight.DestinationL;

                db.Flights.Add(flights);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
        }

        public async Task<ActionResult> Board(int id)
        {
            Flight flight = await db.Flights.FindAsync(id);
            flight.BoardDateAndTime = DateTime.Now.ToString();
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }

        // GET: Flights/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FlightId,FlightL,DestinationL,DateFlight,DateReturn,returnTicket,TotalCost,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,BoardDateAndTime,TicketNumber")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flight flight = await db.Flights.FindAsync(id);
            db.Flights.Remove(flight);
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

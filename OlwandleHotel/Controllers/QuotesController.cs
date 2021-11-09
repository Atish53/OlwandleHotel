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
    public class QuotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quotes
        public async Task<ActionResult> Index()
        {
            return View(await db.Quotes.ToListAsync());
        }

        // GET: Quotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = await db.Quotes.FindAsync(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuoteId,FirstName,LastName,EmailAddress,PhoneNumber,NumAdults,NumKids,DepartureDate,ReturnDate,TourL,CruiseL,estimatedPrice")] Quote quote)
        {
            double estPrice = 1500; //Fixed Price
            int numKids, numAdults = 0;
            if (ModelState.IsValid)
            {
                if (quote.CruiseL != 0)
                {
                    estPrice += 3500;
                }
                if (quote.TourL == 0)
                {
                    estPrice += 3500;
                }

                //Runs through database and checks if date booked is available
                var Dates = from dates in db.Quotes
                            select dates.DepartureDate;
                var EndDates = from dates in db.Quotes
                               select dates.ReturnDate;


                foreach (var item in Dates)
                {
                    if (item == quote.DepartureDate)
                    {
                        return View("Unavailable");
                    }
                }
                numKids = quote.NumKids;
                numAdults = quote.NumAdults;
                estPrice *= (numAdults + numKids / 5);
                quote.EmailAddress = User.Identity.Name;
                quote.estimatedPrice = estPrice;
                db.Quotes.Add(quote);
                await db.SaveChangesAsync();
                return RedirectToAction("ViewRecent");
            }
            return View(quote);
        }

            // GET: Quotes/Edit/5
            public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = await db.Quotes.FindAsync(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuoteId,FirstName,LastName,EmailAddress,PhoneNumber,NumAdults,NumKids,DepartureDate,ReturnDate,TourL,CruiseL,estimatedPrice")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quote);
        }

        // GET: Quotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = await db.Quotes.FindAsync(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Quote quote = await db.Quotes.FindAsync(id);
            db.Quotes.Remove(quote);
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

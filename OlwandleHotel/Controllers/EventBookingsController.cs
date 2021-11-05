﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OlwandleHotel.Models;
using System.IO;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf;

namespace OlwandleHotel.Controllers
{
    public class EventBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventBookings
        public async Task<ActionResult> Index()
        {
            var eventBookings = db.EventBookings.Include(e => e.Event);
            return View(await eventBookings.ToListAsync());
        }


        public async Task<ActionResult> Print(int id) //Event Booking - id is the Event Id... Booking is stored to the EventBookings Table
        {
            EventBooking eventBooking = await db.EventBookings.FindAsync(id);
            string FirstName = eventBooking.CustomerName;
            string LastName = eventBooking.CustomerSurname;
            string IdNumber = eventBooking.IdNumber;
            string Location = eventBooking.Event.Location;
            string Description = eventBooking.Event.Description;
            double Price = eventBooking.Event.Price;
            string Name = eventBooking.Event.Name;

            MemoryStream msS = new MemoryStream(eventBooking.Event.EventPicture);

            //New Email.
            //Creates a new PDF document
            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            //Loads the image from disk
            PdfImage image = PdfImage.FromStream(msS);
            RectangleF bounds = new RectangleF(10, 10, 200, 200);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 13);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("Thank you! You have successfully booked the " + Name + " Event!", subHeadingFont);
            element.Brush = PdfBrushes.White;
            //Draws the heading on the page
            PdfLayoutResult res = element.Draw(page, new PointF(10, bounds.Top + 8));
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("This ticket belongs to: " + FirstName + " " + LastName, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(16, 36, 7));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 45));
            element = new PdfTextElement("Identity Number: " + IdNumber, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(16, 36, 7));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));
            element = new PdfTextElement("Event Location: " + Location + ".", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));
            element = new PdfTextElement("Description: " + Description + ".", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));
            element = new PdfTextElement("Price: R" + Price, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));

            PdfPen linePen = new PdfPen(new PdfColor(80, 138, 4), 0.70f);
            PointF startPoint = new PointF(0, res.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 5);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            PdfFont notTimesRoman = new PdfStandardFont(PdfFontFamily.Courier, 16);
            element = new PdfTextElement("Your Ticket Number is" + eventBooking.TicketNumber, notTimesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(48, 5, 5));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));

            linePen = new PdfPen(new PdfColor(80, 138, 4), 0.70f);
            startPoint = new PointF(0, res.Bounds.Bottom + 3);
            endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 5);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);            

            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);            
            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;    filename= eTicket - " + eventBooking.TicketNumber + ".pdf");
            Response.BinaryWrite(bytesInStream);
            Response.End();

            return View();
        }

            // GET: EventBookings/Details/5
            public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = await db.EventBookings.FindAsync(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            return View(eventBooking);
        }

        // GET: EventBookings/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title");
            return View();
        }

        // POST: EventBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EventBookingId,EventId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,TicketNumber")] EventBooking eventBooking)
        {
            if (ModelState.IsValid)
            {
                db.EventBookings.Add(eventBooking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventBooking.EventId);
            return View(eventBooking);
        }        

        // GET: EventBookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = await db.EventBookings.FindAsync(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventBooking.EventId);
            return View(eventBooking);
        }

        // POST: EventBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EventBookingId,EventId,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,TicketNumber")] EventBooking eventBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventBooking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Title", eventBooking.EventId);
            return View(eventBooking);
        }

        // GET: EventBookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventBooking eventBooking = await db.EventBookings.FindAsync(id);
            if (eventBooking == null)
            {
                return HttpNotFound();
            }
            return View(eventBooking);
        }

        // POST: EventBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EventBooking eventBooking = await db.EventBookings.FindAsync(id);
            db.EventBookings.Remove(eventBooking);
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

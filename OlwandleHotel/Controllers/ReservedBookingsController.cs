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
using System.Text;
using System.Net.Mail;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf.Grid;
using System.IO;

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

        // GET: /Checkout/AddressAndPayment
        public ActionResult Payment()
        {            
                return View();
        }



        [HttpPost]        
        public async Task<ActionResult> Payment(int id)
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomLocation");
            ReservedBooking reservedBooking = new ReservedBooking();
            reservedBooking.Name = User.Identity.Name;
            reservedBooking.LastName = User.Identity.GetLastName();
            reservedBooking.IDNumber = User.Identity.GetIDNumber();


            string ids = reservedBooking.ReservedBookingId.ToString();
            Room room = await db.Rooms.FindAsync(id);

            reservedBooking.RoomId = 1;
            reservedBooking.ReservedCost = 600;

            db.ReservedBookings.Add(reservedBooking);

            await db.SaveChangesAsync();

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
            PdfImage image = PdfImage.FromFile(Server.MapPath("~/Photos/EmailLogo.png"));
            RectangleF bounds = new RectangleF(10, 10, 200, 200);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("Reservation Number # " + reservedBooking.ReservedBookingId + " for" + " " + User.Identity.Name, subHeadingFont);
            element.Brush = PdfBrushes.White;

            //Draws the heading on the page
            PdfLayoutResult res = element.Draw(page, new PointF(10, bounds.Top + 8));
            string currentDate = "Date Purchased " + System.DateTime.Today.Date;
            //Measures the width of the text to place it in the correct location
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, res.Bounds.Y);
            //Draws the date by using DrawString method
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("Bill To " + User.Identity.Name.ToString() + ", ", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 25));
            element = new PdfTextElement("Total Price R " + reservedBooking.ReservedCost.ToString(), timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 25));
            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, res.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 3);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            //Creates the datasource for the table
            DataTable invoiceDetails = new DataTable();

            //Add columns to the DataTable
            invoiceDetails.Columns.Add("Hotel Name");
            invoiceDetails.Columns.Add("Beds");
            invoiceDetails.Columns.Add("Occupies");
            invoiceDetails.Columns.Add("Reservation Cost");

            

            //Add rows to the DataTable
            
            invoiceDetails.Rows.Add(new object[] { reservedBooking.Room.RoomLocation,reservedBooking.Room.NumBeds, reservedBooking.Room.MaxOccupants, reservedBooking.ReservedCost });
            


            //Creates text elements to add the address and draw it to the page.



            //Creates a PDF grid
            PdfGrid grid = new PdfGrid();
            //Adds the data source
            grid.DataSource = invoiceDetails;
            //Creates the grid cell styles
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            //Creates the header style
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);

            //Adds cell customizations
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }

            //Applies the header style
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, res.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);

            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;

            var invoicePdf = new System.Net.Mail.Attachment(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            string docname = "Invoice.pdf";
            invoicePdf.ContentDisposition.FileName = docname;

            MailMessage mail = new MailMessage();
            string emailTo = User.Identity.Name;
            MailAddress from = new MailAddress("21642835@dut4life.ac.za");
            mail.From = from;
            mail.Subject = "Your invoice for reservation number #" + ids;
            mail.Body = "Dear " + User.Identity.Name + ", find your invoice in the attached PDF document.";
            mail.To.Add(emailTo);

            mail.Attachments.Add(invoicePdf);

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential("21642835@dut4life.ac.za", "$$Dut980514");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
            //Clean-up.
            //Close the document.
            document.Close(true);
            //Dispose of email.
            mail.Dispose();
            try
            {
                // Retrieve required values for the PayFast Merchant
                string name = "ParadiseTravels Reservation #" + reservedBooking.ReservedBookingId;
                string description = "This is a once-off and non-refundable reservation payment. ";

                string site = "https://sandbox.payfast.co.za/eng/process";
                string merchant_id = "";
                string merchant_key = "";

                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10000100";
                    merchant_key = "46f0cd694581a";
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(reservedBooking.ReservedBookingId.ToString()));
                str.Append("&amount=" + HttpUtility.UrlEncode(reservedBooking.Room.FixedCost.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());
            }
            catch (Exception)
            {
                throw;
            }
           
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

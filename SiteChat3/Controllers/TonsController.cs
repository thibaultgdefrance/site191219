using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiteChat3.Models;

namespace SiteChat3.Controllers
{
    public class TonsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Tons
        public async Task<ActionResult> Index()
        {
            return View(await db.Ton.ToListAsync());
        }

        // GET: Tons/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ton ton = await db.Ton.FindAsync(id);
            if (ton == null)
            {
                return HttpNotFound();
            }
            return View(ton);
        }

        // GET: Tons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tons/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTon,LibelleTon")] Ton ton)
        {
            if (ModelState.IsValid)
            {
                db.Ton.Add(ton);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ton);
        }

        // GET: Tons/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ton ton = await db.Ton.FindAsync(id);
            if (ton == null)
            {
                return HttpNotFound();
            }
            return View(ton);
        }

        // POST: Tons/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTon,LibelleTon")] Ton ton)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ton).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ton);
        }

        // GET: Tons/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ton ton = await db.Ton.FindAsync(id);
            if (ton == null)
            {
                return HttpNotFound();
            }
            return View(ton);
        }

        // POST: Tons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ton ton = await db.Ton.FindAsync(id);
            db.Ton.Remove(ton);
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

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
    public class AccesController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Acces
        public async Task<ActionResult> Index()
        {
            return View(await db.Acces.ToListAsync());
        }

        // GET: Acces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acces acces = await db.Acces.FindAsync(id);
            if (acces == null)
            {
                return HttpNotFound();
            }
            return View(acces);
        }

        // GET: Acces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acces/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdAcces,LibelleAcces,ValeurAcces")] Acces acces)
        {
            if (ModelState.IsValid)
            {
                db.Acces.Add(acces);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(acces);
        }

        // GET: Acces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acces acces = await db.Acces.FindAsync(id);
            if (acces == null)
            {
                return HttpNotFound();
            }
            return View(acces);
        }

        // POST: Acces/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdAcces,LibelleAcces,ValeurAcces")] Acces acces)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acces).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(acces);
        }

        // GET: Acces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acces acces = await db.Acces.FindAsync(id);
            if (acces == null)
            {
                return HttpNotFound();
            }
            return View(acces);
        }

        // POST: Acces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Acces acces = await db.Acces.FindAsync(id);
            db.Acces.Remove(acces);
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

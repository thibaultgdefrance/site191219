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
    public class NiveauxController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Niveaux
        public async Task<ActionResult> Index()
        {
            return View(await db.Niveau.ToListAsync());
        }

        // GET: Niveaux/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveau niveau = await db.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return HttpNotFound();
            }
            return View(niveau);
        }

        // GET: Niveaux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Niveaux/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdNiveau,LibelleNiveau")] Niveau niveau)
        {
            if (ModelState.IsValid)
            {
                db.Niveau.Add(niveau);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(niveau);
        }

        // GET: Niveaux/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveau niveau = await db.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return HttpNotFound();
            }
            return View(niveau);
        }

        // POST: Niveaux/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdNiveau,LibelleNiveau")] Niveau niveau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(niveau).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(niveau);
        }

        // GET: Niveaux/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Niveau niveau = await db.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return HttpNotFound();
            }
            return View(niveau);
        }

        // POST: Niveaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Niveau niveau = await db.Niveau.FindAsync(id);
            db.Niveau.Remove(niveau);
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

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
    public class MaintenancesController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Maintenances
        public async Task<ActionResult> Index()
        {
            var maintenance = db.Maintenance.Include(m => m.Utilisateur);
            return View(await maintenance.ToListAsync());
        }

        // GET: Maintenances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // GET: Maintenances/Create
        public ActionResult Create()
        {
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Maintenances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMaintenance,DescriptionMaintenance,IdUtilisateur")] Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Maintenance.Add(maintenance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", maintenance.IdUtilisateur);
            return View(maintenance);
        }

        // GET: Maintenances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", maintenance.IdUtilisateur);
            return View(maintenance);
        }

        // POST: Maintenances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMaintenance,DescriptionMaintenance,IdUtilisateur")] Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", maintenance.IdUtilisateur);
            return View(maintenance);
        }

        // GET: Maintenances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return HttpNotFound();
            }
            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Maintenance maintenance = await db.Maintenance.FindAsync(id);
            db.Maintenance.Remove(maintenance);
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

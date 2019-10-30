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
    public class TypeNotificationsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: TypeNotifications
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeNotification.ToListAsync());
        }

        // GET: TypeNotifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            if (typeNotification == null)
            {
                return HttpNotFound();
            }
            return View(typeNotification);
        }

        // GET: TypeNotifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeNotifications/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTypeNotification,LibelleTypeNotification")] TypeNotification typeNotification)
        {
            if (ModelState.IsValid)
            {
                db.TypeNotification.Add(typeNotification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeNotification);
        }

        // GET: TypeNotifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            if (typeNotification == null)
            {
                return HttpNotFound();
            }
            return View(typeNotification);
        }

        // POST: TypeNotifications/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTypeNotification,LibelleTypeNotification")] TypeNotification typeNotification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeNotification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeNotification);
        }

        // GET: TypeNotifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            if (typeNotification == null)
            {
                return HttpNotFound();
            }
            return View(typeNotification);
        }

        // POST: TypeNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeNotification typeNotification = await db.TypeNotification.FindAsync(id);
            db.TypeNotification.Remove(typeNotification);
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

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
    public class NotificationsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Notifications
        public async Task<ActionResult> Index()
        {
            var notification = db.Notification.Include(n => n.Utilisateur).Include(n => n.Utilisateur1).Include(n => n.TypeNotification);
            return View(await notification.ToListAsync());
        }

        // GET: Notifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notification.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            ViewBag.IdCreateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            ViewBag.IdDestinataire = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            ViewBag.IdTypeNotification = new SelectList(db.TypeNotification, "IdTypeNotification", "LibelleTypeNotification");
            return View();
        }

        // POST: Notifications/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdNotification,TexteNotification,StatutNotification,IdCreateur,IdDestinataire,IdDiscussion,IdTypeNotification")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Notification.Add(notification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdCreateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdCreateur);
            ViewBag.IdDestinataire = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdDestinataire);
            ViewBag.IdTypeNotification = new SelectList(db.TypeNotification, "IdTypeNotification", "LibelleTypeNotification", notification.IdTypeNotification);
            return View(notification);
        }

        // GET: Notifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notification.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCreateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdCreateur);
            ViewBag.IdDestinataire = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdDestinataire);
            ViewBag.IdTypeNotification = new SelectList(db.TypeNotification, "IdTypeNotification", "LibelleTypeNotification", notification.IdTypeNotification);
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdNotification,TexteNotification,StatutNotification,IdCreateur,IdDestinataire,IdDiscussion,IdTypeNotification")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCreateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdCreateur);
            ViewBag.IdDestinataire = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", notification.IdDestinataire);
            ViewBag.IdTypeNotification = new SelectList(db.TypeNotification, "IdTypeNotification", "LibelleTypeNotification", notification.IdTypeNotification);
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.Notification.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Notification notification = await db.Notification.FindAsync(id);
            db.Notification.Remove(notification);
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

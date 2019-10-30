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
    public class MessagesController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var message = db.Message.Include(m => m.Discussion).Include(m => m.Ton).Include(m => m.Utilisateur);
            return View(await message.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion");
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Messages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdMessage,IdUtilisateur,DateEnvoi,TexteMessage,IdDiscussion,IdTon,StatutMessage")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Message.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // POST: Messages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdMessage,IdUtilisateur,DateEnvoi,TexteMessage,IdDiscussion,IdTon,StatutMessage")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", message.IdDiscussion);
            ViewBag.IdTon = new SelectList(db.Ton, "IdTon", "LibelleTon", message.IdTon);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", message.IdUtilisateur);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Message message = await db.Message.FindAsync(id);
            db.Message.Remove(message);
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

        public ActionResult MessagesApi()
        {
            return View();
        }
    }
}

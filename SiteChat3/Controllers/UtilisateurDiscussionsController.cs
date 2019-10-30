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
    public class UtilisateurDiscussionsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: UtilisateurDiscussions
        public async Task<ActionResult> Index()
        {
            var utilisateurDiscussion = db.UtilisateurDiscussion.Include(u => u.Discussion).Include(u => u.Niveau).Include(u => u.Utilisateur);
            return View(await utilisateurDiscussion.ToListAsync());
        }

        // GET: UtilisateurDiscussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            if (utilisateurDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(utilisateurDiscussion);
        }

        // GET: UtilisateurDiscussions/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion");
            ViewBag.IdNiveau = new SelectList(db.Niveau, "IdNiveau", "LibelleNiveau");
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: UtilisateurDiscussions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUtilisateurDiscussion,IdUtilisateur,IdDiscussion,IdNiveau")] UtilisateurDiscussion utilisateurDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.UtilisateurDiscussion.Add(utilisateurDiscussion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", utilisateurDiscussion.IdDiscussion);
            ViewBag.IdNiveau = new SelectList(db.Niveau, "IdNiveau", "LibelleNiveau", utilisateurDiscussion.IdNiveau);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", utilisateurDiscussion.IdUtilisateur);
            return View(utilisateurDiscussion);
        }

        // GET: UtilisateurDiscussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            if (utilisateurDiscussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", utilisateurDiscussion.IdDiscussion);
            ViewBag.IdNiveau = new SelectList(db.Niveau, "IdNiveau", "LibelleNiveau", utilisateurDiscussion.IdNiveau);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", utilisateurDiscussion.IdUtilisateur);
            return View(utilisateurDiscussion);
        }

        // POST: UtilisateurDiscussions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUtilisateurDiscussion,IdUtilisateur,IdDiscussion,IdNiveau")] UtilisateurDiscussion utilisateurDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateurDiscussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscussion = new SelectList(db.Discussion, "IdDiscussion", "TitreDiscussion", utilisateurDiscussion.IdDiscussion);
            ViewBag.IdNiveau = new SelectList(db.Niveau, "IdNiveau", "LibelleNiveau", utilisateurDiscussion.IdNiveau);
            ViewBag.IdUtilisateur = new SelectList(db.Utilisateur, "IdUtilisateur", "NomUtilisateur", utilisateurDiscussion.IdUtilisateur);
            return View(utilisateurDiscussion);
        }

        // GET: UtilisateurDiscussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            if (utilisateurDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(utilisateurDiscussion);
        }

        // POST: UtilisateurDiscussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UtilisateurDiscussion utilisateurDiscussion = await db.UtilisateurDiscussion.FindAsync(id);
            db.UtilisateurDiscussion.Remove(utilisateurDiscussion);
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

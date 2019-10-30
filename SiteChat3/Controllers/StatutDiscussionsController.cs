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
    public class StatutDiscussionsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: StatutDiscussions
        public async Task<ActionResult> Index()
        {
            return View(await db.StatutDiscussion.ToListAsync());
        }

        // GET: StatutDiscussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            if (statutDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(statutDiscussion);
        }

        // GET: StatutDiscussions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatutDiscussions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdStatutDiscussion,LibelleStatutDiscussion")] StatutDiscussion statutDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.StatutDiscussion.Add(statutDiscussion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(statutDiscussion);
        }

        // GET: StatutDiscussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            if (statutDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(statutDiscussion);
        }

        // POST: StatutDiscussions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdStatutDiscussion,LibelleStatutDiscussion")] StatutDiscussion statutDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statutDiscussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statutDiscussion);
        }

        // GET: StatutDiscussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            if (statutDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(statutDiscussion);
        }

        // POST: StatutDiscussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StatutDiscussion statutDiscussion = await db.StatutDiscussion.FindAsync(id);
            db.StatutDiscussion.Remove(statutDiscussion);
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

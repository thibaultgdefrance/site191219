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
    public class DiscussionsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: Discussions
        public async Task<ActionResult> Index()
        {
            var discussion = db.Discussion.Include(d => d.StatutDiscussion1).Include(d => d.TypeDiscussion);
            return View(await discussion.ToListAsync());
        }

        // GET: Discussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // GET: Discussions/Create
        public ActionResult Create()
        {
            ViewBag.IdStatutDiscussion = new SelectList(db.StatutDiscussion, "IdStatutDiscussion", "LibelleStatutDiscussion");
            ViewBag.IdTypeDiscussion = new SelectList(db.TypeDiscussion, "IdTypeDiscussion", "LibelleTypeDiscussion");
            return View();
        }

        // POST: Discussions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdDiscussion,TitreDiscussion,DateCreationDiscussion,DescriptionDiscussion,StatutDiscussion,IdTypeDiscussion,IdStatutDiscussion")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Discussion.Add(discussion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdStatutDiscussion = new SelectList(db.StatutDiscussion, "IdStatutDiscussion", "LibelleStatutDiscussion", discussion.IdStatutDiscussion);
            ViewBag.IdTypeDiscussion = new SelectList(db.TypeDiscussion, "IdTypeDiscussion", "LibelleTypeDiscussion", discussion.IdTypeDiscussion);
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStatutDiscussion = new SelectList(db.StatutDiscussion, "IdStatutDiscussion", "LibelleStatutDiscussion", discussion.IdStatutDiscussion);
            ViewBag.IdTypeDiscussion = new SelectList(db.TypeDiscussion, "IdTypeDiscussion", "LibelleTypeDiscussion", discussion.IdTypeDiscussion);
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdDiscussion,TitreDiscussion,DateCreationDiscussion,DescriptionDiscussion,StatutDiscussion,IdTypeDiscussion,IdStatutDiscussion")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdStatutDiscussion = new SelectList(db.StatutDiscussion, "IdStatutDiscussion", "LibelleStatutDiscussion", discussion.IdStatutDiscussion);
            ViewBag.IdTypeDiscussion = new SelectList(db.TypeDiscussion, "IdTypeDiscussion", "LibelleTypeDiscussion", discussion.IdTypeDiscussion);
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = await db.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Discussion discussion = await db.Discussion.FindAsync(id);
            db.Discussion.Remove(discussion);
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

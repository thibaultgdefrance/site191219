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
    public class TypeDiscussionsController : Controller
    {
        private Chat2Entities db = new Chat2Entities();

        // GET: TypeDiscussions
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeDiscussion.ToListAsync());
        }

        // GET: TypeDiscussions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            if (typeDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(typeDiscussion);
        }

        // GET: TypeDiscussions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeDiscussions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTypeDiscussion,LibelleTypeDiscussion")] TypeDiscussion typeDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.TypeDiscussion.Add(typeDiscussion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeDiscussion);
        }

        // GET: TypeDiscussions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            if (typeDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(typeDiscussion);
        }

        // POST: TypeDiscussions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTypeDiscussion,LibelleTypeDiscussion")] TypeDiscussion typeDiscussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeDiscussion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeDiscussion);
        }

        // GET: TypeDiscussions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            if (typeDiscussion == null)
            {
                return HttpNotFound();
            }
            return View(typeDiscussion);
        }

        // POST: TypeDiscussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeDiscussion typeDiscussion = await db.TypeDiscussion.FindAsync(id);
            db.TypeDiscussion.Remove(typeDiscussion);
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

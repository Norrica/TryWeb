using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TryWeb.Models;
using System.Xml.Serialization;

namespace TryWeb.Controllers
{
    public class AspectsController : Controller
    {
        private AspectsContext db = new AspectsContext();

        // GET: Aspects
        public async Task<ActionResult> Index()
        {
            return View(await db.Aspects.ToListAsync());
        }

        // GET: Aspects/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspects aspects = await db.Aspects.FindAsync(id);
            if (aspects == null)
            {
                return HttpNotFound();
            }
            return View(aspects);
        }

        // GET: Aspects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aspects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Score,Category,IsWatched")] Aspects aspects)
        {
            if (ModelState.IsValid)
            {
                db.Aspects.Add(aspects);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aspects);
        }

        // GET: Aspects/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspects aspects = await db.Aspects.FindAsync(id);
            if (aspects == null)
            {
                return HttpNotFound();
            }
            return View(aspects);
        }

        // POST: Aspects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Score,Category,IsWatched")] Aspects aspects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspects).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aspects);
        }

        // GET: Aspects/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aspects aspects = await db.Aspects.FindAsync(id);
            if (aspects == null)
            {
                return HttpNotFound();
            }
            return View(aspects);
        }

        // POST: Aspects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Aspects aspects = await db.Aspects.FindAsync(id);
            db.Aspects.Remove(aspects);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
		public ActionResult Upload(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
			{
				List<Aspects> buffer = new List<Aspects>();
				buffer = (List<Aspects>)new XmlSerializer(typeof(List<Aspects>)).Deserialize(file.InputStream);
				foreach (var value in buffer)
				{

					db.Aspects.Add(value);
					db.SaveChanges();
				}
			}
			return View();
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

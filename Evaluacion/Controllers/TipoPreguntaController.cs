using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evaluacion.DAL;
using Evaluacion.Models;

namespace Evaluacion.Controllers
{
    public class TipoPreguntaController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: TipoPregunta
        public ActionResult Index()
        {
            return View(db.TipoPregunta.ToList());
        }

        // GET: TipoPregunta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPregunta tipoPregunta = db.TipoPregunta.Find(id);
            if (tipoPregunta == null)
            {
                return HttpNotFound();
            }
            return View(tipoPregunta);
        }

        // GET: TipoPregunta/Create
        public ActionResult Create(int? id)
        {
            return View();
        }

        // POST: TipoPregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,IsListado,IsMultiple")] TipoPregunta tipoPregunta)
        {
            if (ModelState.IsValid)
            {
                db.TipoPregunta.Add(tipoPregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoPregunta);
        }

        // GET: TipoPregunta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPregunta tipoPregunta = db.TipoPregunta.Find(id);
            if (tipoPregunta == null)
            {
                return HttpNotFound();
            }
            return View(tipoPregunta);
        }

        // POST: TipoPregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,IsListado,IsMultiple")] TipoPregunta tipoPregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoPregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoPregunta);
        }

        // GET: TipoPregunta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPregunta tipoPregunta = db.TipoPregunta.Find(id);
            if (tipoPregunta == null)
            {
                return HttpNotFound();
            }
            return View(tipoPregunta);
        }

        // POST: TipoPregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPregunta tipoPregunta = db.TipoPregunta.Find(id);
            db.TipoPregunta.Remove(tipoPregunta);
            db.SaveChanges();
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

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
    public class ModuloController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Modulo
        public ActionResult Index()
        {
            return View(db.Modulo.ToList());
        }

        // GET: Modulo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Modulo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,IsActivo,ActionName,ControllerName")] Modulo modulo)
        {
            string msjError = "Es necesario llenar este campo";

            if (string.IsNullOrWhiteSpace(modulo.Nombre))
            {
                ModelState.AddModelError("Nombre", (msjError));
            }
            if(string.IsNullOrWhiteSpace(modulo.ActionName))
            {
                ModelState.AddModelError("ActionName", (msjError));
            }
            if (string.IsNullOrWhiteSpace(modulo.ControllerName))
            {
                ModelState.AddModelError("ControllerName", (msjError));
            }

            if (ModelState.IsValid)
            {
                db.Modulo.Add(modulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modulo);
        }

        // GET: Modulo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,IsActivo,ActionName,ControllerName")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modulo);
        }

        // GET: Modulo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modulo modulo = db.Modulo.Find(id);
            db.Modulo.Remove(modulo);
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

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
    public class PreguntaController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Pregunta
        public ActionResult Index(int pExamenID)
        {
            
            Examen examen = db.Examen.FirstOrDefault(e=>e.ID == pExamenID);
            ViewBag.ExamenID = examen;
            var pregunta = db.Pregunta.Include(p => p.Examen).Include(p => p.TipoPregunta).Where(p=>p.ExamenID == pExamenID);
            return View(pregunta.ToList());

            
        }

        // GET: Pregunta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // GET: Pregunta/Create
        public ActionResult Create(int pExamenID)
        {
            Examen examen = db.Examen.FirstOrDefault(e=>e.ID == pExamenID);
            ViewBag.Examen = examen;
            ViewBag.TipoPreguntaID = new SelectList(db.TipoPregunta, "ID", "Nombre");

            Pregunta pregunta = new Pregunta();
            int? orden = null;
            try
            {
               orden  = db.Pregunta.Where(p => p.ExamenID == pExamenID).Select(p => p.Orden).Count();
                if (!orden.HasValue)
                {
                    pregunta.Orden = 1;
                }
                else
                {
                    pregunta.Orden = orden.Value + 1;
                }
            }
            catch(Exception ex)
            {
                
                pregunta.Orden = 1;
            }
            
            
            return View(pregunta);
        }

        // POST: Pregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,ExamenID,TipoPreguntaID,Orden")] Pregunta pregunta)
        {
            if(string.IsNullOrWhiteSpace(pregunta.Nombre))
            {
                //Código para realizar una validación (Texto de error a mostrar, mensaje)
                ModelState.AddModelError("Nombre", ("Es necesario llenar este campo"));
            }

            if (ModelState.IsValid)
            {
                db.Pregunta.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("Index", new { pExamenID = pregunta.ExamenID});
            }

            Examen examen = db.Examen.FirstOrDefault(e => e.ID == pregunta.ExamenID);
            ViewBag.Examen = examen;
            ViewBag.TipoPreguntaID = new SelectList(db.TipoPregunta, "ID", "Nombre", pregunta.TipoPreguntaID);
            return View(pregunta);
        }

        // GET: Pregunta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "NombreExamen", pregunta.ExamenID);
            ViewBag.TipoPreguntaID = new SelectList(db.TipoPregunta, "ID", "Nombre", pregunta.TipoPreguntaID);
            return View(pregunta);
        }

        // POST: Pregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,ExamenID,TipoPreguntaID,Orden")] Pregunta pregunta)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();
                Pregunta mPregunta = db.Pregunta.FirstOrDefault(p => p.ID == pregunta.ID);
                return RedirectToAction("Index", new { pExamenID = mPregunta.ExamenID });
            }
            ViewBag.ExamenID = new SelectList(db.Examen, "ID", "NombreExamen", pregunta.ExamenID);
            ViewBag.TipoPreguntaID = new SelectList(db.TipoPregunta, "ID", "Nombre", pregunta.TipoPreguntaID);
            return View(pregunta);
        }

        // GET: Pregunta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregunta pregunta = db.Pregunta.Find(id);
            db.Pregunta.Remove(pregunta);
            db.SaveChanges();
            return RedirectToAction("Index", new { pExamenID = pregunta.ExamenID});
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

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
    public class OpcionController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Opcion
        public ActionResult Index()
        {
            var opcion = db.Opcion.Include(o => o.Pregunta);
            return View(opcion.ToList());
        }


        public PartialViewResult AltaOpcion(int pPreguntaID)
        {
            Pregunta pregunta = db.Pregunta.FirstOrDefault(p=>p.ID == pPreguntaID);
            ViewBag.Pregunta = pregunta;
            List<Opcion> lstOpciones = db.Opcion.Where(o=>o.PreguntaID == pPreguntaID).ToList();
            ViewBag.Opciones = lstOpciones;
            return PartialView("_AltaOpcion");
        }

        public JsonResult CrearOpcion(string txtOpcion, int pPreguntaID)
        {

            if (string.IsNullOrWhiteSpace(txtOpcion))
            {
                Response.StatusCode = 411;
                Response.StatusDescription = "Bad Request";
                HttpStatusCodeResult result = new HttpStatusCodeResult(411, "Bad Request");

                return Json(result);
                
            }
            else
            {
                Opcion opcion = new Opcion();
                opcion.Valor = txtOpcion;
                opcion.PreguntaID = pPreguntaID;
                db.Opcion.Add(opcion);
                db.SaveChanges();

                return Json("2");
            }
        }
        // GET: Opcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // GET: Opcion/Create
        public ActionResult Create()
        {
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre");
            return View();
        }

        // POST: Opcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Valor,PreguntaID")] Opcion opcion)
        {
            if(string.IsNullOrWhiteSpace(opcion.Valor))
            {
                ModelState.AddModelError("Valor",("Es necesario llenar este campo"));
            }

            if (ModelState.IsValid)
            {
                db.Opcion.Add(opcion);
                db.SaveChanges();
                Opcion option = db.Opcion.Include(o=>o.Pregunta).FirstOrDefault(o=>o.ID == opcion.ID);
                return RedirectToAction("Index", "Pregunta", new { pExamenID = option.Pregunta.ExamenID });
            }

            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", opcion.PreguntaID);
            return View(opcion);
        }

        // GET: Opcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", opcion.PreguntaID);

            return View(opcion);
        }

        // POST: Opcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Valor,PreguntaID")] Opcion opcion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opcion).State = EntityState.Modified;
                db.SaveChanges();

                Pregunta pregunta = db.Pregunta.FirstOrDefault(p=>p.ID == opcion.PreguntaID);

                return RedirectToAction("Index", "Pregunta", new { pExamenID = pregunta.ExamenID });
            }
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", opcion.PreguntaID);
            return View(opcion);
        }

        // GET: Opcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opcion.Find(id);
            if (opcion == null)
            {
                return HttpNotFound();
            }
            return View(opcion);
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opcion option = db.Opcion.FirstOrDefault(o=>o.ID == id);

            Opcion opcion = db.Opcion.Find(id);
            db.Opcion.Remove(opcion);
            db.SaveChanges();

            
            Pregunta pregunta = db.Pregunta.FirstOrDefault(p=>p.ID == option.PreguntaID);
            return RedirectToAction("Index", "Pregunta", new { pExamenID = pregunta.ExamenID });
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

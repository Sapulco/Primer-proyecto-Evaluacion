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
    public class ResultadoController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Resultado
        public ActionResult Index()
        {
            var resultado = db.Resultado.Include(r => r.Pregunta).Include(r => r.Usuario);
            return View(resultado.ToList());
        }

        // GET: Resultado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultado resultado = db.Resultado.Find(id);
            if (resultado == null)
            {
                return HttpNotFound();
            }
            return View(resultado);
        }

        // GET: Resultado/Create
        public ActionResult Create()
        {
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre");
            ViewBag.UsuarioID = new SelectList(db.Usuario, "ID", "CuentaUsuario");
            return View();
        }

        // POST: Resultado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UsuarioID,FechaRespuesta,PreguntaID,Respuesta")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                db.Resultado.Add(resultado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", resultado.PreguntaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "ID", "CuentaUsuario", resultado.UsuarioID);
            return View(resultado);
        }

        // GET: Resultado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultado resultado = db.Resultado.Find(id);
            if (resultado == null)
            {
                return HttpNotFound();
            }
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", resultado.PreguntaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "ID", "CuentaUsuario", resultado.UsuarioID);
            return View(resultado);
        }

        // POST: Resultado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UsuarioID,FechaRespuesta,PreguntaID,Respuesta")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PreguntaID = new SelectList(db.Pregunta, "ID", "Nombre", resultado.PreguntaID);
            ViewBag.UsuarioID = new SelectList(db.Usuario, "ID", "CuentaUsuario", resultado.UsuarioID);
            return View(resultado);
        }

        // GET: Resultado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultado resultado = db.Resultado.Find(id);
            if (resultado == null)
            {
                return HttpNotFound();
            }
            return View(resultado);
        }

        // POST: Resultado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resultado resultado = db.Resultado.Find(id);
            db.Resultado.Remove(resultado);
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

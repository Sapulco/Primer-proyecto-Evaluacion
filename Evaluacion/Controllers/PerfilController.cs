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
    public class PerfilController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Perfil
        public ActionResult Index()
        {
            return View(db.Perfil.ToList());
        }

        // GET: Perfil/Details/5
        public ActionResult Details(int pPerfilID)
        {
            Perfil perfil = db.Perfil.FirstOrDefault(p=>p.ID == pPerfilID);
            ViewBag.PerfilID = perfil;
            List<Modulo> lstModulos = db.Modulo.Where(m=>m.IsActivo == true).ToList();
            ViewBag.Modulos = lstModulos;
            List<PerfilModulo> ModulosPerfil = db.PerfilModulo.Where(pm=>pm.PerfilID == pPerfilID).Include(pm=>pm.Modulo).ToList();
            

            return View(ModulosPerfil);
        }
        public JsonResult ActualizarAcceso(int pModuloID, int pPerfilID, bool pIsChecked)
        {
            PerfilModulo perfilmodulo = new PerfilModulo();
            int existe = db.PerfilModulo.Where(pm=>pm.ModuloID == pModuloID && pm.PerfilID == pPerfilID).Count();

            if(existe < 1 &&pIsChecked == true)
            {
                perfilmodulo.ModuloID = pModuloID;
                perfilmodulo.PerfilID = pPerfilID;
                db.PerfilModulo.Add(perfilmodulo);
            }
            else
            {
                List<PerfilModulo> lstPerfilMOdulo = db.PerfilModulo.Where(pm=>pm.ModuloID == pModuloID && pm.PerfilID == pPerfilID).ToList();
                db.PerfilModulo.RemoveRange(lstPerfilMOdulo);
                
            }

            db.SaveChanges();

            return Json("");
        }
        // GET: Perfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,IsAdministrador")] Perfil perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nombre))
            {
                ModelState.AddModelError("Nombre",("Es necesario llenar este campo"));
            }

            if (ModelState.IsValid)
            {
                db.Perfil.Add(perfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(perfil);
        }

        // GET: Perfil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil perfil = db.Perfil.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,IsAdministrador")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(perfil);
        }

        // GET: Perfil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil perfil = db.Perfil.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perfil perfil = db.Perfil.Find(id);
            db.Perfil.Remove(perfil);
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

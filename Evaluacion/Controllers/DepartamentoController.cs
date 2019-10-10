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
    public class DepartamentoController : Controller
    {
        private Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Departamento
        public ActionResult Index()
        {
            return View(db.Departamento.ToList());
        }
        public ActionResult DepartamentosPerfil(int pPerfilID)
        {
            
            Perfil perfil = db.Perfil.FirstOrDefault(p=>p.ID == pPerfilID);
            ViewBag.PerfilID = perfil;
            List<Departamento> lstDepartamentos = db.Departamento.ToList();
            ViewBag.DepartamentosID = lstDepartamentos;
            List<PerfilDepartamento> DepartamentosPerfil = db.PerfilDepartamento.Where(pd=>pd.PerfilID == pPerfilID).Include(pd=>pd.Departamento).ToList();
            return View(DepartamentosPerfil);
        }

        public JsonResult ActualizarAcceso(int pPerfilID, int pDepartamentoID, bool pIsChecked)
        {
            PerfilDepartamento perfilDepartamento = new PerfilDepartamento();
            int cantidad = db.PerfilDepartamento.Where(pd => pd.PerfilID == pPerfilID && pd.DepartamentoID == pDepartamentoID).Count();

            if (cantidad < 1 && pIsChecked == true)
            {
                perfilDepartamento.PerfilID = pPerfilID;
                perfilDepartamento.DepartamentoID = pDepartamentoID;

                db.PerfilDepartamento.Add(perfilDepartamento);
            }
            else
            {
                List<PerfilDepartamento> lstPerfilDepartamento = db.PerfilDepartamento.Where(pd=>pd.PerfilID == pPerfilID && pd.DepartamentoID == pDepartamentoID).ToList();

                db.PerfilDepartamento.RemoveRange(lstPerfilDepartamento);

            }
            db.SaveChanges();
            return Json("");
        }

        // GET: Departamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre")] Departamento departamento)
        {
            if (string.IsNullOrWhiteSpace(departamento.Nombre))
            {
                ModelState.AddModelError("Nombre",("Es necesario llenar este campo"));
            }


            if (ModelState.IsValid)
            {
                db.Departamento.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        // GET: Departamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = db.Departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamento.Find(id);
            db.Departamento.Remove(departamento);
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

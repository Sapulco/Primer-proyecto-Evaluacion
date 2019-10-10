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

    public class ExamenController : Controller
    {
        #region Variables Globales

            private Evaluacion_DbContext db = new Evaluacion_DbContext();
            private Usuario usr = new Usuario();

        #endregion

        public int SumarDosNumeros(int a, int b)
        {
            return a + b;
        }

        public int SumarDosNumeros(string a, string b)
        {
            return Convert.ToInt32(a) + Convert.ToInt16(b);
        }

        public int SumarDosNumeros(string a)
        {
            try
            {

                int resultado = Convert.ToInt32(a) + 16;
                return resultado;

            }
            catch
            {
                throw;
            }
        }

        public void ObtenereUsuarioLogeado()
        {
            try
            {
                usr = db.Usuario.Include(u => u.Perfil.PerfilDepartamentos.Select(pd => pd.Departamento.Examenes)).FirstOrDefault(u => u.CuentaUsuario == User.Identity.Name);
            }
            catch
            {
                throw;
            }

        }


        [Authorize]
        // GET: Examen
        public ActionResult ListadoXDepartamento()
        {
            try
            {

                ObtenereUsuarioLogeado();



            //string varX = "56";
            int resultado = SumarDosNumeros("57");

                //Usuario usr = db.Usuario.Include(u => u.Perfil.PerfilDepartamentos.Select(pd => pd.Departamento.Examenes)).FirstOrDefault(u => u.CuentaUsuario == User.Identity.Name);
                List<Departamento> lstDepartamentos = new List<Departamento>();
                if (usr.Perfil.IsAdministrador)
                {
                    lstDepartamentos = db.Departamento.Include(d => d.Examenes).ToList();
                }
                else
                {
                    lstDepartamentos = usr.Perfil.PerfilDepartamentos.Select(pd => pd.Departamento).ToList();
                }


            

                return View(lstDepartamentos);
            }
            catch (Exception ex)
            {
                //strError = ex.Message;

                //if (!string.IsNullOrWhiteSpace(strError))
                //{
                    ViewBag.Error = ex.Message;
                //}
                
            }

            return View();
        }

        public ActionResult Index()
        {

            Usuario usr = db.Usuario.Include(u=>u.Perfil.PerfilDepartamentos.Select(pd=>pd.Departamento.Examenes.Select(e=>e.Preguntas))).FirstOrDefault(u=>u.CuentaUsuario == User.Identity.Name);
            ViewBag.UserP = usr;

            List<Departamento> lstDpto = new List<Departamento>();
            if (usr.Perfil.IsAdministrador)
            {
                lstDpto = db.Departamento.Include(d=>d.Examenes.Select(e=>e.Preguntas)).ToList();
                
            }
            else
            {
                lstDpto = usr.Perfil.PerfilDepartamentos.Select(pd => pd.Departamento).ToList();
            }
            return View(lstDpto);
        }

        public PartialViewResult ExamenesInactivos(int pDepartamentoID)
        {
            List<Examen> lstExamenesInactivos = db.Examen.Where(e=>e.DepartamentoID == pDepartamentoID && e.IsLiberado == false).ToList();
            return PartialView("_ExamenesInactivos", lstExamenesInactivos);
        }
        public ActionResult Examenes(string strPalabraBuscar)
        {
            ViewBag.PalabraBuscar = strPalabraBuscar;
            //string strMinusPalabraBuscar = strPalabraBuscar.ToLower();

            string strMinusPalabraBuscar = string.Empty;

            if (!string.IsNullOrWhiteSpace(strPalabraBuscar))
            {
                strMinusPalabraBuscar = strPalabraBuscar.ToLower();
            }

            Usuario usr = db.Usuario.Include(u=>u.Perfil.PerfilDepartamentos.Select(pd=>pd.Departamento.Examenes)).FirstOrDefault(u=>u.CuentaUsuario == User.Identity.Name);

            List<Examen> lstExamenes = new List<Examen>();
            if(string.IsNullOrWhiteSpace(strMinusPalabraBuscar))
            {
                if (usr.Perfil.IsAdministrador)
                {
                    lstExamenes = db.Examen.Where(e => e.IsLiberado == true).ToList();
                }
                else
                {
                    lstExamenes = usr.Perfil.PerfilDepartamentos.SelectMany(pd => pd.Departamento.Examenes.Where(e => e.IsLiberado == true)).ToList();
                }

                return View(lstExamenes);
            }
            else
            {
                if (usr.Perfil.IsAdministrador)
                {
                    lstExamenes = db.Examen.Where(e => e.IsLiberado == true && e.NombreExamen.ToLower().Contains(strMinusPalabraBuscar)).ToList();
                }
                else
                {
                    lstExamenes = usr.Perfil.PerfilDepartamentos.SelectMany(pd => pd.Departamento.Examenes.Where(e => e.IsLiberado == true && e.NombreExamen.ToLower().Contains(strMinusPalabraBuscar))).ToList();
                }

                return View(lstExamenes);
            }
            

        }

        public ActionResult Responder(int pExamenID)
        {
            Examen examen = db.Examen.Include(e => e.Preguntas.Select(p=>p.Opciones))
                .FirstOrDefault(e=>e.ID == pExamenID && e.IsLiberado);
            if (examen != null)
            {
                return View(examen);
            }
            else
            {
                return RedirectToAction("Index", new { pExamenID });
            }
            
        }
        [HttpPost, ActionName("Responder")]
        public ActionResult Resultado(int pExamenID)
        {
            Resultado resultado = new Resultado();
            Usuario usr = db.Usuario.FirstOrDefault(u=>u.CuentaUsuario == User.Identity.Name);
            Examen examen = db.Examen.Include(e=>e.Preguntas.Select(p=>p.Opciones)).FirstOrDefault(e=>e.ID == pExamenID);

            foreach(Pregunta pregunta in examen.Preguntas)
            {
                string srtResultado = string.Empty;

                //if (pregunta.TipoPregunta.IsListado && !pregunta.TipoPregunta.IsMultiple)
                //{
                //    srtResultado = Request["txtPregunta_" + pregunta.ID];
                //}
                //else if (pregunta.TipoPregunta.IsListado && pregunta.TipoPregunta.IsMultiple)
                //{
                //    srtResultado = Request.Form["txtPregunta_" + pregunta.ID];
                //}
                //else
                //{
                //    srtResultado = Request["txtPregunta_" + pregunta.ID];
                //}

                ////Esto es sin el if
                srtResultado = Request["txtPregunta_" + pregunta.ID];




                resultado.UsuarioID = usr.ID;
                resultado.FechaRespuesta = DateTime.Now;
                resultado.PreguntaID = pregunta.ID;
                resultado.Respuesta = srtResultado;
                db.Resultado.Add(resultado);
                db.SaveChanges();
                
            }
            return RedirectToAction("PreguntaGuardada");
        }



        // GET: Examen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = db.Examen.Find(id);
            if (examen == null)
            {
                return HttpNotFound();
            }
            return View(examen);
        }

        // GET: Examen/Create
        public ActionResult Create(int? pDepartamentoID)
        {
            ViewBag.Departamento = db.Departamento.FirstOrDefault(d=>d.ID ==  pDepartamentoID);
            ViewBag.UsuarioAltaID = new SelectList(db.Usuario, "ID", "CuentaUsuario");
            
            return View();
        }

        // POST: Examen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NombreExamen,DepartamentoID,UsuarioAltaID,FechaAlta,IsLiberado,vmNombreExamen")] Examen examen)
        {

            if(string.IsNullOrWhiteSpace(examen.NombreExamen))
            {
                ModelState.AddModelError("vmNombreExamen", ("Es necesario llenar este campo"));
            }
            if (ModelState.IsValid)
            {
                Usuario usr = db.Usuario.FirstOrDefault(u=>u.CuentaUsuario == User.Identity.Name);
                examen.UsuarioAltaID = usr.ID;
                examen.FechaAlta = DateTime.Now;
                db.Examen.Add(examen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Nombre", examen.DepartamentoID);
            ViewBag.UsuarioAltaID = new SelectList(db.Usuario, "ID", "CuentaUsuario", examen.UsuarioAltaID);
            ViewBag.Departamento = db.Departamento.FirstOrDefault(d => d.ID == examen.DepartamentoID);
            return View(examen);
        }

        // GET: Examen/Edit/5
        public ActionResult Edit(int? pExamenID)
        {
            if (pExamenID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = db.Examen.Find(pExamenID);
            if (examen == null)
            {
                return HttpNotFound();
            }
            return View(examen);
        }

        // POST: Examen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NombreExamen,DepartamentoID,UsuarioAltaID,FechaAlta,IsLiberado")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(examen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartamentoID = new SelectList(db.Departamento, "ID", "Nombre", examen.DepartamentoID);
            ViewBag.UsuarioAltaID = new SelectList(db.Usuario, "ID", "CuentaUsuario", examen.UsuarioAltaID);
            return View(examen);
        }

        // GET: Examen/Delete/5
        public ActionResult Delete(int? pExamenID)
        {
            if (pExamenID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = db.Examen.Find(pExamenID);
            if (examen == null)
            {
                return HttpNotFound();
            }
            return View(examen);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Examen examen = db.Examen.Find(id);
            db.Examen.Remove(examen);
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

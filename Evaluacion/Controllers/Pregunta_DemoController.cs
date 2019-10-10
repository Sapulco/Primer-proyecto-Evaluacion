using Evaluacion.DAL;
using Evaluacion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluacion.Controllers
{
    public class Pregunta_DemoController : Controller
    {
        Evaluacion_DbContext db = new Evaluacion_DbContext();

        // GET: Pregunta_Demo
        public ActionResult CargarArchivo()
        {
            return View();
        }
        [HttpPost, ActionName("CargarArchivo")]
        public ActionResult ArchivoCargar()
        {
            DataSet ds = new DataSet();

            if (Request.Files["CargarArchivo"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["CargarArchivo"].FileName);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string filename = Request.Files["CargarArchivo"].FileName;
                    //string fileLocation = Server.MapPath("") + filename;
                    string pathToSave = Server.MapPath("~/Content/Cargas/Preguntas/");
                    //filename = Path.GetFileName(Request.Files["archivo"].FileName);

                    if (System.IO.File.Exists(Path.Combine(pathToSave, filename)))
                    {
                        Random rand = new Random();
                        filename = rand.Next(10000000) + "_" + filename;

                        //System.IO.File.Delete(Path.Combine(pathToSave, filename));
                    }

                    string fileLocation = Path.Combine(pathToSave, filename);

                    Request.Files["CargarArchivo"].SaveAs(fileLocation);

                    ////Leer el archivo cargado en la ubicación en la que se guardó
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                }
                else
                {
                    ModelState.AddModelError("Resultado", "El formato del archivo debe ser .xls ó .xlsx.");
                }
            }
            else
            {
                ModelState.AddModelError("Resultado", "El archivo está vacío.");
            }


            ///////A partir de aquì, leemos el objeto "ds" el cual se llenò con cada hoja del libro del excel cargado
            if (ModelState.IsValid)
            {
                Usuario usr = db.Usuario.FirstOrDefault(u=>u.CuentaUsuario == User.Identity.Name);
                string departamento = string.Empty;
                string examen = string.Empty;
                string orden = string.Empty;
                string tipopregunta = string.Empty;

                Departamento objDepartamento;
                Examen objExamen;
                TipoPregunta objTipoPregunta;
                Pregunta objPregunta;

                foreach (DataRow registro in ds.Tables[0].Rows)
                {

                    departamento = registro["Departamento"].ToString().ToLower();
                    examen = registro["Examen"].ToString().ToLower();


                    objDepartamento = db.Departamento.FirstOrDefault(d=>d.Nombre.ToLower() == departamento);

                    if (objDepartamento == null)
                    {
                        objDepartamento = new Departamento();
                        objDepartamento.Nombre = registro["Departamento"].ToString();

                        db.Departamento.Add(objDepartamento);
                        db.SaveChanges();
                    }

                    objExamen = db.Examen.FirstOrDefault(e=>e.NombreExamen.ToLower() == examen);
                    if (objExamen == null)
                    {
                        objExamen = new Examen();
                        objExamen.DepartamentoID = objDepartamento.ID;
                        objExamen.NombreExamen = registro["Examen"].ToString();
                        objExamen.UsuarioAltaID = usr.ID;
                        objExamen.IsLiberado = false;
                        objExamen.FechaAlta = DateTime.Now;

                        db.Examen.Add(objExamen);
                        db.SaveChanges();
                    }

                    
                    orden = registro["Orden"].ToString();
                    tipopregunta = registro["TipoPregunta"].ToString().ToLower();

                    objTipoPregunta = db.TipoPregunta.FirstOrDefault(tp=>tp.Nombre.ToLower() == tipopregunta);

                    objPregunta = new Pregunta();
                    objPregunta.Nombre = registro["Texto de la pregunta"].ToString();
                    objPregunta.Orden = Int32.Parse(orden);
                    objPregunta.TipoPreguntaID = objTipoPregunta.ID;
                    objPregunta.ExamenID = objExamen.ID;


                    db.Pregunta.Add(objPregunta);
                    db.SaveChanges();

                }


            }

            



            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Departamento
    {
        public int ID { get; set; }
        public string Nombre { get; set; }



        [InverseProperty("Departamento")]
        public ICollection<PerfilDepartamento> PerfilDepartamentos { get; set; }
        [InverseProperty("Departamento")]
        public ICollection<Examen> Examenes { get; set; }
    }
}
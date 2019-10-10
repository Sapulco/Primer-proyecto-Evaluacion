using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class PerfilDepartamento
    {
        public int ID { get; set; }
        [ForeignKey("Perfil")]
        public int PerfilID { get; set; }
        [ForeignKey("Departamento")]
        public int DepartamentoID { get; set; }




        public virtual Perfil Perfil { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
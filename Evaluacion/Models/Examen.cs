using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Examen
    {
        public int ID { get; set; }
        
        [DisplayName("Nombre del Examen")]
        public string NombreExamen { get; set; }
        [ForeignKey("Departamento")]
        public int DepartamentoID { get; set; }
        [ForeignKey("UsuarioAlta")]
        public int UsuarioAltaID { get; set; }
        [DisplayName("Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        public bool IsLiberado { get; set; }



        public virtual Departamento Departamento { get; set; }
        public virtual Usuario UsuarioAlta { get; set; }
        [InverseProperty("Examen")]
        public ICollection<Pregunta> Preguntas { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Pregunta
    {
        #region Propiedades
        public int ID { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Examen")]
        public int ExamenID { get; set; }
        [ForeignKey("TipoPregunta")]
        public int TipoPreguntaID { get; set; }
        [DisplayName("Orden Pregunta")]
        public int Orden { get; set; }

        #endregion

        #region Relaciones

        public virtual Examen Examen { get; set; }
        public virtual TipoPregunta TipoPregunta { get; set; }
        [InverseProperty("Pregunta")]
        public ICollection<Resultado> Resultados { get; set; }
        [InverseProperty("Pregunta")]
        public ICollection<Opcion> Opciones { get; set; }

        #endregion 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class TipoPregunta
    {
        #region Propiedades

        public int ID { get; set; }
        public string Nombre { get; set; }
        public bool IsListado { get; set; }
        public bool IsMultiple { get; set; }
        #endregion


        #region Relaciones
        [InverseProperty("TipoPregunta")]
        public ICollection<Pregunta> Preguntas { get; set; }

        #endregion
    }
}
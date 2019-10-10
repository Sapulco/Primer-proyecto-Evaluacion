using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Opcion
    {
        #region Propiedades
        public int ID { get; set; }
        public string Valor { get; set; }
        [ForeignKey("Pregunta")]
        public int PreguntaID { get; set; }
        #endregion


        #region

        public virtual Pregunta Pregunta { get; set; }

        #endregion
    }
}
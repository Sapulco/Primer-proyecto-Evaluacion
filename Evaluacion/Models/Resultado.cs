using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Resultado
    {
        #region Relaciones
        public int ID { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioID { get; set; }
        [DisplayName("Fecha de Respuesta")]
        public DateTime FechaRespuesta { get; set; }
        [ForeignKey("Pregunta")]
        public int PreguntaID { get; set; }
        public string Respuesta { get; set; }
        #endregion



        #region Relaciones
        public virtual Usuario Usuario { get; set; }
        public virtual Pregunta Pregunta { get; set; }

        #endregion
    }
}
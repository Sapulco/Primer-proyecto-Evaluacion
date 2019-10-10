using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class PerfilModulo
    {
        #region Propiedades
        public int ID { get; set; }
        [ForeignKey("Perfil")]
        public int PerfilID { get; set; }
        [ForeignKey("Modulo")]
        public int ModuloID { get; set; }
        #endregion


        #region Relaciones

        public virtual Perfil Perfil { get; set; }
        public virtual Modulo Modulo { get; set; }

        #endregion
    }
}
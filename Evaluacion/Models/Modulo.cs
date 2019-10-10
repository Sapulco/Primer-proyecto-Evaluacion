using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Modulo
    {
        #region Propiedades
        public int ID { get; set; }
        public string Nombre { get; set; }
        public bool IsActivo { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        #endregion

        #region Relaciones
        [InverseProperty("Modulo")]
        public ICollection<PerfilModulo> PerfilModulos { get; set; }

        #endregion

    }
}
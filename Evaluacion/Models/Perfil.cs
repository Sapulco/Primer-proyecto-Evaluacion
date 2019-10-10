using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Perfil
    {
        #region Propiedades
        public int ID { get; set; }
        public string Nombre { get; set; }
        public bool IsAdministrador { get; set; }
        #endregion




        #region Relaciones
        [InverseProperty("Perfil")]
        public ICollection<Usuario> Usuarios { get; set; }
        [InverseProperty("Perfil")]
        public ICollection<PerfilModulo> PerfilModulos { get; set; }
        [InverseProperty("Perfil")]
        public ICollection<PerfilDepartamento> PerfilDepartamentos { get; set; }

        #endregion
    }
}
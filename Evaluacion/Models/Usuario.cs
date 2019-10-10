using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evaluacion.Models
{
    public class Usuario
    {
        #region Propiedades
        public int ID { get; set; }
        [DisplayName("Cuenta de Usuario")]
        public string CuentaUsuario { get; set; }
        [DisplayName("Contrasena")]
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        public string Paterno { get; set; }
        [DisplayName("Apellido Materno")]
        public string Materno { get; set; }
        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [ForeignKey("Perfil")]
        public int PerfilID { get; set; }
        #endregion

        #region Relaciones

        public virtual Perfil Perfil { get; set; }
        [InverseProperty("UsuarioAlta")]
        public ICollection<Examen> AltaExamenes { get; set; }
        [InverseProperty("Usuario")]
        public ICollection<Resultado> Resultados { get; set;}
        #endregion

    }
}
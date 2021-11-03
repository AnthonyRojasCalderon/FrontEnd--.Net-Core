using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class UsuarioModel
    {
        #region Propiedades
        public string ID { get; set; }
        public string NomUsuario { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        #endregion

        #region Constructor
        public UsuarioModel()
        {
            ID = string.Empty;
            NomUsuario = string.Empty;
            Password = string.Empty;
            Estado = false;
            FechaCreacion = DateTime.MinValue;
        }
        #endregion
    }
}

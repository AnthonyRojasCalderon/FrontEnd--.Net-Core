using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class UsuarioController : Controller
    {
        #region Propiedades

        public HttpClient ClienteConexion { get; set; }

        #endregion

        #region Constructor

        public UsuarioController()
        {
            ClienteConexion = new HttpClient();
            EstablecerParametrosClienteHttp();
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Metodo para inicializar el ClienteConexion hacia el web api a consumir
        /// </summary>
        private void EstablecerParametrosClienteHttp()
        {
            ClienteConexion.BaseAddress = new Uri("http://localhost:62607/");
            ClienteConexion.DefaultRequestHeaders.Accept.Clear();
            ClienteConexion.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("aplication/json"));
        }

        #region Persona
        /// <summary>
        /// Metodo para listar personas de la base de datos
        /// </summary>
        /// <returns></returns>
        private async Task<List<UsuarioModel>> ListarUsuarios()
        {
            List<UsuarioModel> lstResultado = new List<UsuarioModel>();

            //Construyo la URL a la cual se llamara al api
            string url = "api/Usuarios/Consulta";
            HttpResponseMessage resultado = await ClienteConexion.GetAsync(url);

            //valida los datos y se convierten
            if (resultado.IsSuccessStatusCode)
            {
                var jsonString = await resultado.Content.ReadAsStringAsync();
                lstResultado = JsonConvert.DeserializeObject<List<UsuarioModel>>(jsonString);
            }

            return lstResultado;
        }

        /// <summary>
        /// Metodo para crear un usuario
        /// </summary>
        /// <param name="U_Modelo"></param>
        /// <returns></returns>
        private async Task<bool> GuardarUsuario(UsuarioModel U_Modelo)
        {
            string url = "api/Usuarios/Agregar";
            HttpResponseMessage resultado = await ClienteConexion.PostAsJsonAsync(url,U_Modelo);
            return resultado.IsSuccessStatusCode;
        }

        /// <summary>
        /// Metodo para editar un usuario
        /// </summary>
        /// <param name="U_Modelo"></param>
        /// <returns></returns>
        private async Task<bool> ModificarUsuario(UsuarioModel U_Modelo)
        {
            string url = "api/Usuarios/Modificar";
            HttpResponseMessage resultado = await ClienteConexion.PostAsJsonAsync(url, U_Modelo);
            return resultado.IsSuccessStatusCode;
        }

        /// <summary>
        /// Metodo para eliminar un usuario
        /// </summary>
        /// <param name="U_Modelo"></param>
        /// <returns></returns>
        private async Task<bool> DeleteUser(UsuarioModel U_Modelo)
        {
            string url = "api/Usuarios/Eliminar";
            HttpResponseMessage resultado = await ClienteConexion.PostAsJsonAsync(url, U_Modelo);
            return resultado.IsSuccessStatusCode;
        }

        #endregion

        #endregion

        #region Eventos del Controlador

        public async Task<IActionResult> Index()
        {            
            List<UsuarioModel> lstUsuario = await ListarUsuarios();           
            return View(lstUsuario);
        }

        public ActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioModel U_Modelo)
        {
            bool v_resultado = await GuardarUsuario(U_Modelo);
            return RedirectToAction("Index");
        }

        public ActionResult EditarUsuario(string id, string nomusuario)
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(UsuarioModel U_Modelo)
        {
            bool v_resultado = await ModificarUsuario(U_Modelo);
            return RedirectToAction("Index");
        }

        public ActionResult EliminarUsuario(string id,string nomusuario,bool estado, DateTime fechacreacion)
        {
            UsuarioModel datos = new UsuarioModel();
            datos.ID = id;
            datos.NomUsuario = nomusuario;
            datos.Estado = estado;
            datos.FechaCreacion = fechacreacion;
            return View(datos);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(UsuarioModel U_Modelo)
        {
            bool v_resultado = await DeleteUser(U_Modelo);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
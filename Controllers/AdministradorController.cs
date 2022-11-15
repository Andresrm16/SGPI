using Microsoft.AspNetCore.Mvc;
using SGPI_Andres.Models;

namespace SGPI.Controllers
{
    public class AdministradorController : Controller
    {

        SGPIDBContext context = new SGPIDBContext();
        public IActionResult Login()
        {
          /*   //Create
           Usuario usr = new Usuario();
            usr.PrimerNombre = "Mauricio";
            usr.SegundoNombre = String.Empty;
            usr.PrimerApellido = "Amariles";
            usr.SegundoApellido = "Camacho";
            usr.Email = "mauricio.amariles@tdea-edu.co";
            usr.IdDoc = 1;
            usr.IdGenero = 1;
            usr.IdRol = 1;
            usr.IdPrograma = 1;
            usr.NumeroDocumento = "123456789";
            usr.Password = "123456789";

            context.Add(usr);
            context.SaveChanges();*/
/*

            //QUERY
            Usuario usuario = new Usuario();
            usuario = context.Usuarios
                .Single(b => b.NumeroDocumento == "123456789");
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = context.Usuarios.ToList();

            //Update
            var usr = context.Usuarios
                .Where(cursor => cursor.IdUsuario == 1)
                .FirstOrDefault();
            if (usr != null)
            {
                usr.SegundoNombre = "Diego";
                usr.SegundoApellido = "Camacho";
                context.Usuarios.Update(usr);
                context.SaveChanges();
            }

            //Delete
            var usuarioEliminar = context.Usuarios
                .Where(cursor => cursor.IdUsuario == 1)
                .FirstOrDefault();
            context.Usuarios.Remove(usuarioEliminar);
*/
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(Usuario user)
        {
            string numeroDoc = user.NumeroDocumento;
            string pass = user.Password;

            var usuarioLogin = context.Usuarios
                .Where(Consulta =>
                Consulta.NumeroDocumento == numeroDoc &&
                Consulta.Password == pass).FirstOrDefault();
            if (usuarioLogin != null)
            {
                //ADMINISTRADOR
                if (usuarioLogin.IdRol == 1) {
                    CrearUsuario();
                    return Redirect("Administrador/CrearUsuario");
                }
                //Coordinador
                else if (usuarioLogin.IdRol == 2) {
                    return Redirect(""+
                        "Coordinador/BuscarCoordinador");
                }
                //Estudiante
                else if (usuarioLogin.IdRol == 3)
                {
                    return Redirect("Estudiante/Actualizar/?IdUsuario=" + usuarioLogin.IdUsuario);
                }
                //No existe este rol
                else { }
            }
            else {
                return ViewBag.mensaje = "Usuario no existe";

            }
            return View();
        }
        //Agrego el de olvidar contraseña
        public IActionResult OlvidarContrasena()
        {
            return View();
        }
        //Agrego el de crear usuario
        public IActionResult CrearUsuario()
        {
            ViewBag.genero = context.Generos.ToList();
            ViewBag.documento = context.Documentos.ToList();
            ViewBag.rol = context.Rols.ToList();
            ViewBag.programa = context.Programas.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CrearUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            ViewBag.mensaje = "Usuario creado exitosamente";
            
            ViewBag.genero = context.Generos.ToList();
            ViewBag.documento = context.Documentos.ToList();
            ViewBag.rol = context.Rols.ToList();
            ViewBag.programa = context.Programas.ToList();

            return View();

        }

        public IActionResult BuscarUsuario()
        {
            Usuario us = new Usuario();
            return View(us);
        }
        
        
        [HttpPost]
        public IActionResult BuscarUsuario(Usuario usuario)
        {
            string numeroDoc = usuario.NumeroDocumento;
            var user = context.Usuarios
                .Where(consulta => consulta.NumeroDocumento == numeroDoc).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            else 
                return View(); 
        }

        [HttpPost]
        public IActionResult EliminarUsuario(Usuario usuario)
        {

            context.Update(usuario);
            context.SaveChanges();
            return Redirect("/Administrador/BuscarUsuario");
        }

        public IActionResult EliminarUsuario(int? IdUsuario)
        {
            Usuario user = context.Usuarios.Find(IdUsuario);
            if (user != null)
            {
                context.Remove(user);
                context.SaveChanges();
            }
            return Redirect("/Administrador/BuscarUsuario");
        }


        public IActionResult ModificarUsuario(int ? IdUsuario)
        {
            Usuario usuario = context.Usuarios.Find(IdUsuario);
            if (usuario != null)
            {
                ViewBag.genero = context.Generos.ToList();
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.rol = context.Rols.ToList();
                ViewBag.programa = context.Programas.ToList();
                return View(usuario);
            }
            else
            {
                return Redirect("/Administrador/BuscarUsuario");
            }
        }
        
        
        [HttpPost]
        public IActionResult ModificarUsuario(Usuario usuario)
        {
            context.Update(usuario);
            context.SaveChanges();
            return Redirect("/Administrador/BuscarUsuario");
        }


        public IActionResult Reportes()
        {
            ViewBag.documento = context.Documentos.ToList();
            return View();
        }
    }
}

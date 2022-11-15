using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SGPI_Andres.Models;

namespace SGPI_Andres.Controllers
{
    public class EstudianteController : Controller
    {
        SGPIDBContext context = new SGPIDBContext();
        public IActionResult Actualizar(int? IdUsuario)
        {
            Usuario usuario = context.Usuarios.Find(IdUsuario);

            if (usuario != null)
            {
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.genero = context.Generos.ToList();
                ViewBag.programa = context.Programas.ToList();
                return View(usuario);
            }
            else
            {
                return Redirect("/Estudiante/Actualizar/?Idusuario=" + usuario.IdUsuario);
            }
        }
        [HttpPost]

        public IActionResult Actualizar(Usuario usuario)
        {
            var usuarioActualizar = context.Usuarios.Where(consulta => consulta.IdUsuario == usuario.IdUsuario).FirstOrDefault();

            //Usuario usuarioActualizar = context.Usuarios.Find(usuario);
            usuarioActualizar.NumeroDocumento = usuario.NumeroDocumento;
            usuarioActualizar.PrimerNombre = usuario.PrimerNombre;
            usuarioActualizar.SegundoNombre = usuario.SegundoNombre;
            usuarioActualizar.PrimerApellido = usuario.PrimerApellido;
            usuarioActualizar.SegundoApellido = usuario.SegundoApellido;
            usuarioActualizar.IdPrograma = usuario.IdPrograma;
            usuarioActualizar.IdGenero = usuario.IdGenero;
            usuarioActualizar.Password = usuario.Password;

            context.Update(usuarioActualizar);
            context.SaveChanges();

            return Redirect("/Estudiante/Actualizar/?Idusuario=" + usuarioActualizar.IdUsuario);
        }

        
        
        
        public IActionResult EstudiantePago(Pago pago)
        {
            return View();
        }



        [HttpPost]
        public IActionResult EstudiantePago(Pago pago, Estudiante estudiante)
        {
            var Actualizar = context.Estudiantes
                .Where(consulta =>
                consulta.IdUsuario == estudiante.IdUsuario).FirstOrDefault();
            pago.Estado = true;
            context.Pagos.Add(pago);
            context.SaveChanges();
            ViewBag.mensaje = "Pago realizado";

            Actualizar.IdPago = pago.IdPago;
            Actualizar.IdUsuario = estudiante.IdUsuario;
            Actualizar.Archivo = "";
            Actualizar.Egreado = true;

            context.Update(Actualizar);
            context.SaveChanges();

            return View();


        }

    }
}

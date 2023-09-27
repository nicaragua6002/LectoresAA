using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LectoresAA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LectoresAA
{
    /// <summary>
    /// Descripción breve de LectoresAAWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class LectoresAAWS : System.Web.Services.WebService
    {
        LectoresAAContainer bd = new LectoresAAContainer();
        ApplicationDbContext context = new ApplicationDbContext();
        [WebMethod]
        public ResultRegister RegistrarUsuario(string Email, string Pass)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ResultRegister Value = new ResultRegister();


                var user = new ApplicationUser();
                user.UserName = Email;
                user.Email = Email;

                string userPWD = Pass;

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role User    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Autor");

                     Value.Band = true;
                    Value.Mensaje = "Se creo con exito";

                }
                else
                {
                    Value.Band = false;
                    Value.Mensaje = "Ocurrio un error no se pudo crear";
                }
            
          
            return Value;
        }


        [WebMethod]
        public bool LogueoUsuario(string Email, string Pass)
        {
            return Validar(Email, Pass);
        }

        bool Validar(string AccountName, string Pass)
        {

            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            // var  result = SignInManager.PasswordSignInAsync(user, pass, false, shouldLockout: false);
            var result = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().PasswordSignIn(AccountName, Pass, false, false);


            if (result == SignInStatus.Success)
            {
                return true;
            }
            else
                return false;
        }


        [WebMethod]
        public int AgregarPublicacion(string AccountName, string Pass, Publicacion x)
        {
            if (Validar(AccountName, Pass))
            {
                bd.Publicaciones.Add(new Models.Publicacion()
                {
                    Id = x.Id,
                    //From = x.From,
                    //To = x.To,
                    //Content = x.Content,
                    //Date = x.Date,
                    //Status = x.Status
                });
                return bd.SaveChanges();
            }
            else
                return 0;
        }

        [WebMethod]
        public string ListarPublicaciones()
        {
            return "Hola a todos";
        }
        [WebMethod]
        public string ListarCapitulos(int idPublicacion)
        {
            return "Hola a todos";
        }
        [WebMethod]
        public string AgregarComentario()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public string ListarComentarios(int idCapitulo)
        {
            return "Hola a todos";
        }


        public class ResultRegister
        {
            public bool Band { get; set; }
            public string Mensaje { get; set; }
        }

        public class Publicacion
        {
            public int Id { get; set; }
            public string Autor { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Fecha { get; set; }
            public string User { get; set; }
            public int TipoId { get; set; }

        }

        public class Capitulo
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Fecha { get; set; }
            public string Contenido { get; set; }
            public int PublicacionId { get; set; }
        }

        public class Comentario
        {
            public int Id { get; set; }
            public string User { get; set; }
            public string Fecha { get; set; }
            public string Contenido { get; set; }
            public int CapituloId { get; set; }
        }

    }
}

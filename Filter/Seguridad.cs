using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Filter
{
    public class Seguridad : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //En LoginController
            //HttpContext.Session.SetString("usuarioId", User.UsuarioId.ToString());
            var user = context.HttpContext.Session.GetString("UsuarioId");
            if (user == null)
            {
                context.Result = new RedirectResult("Login");
            }
        }
    }
}

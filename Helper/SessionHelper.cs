using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;//Referencia agregada para poder usar HttpContext y FormsAuthentication
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

namespace Helper
{
    /* Link de ayuda
     * https://anexsoft.com/clase-para-agilizar-la-autenticacion-en-asp-net-mvc
     */
    public class SessionHelper
    {
        //Validar si el usuario existe en la sesion
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        //Destruir la sesion actual para el acceso del usuario
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }

        //Obtener el usuario, recuperando el id de usuario
        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }

        //Agregar el usuario a la sesion , recibiendo el parametro id
        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
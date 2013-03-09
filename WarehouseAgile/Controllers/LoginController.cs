using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WarehouseAgile.Models;

namespace WarehouseAgile.Controllers
{
    public class LoginController : Controller
    {

        //
        // GET: /Login/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Login/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Main");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nazwa użytkownika lub hasło jest nieprawidłowe.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Login/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Main");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Login/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Aktualne lub nowe hasło jest nieprawidłowe.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Login/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Użytkownik już istnieje. Wprowadź inną nazwę użytkownika.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Już istnieje użytkownik skojarzony z podanym adresem e-mail. Podaj inny adres e-mail.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Wprowadzone hasło jest nieprawidłowe. Podaj inne hasło.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Wprowadzony adres e-mail jest nieprawidłowy.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Odpowiedź do pytania odzyskiwania hasła jest nieprawidłowa.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Pytanie odzyskiwania hasła jest nieprawidłowe.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Nazwa użytkownika jest nieprawidłowa. Podaj inną nazwę użytkownika.";

                case MembershipCreateStatus.ProviderError:
                    return "Dostawca autentykacji zwrócił błąd. Sprawdź dane i spróbuj ponownie, jeżeli błąd nadal występuje skontaktuj się z administratorem strony.";

                case MembershipCreateStatus.UserRejected:
                    return "Żądanie użytkownia zostało odrzucone. Sprawdź dane i spróbuj ponownie, jeżeli błąd nadal występuje skontaktuj się z administratorem strony.";

                default:
                    return "Wystąpił nieznany błąd. Sprawdź dane i spróbuj ponownie, jeżeli błąd nadal występuje skontaktuj się z administratorem strony.";
            }
        }
        #endregion
    }
}

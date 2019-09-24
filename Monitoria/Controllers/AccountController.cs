using Monitoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Monitoria.Controllers
{
    public class AccountController : Controller
    {
        private MonitoriaContext db = new MonitoriaContext();

        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View();
        }

        // <param name = "login" ></ param >
        // < param name="returnUrl"></param>
        // <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var vLogin = db.Usuario.Where(m => m.Login == model.Login).FirstOrDefault();

                if (vLogin != null)
                {

                    if (!Equals(vLogin.Email, ""))
                    {

                        if (Equals(vLogin.Senha, model.Senha))
                        {
                            FormsAuthentication.SetAuthCookie(vLogin.Login, false);
                            if (Url.IsLocalUrl(returnUrl)
                            && returnUrl.Length > 1
                            && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//")
                            && returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            Session["IdUsuario"] = vLogin.IdUsuario;
                            Session["Nome"] = vLogin.Nome;
                            Session["Cargo"] = vLogin.Cargo;
                            return RedirectToAction("Index", "Home");
                        }

                        else
                        {
                            ModelState.AddModelError("senha", "Senha informada Inválida!");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Usuário sem autorização para usar o sistema!");
                        return View(model);
                    }
                }

                else
                {
                    ModelState.AddModelError("Login", "Login informado não localizado!");
                    return View(model);
                }
            }

            return View(model);
        }




        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                /// cria um novo usuario com os dados vindos do formulario
                db.Usuario.Add(new Usuario
                {
                    Cpf = model.Cpf,
                    Nome = model.Nome,
                    Login = model.Login,
                    Email = model.Email,
                    Senha = model.Senha,
                    Cargo = model.Cargo
                });

                try
                {
                    db.SaveChanges();
                    var user = new LoginModel { Login = model.Login, Senha = model.Senha };
                    return Login(user, null);
                }
                catch(Exception Ex)
                {
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Usuario/Create
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Usuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPaswordModel model)
        {
            if (ModelState.IsValid)
            {

                Usuario usuario = db.Usuario.Where(p => p.Login.Equals(model.Login)).FirstOrDefault();

                if (usuario != null)
                {
                    if (Equals(model.Senha, model.ConfirmaSenha))
                    {
                        usuario.Senha = model.Senha;
                        
                        try
                        {
                            db.SaveChanges();
                            var user = new LoginModel { Login = model.Login, Senha = model.Senha };
                            return Login(user, null);
                        }
                        catch (Exception Ex)
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("confirmaSenha", "AS senhas informadas não são iguais!");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Não foi possível localizar o CPF informado");

                }


            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }







    }
}
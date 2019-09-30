using Monitoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                var Login = db.Usuario.Include("Cargo").Where(m => m.Login == model.Login).FirstOrDefault();

                if (Login != null)
                {
                    if (!Equals(Login.Status, 0))
                    {
                        if (Equals(Login.Senha, model.Senha))
                        {
                            FormsAuthentication.SetAuthCookie(Login.Login, false);
                            if (Url.IsLocalUrl(returnUrl)
                            && returnUrl.Length > 1
                            && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//")
                            && returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            Session["IdUsuario"] = Login.IdUsuario;
                            Session["Cpf"] = Login.Cpf;
                            Session["Login"] = Login.Login;
                            Session["Nome"] = Login.Nome;
                            Session["Genero"] = Login.Genero;
                            Session["Cargo"] = Login.Cargo.NomeCargo;
                            Session["Tema"] = Login.Tema;
                            Session["UrlIcone"] = Login.UrlIcone;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Senha", "Senha informada Inválida!");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Usuário sem autorização para usar o sistema!" +
                            "Contate o administrador.");
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
            ViewData["IdCargo"] = new SelectList(db.Cargos, "IdCargo", "NomeCargo");
            return View();
        }

        // POST: Account/Register
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["IdCargo"] = new SelectList(db.Cargos, "IdCargo", "NomeCargo");
                /// cria um novo usuario com os dados vindos do formulario
                /// 
                if (db.Usuario.FirstOrDefault(x => x.Cpf == model.Cpf) != null)
                {
                    ModelState.AddModelError("Cpf", "O CPF informado já está cadastrado no sistema!");
                    return View(model);
                }
                else if (db.Usuario.FirstOrDefault(x => x.Login == model.Login) != null)
                {
                    ModelState.AddModelError("Login", "O Login informado já está cadastrado no sistema!");
                    return View(model);
                }
                else if (db.Usuario.FirstOrDefault(x => x.Email == model.Email) != null)
                {
                    ModelState.AddModelError("Email", "O E-mail informado já está cadastrado no sistema!");
                    return View(model);
                }
                else
                {
                    db.Usuario.Add(new Usuario
                    {
                        Cpf = model.Cpf,
                        Nome = model.Nome,
                        Genero = model.Genero,
                        Login = model.Login,
                        Email = model.Email,
                        Senha = model.Senha,
                        IdCargo = model.IdCargo,
                        Tema = "default",
                        UrlIcone ="user.jpg",
                        Status = 1,
                        PrimeiroAcesso = 0

                    });
                    db.SaveChanges();
                    var user = new LoginModel { Login = model.Login, Senha = model.Senha };
                    return Login(user, null);
                }
            }
            ViewData["IdCargo"] = new SelectList(db.Cargos, "IdCargo", "NomeCargo");
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


        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangePasswordModel changePassword = new ChangePasswordModel
            {
                IdUsuario = id
            };
            return View(changePassword);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = db.Usuario.Find(model.IdUsuario);
                if (usuario != null)
                {
                    if (!Equals(usuario.Senha, model.NovaSenha))
                    {
                        usuario.Senha=model.NovaSenha;
                        db.SaveChanges();
                        TempData["message"] = "Senha alterada com sucesso!";
                        TempData["class"] = "success";

                        return RedirectToAction("Details", "Usuario",new { id = usuario.IdUsuario });
                    }
                    else
                    {
                        ModelState.AddModelError("NovaSenha", "A nova senha não pode ser igual à senha atual!");
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("NovaSenha", "Usuario não encontrado.");
                    
                }
               
            }
            TempData["message"] = "Não foi possível alterar a senha!";
            TempData["class"] = "danger";
            return View(model);
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
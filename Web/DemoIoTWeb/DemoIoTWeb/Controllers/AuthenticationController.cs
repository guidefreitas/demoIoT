using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DemoIoTWeb.ViewModel;
using DemoIoTWeb.Models;

namespace DemoIoTWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        DemoIoTContext db = new DemoIoTContext();

        public ActionResult Login()
        {
            VMLogin vm = new VMLogin();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(VMLogin vm)
        {
            if (ModelState.IsValid)
            {
                //Busca o usuário no banco com o mesmo email que foi informado na tela
                User usuarioDb = db.Users.Where(u => u.Email == vm.Email).FirstOrDefault();


                if (usuarioDb == null)
                {
                    ModelState.AddModelError("", "Não existe usuário com este email");
                    return View(vm);
                }

                //Compara a senha que foi informada na tela com a senha criptografada armazenada
                //no banco
                bool senhaConfere = Crypto.VerifyHashedPassword(usuarioDb.Password, vm.Password);

                if (!senhaConfere)
                {
                    ModelState.AddModelError("", "Senha incorreta");
                    return View(vm);
                }

                //Gera um token de autenticação único.
                //O método Guid.NewGuid() gera uma string aleatória que nunca se repete
                //Ex: f61dbbae2-2e29-4c6c-a445-aetdop12
                string authId = Guid.NewGuid().ToString();
                Session["AuthID"] = authId;

                //Cria um novo cookie com a identificacao AuthID
                var cookie = new HttpCookie("AuthID");

                //Programa o cookie para expirar após uma semana, assim o usuário não precisa ficar 
                //logando toda hora, mesmo se fechar o browser e voltar.
                cookie.Expires = DateTime.Now.AddDays(7);

                //Seta o valor do cookie com o token de identificação 
                cookie.Value = authId;

                //Faz o cookie ser enviado para o browser do usuário junto com a resposta da página
                Response.Cookies.Add(cookie);

                //Atualizar o usuário do banco com o token de autenticação
                usuarioDb.AuthId = authId;
                db.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            return View(vm);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies.AllKeys.Contains("AuthID"))
            {
                String authId = Request.Cookies["AuthID"].Value;
                User usuarioDb = db.Users.Where(c => c.AuthId == authId).FirstOrDefault();
                if (usuarioDb != null)
                {
                    usuarioDb.AuthId = "";
                    db.SaveChanges();
                }
                Request.Cookies.Remove("AuthID");

                if (Session["AuthID"] != null)
                {
                    Session.Remove("AuthID");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
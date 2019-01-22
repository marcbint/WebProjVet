using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados;
using WebProjVet.Models;
using WebProjVet.Util;

namespace WebProjVet.Controllers
{
    public class UsuarioController : Controller
    {
        //https://www.c-sharpcorner.com/article/asp-net-core-razor-pages-simple-login-using-entity-framework-database-first-app/
        //http://jasonwatmore.com/post/2018/06/26/aspnet-core-21-simple-api-for-authentication-registration-and-user-management
        //http://future-shock.net/blog/post/creating-a-simple-login-in-asp.net-core-2-using-authentication-and-authorization-not-identity
        //https://www.tutorialspoint.com/asp.net_core/asp.net_core_log_in_and_log_out.htm

        private readonly WebProjVetContext _context;

        public UsuarioController(WebProjVetContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public ActionResult Index()
        {

            var usuarios = _context.Usuarios
                .OrderBy(o => o.Nome)
                .ToList();

            return View(usuarios);
        }

        public IActionResult Login(Usuario usuario)
        {


            var usuarios = _context.Usuarios.Where(p => p.Login.Equals(usuario.Login)).FirstOrDefault();
            //Validar se instancia de usuário foi criada.
            var hashCode = usuarios.Salt;

            if (hashCode != null)
            {
                //Realiza do decript de acordo com a senha informada e codigo SALT do usuário localizado.
                var encodingPasswordString = Salt.EncodePassword(usuario.Senha, hashCode);


                //Validar o acesso
                if (usuarios.Senha == encodingPasswordString && usuarios.Login.ToUpper() == usuario.Login.ToUpper())
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            return View();
        }


        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = _context.Empresas
                .Where(e => e.Situacao == Situacao.ATIVO)
                .OrderBy(o => o.Nome)
                .ToList();

            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // TODO: Add insert logic here
                var keyNew = Salt.GeneratePassword(10);
                var password = Salt.EncodePassword(usuario.Senha, keyNew);

                usuario.Senha = password;
                usuario.Salt = keyNew;

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
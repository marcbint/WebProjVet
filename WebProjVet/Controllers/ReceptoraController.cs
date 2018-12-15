using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class ReceptoraController : Controller
    {

        private readonly WebProjVetContext _context;
        //private readonly IAnimalReceptoraRepository _receptoraRepository;
        private readonly IRepository<Receptora> _receptoraRepository;

        public ReceptoraController(WebProjVetContext context, IRepository<Receptora> receptoraRepository)
        {
            _context = context;
            _receptoraRepository = receptoraRepository;
        }

        public IActionResult Index()
        {
            var receptora = _receptoraRepository.All();
            if (receptora.Any())
                return View(receptora);

            return View(receptora);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Receptora animalReceptora)
        {
            _receptoraRepository.Save(animalReceptora);
            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {

            if (id > 0)
            {
                var animal = _receptoraRepository.GetById(id);
                
                return View(animal);
            }

            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Receptora receptora)
        {
            if (ModelState.IsValid)
            {
                _receptoraRepository.Update(receptora);
                return RedirectToAction("Index");
            }
            return View(receptora);
        }


        public IActionResult Details(int id)
        {
         
            if (id > 0)
            {
                var animal = _receptoraRepository.GetById(id);
                
                return View(animal);
            }

            return View();   
        }


        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    var receptora = _receptoraRepository.GetById(id);
                    return View(receptora);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Receptora receptora)
        {
            try
            {
                _receptoraRepository.Delete(receptora);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        public IActionResult CreateOrEdit(int id)
        {
            if (id > 0)
            {
                //var receptora = _receptoraRepository.ObterPorId(id);
                var receptora = _receptoraRepository.GetById(id);
                return View();
            }
            return View();

        }

        [HttpPost]
        public IActionResult CreateOrEdit(Receptora animalReceptora)
        {
            //_receptoraRepository.Salvar(animalReceptora);
            _receptoraRepository.Save(animalReceptora);
            return RedirectToAction("Index");
        }

    }
}
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
    public class AnimalReceptoraController : Controller
    {

        private readonly WebProjVetContext _context;
        //private readonly IAnimalReceptoraRepository _receptoraRepository;
        private readonly IRepository<AnimalReceptora> _receptoraRepository;

        public AnimalReceptoraController(WebProjVetContext context, IRepository<AnimalReceptora> receptoraRepository)
        {
            _context = context;
            _receptoraRepository = receptoraRepository;
        }

        public IActionResult Index()
        {
            var receptora = _receptoraRepository.All();
            if (receptora.Any())
                return View(receptora);

            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnimalReceptora animalReceptora)
        {
            _receptoraRepository.Save(animalReceptora);
            return RedirectToAction("Index");
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
        public IActionResult CreateOrEdit(AnimalReceptora animalReceptora)
        {
            //_receptoraRepository.Salvar(animalReceptora);
            _receptoraRepository.Save(animalReceptora);
            return RedirectToAction("Index");
        }

    }
}
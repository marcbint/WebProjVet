using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class AnimalGaranhaoController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalGaranhaoRepository _animalGaranhaoRepository;
        

        public AnimalGaranhaoController(IAnimalGaranhaoRepository animalGaranhaoRepository)
        {
            _animalGaranhaoRepository = animalGaranhaoRepository;

        }


        public IActionResult Index()
        {
           
            return View(_animalGaranhaoRepository.Listar());

        }


        public IActionResult CreateOrEdit(int id)
        {
            var viewModel = new AnimalGaranhao();

       
            if (id > 0)
            {
                var animal = _animalGaranhaoRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateOrEdit(AnimalGaranhao garanhao)
        {

            return View();
        }



        public IActionResult Create(int id)
        {
            var viewModel = new AnimalGaranhao();
            
            if (id > 0)
            {
                var animal = _animalGaranhaoRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                return View(viewModel);
            }

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnimalGaranhao garanhao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _animalGaranhaoRepository.Salvar(garanhao);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro:n{ex.Message}");
                }
            }
            else
            {
                //ModelState.ErrorCount();
            }
            //return View(animal);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {

            var viewModel = new AnimalGaranhao();
            
            
            if (id > 0)
            {
                var animal = _animalGaranhaoRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                return View(viewModel);
            }

            return View(viewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AnimalGaranhao garanhao)
        {
            if (ModelState.IsValid)
            {
                _animalGaranhaoRepository.Editar(garanhao);
                return RedirectToAction("Index");
            }
            return View(garanhao);
        }


        public IActionResult Details(int id)
        {
            var viewModel = new AnimalGaranhao();
            
            if (id > 0)
            {
                var animal = _animalGaranhaoRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                return View(viewModel);
            }

            return View(viewModel);



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
                    var garanhao = _animalGaranhaoRepository.ObterPorId(id);
                    return View(garanhao);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AnimalGaranhao garanhao)
        {
            try
            {
                _animalGaranhaoRepository.Remover(garanhao);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        //Especificado do verbo http que será utilizado
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //retorno de mensagem bem sucedida
                return Ok(_animalGaranhaoRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var animal = _animalGaranhaoRepository.ObterPorId(id);
                return Ok(animal);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]AnimalGaranhao garanhao)
        {
            try
            {
                _animalGaranhaoRepository.Salvar(garanhao);
                return Created("/api/AnimalGaranhao", garanhao);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
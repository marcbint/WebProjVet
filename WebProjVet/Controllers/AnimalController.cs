using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class AnimalController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalRepository _animalRepository;
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


        public IActionResult Index()
        {
            return View(_animalRepository.Listar());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _animalRepository.Salvar(animal);
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
            return View(animal);
        }


        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");

            }
            else
            {
                try
                {
                    var animal = _animalRepository.ObterPorId(id);
                    return View(animal);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: { ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.Editar(animal);
                return RedirectToAction("Index");
            }
            return View(animal);
        }


        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    var animal = _animalRepository.ObterPorId(id);
                    return View(animal);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
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
                    var animal = _animalRepository.ObterPorId(id);
                    return View(animal);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Animal animal)
        {
            try
            {
                _animalRepository.Remover(animal);
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
                return Ok(_animalRepository.Listar());
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
                var animal = _animalRepository.ObterPorId(id);
                return Ok(animal);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Animal animal)
        {
            try
            {
                _animalRepository.Salvar(animal);
                return Created("/api/Animal", animal);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
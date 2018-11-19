﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Servicos;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class AnimalController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalRepository _animalRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        private readonly ProprietarioViewModel _proprietariolViewModel;

        private readonly IRepository<Animal> _animalRepository2;
        private readonly IRepository<Proprietario> _proprietarioRepository2;

        public AnimalController(IAnimalRepository animalRepository, IProprietarioRepository proprietarioRepository)
        {
            _animalRepository = animalRepository;
            _proprietarioRepository = proprietarioRepository;
            
        }


        public IActionResult Index()
        {
            /*var animais = _animalRepository.Listar();
            if (animais.Any())
            {
                var viewsModels = animais.Select(p => new AnimalViewModel { Id = p.Id, Nome = p.Nome });
                return View(viewsModels);
            }

            return View();*/
            return View(_animalRepository.Listar());

        }


        public IActionResult CreateOrEdit(int id)
        {
            var viewModel = new Animal();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new ProprietarioViewModel { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
                return View(viewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateOrEdit(Animal animal)
        {
           
            return View();
        }



        public IActionResult Create(int id)
        {
            var viewModel = new Animal();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new ProprietarioViewModel { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
                return View(viewModel);
            }

            return View(viewModel);
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
            //return View(animal);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {

            var viewModel = new Animal();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new ProprietarioViewModel { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
                return View(viewModel);
            }

            return View(viewModel);


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
            var viewModel = new Animal();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new ProprietarioViewModel { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
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
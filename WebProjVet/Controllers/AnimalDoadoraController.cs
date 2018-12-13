using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Servicos;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.Controllers
{
    public class AnimalDoadoraController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalDoadoraRepository _animalRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        //private readonly ProprietarioViewModel _proprietariolViewModel;
        private readonly Proprietario _proprietariolViewModel;
        private readonly WebProjVetContext _context;

        //private readonly IRepository<AnimalDoadora> _animalRepository2;
        //private readonly IRepository<Proprietario> _proprietarioRepository2;

        public AnimalDoadoraController(IAnimalDoadoraRepository animalRepository, IProprietarioRepository proprietarioRepository, WebProjVetContext webProjVetContext)
        {
            _animalRepository = animalRepository;
            _proprietarioRepository = proprietarioRepository;
            _context = webProjVetContext;
            
        }


        public IActionResult Index()
        {
            //O include retorna as informações das entidades necessárias.
            var doadoras = _context.Doadoras.Include(p => p.Proprietario).ToList();

            /*var animais = _animalRepository.Listar();
            if (animais.Any())
            {
                var viewsModels = animais.Select(p => new AnimalViewModel { Id = p.Id, Nome = p.Nome });
                return View(viewsModels);
            }

            return View();*/
            //return View(_animalRepository.Listar());
            return View(doadoras);

        }


        public IActionResult CreateOrEdit(int id)
        {
            ViewBag.Proprietarios = _context.Proprietarios.ToList();
            var doadoras = _context.Doadoras.First(p => p.Id == id);

            /*
            var viewModel = new AnimalDoadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                //viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
                return View(viewModel);
            }


            return View(viewModel);
            */
            return View(doadoras);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(AnimalDoadora animal)
        {
            /* Forma alternativa para localizar o proprietário e adicionar à entidade Propriedade
            var proprietario = _webProjVetContext.Proprietarios.First(c => c.Id == animal.ProprietarioId);
            animal.Proprietario = proprietario;
            */

            if(animal.Id == 0)
                _context.Doadoras.Add(animal);
            else
            {
                var doadoraSalvo = _context.Doadoras.First(p => p.Id == animal.Id);
                doadoraSalvo.Id = animal.Id;
                doadoraSalvo.Nome = animal.Nome;
                doadoraSalvo.Abqm = animal.Abqm;
                doadoraSalvo.ProprietarioId = animal.ProprietarioId;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }



        public IActionResult Create(int id)
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();

            var viewModel = new AnimalDoadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnimalDoadora animal)
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

            var viewModel = new AnimalDoadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                //viewModel.AnimalTipo = animal.AnimalTipo;
                viewModel.ProprietarioId = animal.ProprietarioId;
                return View(viewModel);
            }

            return View(viewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AnimalDoadora animal)
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
            var viewModel = new AnimalDoadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            if (proprietarios.Any())
                viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                //viewModel.AnimalTipo = animal.AnimalTipo;
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
                    //var animal = _animalRepository.ObterPorId(id);
                    var animal = _context.Doadoras.First(p => p.Id == id);
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
        public async Task<IActionResult> Delete(AnimalDoadora animal)
        {
            try
            {
                //_animalRepository.Remover(animal);
                _context.Remove(animal);
                await _context.SaveChangesAsync();
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
        public IActionResult Post([FromBody]AnimalDoadora animal)
        {
            try
            {
                _animalRepository.Salvar(animal);
                return Created("/api/AnimalDoadora", animal);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Entidades;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Servicos;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.Controllers
{
    public class DoadoraController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalDoadoraRepository _animalRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        //private readonly ProprietarioViewModel _proprietariolViewModel;
        //private readonly Proprietario _proprietariolViewModel;

        private readonly WebProjVetContext _context;
        



        //private readonly IRepository<Doadora> _animalRepository2;
        //private readonly IRepository<Proprietario> _proprietarioRepository2;

        public DoadoraController(IAnimalDoadoraRepository animalRepository, IProprietarioRepository proprietarioRepository, WebProjVetContext webProjVetContext)
        {
            _animalRepository = animalRepository;
            _proprietarioRepository = proprietarioRepository;
            _context = webProjVetContext;
            
        }


        public IActionResult Index()
        {
            //O include retorna as informações das entidades necessárias.
            //var doadoras = _context.Doadoras.Include(p => p.Proprietario).ToList();
            var doadoras = _context.Doadoras.ToList();

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
        public async Task<IActionResult> CreateOrEdit(Doadora animal)
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
                //doadoraSalvo.ProprietarioId = animal.ProprietarioId;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
        }



        public IActionResult Create(int id)
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();

            var viewModel = new Doadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            //if (proprietarios.Any())
                //viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            

            return View();
        }


        [HttpPost]
        public IActionResult Create(Doadora doadora)
        {
            _animalRepository.Salvar(doadora);
            var doadoraId = doadora.Id;

            //Realiza a inclusão se existirem itens
            if (doadora.DoadoraProprietariosJson != null)
            {
                //Processo de inclusão de itens
                List<DoadoraProprietario> lista = JsonConvert.DeserializeObject<List<DoadoraProprietario>>(doadora.DoadoraProprietariosJson);

                if (lista.Count > 0)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista[i].Id == 0)
                        {
                            DoadoraProprietario objLista = new DoadoraProprietario();
                            objLista.DoadoraId = doadoraId;
                            objLista.ProprietarioId = lista[i].ProprietarioId;                      
                            objLista.Data = DateTime.Now;

                            _context.DoadoraProprietarios.Add(objLista);
                            _context.SaveChanges();
                        }

                    }
                }
            }
            
            
            return RedirectToAction("Index");    
            
        }


        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                var doadoras = _context.Doadoras
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.DoadoraProprietarios)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);
                

                return View(doadoras);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doadora doadora)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.Editar(doadora);
                var doadoraId = doadora.Id;

                //Realiza a inclusão se existirem itens
                if (doadora.DoadoraProprietariosJson != null)
                {
                    //Processo de inclusão de itens
                    List<DoadoraProprietario> lista = JsonConvert.DeserializeObject<List<DoadoraProprietario>>(doadora.DoadoraProprietariosJson);

                    if (lista.Count > 0)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].Id == 0)
                            {
                                DoadoraProprietario objLista = new DoadoraProprietario();
                                objLista.DoadoraId = doadoraId;
                                objLista.ProprietarioId = lista[i].ProprietarioId;
                                objLista.Data = DateTime.Now;

                                _context.DoadoraProprietarios.Add(objLista);
                                
                            }

                        }
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doadora);
        }


        public IActionResult Details(int id)
        {
            var viewModel = new Doadora();
            var proprietarios = _proprietarioRepository.ListarProprietarios();

            //if (proprietarios.Any())
                //viewModel.Proprietarios = proprietarios.Select(c => new Proprietario { Id = c.Id, Nome = c.Nome });

            if (id > 0)
            {
                var animal = _animalRepository.GetById(id);
                viewModel.Id = animal.Id;
                viewModel.Nome = animal.Nome;
                viewModel.Abqm = animal.Abqm;
                //viewModel.AnimalTipo = animal.AnimalTipo;
                //viewModel.ProprietarioId = animal.ProprietarioId;
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
        public async Task<IActionResult> Delete(Doadora animal)
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
        public IActionResult Post([FromBody]Doadora animal)
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
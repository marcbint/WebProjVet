using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class GaranhaoController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalGaranhaoRepository _animalGaranhaoRepository;
        private readonly WebProjVetContext _context;
        

        public GaranhaoController(IAnimalGaranhaoRepository animalGaranhaoRepository, WebProjVetContext context)
        {
            _animalGaranhaoRepository = animalGaranhaoRepository;
            _context = context;

        }


        public IActionResult Index()
        {
           
            return View(_animalGaranhaoRepository.Listar());

        }


        public IActionResult CreateOrEdit(int id)
        {
            var viewModel = new Garanhao();

       
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
        public IActionResult CreateOrEdit(Garanhao garanhao)
        {

            return View();
        }



        public IActionResult Create()
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();

            return View();
        }

        


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Garanhao garanhao)
        {
            try
            {
                _animalGaranhaoRepository.Salvar(garanhao);
                var idTemp = garanhao.Id;

                //Realiza a inclusão se existirem itens
                if (garanhao.TabelaItensJson != null)
                {
                    //Processo de inclusão de itens
                    List<GaranhaoProprietario> lista = JsonConvert.DeserializeObject<List<GaranhaoProprietario>>(garanhao.TabelaItensJson);

                    if (lista.Count > 0)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].Id == 0)
                            {
                                GaranhaoProprietario objLista = new GaranhaoProprietario();
                                objLista.GaranhaoId = idTemp;
                                objLista.ProprietarioId = lista[i].ProprietarioId;
                                objLista.Data = DateTime.Now;

                                _context.GaranhaoProprietarios.Add(objLista);
                                _context.SaveChanges();
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro:n{ex.Message}");
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                var garanhao = _context.Garanhoes
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.GaranhaoProprietarios)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);

                //Cria a session utilizada para realizar a consulta do datatable
                //https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/                

                //Seta a session
                HttpContext.Session.SetString("sessionId", id.ToString());

                return View(garanhao);
            }

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(Garanhao garanhao)
        {
            if (ModelState.IsValid)
            {               
                _animalGaranhaoRepository.Editar(garanhao);
                var idTemp = garanhao.Id;

                /*
                //Realiza a inclusão se existirem itens
                if (garanhao.TabelaItensJson != null)
                {
                    //Processo de inclusão de itens
                    List<GaranhaoProprietario> lista = JsonConvert.DeserializeObject<List<GaranhaoProprietario>>(garanhao.TabelaItensJson);

                    if (lista.Count > 0)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].Id == 0)
                            {
                                GaranhaoProprietario objLista = new GaranhaoProprietario();
                                objLista.GaranhaoId = idTemp;
                                objLista.ProprietarioId = lista[i].ProprietarioId;
                                objLista.Data = DateTime.Now;

                                _context.GaranhaoProprietarios.Add(objLista);

                            }

                        }
                    }
                }

                _context.SaveChanges();
                */
                return RedirectToAction("Index");
            }
            return View(garanhao);
        }

        [Route("api/[controller]/AddProprietarioTable/{id}")]
        public IActionResult PostAddProprietario(int id)
        {
            //https://www.talkingdotnet.com/handle-ajax-requests-in-asp-net-core-razor-pages/
            //http://binaryintellect.net/articles/16ecfe49-98df-4305-b53f-2438d836f0d0.aspx

            string retorno = null;

            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<GaranhaoProprietario>(requestBody);
                        GaranhaoProprietario ObjTabela = JsonConvert.DeserializeObject<GaranhaoProprietario>(requestBody);
                        ObjTabela.Data = DateTime.Now;

                        //Verifica se o Proprietário já esta adicionado à Doadora
                        int count = _context.GaranhaoProprietarios
                            .Where(a => a.GaranhaoId == ObjTabela.GaranhaoId
                       && a.ProprietarioId == ObjTabela.ProprietarioId).Count();

                        if (count == 0)
                        {
                            _context.GaranhaoProprietarios.Add(ObjTabela);
                            _context.SaveChanges();
                            retorno = "NOVO";
                            return new JsonResult(retorno);
                        }
                        else
                        {
                            retorno = "EXISTE";
                            return new JsonResult(retorno);
                        }

                    }
                }

            }

            return new JsonResult(retorno);
        }





        public IActionResult Details(int id)
        {
            var viewModel = new Garanhao();
            
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
        public IActionResult Delete(Garanhao garanhao)
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
        public IActionResult Post([FromBody]Garanhao garanhao)
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



        /*
        public IActionResult Create(int id)
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();

            var viewModel = new Garanhao();
            
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

        public IActionResult Edit(int id)
        {

            var viewModel = new Garanhao();
            
            
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
        public IActionResult Edit(Garanhao garanhao)
        {
            if (ModelState.IsValid)
            {
                _animalGaranhaoRepository.Editar(garanhao);
                return RedirectToAction("Index");
            }
            return View(garanhao);
        }




        */








    }
}
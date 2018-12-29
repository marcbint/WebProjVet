using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{

    public class AnimalController : Controller
    {
        //Injeção de dependência
        private readonly IAnimalRepository _animalRepository;
        private readonly IProprietarioRepository _proprietarioRepository;
        //private readonly ProprietarioViewModel _proprietariolViewModel;
        //private readonly Proprietario _proprietariolViewModel;

        private readonly WebProjVetContext _context;
        



        //private readonly IRepository<Doadora> _animalRepository2;
        //private readonly IRepository<Proprietario> _proprietarioRepository2;

        public AnimalController(IAnimalRepository animalRepository, IProprietarioRepository proprietarioRepository, WebProjVetContext webProjVetContext)
        {
            _animalRepository = animalRepository;
            _proprietarioRepository = proprietarioRepository;
            _context = webProjVetContext;
            
        }


        public IActionResult Index()
        {
            var animais = _context.Animais.ToList();

            /*var animais = _animalRepository.Listar();
            if (animais.Any())
            {
                var viewsModels = animais.Select(p => new AnimalViewModel { Id = p.Id, Nome = p.Nome });
                return View(viewsModels);
            }

            return View();*/
            //return View(_animalRepository.Listar());
            return View(animais);

        }


        
        public IActionResult Create()
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();
            ViewBag.ServicoId = _context.Servicos.Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Create(Animais animais)
        {
            _animalRepository.Salvar(animais);
            var animaisId = animais.Id;

            //Realiza a inclusão se existirem itens
            if (animais.AnimaisProprietariosJson != null)
            {
                //Processo de inclusão de itens
                List<DoadoraProprietario> lista = JsonConvert.DeserializeObject<List<DoadoraProprietario>>(animais.AnimaisProprietariosJson);

                if (lista.Count > 0)
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (lista[i].Id == 0)
                        {
                            AnimaisProprietario objLista = new AnimaisProprietario();
                            objLista.AnimaisId = animaisId;
                            objLista.ProprietarioId = lista[i].ProprietarioId;                      
                            objLista.Data = DateTime.Now;

                            _context.AnimaisProprietarios.Add(objLista);
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
                ViewBag.ServicoId = _context.Servicos.Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

                var animais = _context.Animais
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.AnimaisProprietarios)
                    .Include(s => s.AnimaisServicos)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);                

                return View(animais);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animais animais)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.Editar(animais);
                var animaisId = animais.Id;

                //Realiza a inclusão se existirem itens
                if (animais.AnimaisProprietariosJson != null)
                {
                    //Processo de inclusão de itens
                    List<AnimaisProprietario> lista = JsonConvert.DeserializeObject<List<AnimaisProprietario>>(animais.AnimaisProprietariosJson);

                    if (lista.Count > 0)
                    {
                        for (int i = 0; i < lista.Count; i++)
                        {
                            if (lista[i].Id == 0)
                            {
                                AnimaisProprietario objLista = new AnimaisProprietario();
                                objLista.AnimaisId = animaisId;
                                objLista.ProprietarioId = lista[i].ProprietarioId;
                                objLista.Data = DateTime.Now;

                                _context.AnimaisProprietarios.Add(objLista);
                                
                            }

                        }
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animais);
        }



        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                ViewBag.ProprietarioId = _context.Proprietarios.ToList();
                ViewBag.ServicoId = _context.Servicos.Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

                var animais = _context.Animais
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.AnimaisProprietarios)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);
              
                return View(animais);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Animais animais)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.Remover(animais);
                var doadoraId = animais.Id;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animais);
        }


        public IActionResult DeleteProprietario(int id)
        {
            if (id > 0)
            {
                //ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                
                var animaisProprietario = _context.AnimaisProprietarios
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.Proprietario)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);
                    
              

                return View(animaisProprietario);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProprietario(AnimaisProprietario animaisProprietario)
        {
            AnimaisProprietario registro = _context.AnimaisProprietarios
                .Where(p => p.Id.Equals(animaisProprietario.Id))
                .FirstOrDefault(p => p.Id == animaisProprietario.Id);

            _context.AnimaisProprietarios.Remove(registro);
            _context.SaveChanges();            
            
            return RedirectToRoute(new { Controller = "Animal", Action = "Edit", id = animaisProprietario.AnimaisId });

        }
    

        [Route("api/[controller]/AddProprietario/{idAnimais}")]
        public IActionResult AddProprietario(int idAnimais)
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
                        var obj = JsonConvert.DeserializeObject<AnimaisProprietario>(requestBody);
                        AnimaisProprietario ObjAnimaisProprietarios = JsonConvert.DeserializeObject<AnimaisProprietario>(requestBody);
                        ObjAnimaisProprietarios.Data = DateTime.Now;

                        //Verifica se o Proprietário já esta adicionado à Doadora
                        int count = _context.DoadoraProprietarios
                            .Where(a => a.DoadoraId == ObjAnimaisProprietarios.AnimaisId
                       && a.ProprietarioId == ObjAnimaisProprietarios.ProprietarioId).Count();

                        if (count == 0)
                        {
                            _context.AnimaisProprietarios.Add(ObjAnimaisProprietarios);
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


        [Route("api/[controller]/AddServico/{idAnimais}")]
        public IActionResult AddServico(int idAnimais)
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
                        var obj = JsonConvert.DeserializeObject<AnimaisServicos>(requestBody);

                        AnimaisServicos ObjAnimaisServicos = JsonConvert.DeserializeObject<AnimaisServicos>(requestBody);                      

                        _context.AnimaisServicos.Add(ObjAnimaisServicos);
                        _context.SaveChanges();
                        retorno = "NOVO";
                        return new JsonResult(retorno);

                    }
                }

            }

            return new JsonResult(retorno);
        }









    }
}
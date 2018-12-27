using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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


        
        public IActionResult Create()
        {
            ViewBag.ProprietarioId = _context.Proprietarios.ToList();
            
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

                //Cria a session utilizada para realizar a consulta do datatable
                //https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/
                var sessionDoadora = id.ToString();
                //Seta a session
                HttpContext.Session.SetString("sessionDoadora", id.ToString());

                


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
        public IActionResult Delete(Doadora doadora)
        {
            if (ModelState.IsValid)
            {
                _animalRepository.Remover(doadora);
                var doadoraId = doadora.Id;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doadora);
        }


        public IActionResult DeleteProprietario(int id)
        {
            if (id > 0)
            {
                //ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                
                var doadoraProprietario = _context.DoadoraProprietarios
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.Proprietario)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);
                    
              

                return View(doadoraProprietario);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProprietario(DoadoraProprietario doadoraProprietario)
        {
            DoadoraProprietario registro = _context.DoadoraProprietarios
                .Where(p => p.Id.Equals(doadoraProprietario.Id))
                .FirstOrDefault(p => p.Id == doadoraProprietario.Id);

            _context.DoadoraProprietarios.Remove(registro);
            _context.SaveChanges();            
            
            return RedirectToRoute(new { Controller = "Doadora", Action = "Edit", id = doadoraProprietario.DoadoraId });

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

        [Route("api/[controller]/lista")]
        //[Route("lista")]
        public IActionResult Post()
        {
            //Get form data from client side
            var requestFormData = Request.Form;

            //Pega a session
            var value = HttpContext.Session.GetString("sessionDoadora");

            List<Proprietario> lstItems = GetData(Convert.ToInt32(value));

            //Destruir a session
            HttpContext.Session.Remove("sessionDoadora");
            HttpContext.Session.Clear();

            var listItems = ProcessCollection(lstItems, requestFormData);

            // Custom response to bind information in client side
            dynamic response = new
            {
                Data = listItems,
                Draw = requestFormData["draw"],
                RecordsFiltered = lstItems.Count,
                RecordsTotal = lstItems.Count
            };
            return Ok(response);
        }

        /// <summary>
        /// Get a list of Items
        /// </summary>
        /// <returns>list of items</returns>
        private List<Proprietario> GetData(int id)
        {


            var query = (from propi in _context.Proprietarios
                         join doaprop in _context.DoadoraProprietarios on propi.Id equals doaprop.ProprietarioId
                         where doaprop.DoadoraId == id
                         select new { Proprietario = propi }
                         ).ToList();

            var lstItems = new List<Proprietario>();
            foreach (var h in query)
            {
                var model = new Proprietario();
                model.Id = h.Proprietario.Id;
                model.Nome = h.Proprietario.Nome;
                lstItems.Add(model);
            }

            //List<Proprietario> lstItems = _context.Doadoras.ToList();          
            //List<Proprietario> lstItems = _context.Proprietarios.ToList();            
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();
            


            return lstItems;
        }

        /// <summary>
        /// Get a property info object from Item class filtering by property name.
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <returns>property info object</returns>
        private PropertyInfo getProperty(string name)
        {
            var properties = typeof(Proprietario).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            return prop;
        }

        /// <summary>
        /// Process a list of items according to Form data parameters
        /// </summary>
        /// <param name="lstElements">list of elements</param>
        /// <param name="requestFormData">collection of form data sent from client side</param>
        /// <returns>list of items processed</returns>
        private List<Proprietario> ProcessCollection(List<Proprietario> lstElements, IFormCollection requestFormData)
        {
            var skip = Convert.ToInt32(requestFormData["start"].ToString());
            var pageSize = Convert.ToInt32(requestFormData["length"].ToString());
            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] { "" };


            if (requestFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var columnIndex = requestFormData["order[0][column]"].ToString();
                var sortDirection = requestFormData["order[0][dir]"].ToString();
                tempOrder = new[] { "" };
                if (requestFormData.TryGetValue($"columns[{columnIndex}][data]", out tempOrder))
                {
                    var columName = requestFormData[$"columns[{columnIndex}][data]"].ToString();

                    if (pageSize > 0)
                    {
                        var prop = getProperty(columName);
                        if (sortDirection == "asc")
                        {
                            return lstElements.OrderBy(prop.GetValue).Skip(skip).Take(pageSize).ToList();
                        }
                        else
                            return lstElements.OrderByDescending(prop.GetValue).Skip(skip).Take(pageSize).ToList();
                    }
                    else
                        return lstElements;
                }
            }

            return null;
        }



        [Route("api/[controller]/AddProprietario/{idDoadora}")]
        public IActionResult PostAddProprietario(int idDoadora)
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
                        var obj = JsonConvert.DeserializeObject<DoadoraProprietario>(requestBody);
                        DoadoraProprietario ObjDoadoraProprietarios = JsonConvert.DeserializeObject<DoadoraProprietario>(requestBody);
                        ObjDoadoraProprietarios.Data = DateTime.Now;

                        //Verifica se o Proprietário já esta adicionado à Doadora
                        int count = _context.DoadoraProprietarios
                            .Where(a => a.DoadoraId == ObjDoadoraProprietarios.DoadoraId
                       && a.ProprietarioId == ObjDoadoraProprietarios.ProprietarioId).Count();

                        if (count == 0)
                        {
                            _context.DoadoraProprietarios.Add(ObjDoadoraProprietarios);
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









    }
}
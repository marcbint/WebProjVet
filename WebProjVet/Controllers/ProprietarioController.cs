using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    //[Route("api/[Controller]")]
    public class ProprietarioController : Controller
    {
        //Injeção de dependência
        private readonly IProprietarioRepository _proprietarioRepository;
        private readonly IProprietarioEnderecoRepository _proprietarioEnderecoRepository;
        private readonly WebProjVetContext _context;
        private ProprietarioEndereco objProprietarioEndereco { get; set; }

        public ProprietarioController(IProprietarioRepository proprietarioRepository, WebProjVetContext context, IProprietarioEnderecoRepository proprietarioEnderecoRepository)
        {
            _proprietarioRepository = proprietarioRepository;
            _proprietarioEnderecoRepository = proprietarioEnderecoRepository;
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_proprietarioRepository.ListarProprietarios());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proprietario proprietario)
        {
            try
            {
                _proprietarioRepository.Salvar(proprietario);
                var idTemp = proprietario.Id;

                //Realiza a inclusão se existirem itens
                if (proprietario.TabelaItensEnderecoJson != null)
                {
                    //Processo de inclusão de itens
                    List<ProprietarioEndereco> listaEndereco = JsonConvert.DeserializeObject<List<ProprietarioEndereco>>(proprietario.TabelaItensEnderecoJson);

                    if (listaEndereco.Count > 0)
                    {
                        for (int i = 0; i < listaEndereco.Count; i++)
                        {
                            if (listaEndereco[i].Id == 0)
                            {
                                ProprietarioEndereco objLista = new ProprietarioEndereco();
                                objLista.ProprietarioId = idTemp;
                                objLista.EnderecoTipo = listaEndereco[i].EnderecoTipo;
                                objLista.Endereco = listaEndereco[i].Endereco;
                                objLista.Complemento = listaEndereco[i].Complemento;
                                objLista.Cidade = listaEndereco[i].Cidade;
                                objLista.Uf = listaEndereco[i].Uf;                                

                                _context.ProprietarioEnderecos.Add(objLista);
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
                //ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                var proprietario = _context.Proprietarios
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.ProprietarioEnderecos)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);


                //Cria a session utilizada para realizar a consulta do datatable
                //https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/                

                //Seta a session
                HttpContext.Session.SetString("sessionId", id.ToString());

                return View(proprietario);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Proprietario proprietario)
        {
            if(proprietario.PessoaTipo.ToString() == "FÍSICA")
            {
                proprietario.InscricaoEstadual = null;
                proprietario.RazaoSocial = null;

            }

                _proprietarioRepository.Editar(proprietario);
                return RedirectToAction("Index");
            
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                //ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                var proprietario = _context.Proprietarios
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.ProprietarioEnderecos)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);


                //Cria a session utilizada para realizar a consulta do datatable
                //https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/                

                //Seta a session
                //HttpContext.Session.SetString("sessionId", id.ToString());

                return View(proprietario);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Proprietario proprietario)
        {
            if (proprietario.PessoaTipo.ToString() == "FÍSICA")
            {
                proprietario.InscricaoEstadual = null;
                proprietario.RazaoSocial = null;

            }

            _proprietarioRepository.Remover(proprietario);
            return RedirectToAction("Index");

        }


        [Route("api/[controller]/AddEnderecoTable/{id}")]
        public IActionResult AddEndereco(int id)
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
                        var obj = JsonConvert.DeserializeObject<ProprietarioEndereco>(requestBody);
                        ProprietarioEndereco ObjTabela = JsonConvert.DeserializeObject<ProprietarioEndereco>(requestBody);
                        

                        //Verifica se o Proprietário já esta adicionado à Doadora
                        int count = _context.ProprietarioEnderecos
                            .Where(a => a.EnderecoTipo == ObjTabela.EnderecoTipo
                       && a.ProprietarioId == ObjTabela.ProprietarioId).Count();

                        if (count == 0)
                        {
                            _context.ProprietarioEnderecos.Add(ObjTabela);
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

        public IActionResult EditEndereco(int id)
        {
            if (id > 0)
            {
                ViewBag.ProprietarioId = _context.Proprietarios.ToList();

                ProprietarioEndereco proprietarioEndereco = _context.ProprietarioEnderecos
                    .Where(p => p.Id.Equals(id))                    
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);
               
                return View(proprietarioEndereco);
            }

            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult EditEndereco(ProprietarioEndereco proprietarioEndereco)
        {
           
            _proprietarioEnderecoRepository.Editar(proprietarioEndereco);
           
            return RedirectToRoute(new {Controller="Proprietario", Action="Edit", id = proprietarioEndereco.ProprietarioId });            
        }


        public IActionResult DeleteEndereco(int id)
        {
            if (id > 0)
            {
              
                ProprietarioEndereco proprietarioEndereco = _context.ProprietarioEnderecos
                    .Where(p => p.Id.Equals(id))
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);

                return View(proprietarioEndereco);
            }

            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteEndereco(ProprietarioEndereco proprietarioEndereco)
        {

            _proprietarioEnderecoRepository.Remover(proprietarioEndereco);

            return RedirectToRoute(new { Controller = "Proprietario", Action = "Edit", id = proprietarioEndereco.ProprietarioId });
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
                    var propriedade = _proprietarioRepository.ObterProprietarioPorId(id);
                    return View(propriedade);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        /*
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
                    var propriedade = _proprietarioRepository.ObterProprietarioPorId(id);
                    return View(propriedade);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Proprietario proprietario)
        {
            try
            {
                _proprietarioRepository.Remover(proprietario);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        */

        //Especificado do verbo http que será utilizado
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //retorno de mensagem bem sucedida
                return Ok(_proprietarioRepository.ListarProprietarios());
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
                var proprietario = _proprietarioRepository.ObterProprietarioPorId(id);
                return Ok(proprietario);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Proprietario proprietario)
        {
            try
            {
                _proprietarioRepository.Salvar(proprietario);
                return Created("/api/proprietario", proprietario);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }



    }
}
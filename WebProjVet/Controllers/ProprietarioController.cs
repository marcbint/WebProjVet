using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    [Route("api/[Controller]")]
    public class ProprietarioController : Controller
    {
        //Injeção de dependência
        private readonly IProprietarioRepository _proprietarioRepository;
        public ProprietarioController(IProprietarioRepository proprietarioRepository)
        {
            _proprietarioRepository = proprietarioRepository;
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
            if (ModelState.IsValid)
            {
                try
                {
                    _proprietarioRepository.Salvar(proprietario);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Erro:n{ex.Message}");
                }
            }
            return View(proprietario);
        }


        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");

            }
            else
            {
                try
                {
                    var proprietario = _proprietarioRepository.ObterProprietarioPorId(id);
                    return View(proprietario);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Erro: { ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Proprietario proprietario)
        {
            if (ModelState.IsValid)
            {
                _proprietarioRepository.Editar(proprietario);
                return RedirectToAction("Index");
            }
            return View(proprietario);
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
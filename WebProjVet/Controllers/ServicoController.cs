﻿using Microsoft.AspNetCore.Mvc;
using System;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{

    //[Route("api/[Controller]")]
    public class ServicoController : Controller
    {
        private readonly IServicoRepository _servicoRepository;

        //Injeção de Dependência
        public ServicoController(IServicoRepository servicoRepository)
        {
            //recebe a instância do serviço repository
            _servicoRepository = servicoRepository;
        }


        public IActionResult Index()
        {
            //Envia a listagem de serviços para view
            return View(_servicoRepository.ListarServicos());    
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Servico servico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicoRepository.Salvar(servico);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }

            return View(servico);
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
                    var servico = _servicoRepository.ObterServicoPorId(id);

                    return View(servico);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Servico servico)
        {
            if (ModelState.IsValid)
            {
                _servicoRepository.Editar(servico);
                
                return RedirectToAction("Index");
            }
            return View(servico);
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
                    var servico = _servicoRepository.ObterServicoPorId(id);

                    return View(servico);
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
                    var servico = _servicoRepository.ObterServicoPorId(id);

                    return View(servico);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message}");
                }
            }
        }


        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Servico servico)
        {
            try
            {
                _servicoRepository.Remover(servico);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //retorno de mensagem bem sucedida
                return Ok(_servicoRepository.ListarServicos());
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
                var servico = _servicoRepository.ObterServicoPorId(id);
                return Ok(servico);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Servico servico)
        {
            try
            {
                _servicoRepository.Salvar(servico);
                return Created("/api/servico", servico);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
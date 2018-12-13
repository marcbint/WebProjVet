using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.AcessoDados.Interfaces;

namespace WebProjVet.Controllers
{
    [Route("api/[controller]")]
    public class ServicoAPIController : ControllerBase
    {
        private IServicoRepository _servicoRepository;

        /* Injeção de uma instancia de IServicoRepository ao criar
        uma instancia de BookController */
        public ServicoAPIController(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        //[HttpGet]
        public  IActionResult Get2()
        {
            var result = _servicoRepository.ListarServicos();
            return Ok(result);
        }

        
        [Route("demo5")]
        public IActionResult Get()
        {
            return new JsonResult(_servicoRepository.ListarServicos());
        }

        [Route("getvalor/{id}")]
        public IActionResult GetValor(int id)
        {
            return new JsonResult(_servicoRepository.ObterServicoPorId(id));
        }

    }
}
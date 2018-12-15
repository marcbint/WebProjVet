using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Entidades;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    [Route("api/[controller]")]
    public class DoadoraApiController : Controller
    {
        private readonly WebProjVetContext _context;
        private readonly IAnimalDoadoraRepository _doadoraRepository;


        /* Injeção de uma instancia de IServicoRepository ao criar
        uma instancia de BookController */
        public DoadoraApiController(WebProjVetContext contex, IAnimalDoadoraRepository doadoraRepository)
        {
            _context = contex;
            _doadoraRepository = doadoraRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("getTeste/{fullName}")]
        public IActionResult GetValor(string fullName)
        {
            //https://www.talkingdotnet.com/handle-ajax-requests-in-asp-net-core-razor-pages/
            string sPostValue1 = "";
            string sPostValue2 = "";
            string sPostValue3 = "";
            {
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var obj = JsonConvert.DeserializeObject<PostData>(requestBody);
                        if (obj != null)
                        {
                            sPostValue1 = obj.Item1;
                            sPostValue2 = obj.Item2;
                            sPostValue3 = obj.Item3;
                        }
                    }
                }
            }
            List<string> lstString = new List<string>()
            {
                sPostValue1,
                sPostValue2,
                sPostValue3
            };
            return new JsonResult(lstString);
        }


            //_context.DoadoraProprietarios.Add();
            return new JsonResult("funcionou" + fullName);
        }
    }
}
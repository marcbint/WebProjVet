using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProjVet.Models;
using System.IO;
using Newtonsoft.Json;
using WebProjVet.AcessoDados.Entidades;

namespace WebProjVet.Controllers
{
    /// <summary>
    /// http://learningprogramming.net/net/asp-net-core-mvc/ajax-in-asp-net-core-mvc/
    /// </summary>
    [Route("demo")]
    //[Route("api/[controller]")]
    public class DemoController : Controller
    {
        //[Route("")]
        [Route("index")]
        //[Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("demo1")]
        public IActionResult Demo1()
        {
            return new JsonResult("Hello");
        }

        [Route("demo2/{fullName}")]
        public IActionResult Demo2(string fullName)
        {
            return new JsonResult("Hello " + fullName);
        }

        [Route("demo3")]
        public IActionResult Demo3()
        {
            var product = new Product()
            {
                Id = "p01",
                Name = "name 1",
                Price = "123"
            };
            return new JsonResult(product);
        }

        [Route("demo4")]
        public IActionResult Demo4()
        {
            var products = new List<Product>()
            {
                new Product() {
                    Id = "p01",
                    Name = "name 1",
                    Price = "123"
                },
                new Product() {
                    Id = "p02",
                    Name = "name 2",
                    Price = "456"
                },
                new Product() {
                    Id = "p03",
                    Name = "name 3",
                    Price = "789"
                }
            };
            return new JsonResult(products);
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

    }
}
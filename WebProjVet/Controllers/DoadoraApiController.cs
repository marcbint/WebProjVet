using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Entidades;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace WebProjVet.Controllers
{
    [Route("api/[controller]")]
    public class DoadoraAPIController : Controller
    {
        private readonly WebProjVetContext _context;
        private readonly IAnimalDoadoraRepository _doadoraRepository;


        /* Injeção de uma instancia de IServicoRepository ao criar
        uma instancia de BookController */
        public DoadoraAPIController(WebProjVetContext contex, IAnimalDoadoraRepository doadoraRepository)
        {
            _context = contex;
            _doadoraRepository = doadoraRepository;
        }

        [Route("getProprietario")]
        public IActionResult GetProprietario()
        {
            /*
            var query = (from propi in _context.Proprietarios
                         join doaprop in _context.DoadoraProprietarios on propi.Id equals doaprop.ProprietarioId
                         //where doaprop.DoadoraId == 1
                         select new { Proprietario = propi }
                         ).ToList();

            //List<Proprietario> lstItems = _context.Doadoras.ToList();
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();

            //List<Proprietario> lstItems = _context.Proprietarios.ToList();
            //List<Proprietario> lstObjeto = new List<Proprietario>();

            var lstItems = new List<Proprietario>();
            foreach(var h in query)
            {
                var model = new Proprietario();
                model.Id = h.Proprietario.Id;
                model.Nome = h.Proprietario.Nome;
                lstItems.Add(model);
            }

            //List<Proprietario> lstItems = new List<Proprietario>(query.Cast<Proprietario>());
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();


            //return lstItems;



            //return new JsonResult(lstItems);
            */
            return new JsonResult("NOVO");
        }

        //Exemplo de requisição Post
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



        [Route("postAddProprietario/{idDoadora}")]
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

        [Route("listaProprietario/{id}")]
        public IActionResult ListaProprietario(int id)
        {
            var doadoras = new Doadora();

            if (id > 0)
            {

                 doadoras = _context.Doadoras
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.DoadoraProprietarios)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);               
            }

            return new JsonResult(doadoras);
        }


        public ActionResult Students()
        {
            DataTable dataTable = new DataTable();
            //dataTable.draw = int.Parse(Request.QueryString["draw"]);

            List<Student> students = new List<Student>();
            students.Add(new Student { Id = 1, Name = "Mike", SurName = "Mikey", ClassRoom = "8A" });
            students.Add(new Student { Id = 2, Name = "John", SurName = "Salary", ClassRoom = "8B" });
            students.Add(new Student { Id = 3, Name = "Steve", SurName = "Brown", ClassRoom = "7A" });

            

            var result = from s in students                         
                         select s;
            dataTable.data = result.ToArray();
            dataTable.recordsTotal = students.Count;
            dataTable.recordsFiltered = result.Count();
            return Json(dataTable);
        }

        [Route("LoadData-bkp")]
        public IActionResult LoadData_bkp()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = _doadoraRepository.Listar();//_context.Doadoras;

                    //(from tempcustomer in _context.CustomerTB
                    //select tempcustomer);

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //customerData = customerData.Where(m => m.Name == searchValue || m.Phoneno == searchValue || m.City == searchValue);
                }

                //total number of rows count 
                recordsTotal = customerData.Count();
                //Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [Route("LoadData")]
        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = _doadoraRepository.Listar();//_context.Doadoras;

                //(from tempcustomer in _context.CustomerTB
                //select tempcustomer);

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    //customerData = customerData.Where(m => m.Name == searchValue || m.Phoneno == searchValue || m.City == searchValue);
                }

                //total number of rows count 
                recordsTotal = customerData.Count();
                //Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        
        // POST api/values
        [HttpPost]
        [Route("lista")]
        public IActionResult Post()
        {
            //Get form data from client side
            var requestFormData = Request.Form;
            List<Proprietario> lstItems = GetData();

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
        private List<Proprietario> GetData()
        {
            
            
            var query = (from propi in _context.Proprietarios
                         join doaprop in _context.DoadoraProprietarios on propi.Id equals doaprop.ProprietarioId
                         //where doaprop.DoadoraId == 1
                         select new { Proprietario = propi }
                         ).ToList();

            //List<Proprietario> lstItems = _context.Doadoras.ToList();
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();

            List<Proprietario> lstItems = _context.Proprietarios.ToList();
            //List<Proprietario> lstObjeto = new List<Proprietario>();

           

            //List<Proprietario> lstItems = new List<Proprietario>(query.Cast<Proprietario>());
            //List<Proprietario> lstItems = query.AsEnumerable().Cast<Proprietario>().ToList();
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
        

/*
        // POST api/values
        [HttpPost]
        [Route("item")]
        public IActionResult Post()
        {
            //Get form data from client side
            var requestFormData = Request.Form;
            List<Item> lstItems = GetData();

            //Testar se pode remover
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
        private List<Item> GetData()
        {
            List<Item> lstItems = new List<Item>()
            {
                new Item() { ItemId =1030,Name ="Bose Mini II", Description ="Wireless and ultra-compact so you can take Bose sound anywhere"  },
                new Item() { ItemId =1031,Name ="Ape Case Envoy Compact - Black (AC520BK)", Description ="Ape Case Envoy Compact Messenger-Style Case for Camera - Black (AC520BK) Removable padded interior in Ape Case signature Hi-Vis yellow protects your equipment"  },
                new Item() { ItemId =1032,Name ="Xbox Wireless Controller - White", Description ="Precision controller compatible with Xbox One, Xbox One S and Windows 10."  },
                new Item() { ItemId =1033,Name ="GoPro HERO5 Black", Description ="Stunning 4K video and 12MP photos in Single, Burst and Time Lapse modes."  },
                new Item() { ItemId =1034,Name ="PNY Elite 240GB USB 3.0 Portable SSD", Description ="PNY Elite 240GB USB 3.0 Portable Solid State Drive (SSD) - (PSD1CS1050-240-FFS)"  },
                new Item() { ItemId =1035,Name ="Quick Charge 2.0 AUKEY 3-Port USB Wall Charger", Description ="Quick Charge 2.0 - Charge compatible devices up to 75% faster than conventional charging"  },
                new Item() { ItemId =1036,Name ="Bose SoundLink Color Bluetooth speaker II - Soft black", Description ="Innovative Bose technology packs bold sound into a small, water-resistant speaker"  },
                new Item() { ItemId = 1010,Name ="RayBan 12300", Description ="Polarized sunglasses"  },
                new Item() { ItemId =1011,Name ="HDMI Cable", Description ="Amzon Basic hdmi cable 3 feet"  },
                new Item() { ItemId =1020,Name ="Anket Portable Charger 500", Description =@"PowerCore Slim 5000
The Slimline Portable Charger

From ANKER, America's Leading USB Charging Brand
• Faster and safer charging with our advanced technology
• 20 million+ happy users and counting"  },
                new Item() { ItemId =1021,Name ="Zippo lighter", Description ="Zippo pocket lighter, black matte"  }

            };

            return lstItems;
        }

        /// <summary>
        /// Get a property info object from Item class filtering by property name.
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <returns>property info object</returns>
        private PropertyInfo getProperty(string name)
        {
            //Recebe o nome dos atributos do objeto
            var properties = typeof(Item).GetProperties();
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
        private List<Item> ProcessCollection(List<Item> lstElements, IFormCollection requestFormData)
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
        */

    }
}
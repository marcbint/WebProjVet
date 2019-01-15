using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.ProprietarioId = _context.Proprietarios.OrderBy(x => x.Nome).ToList();
            ViewBag.ServicoId = _context.Servicos.OrderBy(x => x.Nome).Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

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
                            objLista.DataInclusao = DateTime.Now;

                            _context.AnimaisProprietarios.Add(objLista);
                            _context.SaveChanges();
                        }

                    }
                }
            }
            
            
            //return RedirectToAction("Index");
            return RedirectToRoute(new { Controller = "Animal", Action = "Edit", id = animais.Id });

        }

       
        public IActionResult Edit(int id)
        {
            if (id > 0)
            {

                #region Processo de somatória dos serviços realizados           
                var lstItens = new List<double>();
                List<AnimaisServicos> Lista = _context.AnimaisServicos
                    .Where(x => x.AnimaisId == id 
                    && x.ServicoSituacao != ServicoSituacao.CANCELADO)
                    .ToList();

                double valor;
                double valorFinal;
                foreach (var h in Lista)
                {

                    valor = Convert.ToDouble(h.Valor);
                    lstItens.Add(valor);
                }
                valorFinal = lstItens.Sum() / 100;
                ViewBag.Total = string.Format(new CultureInfo("pt-BR"), "{0:N}", valorFinal);
                #endregion

                ViewBag.ProprietarioId = _context.Proprietarios.OrderBy(x => x.Nome).ToList();
                ViewBag.ServicoId = _context.Servicos.OrderBy(x => x.Nome).Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();
                ViewBag.Diaria = _context.Servicos.Where(s => s.ServicoTipo == ServicoTipo.DIÁRIA && s.Situacao == Situacao.ATIVO).ToList();
                ViewBag.DoadoraId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.DOADORA 
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.GaranhaoId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.GARANHÃO
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.ReceptoraId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.RECEPTORA
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.SemenId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.SÉMEN
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();

                ViewBag.SimNao = new SelectList(Enum.GetValues(typeof(EnumSimNao)));

                ViewBag.TipoCasco = new SelectList(Enum.GetValues(typeof(AnimalTipoCasco)));



                var animais = _context.Animais
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.AnimaisProprietarios)
                    .Include(s => s.AnimaisServicos)
                    .Include(t => t.AnimaisEntradas)
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
                /*
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
                                objLista.DataInclusao = DateTime.Now;

                                _context.AnimaisProprietarios.Add(objLista);
                                
                            }

                        }
                    }
                }*/

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animais);
        }



        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                //Somar o valor de serviços lançados no animal
                ViewBag.Total = _context.AnimaisServicos
                    .Where(x => x.AnimaisId == id)
                    .Sum(x => Convert.ToDecimal( x.Valor));
                ViewBag.ProprietarioId = _context.Proprietarios.OrderBy(x => x.Nome).ToList();
                ViewBag.ServicoId = _context.Servicos.OrderBy(x => x.Nome).Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

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

                //https://www.c-sharpcorner.com/article/how-to-use-join-operations-with-database-using-linq/
                //http://www.macoratti.net/18/08/efcore_join1.htm
                //http://www.macoratti.net/18/08/efcore_join2.htm
                //http://www.macoratti.net/18/08/efcore_join3.htm


                var animaisProprietario = _context.AnimaisProprietarios
                    .Where(p => p.Id.Equals(id))
                    .Include(e => e.Proprietario)
                    .FirstOrDefault();

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

            registro.DataDesassociacao = animaisProprietario.DataDesassociacao;
            registro.Motivo = animaisProprietario.Motivo;
        
            _context.AnimaisProprietarios.Update(registro);
            _context.SaveChanges();

            return RedirectToRoute(new { Controller = "Animal", Action = "Edit", id = animaisProprietario.AnimaisId });

        }


        public IActionResult DeleteProprietario_bkp(int id)
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
        public IActionResult DeleteProprietario_bkp(AnimaisProprietario animaisProprietario)
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
                        ObjAnimaisProprietarios.DataInclusao = DateTime.Now;

                        //Verifica se o Proprietário já esta adicionado à Doadora
                        int count = _context.DoadoraProprietarios
                            .Where(a => a.DoadoraId == ObjAnimaisProprietarios.AnimaisId
                       && a.ProprietarioId == ObjAnimaisProprietarios.ProprietarioId).Count();

                        if (count == 0)
                        {
                            ObjAnimaisProprietarios.DataDesassociacao = Convert.ToDateTime("31/12/9999");
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
                        //var obj = JsonConvert.DeserializeObject<AnimaisServicos>(requestBody);

                        AnimaisServicos ObjAnimaisServicos = JsonConvert.DeserializeObject<AnimaisServicos>(requestBody);

                        //Se animal for Doadora ou Garanhão
                        var inClause = new List<string> { "1", "2" };
                        int regra = _context.Animais
                            .Where(a => inClause.Contains(a.AnimalTipo.ToString()) 
                            && 
                            //.Where(a => 
                            a.Id == ObjAnimaisServicos.AnimaisId)
                            //.Select(a => a.Id)
                            .Count();
                        // O animal é garanhão ou doadora e deve ter pelo menos um proprietário ativo e vigente.
                        if (regra >= 1)
                        {

                            //Verifica se o animal precisa ter proprietário
                            int total = (from ap in _context.AnimaisProprietarios
                                         join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                                         join a in _context.Animais on ap.AnimaisId equals a.Id
                                         where ap.AnimaisId == ObjAnimaisServicos.AnimaisId
                                         && p.Situacao == Situacao.ATIVO
                                         && ap.DataAquisicao <= ObjAnimaisServicos.Data                                 
                                         && ap.DataDesassociacao >=ObjAnimaisServicos.Data
                                         select p.Id).Count();
                            //Se possui proprietário ativo ou vigente
                            if (total >= 1)
                            {
                                //Loop de Proprietarios com rateio


                                _context.AnimaisServicos.Add(ObjAnimaisServicos);
                                _context.SaveChanges();
                                retorno = "NOVO";
                            }
                            else
                            {
                                retorno = "SEMPROPRIETARIO";
                            }
                        }
                        else
                        {//Animal não é do tipo Doadora ou garanhão
                            _context.AnimaisServicos.Add(ObjAnimaisServicos);
                            _context.SaveChanges();
                            retorno = "NOVO";
                        }


                        //string valor = ObjAnimaisServicos.Valor.ToString().Replace(",", ".");
                        //https://pt.stackoverflow.com/questions/243124/como-limitar-casas-decimais-usando-c
                        //ObjAnimaisServicos.Valor = Convert.ToDecimal(ObjAnimaisServicos.Valor, new CultureInfo("pt-BR"));                        


                        return new JsonResult(retorno);

                    }
                }

            }

            return new JsonResult(retorno);
        }


        [Route("api/[controller]/AddEntrada/{idAnimais}")]
        public IActionResult AddEntrada(int idAnimais)
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
                        //var obj = JsonConvert.DeserializeObject<AnimaisServicos>(requestBody);

                        AnimaisEntrada ObjAnimaisEntrada = JsonConvert.DeserializeObject<AnimaisEntrada>(requestBody);

                        //string teste = ObjAnimaisServicos.ValorOriginal

                        //string valor = ObjAnimaisServicos.Valor.ToString().Replace(",", ".");
                        //https://pt.stackoverflow.com/questions/243124/como-limitar-casas-decimais-usando-c
                        //ObjAnimaisServicos.Valor = Convert.ToDecimal(ObjAnimaisServicos.Valor, new CultureInfo("pt-BR"));                        

                        _context.AnimaisEntradas.Add(ObjAnimaisEntrada);
                        _context.SaveChanges();
                        retorno = "NOVO";
                        return new JsonResult(retorno);

                    }
                }

            }

            return new JsonResult(retorno);
        }


        [Route("api/[controller]/GetTotalProprietario/{id}")]
        public IActionResult GetTotalProprietario(int id)
        {
            //int total = _context.AnimaisProprietarios.Where(x => x.AnimaisId.Equals(id)).Count();

            var total = (from ap in _context.AnimaisProprietarios
                        join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                        where ap.AnimaisId == id
                        && p.Situacao == Situacao.ATIVO
                        select p.Id).Count();
                                              
            return new JsonResult(total);
        }



        public IActionResult EditLancamentoServico(int id)
        {
            if (id > 0)
            {
                ViewBag.ServicoId = _context.Servicos.OrderBy(x => x.Nome).Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();

                ViewBag.DoadoraId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.DOADORA
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.GaranhaoId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.GARANHÃO
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.ReceptoraId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.RECEPTORA
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();
                ViewBag.SemenId = _context.Animais
                                    .Where(x => x.AnimalTipo == AnimalTipo.SÉMEN
                                    && x.Situacao == Situacao.ATIVO)
                                    .ToList();

                


                AnimaisServicos servicoLancado = _context.AnimaisServicos
                    .Include(p => p.Servico)
                    .Include(a => a.Animais)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);



                #region Processo que envia o tipo do animal para o processo de edição do serviço lançado.
                string tipo = _context.Animais.Where(x => x.Id == servicoLancado.AnimaisId).Select(p => p.AnimalTipo.ToString() ).FirstOrDefault();

                ViewBag.TipoAnimal = tipo;
                #endregion

                return View(servicoLancado);

            }
            return View();
        }

        [HttpPost]
        public IActionResult EditLancamentoServico(AnimaisServicos animaisServicos)
        {
            if (animaisServicos.ServicoSituacao == ServicoSituacao.CANCELADO)
            {
                animaisServicos.DataCancelamento = DateTime.Now;
            }
            else
            {
                animaisServicos.Motivo = null;
                animaisServicos.DataCancelamento = null;
            }

           
            _context.AnimaisServicos.Update(animaisServicos);
            _context.SaveChanges();

            return RedirectToRoute(new { Controller = "Animal", Action = "Edit", id = animaisServicos.AnimaisId });
        }


        public IActionResult EditEntrada(int id)
        {
            ViewBag.Diaria = _context.Servicos.Where(s => s.ServicoTipo == ServicoTipo.DIÁRIA && s.Situacao == Situacao.ATIVO).ToList();

            var animaisEntrada = _context.AnimaisEntradas.FirstOrDefault(p => p.Id == id);

            AnimaisServicos servicoLancado = _context.AnimaisServicos
                    .Include(p => p.Servico)
                    .Include(a => a.Animais)
                    .ToList()
                    .FirstOrDefault(p => p.Id == id);


            return View(animaisEntrada);
        }

        [HttpPost]
        public IActionResult EditEntrada(AnimaisEntrada animaisEntrada)
        {
            /*if(animaisEntrada.CobraDiaria == EnumSimNao.NÃO)
            {
                //animaisEntrada.ServicoId = null;
                animaisEntrada.Valor = null;
                animaisEntrada.ValorOriginal = null;

            }*/

            _context.AnimaisEntradas.Update(animaisEntrada);
            _context.SaveChanges();

            return RedirectToRoute(new { Controller = "Animal", Action = "Edit", id = animaisEntrada.AnimaisId });
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.Controllers
{
    //[Route("api/[Controller]")]
    public class TratamentoController : Controller
    {
        private readonly WebProjVetContext _context;
        private readonly IAnimalReceptoraRepository _receptoraRepository;
        private readonly IAnimalGaranhaoRepository _garanhaoRepository;
        private readonly IAnimalDoadoraRepository _doadoraRepository;
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private Tratamento _tratamento;



        public TratamentoController(WebProjVetContext context, 
            IAnimalReceptoraRepository receptoraRepository,
            IAnimalGaranhaoRepository garanhaoRepository, 
            IAnimalDoadoraRepository doadoraRepository,
            ITratamentoRepository tratamentoRepository,
            IServicoRepository servicoRepository
            )
        {
            _context = context;
            _receptoraRepository = receptoraRepository;
            _garanhaoRepository = garanhaoRepository;
            _doadoraRepository = doadoraRepository;
            _tratamentoRepository = tratamentoRepository;
            _servicoRepository = servicoRepository;
        }

        public IActionResult Index()
        {
            //var tratamento = _tratamentoRepository.Listar();

            var tratamento = _context.Tratamentos
                /*.Include(p => p.Receptora)
                .Include(c => c.Doadora)
                .Include(d => d.Garanhao)*/
                .Include(e => e.TratamentoServicos)
                .Include(a => a.TratamentoAnimais)
                .ToList();

            if (tratamento.Any())
                return View(tratamento);

            return View(tratamento);
        }

        public IActionResult Create()
        {
            //Somar o valor de serviços lançados no tratamento
            ViewBag.Total = _context.TratamentoServicos.Where(x => x.TratamentoId == 0).Sum(x => x.Valor);


            ViewBag.ReceptoraId = _context.Receptoras.ToList();
            ViewBag.DoadoraId = _context.Doadoras.ToList();
            ViewBag.GaranhaoId = _context.Garanhoes.ToList();
            ViewBag.ServicoId = _context.Servicos.Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();
            ViewBag.Diaria = _context.Servicos.Where(s => s.ServicoTipo == ServicoTipo.DIÁRIA).ToList();
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tratamento tratamento)
        {
            
            _context.Tratamentos.Add(tratamento);
            await _context.SaveChangesAsync();

            var tratamentoId = tratamento.Id;

            //Realiza a inclusão se existirem itens
            if (tratamento.TratamentoServicosJson != null)
            {
                //Processo de inclusão de itens
                List<TratamentoServico> listaTratamentoServico = JsonConvert.DeserializeObject<List<TratamentoServico>>(tratamento.TratamentoServicosJson);

                if (listaTratamentoServico.Count > 0)
                {
                    for (int i = 0; i < listaTratamentoServico.Count; i++)
                    {
                        if (listaTratamentoServico[i].Id == 0)
                        {                            
                            TratamentoServico objTratamentoServico = new TratamentoServico();
                            objTratamentoServico.TratamentoId = tratamentoId;
                            objTratamentoServico.ServicoId = listaTratamentoServico[i].ServicoId;
                            objTratamentoServico.Valor = listaTratamentoServico[i].Valor;
                            objTratamentoServico.Data = listaTratamentoServico[i].Data;
                            objTratamentoServico.ValorOriginal = GetValorOriginal(listaTratamentoServico[i].ServicoId);
                            
                            _context.TratamentoServicos.Add(objTratamentoServico);

                        }

                    }
                }
            }          

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        


        public IActionResult Edit(int id)
        {
            //Somar o valor de serviços lançados no tratamento
            ViewBag.Total = _context.TratamentoServicos.Where(x => x.TratamentoId == id).Sum(x => x.Valor);

            if (id > 0)
            {
                ViewBag.ReceptoraId = _context.Receptoras.ToList();
                ViewBag.DoadoraId = _context.Doadoras.ToList();
                ViewBag.GaranhaoId = _context.Garanhoes.ToList();
                ViewBag.ServicoId = _context.Servicos.Where(s => s.ServicoTipo != ServicoTipo.DIÁRIA).ToList();
                ViewBag.Diaria = _context.Servicos.Where(s => s.ServicoTipo == ServicoTipo.DIÁRIA).ToList();
                ViewBag.AnimaisId = _context.Animais.ToList();



                var animais = from m in _context.Garanhoes
                                  select new
                                  {
                                      Id = m.Id,
                                      Nome = m.Nome,
                                  };
                var animais2 = from m in _context.Doadoras
                               select new
                               {
                                   Id = m.Id,
                                   Nome = m.Nome,
                               };
                animais = animais.Concat(animais2);
                var resultAnimais = animais.OrderBy(m => m.Nome).ToList();

                ViewBag.Animais = resultAnimais;


                //Tratamento tratamento = _tratamentoRepository.ObterPorId(id);

                var tratamento = _context.Tratamentos
                    .Include(e => e.TratamentoServicos)
                    .Include(a => a.TratamentoAnimais)
                    /*.Include(p => p.Receptora)
                    .Include(c => c.Doadora)
                    .Include(d => d.Garanhao)*/
                    .ToList()
                    .First(p => p.Id == id);


                _tratamento = tratamento;
                return View(tratamento);
            }

            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                _tratamentoRepository.Editar(tratamento);

                //Processo de inclusão de serviços
                if (tratamento.TratamentoServicosJson != null)
                {
                    List<TratamentoServico> listaTratamentoServico = JsonConvert.DeserializeObject<List<TratamentoServico>>(tratamento.TratamentoServicosJson);
                    for (int i = 0; i < listaTratamentoServico.Count; i++)
                    {
                        if (listaTratamentoServico[i].Id == 0)
                        {
                            TratamentoServico objTratamentoServico = new TratamentoServico();
                            objTratamentoServico.TratamentoId = listaTratamentoServico[i].TratamentoId;
                            objTratamentoServico.ServicoId = listaTratamentoServico[i].ServicoId;
                            objTratamentoServico.Valor = listaTratamentoServico[i].Valor;
                            objTratamentoServico.Data = listaTratamentoServico[i].Data;
                            objTratamentoServico.ValorOriginal = GetValorOriginal(listaTratamentoServico[i].ServicoId);



                            _context.TratamentoServicos.Add(objTratamentoServico);
                        }

                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tratamento);
        }

        public decimal GetValorOriginal(int id)
        {
            var valor = _context.Servicos.First(p => p.Id.Equals(id)).Valor;
            return valor;
        }


        [HttpPost]
        public void AddServico(TratamentoServico tratamentoServicoViewModel)
        {
            tratamentoServicoViewModel.TratamentoId = 1;

            //var tratamentoServico = new TratamentoServicoViewModel();
            //tratamentoServico.TratamentoId = 1;
            //tratamentoServico.ServicoId = 1;

            _context.TratamentoServicos.Add(tratamentoServicoViewModel);
            _context.SaveChanges();
            //return View();
        }

        public string Teste(string parameter)
        {
            return DateTime.Today.ToString() + "-->>>" + parameter + "!!!!";
        }

        // GET: api/GetAllStudents
        
        //[Route("api/Tratamento/GetAllTratamentos")]
        [Route("GetAllTratamentos")]
        public IActionResult GetAllTratamentos()
        {
            var tratamentos = _context.Tratamentos
                /*.Include(p => p.Receptora)
                .Include(c => c.Doadora)
                .Include(d => d.Garanhao)*/
                .Include(e => e.TratamentoServicos)
                .Include(a => a.TratamentoAnimais)
                .ToList();


            return new JsonResult(tratamentos);
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(_tratamentoRepository.Listar());
        }

    }
}
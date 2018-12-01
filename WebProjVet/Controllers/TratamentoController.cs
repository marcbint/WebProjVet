using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.Controllers
{
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

            var tratamento = _context.Tratamentos.Include(p => p.Receptora).Include(c => c.Doadora).Include(d => d.Garanhao).ToList();

            if (tratamento.Any())
                return View(tratamento);

            return View(tratamento);
        }

        public IActionResult Create()
        {

            ViewBag.ReceptoraId = _context.Receptoras.ToList();
            ViewBag.DoadoraId = _context.Doadoras.ToList();
            ViewBag.GaranhaoId = _context.Garanhoes.ToList();
            ViewBag.ServicoId = _context.Servicos.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tratamento tratamento)
        {
            tratamento.DataAtualizacao = DateTime.Now;

            _context.Tratamentos.Add(tratamento);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {

            if (id > 0)
            {
                ViewBag.ReceptoraId = _context.Receptoras.ToList();
                ViewBag.DoadoraId = _context.Doadoras.ToList();
                ViewBag.GaranhaoId = _context.Garanhoes.ToList();
                ViewBag.ServicoId = _context.Servicos.ToList();

                var tratamento = _tratamentoRepository.ObterPorId(id);

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
                return RedirectToAction("Index");
            }
            return View(tratamento);
        }


        [HttpPost]
        public void AddServico(TratamentoServicoViewModel tratamentoServicoViewModel)
        {
            tratamentoServicoViewModel.TratamentoId = 1;

            //var tratamentoServico = new TratamentoServicoViewModel();
            //tratamentoServico.TratamentoId = 1;
            //tratamentoServico.ServicoId = 1;

            _context.TratamentoServicos.Add(tratamentoServicoViewModel);
            _context.SaveChanges();
            //return View();
        }



    }
}
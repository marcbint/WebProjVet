using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.Controllers
{
    public class FaturamentoController : Controller
    {
        private readonly WebProjVetContext _context;
        private readonly IFaturamentoRepository _faturamentoRepository;

        public FaturamentoController ( WebProjVetContext webProjVetContext)
        {
            _context = webProjVetContext;

            //IFaturamentoRepository faturamentoRepository,
            //_faturamentoRepository = faturamentoRepository;
        }

        public IActionResult Index()
        {
            //Linq Exemplos
            //https://www.devmedia.com.br/linq-e-csharp-efetuando-consultas-com-lambda-expressions/38863
            //http://www.macoratti.net/10/05/c_ulinq.htm


            List<Faturamento> LstFaturamento = (from fs in _context.FaturamentoServicos
                                                join p in _context.Proprietarios on fs.ProprietarioId equals p.Id
                                                group fs by fs.Proprietario into g
                                                select new Faturamento
                                                {
                                                    Proprietario = g.Key,
                                                    Valor = g.Sum(s => Convert.ToDecimal(s.Valor) / 100).ToString(),
                                                    Situacao = FaturamentoSituacao.PENDENTE,
                                                    Referencia = g.First().Referencia.Month.ToString()
                                                    
                                                }
                                                ).ToList();




            //List com Union
            List<Faturamento> listServico = (from p in _context.Proprietarios
                                             join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                             join a in _context.Animais on ap.AnimaisId equals a.Id
                                             join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                             select new Faturamento
                                             {
                                                 ProprietarioId = p.Id

                                             }).ToList();

            List<Faturamento> listDiaria = (from p in _context.Proprietarios
                                            join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                            join a in _context.Animais on ap.AnimaisId equals a.Id
                                            join ae in _context.AnimaisEntradas on a.Id equals ae.AnimaisId
                                            select new Faturamento
                                            {
                                                ProprietarioId = p.Id
                                            }).ToList();

            List<Faturamento> lstProprietario = listServico.Union(listDiaria).Distinct().ToList();



            /*
                        var lstServicoProprietario = (from ap in _context.AnimaisProprietarios
                                     join a in _context.Animais on ap.AnimaisId equals a.Id
                                     join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                     group aass.AnimaisProprietarios.FirstOrDefault()

                                     by ap into g
                                     select new 
                                     {
                                         ProprietarioId = g.FirstOrDefault(),
                                         Valor = g.Sum(s => Convert.ToDecimal(s.Valor) / 100).ToString()

                                     }
                         ).ToList();


                        List<Faturamento> listaClientesVM = new List<Faturamento>();

                        foreach (var item in lstServicoProprietario)
                        {
                            Faturamento cliVM = new Faturamento(); //ViewModel
                            cliVM.ProprietarioId  = item.ProprietarioId;
                            cliVM.Valor = item.Valor;

                            listaClientesVM.Add(cliVM);
                        }*/


            List<FaturamentoServicos> listServicoProprietario = (from p in _context.Proprietarios
                                             join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                             join a in _context.Animais on ap.AnimaisId equals a.Id
                                             join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                             select new FaturamentoServicos
                                             {
                                                 ProprietarioId = p.Id,
                                                 AnimaisServicosId = aass.Id,
                                                 AnimaisId = a.Id,
                                                 ServicoId = aass.ServicoId,
                                                 Valor = aass.ValorTotal,                                                 
                                                 DataFaturamento = DateTime.Now,
                                                 Referencia = DateTime.Now

                                             }).ToList();

            /*
            foreach(var item in listServicoProprietario)
            {
                //FaturamentoServicosProprietario fat= new FaturamentoServicosProprietario(); //ViewModel
                _context.FaturamentoServicos.Add(item);
                _context.SaveChanges();
            }*/


            //FaturaProprietario(1);

            //Retorna as informações para View
            return View(LstFaturamento);

        }

        public IActionResult ServicosAnimais()
        {
            return null;
        }


        public IActionResult Create()
        {
            ViewBag.ProprietarioId = _context.Proprietarios.OrderBy(x => x.Nome).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Faturamento faturamento)
        {
            List<AnimaisServicos> listServicoProprietario = new List<AnimaisServicos>();

            #region Identifica os serviços realizados para animais vinculados em Proprietários para o período.
            if (faturamento.ProprietarioId != 0)
            {
                listServicoProprietario = (from p in _context.Proprietarios
                                                                 join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                                                 join a in _context.Animais on ap.AnimaisId equals a.Id
                                                                 join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                                 where aass.Data.ToString("MM/yyyy") == faturamento.Referencia
                                                                 && p.Id == faturamento.ProprietarioId
                                                                 && p.Situacao == Situacao.ATIVO     
                                                                 && aass.Faturamento == "N"
                                                                 && aass.ServicoSituacao == ServicoSituacao.REALIZADO
                                                                 select
                                                                 (
                                                                    aass
                                                                 )).ToList();
            }
            else
            {
                listServicoProprietario = (from p in _context.Proprietarios
                                                                 join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                                                 join a in _context.Animais on ap.AnimaisId equals a.Id
                                                                 join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                                 where aass.Data.ToString("MM/yyyy") == faturamento.Referencia
                                                                 && p.Situacao == Situacao.ATIVO
                                                                 && aass.Faturamento == "N"
                                                                 && aass.ServicoSituacao == ServicoSituacao.REALIZADO
                                                                 select
                                                                 (
                                                                    aass
                                                                 )).ToList();
            }
            #endregion


            foreach (var item in listServicoProprietario)
            {


                AnimaisServicos ObjAnimaisServicos = _context.AnimaisServicos.FirstOrDefault(s => s.Id == item.Id);

                //Verifica se o animal possui proprietário apto para cobrança de acordo com a data de realização do serviço.
                List<Proprietario> LstProprietario = (from ap in _context.AnimaisProprietarios
                                                      join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                                                      join a in _context.Animais on ap.AnimaisId equals a.Id
                                                      where ap.AnimaisId == ObjAnimaisServicos.AnimaisId
                                                      && p.Situacao == Situacao.ATIVO
                                                      && ap.DataAquisicao <= ObjAnimaisServicos.Data
                                                      && ap.DataDesassociacao >= ObjAnimaisServicos.Data
                                                      select p).ToList();

                int totalProprietario = LstProprietario.Count();

                //Quando o animal do serviço realizado possui mais de um proprietário realiza loop para rateio do serviço que será faturado.
                if (totalProprietario >= 2)
                {
                    
                    for (int i = 0; i < LstProprietario.Count(); i++)
                    {
                        FaturamentoServicos faturamentoServicos = new FaturamentoServicos();

                        faturamentoServicos.ProprietarioId = LstProprietario[i].Id;
                        faturamentoServicos.AnimaisServicosId = ObjAnimaisServicos.Id;
                        faturamentoServicos.AnimaisId = ObjAnimaisServicos.AnimaisId;
                        faturamentoServicos.ServicoId = ObjAnimaisServicos.ServicoId;

                        decimal valor = ((Convert.ToDecimal(ObjAnimaisServicos.ValorTotal) / 100) / totalProprietario);
                        faturamentoServicos.Valor = Decimal.Round(valor, 2).ToString();

                        faturamentoServicos.DataFaturamento = DateTime.Now;

                        DateTime dataReferenciaFormatada = Convert.ToDateTime(faturamento.Referencia);
                        faturamentoServicos.Referencia = dataReferenciaFormatada;

                        //Adiciona o serviço que passou pelo rateio na lista de serviços do proprietário.
                        _context.FaturamentoServicos.Add(faturamentoServicos);   
                        _context.SaveChanges();

                    }
                }
                else
                {
                    FaturamentoServicos faturamentoServicos = new FaturamentoServicos();

                    faturamentoServicos.ProprietarioId = LstProprietario[0].Id;
                    faturamentoServicos.AnimaisServicosId = ObjAnimaisServicos.Id;
                    faturamentoServicos.AnimaisId = ObjAnimaisServicos.AnimaisId;
                    faturamentoServicos.ServicoId = ObjAnimaisServicos.ServicoId;
                    faturamentoServicos.Valor = (Convert.ToDecimal(ObjAnimaisServicos.ValorTotal) / 100).ToString();
                    faturamentoServicos.DataFaturamento = DateTime.Now;

                    DateTime dataReferenciaFormatada =  Convert.ToDateTime(faturamento.Referencia);
                    faturamentoServicos.Referencia = dataReferenciaFormatada;

                    _context.FaturamentoServicos.Add(faturamentoServicos);
                    _context.SaveChanges();

                }

                //Atualiza o serviço realizado no animal para item incluído no processo de faturamento.
                ObjAnimaisServicos.Faturamento = "S";
                _context.AnimaisServicos.Update(ObjAnimaisServicos);
                _context.SaveChanges();

            }


            //Após o processo de validação e rateio dos serviços do proprietário devem ser criadas as informações na tabela Faturamento
            //e atualizar os vinculos com a tabela faturamentoservicos.
            List<Faturamento> LstFaturamento = (from fs in _context.FaturamentoServicos
                                                join p in _context.Proprietarios on fs.ProprietarioId equals p.Id
                                                where fs.Referencia.ToString("MM/yyyy") == faturamento.Referencia
                                                group fs by fs.Proprietario into g
                                                select new Faturamento
                                                {
                                                    Proprietario = g.Key,
                                                    Valor = g.Sum(s => Convert.ToDecimal(s.Valor) / 100).ToString(),
                                                    Situacao = FaturamentoSituacao.PENDENTE,
                                                    Referencia = g.First().Referencia.Month.ToString()

                                                }
                                                ).ToList();


            return View();
        }



        public void FaturaProprietario(int idAnimalServico)
        {

            List<AnimaisServicos> listServicoProprietario = (from p in _context.Proprietarios
                                                                 join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                                                 join a in _context.Animais on ap.AnimaisId equals a.Id
                                                                 join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                                 select 
                                                                 (
                                                                    aass                                                                   
                                                                 )).ToList();

            foreach (var item in listServicoProprietario)
            {


                AnimaisServicos ObjAnimaisServicos = _context.AnimaisServicos.FirstOrDefault(s => s.Id == item.Id);

                //Verifica se o animal precisa ter proprietário
                List<Proprietario> LstProprietario = (from ap in _context.AnimaisProprietarios
                                                      join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                                                      join a in _context.Animais on ap.AnimaisId equals a.Id
                                                      where ap.AnimaisId == ObjAnimaisServicos.AnimaisId
                                                      && p.Situacao == Situacao.ATIVO
                                                      && ap.DataAquisicao <= ObjAnimaisServicos.Data
                                                      && ap.DataDesassociacao >= ObjAnimaisServicos.Data
                                                      select p).ToList();

                int totalProprietario = LstProprietario.Count();

                //Realiza o loop de proprietários e realiza o rateio.
                if (totalProprietario >= 2)
                {
                    LstProprietario.Count();

                    for (int i = 0; i < LstProprietario.Count(); i++)
                    {
                        FaturamentoServicos faturamentoServicos = new FaturamentoServicos();

                        faturamentoServicos.ProprietarioId = LstProprietario[i].Id;
                        faturamentoServicos.AnimaisServicosId = ObjAnimaisServicos.Id;
                        faturamentoServicos.AnimaisId = ObjAnimaisServicos.AnimaisId;
                        faturamentoServicos.ServicoId = ObjAnimaisServicos.ServicoId;

                        decimal valor = ((Convert.ToDecimal(ObjAnimaisServicos.ValorTotal) / 100) / totalProprietario);
                        faturamentoServicos.Valor = Decimal.Round(valor, 2).ToString();

                        faturamentoServicos.DataFaturamento = DateTime.Now;
                        faturamentoServicos.Referencia = DateTime.Now;

                        _context.FaturamentoServicos.Add(faturamentoServicos);
                        _context.SaveChanges();

                    }
                }
                else
                {
                    FaturamentoServicos faturamentoServicos = new FaturamentoServicos();

                    faturamentoServicos.ProprietarioId = LstProprietario[0].Id;
                    faturamentoServicos.AnimaisServicosId = ObjAnimaisServicos.Id;
                    faturamentoServicos.AnimaisId = ObjAnimaisServicos.AnimaisId;
                    faturamentoServicos.ServicoId = ObjAnimaisServicos.ServicoId;
                    faturamentoServicos.Valor = (Convert.ToDecimal(ObjAnimaisServicos.ValorTotal) / 100).ToString();
                    faturamentoServicos.DataFaturamento = DateTime.Now;
                    faturamentoServicos.Referencia = DateTime.Now;

                    _context.FaturamentoServicos.Add(faturamentoServicos);
                    _context.SaveChanges();

                }

            }





        }
    }
}


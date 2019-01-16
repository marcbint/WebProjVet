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

            /////////Faturamento dos serviços apurados
            List<Faturamento> LstFaturamento = (from fs in _context.FaturamentoServicos
                                                join p in _context.Proprietarios on fs.ProprietarioId equals p.Id
                                                group fs by fs.Proprietario into g
                                                select new Faturamento
                                                {
                                                    Proprietario = g.Key,
                                                    Valor = Math.Round(g.Sum(s => Convert.ToDecimal(s.Valor)),2).ToString(),
                                                    Situacao = FaturamentoSituacao.PENDENTE,
                                                    Referencia = g.First().Referencia
                                                    
                                                }
                                                ).ToList();


            List<Faturamento> LstFaturamentoEntrada = (from fe in _context.FaturamentoEntradas
                                                join p in _context.Proprietarios on fe.ProprietarioId equals p.Id
                                                group fe by fe.Proprietario into g
                                                select new Faturamento
                                                {
                                                    Proprietario = g.Key,
                                                    Valor = Math.Round(g.Sum(s => Convert.ToDecimal(s.Valor)), 2).ToString(),
                                                    Situacao = FaturamentoSituacao.PENDENTE,
                                                    Referencia = g.First().Referencia

                                                }
                                                ).ToList();

            List<Faturamento> LstFaturamentoGeral = new List<Faturamento>();
            LstFaturamentoGeral = LstFaturamento.Union(LstFaturamentoEntrada).ToList();


            



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
                                                 DataFaturamento = DateTime.Now
                                                 //,Referencia = DateTime.Now

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
            return View(LstFaturamentoGeral);

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
            string mes = faturamento.DataApuracao.Value.Month.ToString().PadLeft(2, '0');
            string ano = faturamento.DataApuracao.Value.Year.ToString();
            string referencia = mes + "/" + ano;


            List<AnimaisServicos> listServicoProprietario = new List<AnimaisServicos>();

            #region Identifica os serviços realizados para animais vinculados em Proprietários para o período.
            if (faturamento.ProprietarioId != 0)
            {
                listServicoProprietario = (from p in _context.Proprietarios
                                                                 join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                                                 join a in _context.Animais on ap.AnimaisId equals a.Id
                                                                 join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                                 where aass.Data.ToString("MM/yyyy") == referencia
                                                                 && p.Id == faturamento.ProprietarioId
                                                                 && aass.Data <= faturamento.DataApuracao
                                                                 && p.Situacao == Situacao.ATIVO
                                                                 && ap.DataAquisicao <= faturamento.DataApuracao
                                                                 && ap.DataValidade > faturamento.DataApuracao
                                                                 && aass.Faturamento == "N"
                                                                 && aass.ServicoSituacao == ServicoSituacao.REALIZADO
                                                                select new AnimaisServicos
                                                                {
                                                                    Id = aass.Id
                                                                }
                                                                 ).Distinct().ToList();
            }
            else
            {
                listServicoProprietario = (from p in _context.Proprietarios
                                                                 join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                                                 join a in _context.Animais on ap.AnimaisId equals a.Id
                                                                 join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                                 where aass.Data.ToString("MM/yyyy") == referencia
                                                                 && aass.Data <= faturamento.DataApuracao
                                                                 && p.Situacao == Situacao.ATIVO
                                                                 && ap.DataAquisicao <= faturamento.DataApuracao
                                                                 && ap.DataValidade > faturamento.DataApuracao
                                                                 && aass.Faturamento == "N"
                                                                 && aass.ServicoSituacao == ServicoSituacao.REALIZADO
                                                                 select new AnimaisServicos
                                                                 {
                                                                    Id = aass.Id
                                                                 }
                                                                 ).Distinct().ToList();
            }
            #endregion


            foreach (var item in listServicoProprietario)
            {
                AnimaisServicos ObjAnimaisServicos = _context.AnimaisServicos.FirstOrDefault(s => s.Id == item.Id);

                if (listServicoProprietario.Count > 0)
                {
                
                    //Verifica se o animal possui proprietário apto para cobrança de acordo com a data de realização do serviço.
                    List<Proprietario> LstProprietario = (from ap in _context.AnimaisProprietarios
                                                          join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                                                          join a in _context.Animais on ap.AnimaisId equals a.Id
                                                          join aass in _context.AnimaisServicos on a.Id equals aass.AnimaisId
                                                          where aass.Id == item.Id
                                                          && ap.AnimaisId == ObjAnimaisServicos.AnimaisId
                                                          && p.Situacao == Situacao.ATIVO
                                                          && ap.DataAquisicao <= ObjAnimaisServicos.Data
                                                          && ap.DataValidade > ObjAnimaisServicos.Data
                                                          && aass.Faturamento == "N"
                                                          && aass.ServicoSituacao == ServicoSituacao.REALIZADO
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

                            decimal valor = ((Convert.ToDecimal(ObjAnimaisServicos.ValorTotal) / 100) / totalProprietario) ;
                            //faturamentoServicos.Valor  = Decimal.Round((valor), 2).ToString();

                            faturamentoServicos.Valor = Convert.ToDecimal(valor).ToString();

                            faturamentoServicos.DataFaturamento = DateTime.Now;

                            //DateTime dataReferenciaFormatada = Convert.ToDateTime(faturamento.Referencia);
                            faturamentoServicos.Referencia = referencia;

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

                        //DateTime dataReferenciaFormatada = Convert.ToDateTime(faturamento.Referencia);
                        faturamentoServicos.Referencia = referencia;

                        _context.FaturamentoServicos.Add(faturamentoServicos);
                        _context.SaveChanges();

                    }
                   
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
                                                //where fs.Referencia.ToString("MM/yyyy") == faturamento.Referencia
                                                where fs.Referencia == referencia
                                                group fs by fs.Proprietario into g
                                                select new Faturamento
                                                {
                                                    Proprietario = g.Key,
                                                    Valor = g.Sum(s => Convert.ToDecimal(s.Valor) / 100).ToString(),
                                                    Situacao = FaturamentoSituacao.PENDENTE,
                                                    Referencia = g.First().Referencia

                                                }
                                                ).ToList();


            //////////////////////////Processo de Faturamento das Diárias///////////////////////////////////////////////
            ///
            List<AnimaisEntrada> listEntradasProprietario = new List<AnimaisEntrada>();

            #region Identifica as entradas para animais vinculados em Proprietários para o período.
            if (faturamento.ProprietarioId != 0)
            {
                listEntradasProprietario = (from p in _context.Proprietarios
                                           join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                           join a in _context.Animais on ap.AnimaisId equals a.Id
                                           join aa in _context.AnimaisEntradas on a.Id equals aa.AnimaisId
                                           where p.Id == faturamento.ProprietarioId
                                           && p.Situacao == Situacao.ATIVO                                          
                                           && aa.DiariaSituacao != DiariaSituacao.CANCELADA
                                           && ap.DataAquisicao < faturamento.DataApuracao
                                           && ap.DataValidade >= faturamento.DataApuracao
                                           && aa.DataUltimaApuracao < faturamento.DataApuracao

                                            select
                                           (
                                              aa
                                           )).ToList();
            }
            else
            {
                listEntradasProprietario = (from p in _context.Proprietarios
                                            join ap in _context.AnimaisProprietarios on p.Id equals ap.ProprietarioId
                                            join a in _context.Animais on ap.AnimaisId equals a.Id
                                            join aa in _context.AnimaisEntradas on a.Id equals aa.AnimaisId
                                            where p.Situacao == Situacao.ATIVO
                                            && ap.DataAquisicao < faturamento.DataApuracao
                                            && ap.DataValidade >= faturamento.DataApuracao
                                            && aa.DiariaSituacao != DiariaSituacao.CANCELADA
                                            && aa.DataUltimaApuracao < faturamento.DataApuracao
                                            select
                                            (
                                               aa
                                            )).ToList();


                
            }
            #endregion

            foreach (var item in listEntradasProprietario)
            {
                if (listEntradasProprietario.Count > 0)
                {

                    AnimaisEntrada ObjAnimaisEntradas = _context.AnimaisEntradas.FirstOrDefault(s => s.Id == item.Id);

                    //Verifica se o animal possui proprietário apto para cobrança de acordo com a data de realização do serviço.
                    List<Proprietario> LstProprietario = (from ap in _context.AnimaisProprietarios
                                                          join p in _context.Proprietarios on ap.ProprietarioId equals p.Id
                                                          join a in _context.Animais on ap.AnimaisId equals a.Id
                                                          join aa in _context.AnimaisEntradas on a.Id equals aa.AnimaisId
                                                          where aa.Id == item.Id
                                                          && ap.AnimaisId == ObjAnimaisEntradas.AnimaisId
                                                          && p.Situacao == Situacao.ATIVO
                                                          && ap.DataAquisicao < ObjAnimaisEntradas.DataUltimaApuracao
                                                          && ap.DataValidade >= ObjAnimaisEntradas.DataUltimaApuracao
                                                          select p).ToList();

                    int totalProprietario = listEntradasProprietario.Count();

                    //Quando o animal do serviço realizado possui mais de um proprietário realiza loop para rateio do serviço que será faturado.
                    if (totalProprietario >= 2)
                    {

                        for (int i = 0; i < listEntradasProprietario.Count(); i++)
                        {
                            FaturamentoEntradas faturamentoEntradas = new FaturamentoEntradas();

                            faturamentoEntradas.ProprietarioId = listEntradasProprietario[i].Id;
                            faturamentoEntradas.AnimaisEntradasId = ObjAnimaisEntradas.Id;
                            faturamentoEntradas.AnimaisId = ObjAnimaisEntradas.AnimaisId;
                            faturamentoEntradas.ServicoId = ObjAnimaisEntradas.ServicoId;

                            decimal diaria = (Convert.ToDecimal(ObjAnimaisEntradas.Valor) / 100);
                            faturamentoEntradas.Diaria = diaria;

                            //Calcular a quantidade de dias do mes ainda não apurados de acordo com a data de apuração informada.
                            //Dias => data apuração informada - data ultima apuração
                            DateTime? data1 = ObjAnimaisEntradas.DataUltimaApuracao;
                            DateTime? data2 = faturamento.DataApuracao;
                            TimeSpan? totalDias = data2 - data1;
                            int dias = totalDias.Value.Days;
                            faturamentoEntradas.Dias = dias;

                            faturamentoEntradas.Valor = (diaria * dias).ToString();                          
                            faturamentoEntradas.DataFaturamento = DateTime.Now;
                            faturamentoEntradas.Referencia = referencia;

                            //Adiciona o serviço que passou pelo rateio na lista de serviços do proprietário.
                            _context.FaturamentoEntradas.Add(faturamentoEntradas);
                            _context.SaveChanges();

                        }
                    }
                    else
                    {
                        FaturamentoEntradas faturamentoEntradas = new FaturamentoEntradas();

                        faturamentoEntradas.ProprietarioId = listEntradasProprietario[0].Id;
                        faturamentoEntradas.AnimaisEntradasId = ObjAnimaisEntradas.Id;
                        faturamentoEntradas.AnimaisId = ObjAnimaisEntradas.AnimaisId;
                        faturamentoEntradas.ServicoId = ObjAnimaisEntradas.ServicoId;
                        decimal diaria = (Convert.ToDecimal(ObjAnimaisEntradas.Valor) / 100);
                        faturamentoEntradas.Diaria = diaria ;

                        //Calcular a quantidade de dias do mes ainda não apurados de acordo com a data de apuração informada.
                        //Dias => data apuração informada - data ultima apuração
                        DateTime? data1 = ObjAnimaisEntradas.DataUltimaApuracao;
                        DateTime? data2 = faturamento.DataApuracao;
                        TimeSpan? totalDias = data2 - data1;
                        int dias = totalDias.Value.Days;
                        faturamentoEntradas.Dias = dias;

                        faturamentoEntradas.Valor = (diaria * dias).ToString();
                        faturamentoEntradas.DataFaturamento = DateTime.Now;
                        faturamentoEntradas.Referencia = referencia;

                        //Adiciona o serviço que passou pelo rateio na lista de serviços do proprietário.
                        _context.FaturamentoEntradas.Add(faturamentoEntradas);
                        _context.SaveChanges();

                    }

                    //Atualiza o serviço realizado no animal para item incluído no processo de faturamento.
                    ObjAnimaisEntradas.DataUltimaApuracao = faturamento.DataApuracao;
                    _context.AnimaisEntradas.Update(ObjAnimaisEntradas);
                    _context.SaveChanges();
                }
            }



            return RedirectToAction("Index");
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
                        //faturamentoServicos.Referencia = DateTime.Now;

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
                    //faturamentoServicos.Referencia = DateTime.Now;

                    _context.FaturamentoServicos.Add(faturamentoServicos);
                    _context.SaveChanges();

                }

            }





        }
    }
}


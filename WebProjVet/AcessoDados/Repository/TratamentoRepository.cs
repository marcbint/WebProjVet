using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Repository
{
    public class TratamentoRepository :ITratamentoRepository
    {
        //Injeção de Dependência
        private readonly WebProjVetContext _webProjVetContext;
        public TratamentoRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        
        public List<Tratamento> Listar()
        {
            return _webProjVetContext.Tratamentos.ToList();
        }

        public Tratamento ObterPorId(int id)
        {
            return _webProjVetContext.Tratamentos.FirstOrDefault(p => p.Id == id);
            
        }

        public void Editar(Tratamento tratamento)
        {
            _webProjVetContext.Entry(tratamento).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();

        }
    }
}

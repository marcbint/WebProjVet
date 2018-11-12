using System.Collections.Generic;
using System.Linq;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Servicos
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly WebProjVetContext _webProjVetContext;

        public ServicoRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        public List<Servico> ListarServicos()
        {
            return _webProjVetContext.Servicos.ToList();
        }

        public Servico ObterServicoPorId(int id)
        {
            return _webProjVetContext.Servicos.FirstOrDefault(p => p.Id == id);
        }

        public void Salvar(Servico servico)
        {
            _webProjVetContext.Servicos.Add(servico);
            _webProjVetContext.SaveChanges();

        }

        public void Editar(Servico servico)
        {
            _webProjVetContext.Entry(servico).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
            
        }

        public void Remover(Servico servico)
        {
            _webProjVetContext.Servicos.Remove(servico);
            _webProjVetContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Servicos
{
    public class ProprietarioRepository : IProprietarioRepository
    {

        //Injeção de dependência
        private readonly WebProjVetContext _webProjVetContext;
        public ProprietarioRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }


        public void Editar(Proprietario proprietario)
        {
            _webProjVetContext.Entry(proprietario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
        }

        public List<Proprietario> ListarProprietarios()
        {
            return _webProjVetContext.Proprietarios.ToList();
        }

        public Proprietario ObterProprietarioPorId(int id)
        {
            return _webProjVetContext.Proprietarios.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(Proprietario proprietario)
        {
            _webProjVetContext.Proprietarios.Remove(proprietario);
            _webProjVetContext.SaveChanges();
        }

        public void Salvar(Proprietario proprietario)
        {
            _webProjVetContext.Proprietarios.Add(proprietario);
            _webProjVetContext.SaveChanges();
        }
    }
}

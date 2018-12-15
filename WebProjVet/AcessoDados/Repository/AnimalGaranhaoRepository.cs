using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Servicos
{
    public class AnimalGaranhaoRepository : IAnimalGaranhaoRepository
    {
        //Injeção de Dependência
        private readonly WebProjVetContext _webProjVetContext;

        public AnimalGaranhaoRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        public void Editar(Garanhao animalGaranhao)
        {
            _webProjVetContext.Entry(animalGaranhao).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
        }

        public List<Garanhao> Listar()
        {
            return _webProjVetContext.Garanhoes.ToList();
        }

        public Garanhao ObterPorId(int id)
        {
            return _webProjVetContext.Garanhoes.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(Garanhao animalGaranhao)
        {
            _webProjVetContext.Garanhoes.Remove(animalGaranhao);
            _webProjVetContext.SaveChanges();
        }

        public void Salvar(Garanhao animalGaranhoes)
        {
            _webProjVetContext.Garanhoes.Add(animalGaranhoes);
            _webProjVetContext.SaveChanges();
        }

        public Garanhao GetById(int id)
        {
            //var query = _webProjVetContext.Set<Animal>().Include(p => p.Proprietarios).Where(e => e.Id == id);

            var query = _webProjVetContext.Garanhoes.FirstOrDefault(p => p.Id == id);

            //if (query.Any())
            return query;
            //return null;
        }

    }
}

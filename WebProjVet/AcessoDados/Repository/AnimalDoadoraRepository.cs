using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Servicos
{
    public class AnimalDoadoraRepository : IAnimalDoadoraRepository
    {
        //Injeção de Dependência
        private readonly WebProjVetContext _webProjVetContext;
        public AnimalDoadoraRepository (WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        public void Editar(AnimalDoadora animal)
        {
            _webProjVetContext.Entry(animal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
        }

        public List<AnimalDoadora> Listar()
        {
            return _webProjVetContext.Doadoras.ToList();
        }

        public AnimalDoadora ObterPorId(int id)
        {
            return _webProjVetContext.Doadoras.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(AnimalDoadora animal)
        {
            _webProjVetContext.Doadoras.Remove(animal);
            _webProjVetContext.SaveChanges();
        }

        public void Salvar(AnimalDoadora animal)
        {
            _webProjVetContext.Doadoras.Add(animal);
            _webProjVetContext.SaveChanges();
        }

        public AnimalDoadora GetById(int id)
        {
            //var query = _webProjVetContext.Set<Animal>().Include(p => p.Proprietarios).Where(e => e.Id == id);

            var query = _webProjVetContext.Doadoras.FirstOrDefault(p => p.Id == id);

            //if (query.Any())
                return query;
            //return null;
        }

        public IEnumerable<AnimalDoadora> All()
        {
            return _webProjVetContext.Set<AnimalDoadora>().Include(p => p.Proprietarios).AsEnumerable();
        }
    }
}

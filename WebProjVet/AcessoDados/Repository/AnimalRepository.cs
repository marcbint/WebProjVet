using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Servicos
{
    public class AnimalRepository : IAnimalRepository
    {
        //Injeção de Dependência
        private readonly WebProjVetContext _webProjVetContext;

        public AnimalRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        public void Editar(Animais animal)
        {
            _webProjVetContext.Entry(animal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
        }

        public List<Animais> Listar()
        {
            return _webProjVetContext.Animais.ToList();
        }

        public Animais ObterPorId(int id)
        {
            return _webProjVetContext.Animais.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(Animais animal)
        {
            _webProjVetContext.Animais.Remove(animal);
            _webProjVetContext.SaveChanges();
        }

        public void Salvar(Animais animal)
        {
            _webProjVetContext.Animais.Add(animal);
            _webProjVetContext.SaveChanges();
        }

        public Animais GetById(int id)
        {

            var query = _webProjVetContext.Animais.FirstOrDefault(p => p.Id == id);

            //if (query.Any())
                return query;
            //return null;
        }

        public IEnumerable<Animais> All()
        {
            //return _webProjVetContext.Set<AnimalDoadora>().Include(p => p.Proprietarios).AsEnumerable();
            return _webProjVetContext.Set<Animais>();
        }
    }
}

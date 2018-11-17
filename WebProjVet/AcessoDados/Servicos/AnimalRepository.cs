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
        public AnimalRepository (WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }

        public void Editar(Animal animal)
        {
            _webProjVetContext.Entry(animal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();
        }

        public List<Animal> Listar()
        {
            return _webProjVetContext.Animais.ToList();
        }

        public Animal ObterPorId(int id)
        {
            return _webProjVetContext.Animais.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(Animal animal)
        {
            _webProjVetContext.Animais.Remove(animal);
            _webProjVetContext.SaveChanges();
        }

        public void Salvar(Animal animal)
        {
            _webProjVetContext.Animais.Add(animal);
            _webProjVetContext.SaveChanges();
        }
    }
}

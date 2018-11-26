using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;
using Microsoft.EntityFrameworkCore;
using WebProjVet.AcessoDados.Interfaces;

namespace WebProjVet.AcessoDados.Servicos
{
    public class AnimalReceptoraRepository : IAnimalReceptoraRepository
    {
        private readonly WebProjVetContext _webProjVetContext;
        public AnimalReceptoraRepository(WebProjVetContext webProjVetContext)
        {
            _webProjVetContext = webProjVetContext;
        }


        public void Editar(AnimalReceptora animal)
        {
            _webProjVetContext.Entry(animal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _webProjVetContext.SaveChanges();

        }

        public List<AnimalReceptora> Listar()
        {
            return _webProjVetContext.Receptoras.ToList();
        }

        public AnimalReceptora ObterPorId(int id)
        {
            return _webProjVetContext.Receptoras.FirstOrDefault(p => p.Id == id);
        }

        public void Remover(AnimalReceptora animal)
        {
            _webProjVetContext.Receptoras.Remove(animal);
            _webProjVetContext.SaveChanges();

        }

        public void Salvar(AnimalReceptora animal)
        {
            _webProjVetContext.Receptoras.Add(animal);
            _webProjVetContext.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalRepository
    {
        List<Animal> Listar();
        Animal ObterPorId(int id);
        void Salvar(Animal animal);
        void Editar(Animal animal);
        void Remover(Animal animal);
    }
}

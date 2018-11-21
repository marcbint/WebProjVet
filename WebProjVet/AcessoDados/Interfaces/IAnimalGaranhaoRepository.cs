using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalGaranhaoRepository
    {
        List<AnimalGaranhao> Listar();
        AnimalGaranhao ObterPorId(int id);
        AnimalGaranhao GetById(int id);
        void Salvar(AnimalGaranhao animal);
        void Editar(AnimalGaranhao animal);
        void Remover(AnimalGaranhao animal);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalDoadoraRepository
    {
        List<AnimalDoadora> Listar();
        AnimalDoadora ObterPorId(int id);
        AnimalDoadora GetById(int id);
        void Salvar(AnimalDoadora animal);
        void Editar(AnimalDoadora animal);
        void Remover(AnimalDoadora animal);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalDoadoraRepository
    {
        List<Doadora> Listar();
        Doadora ObterPorId(int id);
        Doadora GetById(int id);
        void Salvar(Doadora animal);
        void Editar(Doadora animal);
        void Remover(Doadora animal);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalGaranhaoRepository 
    {
        List<Garanhao> Listar();
        Garanhao ObterPorId(int id);
        Garanhao GetById(int id);
        void Salvar(Garanhao animal);
        void Editar(Garanhao animal);
        void Remover(Garanhao animal);
    }
}

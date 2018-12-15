using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalReceptoraRepository
    {
        List<Receptora> Listar();
        Receptora ObterPorId(int id);
        void Salvar(Receptora animal);
        void Editar(Receptora animal);
        void Remover(Receptora animal);
    }
}

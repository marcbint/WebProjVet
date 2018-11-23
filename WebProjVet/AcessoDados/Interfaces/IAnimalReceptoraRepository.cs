using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalReceptoraRepository
    {
        List<AnimalReceptora> Listar();
        AnimalReceptora ObterPorId(int id);
        void Salvar(AnimalReceptora animal);
        void Editar(AnimalReceptora animal);
        void Remover(AnimalReceptora animal);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IAnimalRepository
    {
        List<Animais> Listar();
        Animais ObterPorId(int id);
        Animais GetById(int id);
        void Salvar(Animais animal);
        void Editar(Animais animal);
        void Remover(Animais animal);
    }
}

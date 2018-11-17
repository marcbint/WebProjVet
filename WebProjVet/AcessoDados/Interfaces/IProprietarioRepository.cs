using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IProprietarioRepository
    {
        List<Proprietario> ListarProprietarios();
        Proprietario ObterProprietarioPorId(int id);
        void Salvar(Proprietario proprietario);
        void Editar(Proprietario proprietario);
        void Remover(Proprietario proprietario);
    }
}

using System.Collections.Generic;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IServicoRepository
    {
        List<Servico> ListarServicos();
        Servico ObterServicoPorId(int id);
        void Salvar(Servico servico);
        void Editar(Servico servico);
        void Remover(Servico servico);
    }
}

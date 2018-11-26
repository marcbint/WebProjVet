using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface ITratamentoRepository
    {
        List<Tratamento> Listar();
        Tratamento ObterPorId(int id);
        //void Salvar(Tratamento animal);
        void Editar(Tratamento tratamento);
        //void Remover(Tratamento animal);
    }
}

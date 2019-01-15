using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados.Interfaces
{
    public interface IFaturamentoRepository
    {
        List<Faturamento> Listar();
        Faturamento ObterPorId(int id);
        void Salvar(Faturamento faturamento);
        void Editar(Faturamento faturamento);
        void Remover(Faturamento faturamento);
    }
}

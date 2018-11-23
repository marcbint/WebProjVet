using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.AcessoDados
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> All();
        void Save(TEntity entity);

        //List<TEntity> Listar();
        //TEntity ObterPorId(int id);
        //TEntity GetById(int id);
        //void Salvar(TEntity entity);
        //void Editar(TEntity entity);
        //void Remover(TEntity entity);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> All();
        void Save(TEntity entity);
        void UpdateN(TEntity entity);
        void Delete(TEntity entity);

        //List<TEntity> Listar();
        //TEntity ObterPorId(int id);
        //TEntity GetById(int id);
        //void Salvar(TEntity entity);
        //void Editar(TEntity entity);
        //void Remover(TEntity entity);

        TEntity Create(TEntity entity);
        void Delete(long id);
        bool Exists(long? id);
        List<TEntity> FindAll();
        TEntity FindById(long id);
        TEntity Update(TEntity entity);
    }
}

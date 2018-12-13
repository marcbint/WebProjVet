using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: Entity
    {
        //Injeção de Dependencia
        private readonly WebProjVetContext _context;
        private DbSet<TEntity> dataset;
        public Repository(WebProjVetContext context)
        {
            _context = context;
            dataset = _context.Set<TEntity>();
        }

        

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
        }


        public IEnumerable<TEntity> All()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }


        public void Save(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void UpdateN(TEntity entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity Create(TEntity entity)
        {
            try
            {
                dataset.Add(entity);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null) dataset.Remove(result);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Exists(long? id)
        {
            return dataset.Any(b => b.Id.Equals(id));
        }

        public List<TEntity> FindAll()
        {
            return dataset.ToList();
        }

        public TEntity FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public TEntity Update(TEntity entity)
        {
            if (!Exists(entity.Id)) return null;

            var result = dataset.SingleOrDefault(b => b.Id == entity.Id);
            if(result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
    }
}

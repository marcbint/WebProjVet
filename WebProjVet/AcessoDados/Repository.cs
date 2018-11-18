﻿using System;
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
        public Repository(WebProjVetContext context)
        {
            _context = context;
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
    }
}

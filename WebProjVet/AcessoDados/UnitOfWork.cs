using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.AcessoDados
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebProjVetContext _context;
        public UnitOfWork(WebProjVetContext context)
        {
            _context = context;
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

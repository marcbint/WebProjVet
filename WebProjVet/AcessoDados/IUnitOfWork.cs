using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.AcessoDados
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjVet.Models;

namespace WebProjVet.Util
{
    public class FaturamentoEqualityComparer : IEqualityComparer<Faturamento>
    {
        public bool Equals(Faturamento x, Faturamento y)
        {
            // Two items are equal if their keys are equal.
            return x.ProprietarioId == y.ProprietarioId;
        }

        public int GetHashCode(Faturamento obj)
        {
            return obj.ProprietarioId.GetHashCode();
        }
    }
}

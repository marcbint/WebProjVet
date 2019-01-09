using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum ServicoUnidade 
    {
        [Description("FIXO")] FIXO = 1,
        [Description ("UNITÁRIO")] UNITÁRIO =2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum AnimalTipo
    {
        [Description("DOADOR")] DOADORA = 1,
        [Description("GARANHÃO")] GARANHAO = 2,
        [Description("RECEPTOR")] RECEPTORA = 3
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum AnimalTipo
    {
        [Description("DOADORA")] DOADORA = 1,
        [Description("GARANHÃO")] GARANHÃO = 2,      
        [Description("RECEPTORA")] RECEPTORA = 3,
        [Description("SÉMEN")] SÉMEN = 4
    }
}

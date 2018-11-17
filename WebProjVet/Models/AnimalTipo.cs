using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum AnimalTipo
    {
        [Description("GARANHÃO")] GARANHAO = 1,
        [Description("REPRODUTORA")] REPRODUTORA = 2,
        [Description("MATRIZ")] MATRIZ = 3
    }
}

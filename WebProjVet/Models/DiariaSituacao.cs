using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum DiariaSituacao
    {
        [Description("ABERTA")] ABERTA = 1,
        [Description("CANCELADA")] CANCELADA = 2,
        [Description("FECHADA")] FECHADA = 3

    }
}

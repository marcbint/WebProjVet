using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum TratamentoSituacao
    {
        [Description("ABERTO")] ABERTO = 1,
        [Description("CANCELADO")] CANCELADO = 2,
        [Description("FINALIZADO")] FINALIZADO = 3
     }
}

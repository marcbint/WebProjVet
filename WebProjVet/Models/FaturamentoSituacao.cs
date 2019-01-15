using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum FaturamentoSituacao
    {

        [Description("CANCELADO")] CANCELADO = 1,
        [Description("PAGO")] PAGO = 2,
        [Description("PENDENTE")] PENDENTE = 3

    }
}

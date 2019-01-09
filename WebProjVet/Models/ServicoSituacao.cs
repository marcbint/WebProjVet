using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum ServicoSituacao
    {
        [Description("CANCELADO")] CANCELADO = 1,
        [Description("REALIZADO")] REALIZADO = 2
    }
}

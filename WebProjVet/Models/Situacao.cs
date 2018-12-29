using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum Situacao
    {
        [Description("ATIVO")] ATIVO = 1,
        [Description("INATIVO")] INATIVO = 2
    }
}

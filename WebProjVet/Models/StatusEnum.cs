using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum StatusEnum
    {
        [Description("ATIVO")] ATIVO = 1,
        [Description("INATIVO")] INATIVO = 2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum EnderecoTipo
    {
        [Description("COMERCIAL")] COMERCIAL = 1,
        [Description("RESIDENCIAL")] RESIDENCIAL = 2,
        [Description("RURAL")] RURAL = 3,
    }
}

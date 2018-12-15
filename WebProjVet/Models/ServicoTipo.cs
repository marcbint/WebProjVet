using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum ServicoTipo
    {
        [Description("OUTROS")] OUTROS = 1,
            [Description("DIÁRIA")] DIÁRIA = 2
    }
}

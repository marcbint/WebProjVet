using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public enum PessoaTipo
    {
        [Description("FISICA")] FÍSICA = 1,
        [Description("JURIDICA")] JURÍDICA = 2
    }
}

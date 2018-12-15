using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models.ViewModels
{
    public class DoadoraViewModel
    {
        public Doadora AnimalDoadora { get; set; }
        public IEnumerable<Proprietario> Proprietarios { get; set; }
    }
}

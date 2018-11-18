using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimalViewModel
    {
        
        public int Id { get; set; }

        
        [Required]
        public string Nome { get; set; }


        public string Abqm { get; set; }

        
        
        public int ProprietarioId { get; set; }


        [Required]
        public AnimalTipo AnimalTipo { get; set; }

        public IEnumerable<ProprietarioViewModel> Proprietarios { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models 
{
    [Table("Garanhao")]
    public class Garanhao : Animal
    {
        
        [NotMapped]
        public string TabelaItensJson { get; set; }

        public virtual ICollection<GaranhaoProprietario> GaranhaoProprietarios { get; set; }

        public Garanhao()
        {
            GaranhaoProprietarios = new List<GaranhaoProprietario>();
        }
    }
}

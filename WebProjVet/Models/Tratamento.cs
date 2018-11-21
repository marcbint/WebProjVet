using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Tratamento
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        //public AnimalDoadora Doador { get; set; }
        //public AnimalDoadora Garanhao { get; set; }
        //public AnimalDoadora Receptor { get; set; }
        
    }
}

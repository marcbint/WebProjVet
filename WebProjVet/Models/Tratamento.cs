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
        public Animal Doador { get; set; }
        public Animal Garanhao { get; set; }
        public Animal Receptor { get; set; }
        
    }
}

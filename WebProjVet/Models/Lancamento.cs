using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Lancamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Tratamento Tratamento { get; set; }
        public Servico Servico { get; set; }
    }
}

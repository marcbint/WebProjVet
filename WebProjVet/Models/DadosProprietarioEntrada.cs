using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class DadosProprietarioEntrada
    {
        public int Dias { get; set; }
        public int ProprietarioId { get; set; }
        public DateTime DataAquisicao { get; set; }
        public DateTime DataUltimaApuracao { get; set; }
    }
}

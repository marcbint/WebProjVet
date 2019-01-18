using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class FaturamentoEntradas
    {
        [Key]
        public int Id { get; set; }

        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public Proprietario Proprietario { get; set; }

        public int AnimaisEntradasId { get; set; }

        [ForeignKey("AnimaisEntradasId")]
        public AnimaisEntrada AnimaisEntradas { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public Animais Animais { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public Servico Servico { get; set; }

        public int Dias { get; set; }

        public decimal Diaria { get; set; }

        public string Valor { get; set; }

        public DateTime DataFaturamento { get; set; }

        public int FaturamentoId { get; set; }

        [ForeignKey("FaturamentoId")]
        public Faturamento Faturamento { get; set; }

        public string Referencia { get; set; }


    }
}

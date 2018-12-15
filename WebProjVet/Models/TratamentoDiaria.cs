using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class TratamentoDiaria
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TratamentoId { get; set; }

        [ForeignKey("TratamentoId")]
        public virtual Tratamento Tratamento { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        public DateTime DataInicio { get; set; }

        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [DataType(DataType.Currency)]
        public decimal ValorOriginal { get; set; }

        public DateTime? DataFim { get; set; }
    }
}

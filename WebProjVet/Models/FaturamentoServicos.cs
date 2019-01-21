using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class FaturamentoServicos
    {
        [Key]
        public int Id { get; set; }

        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public Proprietario Proprietario { get; set; }

        public int AnimaisServicosId { get; set; }

        [ForeignKey("AnimaisServicosId")]
        public AnimaisServicos AnimaisServicos { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        public string Valor { get; set; }

        public DateTime DataFaturamento { get; set; }

        public int FaturamentoId { get; set; }

        [ForeignKey("FaturamentoId")]
        public Faturamento Faturamento { get; set; }

        public string Referencia { get; set; }


        public int? DoadoraId { get; set; }
        [ForeignKey("DoadoraId")]
        public virtual Animais Doadora { get; set; }


        public int? GaranhaoId { get; set; }
        [ForeignKey("GaranhaoId")]
        public virtual Animais Garanhao { get; set; }


        public int? ReceptoraId { get; set; }
        [ForeignKey("ReceptoraId")]
        public virtual Animais Receptora { get; set; }


        public int? SemenId { get; set; }
        [ForeignKey("SemenId")]
        public virtual Animais Semen { get; set; }




    }
}

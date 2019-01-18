using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Faturamento
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="PROPRIETÁRIO")]
        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public Proprietario Proprietario { get; set; }


        public string Valor { get; set; }


        [EnumDataType(typeof(FaturamentoSituacao))]
        public FaturamentoSituacao Situacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Display(Name ="REFERÊNCIA")]
        [Required(ErrorMessage ="{0} deve ser informada!")]
        public string Referencia { get; set; }

        [NotMapped]
        public DateTime? DataApuracao { get; set; }

        
        public virtual ICollection<FaturamentoEntradas> FaturamentoEntradas { get; set; }

        
        public virtual ICollection<FaturamentoServicos> FaturamentoServicos { get; set; }


    }
}

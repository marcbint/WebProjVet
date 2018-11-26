using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models.ViewModels
{
    public class TratamentoServicoViewModel
    {
        
        public int Id { get; set; }

        [Display(Name = "VALOR")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get; set; }

        [Display(Name = "TRATAMENTO")]
        public int TratamentoId { get; set; }

        [Display(Name = "TRATAMENTO")]
        [ForeignKey("TratamentoId")]
        public virtual Tratamento Tratamento { get; set; }

        [Display(Name = "SERVIÇO")]
        public int ServicoId { get; set; }

        [Display(Name = "SERVIÇO")]
        [ForeignKey("ServicoId")]
        public virtual Servico Servicos { get; set; }

        [Display(Name = "DATA REGISTRO")]
        public DateTime DataRegistro { get; set; }
    }
}

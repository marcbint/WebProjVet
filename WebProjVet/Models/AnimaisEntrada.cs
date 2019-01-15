using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimaisEntrada
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }

        //[Required]
        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        //[Required(ErrorMessage = "Informe o {0}.")]
        [Display(Name = "VALOR")]
        public string Valor { get; set; }


        [Display(Name = "VALOR ORIGINAL")]
        public string ValorOriginal { get; set; }

        //[Required(ErrorMessage ="Informe a {0}")]
        [Display(Name = "DATA ENTRADA")]
        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "DATA SAÍDA")]
        [DataType(DataType.Date)]
        public DateTime? DataSaida { get; set; }

        //[Required]
        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(DiariaSituacao))]
        public DiariaSituacao DiariaSituacao { get; set; }

        [Display(Name = "DATA CANCELAMENTO")]
        [DataType(DataType.Date)]
        public DateTime? DataCancelamento { get; set; }

        [Display(Name = "MOTIVO")]
        [MaxLength(100)]
        public string Motivo { get; set; }

        //[Display(Name = "COBRA DIÁRIA")]
        //[EnumDataType(typeof(EnumSimNao))]
        //public EnumSimNao CobraDiaria { get; set; }

        [Display(Name = "GTA")]
        [MaxLength(13), StringLength(13)]
        public string Gta { get; set; }

        [Display(Name = "ANEMIA")]
        [EnumDataType(typeof(EnumSimNao))]
        public EnumSimNao Anemia { get; set; }
  
        [Display(Name ="MORMO")]
        [EnumDataType(typeof(EnumSimNao))]
        public EnumSimNao Mormo { get; set; }

        [Display(Name = "TIPO CASCO")]
        [EnumDataType(typeof(AnimalTipoCasco))]
        public AnimalTipoCasco AnimalTipoCasco { get; set; }

        [Display(Name = "OBSERVAÇÕES CLÍNICAS")]
        [MaxLength(300),StringLength(300)]
        public string ObservacoesClinicas { get; set; }




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class AnimaisServicos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AnimaisId { get; set; }

        [ForeignKey("AnimaisId")]
        public virtual Animais Animais { get; set; }

        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico Servico { get; set; }

        [Display(Name ="VALOR")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"[0-9]{1,4}(\.[0-9]{1,2})?")]
        public string Valor { get; set; }

        [Display(Name ="DATA REALIZAÇÃO")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DataType(DataType.Currency)]
        public string ValorOriginal { get; set; }

        [Display(Name ="MOTIVO")]
        [MaxLength(100)]
        public string Motivo { get; set; }

        [Display(Name ="DATA CANCELAMENTO")]
        [DataType(DataType.Date)]
        public DateTime? DataCancelamento { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(ServicoSituacao))]
        public ServicoSituacao ServicoSituacao { get; set; }

        [Required(ErrorMessage ="{0} deve ser informado!")]
        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="{0} deve ser informado!")]
        public string ValorTotal { get; set; }


        public string DoadoraId { get; set; }

        //[ForeignKey("DoadoraId")]
        //public virtual Animais Doadora { get; set; }

        
        public string GaranhaoId { get; set; }

        //[ForeignKey("GaranhaoId")]
        //[NotMapped]
        //public virtual Animais Garanhao { get; set; }


        public string ReceptoraId { get; set; }

        //[ForeignKey("ReceptoraId")]
        //[NotMapped]
        //public virtual Animais Receptora { get; set; }


        public string SemenId { get; set; }


        [MaxLength(1)]
        public string Faturamento { get; set; }

        //[ForeignKey("SemenId")]
        //[NotMapped]
        //public virtual Animais Semen { get; set; }


        //public virtual ICollection<AnimalServicosVinculoAnimais> AnimalServicosVinculoAnimais { get; set; }

        [NotMapped]
        public string AnimaisServicosJson { get; set; }

        
        [NotMapped]
        public virtual ICollection<AnimaisProprietario> AnimaisProprietarios { get; set; }

        [NotMapped]
        public virtual AnimaisProprietario AnimaisProprietariosTeste { get; set; }


    }

}

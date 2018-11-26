using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjVet.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NOME")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Nome { get; set; }

        [Display(Name = "DESCRIÇÃO")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        public string Descricao { get; set; }

        [Display(Name = "VALOR")]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "money")]
        [Column(TypeName = "decimal")]
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.##0,00}")]

        public decimal Valor { get; set; }

        //public virtual Tratamento Tratamento { get; set; }
        
    }
}

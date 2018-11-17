using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Você precisa entrar com o {0}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Valor { get; set; }


        
    }
}

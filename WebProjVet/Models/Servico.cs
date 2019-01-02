using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProjVet.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CÓDIGO"), MaxLength(10)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Nome { get; set; }

        //[Display(Name = "DESCRIÇÃO")]
        //[Required(ErrorMessage = "Você precisa entrar com o {0}")]
        //public string Descricao { get; set; }

        [Display(Name = "VALOR")]
        //https://stackoverflow.com/questions/19811180/best-data-annotation-for-a-decimal18-2       
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        [DataType(DataType.Currency)]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "O campo {0} precisa ser informado.")]
        //[StringLength(5)]
        public decimal Valor { get; set; }

        [Display(Name = "TIPO")]
        [EnumDataType(typeof(ServicoTipo))]
        public ServicoTipo ServicoTipo { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(Situacao))]
        public Situacao Situacao { get; set; }


        public virtual ICollection<AnimaisServicos> AnimaisServicos { get; set; }

        public Servico()
        {
            //AnimaisServicos = new List<AnimaisServicos>();
            //TratamentoServicos = new List<TratamentoServico>();
        }
        
    }
}

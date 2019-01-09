using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class ProprietarioEndereco
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "TIPO")]
        [EnumDataType(typeof(EnderecoTipo))]
        public EnderecoTipo EnderecoTipo { get; set; }

        [Display(Name = "ENDEREÇO"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Endereco { get; set; }

        [Display(Name = "BAIRRO"), MaxLength(600)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Bairro { get; set; }

        [Display(Name = "COMPLEMENTO"), MaxLength(30)]
        public string Complemento { get; set; }

        [Display(Name = "CIDADE"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        [StringLength(2, MinimumLength = 2)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Uf { get; set; }

        
        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        public virtual Proprietario Proprietario { get; set; }

        [Display(Name = "NOME LOCALIDADE"), MaxLength(50)]
        [StringLength(50)]
        public string Nome { get; set; }

        [Display(Name = "CÓDIGO RURAL"), MaxLength(20)]
        [StringLength(20)]
        public string CodigoRural { get; set; }


        [Display(Name = "CPF/CNPJ"), MaxLength(20)]
        [StringLength(20)]
        public string Documento { get; set; }

        [Display(Name = "INSC. ESTADUAL"), MaxLength(20)]
        [StringLength(20)]
        public string InscricaoEstadual { get; set; }


        public ProprietarioEndereco()
        {

        }
    }
}

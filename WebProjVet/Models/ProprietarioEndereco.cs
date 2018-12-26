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

        public ProprietarioEndereco()
        {

        }
    }
}

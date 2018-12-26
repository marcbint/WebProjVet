using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "TIPO")]
        [EnumDataType(typeof(PessoaTipo))]
        public PessoaTipo PessoaTipo { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Nome { get; set; }

        [Display(Name = "CNPJ/CPF"), MaxLength(20)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Documento { get; set; }

    }
}

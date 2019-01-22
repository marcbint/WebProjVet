using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Empresa
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado!")]
        [Display(Name = "NOME"), MaxLength(100)]
        public virtual string Nome { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(Situacao))]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public Situacao Situacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "{0} deve ser informado.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} informado é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        public string Senha { get; set; }

        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum StatusEnum { get; set; }
        
    }
}

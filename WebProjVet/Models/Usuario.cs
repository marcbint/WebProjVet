using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Usuario
    {

        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        public virtual string Nome { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} informado é inválido.")]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        public virtual string Senha { get; set; }

        public virtual string Salt { get; set; }

        [EnumDataType(typeof(Situacao))]
        public virtual Situacao Status { get; set; }

    }
}

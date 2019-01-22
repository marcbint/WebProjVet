using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Usuario
    {

        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "NOME"), MaxLength(100)]
        public virtual string Nome { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} informado é inválido.")]
        [Display(Name = "LOGIN"), MaxLength(30)]
        public virtual string Login { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [Display(Name = "SENHA"), MaxLength(100)]
        public virtual string Senha { get; set; }

        [Required(ErrorMessage = "{0} deve ser informado.")]
        [NotMapped]
        [Display(Name = "CONFIRMAÇÃO"), MaxLength(30)]
        public virtual string ConfirmaSenha { get; set; }

        public virtual string Salt { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(Situacao))]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public Situacao Situacao { get; set; }

        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }

    }
}

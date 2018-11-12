using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Proprietario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Nome { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Uf { get; set; }
    }
}

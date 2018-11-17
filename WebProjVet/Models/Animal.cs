using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Nome { get; set; }

        [Display(Name = "ABQM")]
        public string Abqm { get; set; }

        //[Display(Name = "Proprietário")]
        //[Required(ErrorMessage = "{0} deve ser informado")]
        //public Proprietario Proprietario { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [EnumDataType(typeof(AnimalTipo))]
        public AnimalTipo AnimalTipo { get; set; }

    }
}

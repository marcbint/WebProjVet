using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NOME")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Nome { get; set; }

        [Display(Name = "REGISTRO ABQM")]
        public string Abqm { get; set; }

        [Display(Name = "PROPRIETÁRIO")] 
        public int ProprietarioId { get; set; }

        [ForeignKey("ProprietarioId")]
        //public Proprietario Proprietario { get; set; }
        public IEnumerable<ProprietarioViewModel> Proprietarios { get; set; }

        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [EnumDataType(typeof(AnimalTipo))]
        public AnimalTipo AnimalTipo { get; set; }


    }
}

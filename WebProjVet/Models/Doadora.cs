using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    [Table("Doadoras")]
    public class Doadora 
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Nome { get; set; }

        [Display(Name = "REGISTRO ABQM"), MaxLength(20)]
        public string Abqm { get; set; }
        
        //[Display(Name = "PROPRIETÁRIO")] 
        //public int ProprietarioId { get; set; }

        //[ForeignKey("ProprietarioId")]        
        //public virtual IEnumerable<Proprietario> Proprietarios { get; set; }
        //public virtual Proprietario Proprietario { get; set; }

        public virtual ICollection<DoadoraProprietario> DoadoraProprietarios { get; set; }

        [NotMapped]
        public string DoadoraProprietariosJson { get; set; }

        public Doadora()
        {
            DoadoraProprietarios = new List<DoadoraProprietario>();
        }

        /*
        [Display(Name = "TIPO")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [EnumDataType(typeof(AnimalTipo))]
        public AnimalTipo AnimalTipo { get; set; }
        */

    }
}

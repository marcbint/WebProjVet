using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjVet.Models
{
    public class Animais
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "NOME"), MaxLength(100)]
        [Required(ErrorMessage = "{0} deve ser informado!")]
        public string Nome { get; set; }

        [Display(Name = "REGISTRO ABQM"), MaxLength(20)]
        public string Abqm { get; set; }

        [Display(Name = "TIPO")]
        [EnumDataType(typeof(AnimalTipo))]
        public AnimalTipo AnimalTipo { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(Situacao))]
        public Situacao Situacao { get; set; }

        [NotMapped]
        public string AnimaisProprietariosJson { get; set; }

        public virtual ICollection<AnimaisProprietario> AnimaisProprietarios { get; set; }

        public virtual ICollection<TratamentoAnimal> TratamentoAnimais { get; set; }

        public virtual ICollection<AnimaisServicos> AnimaisServicos { get; set; }
    }
}

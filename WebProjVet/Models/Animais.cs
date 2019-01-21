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
        [Required(ErrorMessage ="Selecione uma opção!")]
        public AnimalTipo AnimalTipo { get; set; }

        [Display(Name = "SITUAÇÃO")]
        [EnumDataType(typeof(Situacao))]
        [Required(ErrorMessage = "Selecione uma opção!")]
        public Situacao Situacao { get; set; }

        [Display(Name = "NASCIMENTO"), MaxLength(10)]
        [StringLength(10)]
        //[Required(ErrorMessage = "{0} deve ser informado!")]
        public string DataNascimento { get; set; }

        [Display(Name = "MÃE"), MaxLength(100)]
        //[Required(ErrorMessage = "{0} deve ser informada!")]
        public string Mae { get; set; }

        [Display(Name = "PAI"), MaxLength(100)]
        //[Required(ErrorMessage = "{0} deve ser informado!")]
        public string Pai { get; set; }

        [Display(Name = "PELAGEM"), MaxLength(50)]
        [StringLength(50)]
        //[Required(ErrorMessage = "{0} deve ser informada!")]
        public string Pelagem { get; set; }

        [NotMapped]
        public string AnimaisProprietariosJson { get; set; }


        [EnumDataType(typeof(EnumSimNao))]
        [NotMapped]
        public EnumSimNao EnumSimNao { get; set; }

        [EnumDataType(typeof(AnimalTipoCasco))]
        [NotMapped]
        public AnimalTipoCasco AnimalTipoCasco { get; set; }

        public virtual ICollection<AnimaisProprietario> AnimaisProprietarios { get; set; }

        public virtual ICollection<TratamentoAnimal> TratamentoAnimais { get; set; }

        public virtual ICollection<AnimaisServicos> AnimaisServicos { get; set; }

        public virtual ICollection<AnimaisEntrada> AnimaisEntradas { get; set; }

        public virtual ICollection<AnimaisServicos> DoadorasAnimaisServicos { get; set; }
        public virtual ICollection<AnimaisServicos> GaranhoesAnimaisServicos { get; set; }
        public virtual ICollection<AnimaisServicos> ReceptorasAnimaisServicos { get; set; }
        public virtual ICollection<AnimaisServicos> SemenAnimaisServicos { get; set; }


        public virtual ICollection<FaturamentoServicos> DoadorasFaturamentoServicos { get; set; }
        public virtual ICollection<FaturamentoServicos> GarahoesFaturamentoServicos { get; set; }
        public virtual ICollection<FaturamentoServicos> ReceptorasFaturamentoServicos { get; set; }
        public virtual ICollection<FaturamentoServicos> SemenFaturamentoServicos { get; set; }

        //public virtual ICollection<AnimalServicosVinculoAnimais> AnimalServicosVinculoAnimais { get; set; }

        public Animais()
        {

        }
    }
}
